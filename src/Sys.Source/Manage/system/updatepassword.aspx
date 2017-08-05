<%@ Page Language="C#" AutoEventWireup="true" CodeFile="updatepassword.aspx.cs" Inherits="Manage_system_updatepassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../Scripts/boot.js" type="text/javascript"></script>
    <script src="../../Scripts/miniui/locale/zh_CN.js" type="text/javascript"></script>
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
        }
    </style>
</head>
<body>
    <!--撑满页面-->
    <div class="mini-fit">
        <fieldset style="border: solid 1px #aaa; padding: 3px;">
            <legend>
              修改密码</legend>
            <div style="padding: 5px;">
                <form id="form1" method="post">
                <table>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;新密码：
                        </td>
                        <td>
                             <input name="Password1" id="Password1" class="mini-password" width="180" />
                        </td>
                    </tr>

                     <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;确认新密码：
                        </td>
                        <td>
                             <input name="Password" id="Password" class="mini-password" width="180" />
                        </td>
                    </tr>
                </table>
                </form>
            </div>
        </fieldset>
        <div style="text-align: center; padding: 10px;">
            <a class="mini-button" onclick="onOk" style="width: 160px; margin-right: 20px;">修改密码</a>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">

    function onOk() {
        mini.parse();

        if (mini.get("Password1").getValue()=="") {
            mini.alert("请输入您的新密码！", "友情提示", function () {
            });
            return;
        }
        if (mini.get("Password").getValue() == "") {
            mini.alert("请输入您的确认密码！", "友情提示", function () {
            });
            return;
        }

        if (mini.get("Password").getValue() != mini.get("Password1").getValue()) {
            mini.alert("2次输入密码不一致，请重新输入！", "友情提示", function () {
            });
            return;
        }
        var messageid = mini.loading("请稍等,密码更新中 ...", "Loading");
        //保存数据
        $.post("/manage/system/updatepassword.aspx", { "act": "updatePwd", "pwd": mini.get("Password").getValue() }, function (data, state) {
            if (state == "success") {
                if (data == "yes") {
                    mini.hideMessageBox(messageid);
                    mini.showTips({
                        content: "<b>  操作提示</b> <br/>密码修改成功",
                        state: "success",
                        x: "center",
                        y: "center",
                        timeout: 3000
                    });
                } else {
                    mini.hideMessageBox(messageid);
                    mini.showTips({
                        content: "<b>  操作提示</b> <br/>密码修改失败",
                        state: "success",
                        x: "center",
                        y: "center",
                        timeout: 3000
                    });
                
                }
            }
        });
    }

</script>