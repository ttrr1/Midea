<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="messageitem.aspx.cs" Inherits="Manage_message_messageitem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../Scripts/boot.js" type="text/javascript"></script>
    <script src="../../Scripts/miniui/locale/zh_CN.js" type="text/javascript"></script>
    <script src="../../Scripts/kindeditor-4.1.10/kindeditor.js" type="text/javascript"></script>
    <style type="text/css">
        html, body
        {
            margin: 0;
            padding: 0;
            border: 0;
            width: 100%;
            height: 100%;
            overflow: hidden;
        }
        
        table td
        {
            height: 26px;
        } .searchbox .mini-buttonedit-icon
        {
            background: url(../../Scripts/miniui/themes/icons/search.gif) no-repeat 50% 50%;
        }
    </style>
</head>
<body>
    <!--撑满页面-->
    <div class="mini-fit">
        <fieldset style="border: solid 1px #aaa; padding: 3px; margin: 8px;">
            <legend>
                <%=Sys.Kernel.Software.Name %>
                系统设置</legend>
            <div style="padding: 5px;">
                <form id="form1" method="post">
                <table>
                    <tr>
                        <td>
                            <img src="../images/help.gif" />
                            &nbsp;信息标题：
                        </td>
                        <td>
                          <input name="ID" id="ID" class="mini-hidden" />
                            <input name="NewsTitle" emptytext="输入信息标题" class="mini-textbox" width="280" required="true" />
                        </td>
                    </tr>

                      <tr>
                    <td style="color: red;">
                        <img src="../images/help.gif" />
                        &nbsp;所属类别：
                    </td>
                    <td>
                        <input id="TypeId" name="TypeId" class="mini-buttonedit searchbox" textname="ParentName" required="true"
                            onbuttonclick="onButtonEdit" allowinput="false" width="280" />
                    </td>
                </tr>
                    <tr>
                        <td>
                            <img src="../images/help.gif" />
                            &nbsp;信息来源：
                        </td>
                        <td>
                            <input name="Source" emptytext="输入信息来源" class="mini-textbox" width="280" required="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img src="../images/help.gif" />
                            &nbsp;信息摘要：
                        </td>
                        <td>
                            <input name="Abstract" emptytext="输入信息的摘要信息" class="mini-textarea" width="480" height="60"
                                required="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img src="../images/help.gif" />
                            &nbsp;信息内容：
                        </td>
                        <td>
                            <textarea id="Content" name="Content" cols="100" rows="8" style="width: 670px; height: 340px;
                                visibility: hidden;"></textarea>
                        </td>
                    </tr>
                </table>
                </form>
            </div>
        </fieldset>
        <div style="text-align: center; padding: 10px;">
            <a class="mini-button" onclick="onOk" style="width: 60px; margin-right: 20px;">确定</a>
            <a class="mini-button" onclick="onCancel" style="width: 60px;">取消</a>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
    mini.parse();
    var form = new mini.Form("form1");
    function onOk() {
     
        var o = form.getData();
        form.validate();
        if (form.isValid() == false) return;
        var messageid = mini.loading("请稍等, 数据保存中 ...", "Loading");

        var json = mini.encode([o]);
      
        $.ajax({
            url: "/data/Ajax_message.aspx?method=SaveData",
            data: { data: json, Content: editor.html() },
            cache: false,
            type: "post",
            success: function (text) {
                mini.hideMessageBox(messageid);
                mini.alert("操作成功！", "提示", function () {
                    CloseWindow("save");
                });
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(jqXHR.responseText);
                CloseWindow();
            }
        });
    }

    ////////////////////
    //标准方法接口定义
    function SetData(data) {
        if (data.action == "edit") {
            //跨页面传递的数据对象，克隆后才可以安全使用
            data = mini.clone(data);
            $.ajax({
                url: "/data/Ajax_message.aspx?method=GetView&ID=" + data.ID,
                cache: false,
                
                success: function (text) {
                 
                    var o = mini.decode(text);
                    form.setData(o);
                    form.setChanged(false);
                    editor.html(o["Content"]);
                }
            });
        }
    }
    function onCancel(e) {
        CloseWindow("cancel");
    }
    function CloseWindow(action) {
        if (action == "close" && form.isChanged()) {
            if (confirm("数据被修改了，是否先保存？")) {
                return false;
            }
        }
        if (window.CloseOwnerWindow) return window.CloseOwnerWindow(action);
        else window.close();
    }



    function onButtonEdit(e) {
        var btnEdit = this;
        mini.open({
            url: "/manage/message/seltype.htm",
            showMaxButton: false,
            title: "信息所属类别选择树",
            width: 350,
            height: 350,
            ondestroy: function (action) {
                if (action == "ok") {
                    var iframe = this.getIFrameEl();
                    var data = iframe.contentWindow.GetData();
                    data = mini.clone(data);
                    if (data) {
                        //alert(data.FlagId);
                        btnEdit.setValue(data.ID);
                        btnEdit.setText(data.TypeName);
                    }
                }
            }
        });

    } 






</script>
<script type="text/javascript">

    var editor = null;
    KindEditor.ready(function (K) {
        editor = K.create('#Content', {
            cssPath: '/Scripts/kindeditor-4.1.10/plugins/code/prettify.css',
            uploadJson: '/Scripts/kindeditor-4.1.10/asp.net/upload_json.ashx',
            fileManagerJson: '/Scripts/kindeditor-4.1.10/asp.net/file_manager_json.ashx',
            allowFileManager: true,
            afterCreate: function () {
                var self = this;
                K.ctrl(document, 13, function () {
                    self.sync();
                    K('form[name=example]')[0].submit();
                });
                K.ctrl(self.edit.doc, 13, function () {
                    self.sync();
                    K('form[name=example]')[0].submit();
                });
            }
        });
        //  prettyPrint();
    });
</script>
