<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberStatusEdit.aspx.cs" Inherits="Manage_Member_MemberStatusEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员状态编辑</title>
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
    <input name="UserID" id="UserID" class="mini-hidden" />
    <fieldset style="border: solid 1px #aaa; padding: 3px;">
        <legend>状态编辑</legend>
        <div style="padding: 5px;">
            <table>
                <tr>
                    <td style="color: red;">
                        <img src="../images/help.gif" />&nbsp;用户状态：
                    </td>
                    <td>
                        <input id="State" name="State" class="mini-combobox" style="width: 180px;" textfield="text"
                            required="true" valuefield="id" url="/data_txt/userstate.txt" 
                            value="" allowinput="false" shownullitem="true" nullitemtext="请选择..." />
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
                url: "/data/Ajax_UserInfo.aspx?method=StatusUpdate",
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
            //跨页面传递的数据对象，克隆后才可以安全使用
            data = mini.clone(data);
            $.ajax({
                url: "/data/Ajax_UserInfo.aspx?method=StatusInit&id=" + data.id,
                cache: false,
                success: function (text) {
                    var o = mini.decode(text);
                    form.setData(o);
                    form.setChanged(false);

                }
            });
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

    </script>
</body>
</html>
