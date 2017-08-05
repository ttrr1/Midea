<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ToolServer.aspx.cs" Inherits="Manage_System_ToolServer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../Scripts/boot.js" type="text/javascript"></script>
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
    </style>
</head>
<body>
    <!--撑满页面-->
    <div class="mini-fit">
        <fieldset style="border: solid 1px #aaa; padding: 3px;">
            <legend>
                <%=Sys.Kernel.Software.Name %>
                软件信息</legend>
            <div style="padding: 5px;">
                <table>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;系统版本号：
                        </td>
                        <td>
                            <%=Sys.Kernel.Software.Version%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;系统数据库：
                        </td>
                        <td>
                            <%=Sys.Kernel.Software.Database%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;数据库名称：
                        </td>
                        <td>
                            <% var bll = new Sys.BLL.Common();
                               Response.Write(bll.DatabaseName());%>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <fieldset style="border: solid 1px #aaa; padding: 3px;">
            <legend>服务器软件信息</legend>
            <div style="padding: 5px;">
                <table>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;服务器名称：
                        </td>
                        <td>
                            <%=Server.MachineName%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;操作系统：
                        </td>
                        <td>
                            <%=Environment.OSVersion.ToString()%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;服务器IP：
                        </td>
                        <td>
                            <%=Request.ServerVariables["LOCAL_ADDR"]%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;服务端脚本执行超时：
                        </td>
                        <td>
                            <%=Server.ScriptTimeout.ToString()%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;服务器现在时间：
                        </td>
                        <td>
                            <%=DateTime.Now.ToString()%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;Session总数：
                        </td>
                        <td>
                            <%=Session.Contents.Count.ToString()%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;Application总数：
                        </td>
                        <td>
                            <%=Application.Contents.Count.ToString()%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;.NET Framework 版本：
                        </td>
                        <td>
                            <%=Environment.Version.ToString()%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;IIS版本：
                        </td>
                        <td>
                            <%=Request.ServerVariables["SERVER_SOFTWARE"]%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;相对路径：
                        </td>
                        <td>
                            <%=Request.ServerVariables["PATH_INFO"]%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;物理路径：
                        </td>
                        <td>
                            <%=Request.ServerVariables["APPL_PHYSICAL_PATH"]%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" />
                            &nbsp;运行时间：
                        </td>
                        <td>
                            <%=(Math.Round(double.Parse((Environment.TickCount / 600 / 60).ToString())) / 100).ToString() + "小时"%>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
    </div>
</body>
</html>
