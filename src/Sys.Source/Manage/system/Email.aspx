<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Email.aspx.cs" Inherits="Manage_System_Email" %>

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
            line-height: 30px;
        }
    </style>
</head>
<body>
    <!--撑满页面-->
    <div class="mini-fit">
        <fieldset style="border: solid 1px #aaa; padding: 3px;">
            <legend>
                <%=Sys.Kernel.Software.Name %>
                邮件设置</legend>
            <div style="padding: 5px;">
                <form id="form1" method="post">
                <table>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" alt="SMTP服务器" />
                            &nbsp;SMTP服务器：
                        </td>
                        <td>
                            <input name="EmailServer" emptytext="发送邮件的SMTP服务器" value="<%=WebConfig.GetString("EmailServer")%>"
                                id="EmailServer" class="mini-textbox" width="440" required="true" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" alt="SMTP端口" />
                            &nbsp;SMTP端口：
                        </td>
                        <td>
                            <input name="EmailPort" emptytext="发送邮件的SMTP端口" id="EmailPort" value="<%=WebConfig.GetString("EmailPort")%>"
                                class="mini-textbox" width="440" required="true" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" alt="邮件帐户" />
                            &nbsp;邮件帐户：
                        </td>
                        <td>
                            <input name="EmailAccount" emptytext="发送邮件的帐户" id="EmailAccount" value="<%=WebConfig.GetString("EmailAccount")%>"
                                class="mini-textbox" width="440" required="true" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" alt="邮件密码" />
                            &nbsp;邮件密码：
                        </td>
                        <td>
                            <input name="EmailPassword" emptytext="发送邮件帐户的密码" id="EmailPassword" class="mini-password"
                                width="440" required="true" value="<%=WebConfig.GetString("EmailPassword")%>" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" alt="邮件发送人" />
                            &nbsp;邮件发送人：
                        </td>
                        <td>
                            <input name="EmailSender" emptytext="发送邮件的发送人" id="EmailSender" class="mini-textbox"
                                width="440" required="true" value="<%=WebConfig.GetString("EmailSender")%>" />
                        </td>
                    </tr>
                </table>
                </form>
            </div>
        </fieldset>
        <div style="text-align: center; padding: 10px;">
            <a class="mini-button" onclick="onOk" style="width: 160px; margin-right: 20px;">更新邮件系统配置</a>
        </div>
        <fieldset style="border: solid 1px #aaa; padding: 3px;">
            <legend>邮件发送测试</legend>
            <div style="padding: 5px;">
                <table>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" alt="邮件发送人" />
                            &nbsp;邮箱地址：
                        </td>
                        <td>
                            <input name="EmailReceive" emptytext="邮件接收地址" id="EmailReceive" class="mini-textbox"
                                width="200" required="true" value="" />
                            <a class="mini-button" onclick="onSender" style="width: 100px; margin-right: 20px;">
                                发送</a>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</body>
</html>
<script type="text/javascript">

    function onOk() {
        mini.parse();
        var form = new mini.Form("form1");
        form.validate();
        if (form.isValid() == false) return;
        var o = form.getData();
        var json = mini.encode([o]);
        var messageid = mini.loading("请稍等, 邮件系统配置更新中 ...", "Loading");

        //保存数据
        $.post("/manage/system/Email.aspx?act=save", { data: json }, function (data, state) {
            if (state == "success") {
                if (data == "yes") {
                    mini.hideMessageBox(messageid);
                    mini.alert("系统配置成功！", "提示", function () {

                        window.location.reload();
                    });
                }
            }
        });
    }


    function onSender() {
        var EmailTest = mini.get("EmailReceive").getValue();
        var messageid = mini.loading("请稍等, 邮件发送中 ...", "Loading");

        //发送邮件测试
        $.post("/manage/system/Email.aspx?act=test", { "EmailTest": EmailTest }, function (data, state) {
            if (state == "success") {
                mini.hideMessageBox(messageid);
                if (data == "yes") {

                    mini.alert("邮件发送成功！", "提示", function () {

                       // window.location.reload();
                    });
                } 
                
                else {
                    mini.alert("邮件发送失败！<br/>"+data, "提示", function () {

                    });
                }
            }
        });
    }

   


</script>
