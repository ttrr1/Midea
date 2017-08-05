<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="Manage_login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>应用系统_登录入口</title>
    <script src="/Scripts/boot.js" type="text/javascript"></script>
    <link href="/Styles/cwn_login.css" rel="stylesheet" type="text/css" />
    <link rel="Shortcut Icon" href="images/favicon.ico" />
    <script type="text/javascript">

        $(function () {
            $("#sub").click(function () {
                sub();
            }).css("cursor", "pointer");

            $("#UserName")[0].focus();
        });


        function myOnkeydown() {

                        if (event.keyCode == 13) {
                            sub();
                        }
        }

        function sub() {
            $("#tishi").html("").hide();
            if (checkInput("UserName", "用户名不能为空！") == false) { return; }
            else if (checkInput("PassWord", "密码不能为空！") == false) { return; }

            else if (checkInput("validatecode", "验证码不能为空！") == false) { return; }
            else {
                var pars = { "act": "checkLogin", "UserName": $("#UserName").val(), "PassWord": $("#PassWord").val(), "validatecode": $("#validatecode").val() };

                var messageid = mini.loading("<span style=\"color:red;\">正在向服务器验证资料, Please wait ...</span>", "Loading");
                setTimeout(function () {
                    $.post("/manage/login.aspx", pars, function (date, state) {
                        if (state == "success") {
                            mini.hideMessageBox(messageid);
                            if (date == "yes") {
                                window.location.href = "main.aspx"
                            } else {
                                var html = "<img class=\"vm\" src=\"images/dl_03.gif\" /> " + date;
                                $("#tishi").html(html).show("slow");
                            }
                        }
                    });

                }, 1000);
            }

        }


        function checkInput(id, msg) {
            var result = true;
            if ($.trim($("#" + id).val()) == "") {
                $("#" + id).focus();
                var html = "<img class=\"vm\" src=\"images/dl_03.gif\" /> " + msg;
                $("#tishi").html(html).show("slow");
                result = false;
            }
            return result;

        }
    </script>
</head>
<body>
    <div class="wrap">
        <div class="ctn">
            <form method="post" id="myform">
            <table id="ul1" cellspacing="0">
                <tr>
                    <td width="29%">
                        用户名：
                    </td>
                    <td>
                        <input class="txt1" type="text" name="UserName" style="padding-left: 8px;" onkeydown="myOnkeydown();"
                            id="UserName" maxlength="16" />
                    </td>
                </tr>
                <tr>
                    <td>
                        密&nbsp;&nbsp;&nbsp;码：
                    </td>
                    <td>
                        <input class="txt1" type="password" style="padding-left: 8px;" id="PassWord" onkeydown="myOnkeydown();"
                            name="PassWord" maxlength="16" />
                    </td>
                </tr>
                <tr>
                    <td>
                        验证码：
                    </td>
                    <td>
                        <input class="w110" type="text" style="padding-left: 8px;" id="validatecode" onkeydown="myOnkeydown();"
                            name="validatecode" maxlength="4" />
                        <img class="vm" style="cursor: pointer; margin-top: -3px;" src="/ImageCode.aspx?rand='+Math.round(Math.random()*1000000);"
                            onclick="this.src='/ImageCode.aspx?rand='+Math.round(Math.random()*1000000);"
                            title="看不清,换一张?" />
                    </td>
                </tr>
                <tr class="dp">
                    <td>
                    </td>
                    <td id="tishi" class="dpl">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <input id="sub" class="btn bg" type="button" value="登录" />
                    </td>
                </tr>
            </table>
            </form>
        </div>
        <div class="footer">
            版权所有：<a href="http://www.jssdw.com" style="color: #fff; text-decoration: none;" target="_blank">
                空调安装 技术支持：仕德伟技术中心</a><br />
            联系地址：江苏省苏州市</div>
    </div>
</body>
</html>
