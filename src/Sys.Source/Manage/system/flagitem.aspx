<%@ Page Language="C#" AutoEventWireup="true" CodeFile="flagitem.aspx.cs" Inherits="UiDesk_flag_flagitem" %>

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
<body>
    <form id="form1" method="post">
    <input name="ID" id="ID" class="mini-hidden" />
    <fieldset style="border: solid 1px #aaa; padding: 3px;">
        <legend>基本信息</legend>
        <div style="padding: 5px;">
            <table>
                <tr>
                    <td style="width: 120px;">
                        <img src="../images/help.gif" />&nbsp; 模块类别：
                    </td>
                    <td style="width: 180px;">
                        <input id="FlagType" class="mini-combobox" style="width: 180px;" textfield="text"
                            valuefield="id" url="/data_txt/flagType.txt" value="1" required="true" allowinput="true"
                            shownullitem="true" nullitemtext="请选择..." />
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px;">
                        <img src="../images/help.gif" />
                        &nbsp;模块名称：
                    </td>
                    <td style="width: 180px;">
                        <input name="FlagName" class="mini-textbox" width="180" required="true" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">
                        <img src="../images/help.gif" />&nbsp;是否导航：
                    </td>
                    <td style="width: 180px;">
                        <input name="IsNav" class="mini-checkbox" text="是否导航？" truevalue="1" falsevalue="0" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">
                        <img src="../images/help.gif" />&nbsp;是否展开：
                    </td>
                    <td style="width: 180px;">
                        <input name="IsOpen" class="mini-checkbox" text="是否展开？" truevalue="1" falsevalue="0" />
                    </td>
                </tr>
                <tr>
                    <td style="color: red;">
                        <img src="../images/help.gif" />
                        &nbsp;父级模块：
                    </td>
                    <td>
                        <input id="ParentID" name="ParentID" class="mini-buttonedit searchbox" textname="ParentName"
                            onbuttonclick="onButtonEdit" allowinput="false" width="180" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px;">
                        <img src="../images/help.gif" />&nbsp; 模块标记：
                    </td>
                    <td style="width: 180px;">
                        <input name="Flag" class="mini-textbox" width="180" required="true" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px;">
                        <img src="../images/help.gif" />&nbsp; 模块行为：
                    </td>
                    <td style="width: 180px;">
                        <input name="FlagAction" class="mini-textbox" width="180" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="../images/help.gif" />&nbsp;模块链接地址：
                    </td>
                    <td>
                        <input name="AppUrl" id="AppUrl" class="mini-textbox" width="180" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="../images/help.gif" />&nbsp;模块分组：
                    </td>
                    <td>
                        <div id="FlagGroup" name="FlagGroup" class="mini-radiobuttonlist" repeatitems="2"
                            repeatlayout="table" repeatdirection="horizontal" textfield="text" valuefield="id"
                            value="1" url="/data_txt/flagGroup.txt">
                        </div>
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
                url: "/data/Ajax_flag.aspx?method=SaveData",
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
                    url: "/data/Ajax_flag.aspx?method=GetView&FlagId=" + data.FlagId,
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
                url: "/manage/system/selflag.htm",
                showMaxButton: false,
                title: "上级模块标记选择树",
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
                            btnEdit.setText(data.FlagName);
                        }
                    }
                }
            });

        } 
    </script>
</body>
</html>
