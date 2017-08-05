<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IpBlockConfig.aspx.cs" Inherits="Manage_System_IpBlockConfig" %>


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
                <%=Sys.Kernel.Software.Name %>
                系统设置</legend>
            <div style="padding: 5px;">
                <form id="form1" method="post">
                <table>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;网站名称：
                        </td>
                        <td>
                            <div id="IpBlockType" style="padding: 10px;" class="mini-radiobuttonlist" repeatitems="3"
                                repeatlayout="table" repeatdirection="vertical" textfield="text" valuefield="IpBlockType"
                                value="<%=Sys.BLL.SysConfig.GetString("WebConfig","AdminIpBlockType") %>" url="/data_txt/IpBlockType.txt">
                            </div>
                        </td>
                    </tr>
                </table>
                </form>
            </div>
        </fieldset>
        <div style="text-align: center; padding: 10px;">
            <a class="mini-button" onclick="onOk" style="width: 160px; margin-right: 20px;">更新系统配置</a>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">

    function onOk() {
        mini.parse();
        var messageid = mini.loading("请稍等, 系统配置更新中 ...", "Loading");
        //保存数据
        $.post("/manage/system/IpBlockConfig.aspx?act=save", { "act": "save", "IpBlockType": mini.get("IpBlockType").getValue() }, function (data, state) {
            if (state == "success") {
                if (data == "yes") {
                    mini.hideMessageBox(messageid);
                    mini.alert("系统配置成功！", "提示", function () {
                    });
                }
            }
        });
    }

</script>
