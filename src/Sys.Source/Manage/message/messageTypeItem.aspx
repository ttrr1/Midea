<%@ Page Language="C#" AutoEventWireup="true" CodeFile="messageTypeItem.aspx.cs" Inherits="Manage_message_messageTypeItem" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>模块标记面板</title>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <script src="../../scripts/boot.js" type="text/javascript"></script>
    <style type="text/css">
        html, body
        {
            font-size: 12px;
            padding: 0;
            margin: 0;
            border: 0;
            height: 100%;
            overflow: hidden;
        }
        
        .searchbox .mini-buttonedit-icon
        {
            background: url(../../Scripts/miniui/themes/icons/search.gif) no-repeat 50% 50%;
        }
    </style>
</head>
<body style="padding:5px;">
    <form id="form1" method="post">
    <input name="ID" id="ID" class="mini-hidden" />
    <fieldset style="border: solid 1px #aaa; padding: 3px;">
        <legend>信息类别基本信息</legend>
        <div style="padding: 5px;">
            <table>
               
                <tr>
                    <td style="width: 120px;">
                        <img src="../images/help.gif" />
                        &nbsp;类别名称：
                    </td>
                    <td style="width: 180px;">
                        <input name="TypeName" class="mini-textbox" width="180" required="true" />
                    </td>
                </tr>
              
               
                <tr>
                    <td style="color: red;">
                        <img src="../images/help.gif" />
                        &nbsp;所属类别：
                    </td>
                    <td>
                        <input id="PId" name="PId" class="mini-buttonedit searchbox" textname="ParentName"
                            onbuttonclick="onButtonEdit" allowinput="false" width="180" />
                    </td>
                </tr>
              
                <tr>
                    <td style="width: 120px;">
                        <img src="../images/help.gif" />&nbsp; 模块行为：
                    </td>
                    <td style="width: 180px;">
                        <input name="TypeAction" class="mini-textbox" width="180" />
                    </td>
                </tr>
              
               
            </table>
        </div>
    </fieldset>
    <div style="text-align: center; padding: 10px;">
        <a class="mini-button" onclick="onOk" style="width: 60px; margin-right: 20px;">确定</a>
        <a class="mini-button" onclick="onCancel" style="width: 60px;">取消</a>
    </div>
    </form>
    <script type="text/javascript">
        mini.parse();
        var form = new mini.Form("form1");
        function SaveData() {
            var o = form.getData();
            form.validate();
            if (form.isValid() == false) return;
            var json = mini.encode([o]);
            $.ajax({
                url: "/data/Ajax_messageType.aspx?method=SaveData",
                data: { data: json },
                cache: false,
                success: function (text) {
                    CloseWindow("save");
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
                    url: "/data/Ajax_messageType.aspx?method=GetView&ID=" + data.ID,
                    cache: false,
                    success: function (text) {
                        var o = mini.decode(text);
                        form.setData(o);
                        form.setChanged(false);

                    }
                });
            }
        }

        function GetData() {
            var o = form.getData();
            return o;
        }
        function CloseWindow(action) {
            window.close();

            if (action == "close" && form.isChanged()) {
                if (confirm("数据被修改了，是否先保存？")) {
                    return false;
                }
            }
            if (window.CloseOwnerWindow) return window.CloseOwnerWindow(action);
            else window.close();
        }
        function onOk(e) {
            SaveData();
        }
        function onCancel(e) {
            CloseWindow("cancel");
        }
        //////////////////////////////////



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
</body>
</html>
