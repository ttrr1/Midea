<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ToolCache.aspx.cs" Inherits="Manage_System_ToolCache" %>

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
    </style>
</head>
<body>
    <!--撑满页面-->
    <div class="mini-fit">
        <fieldset style="border: solid 1px #aaa; padding: 3px;">
            <legend>缓存内容管理</legend>
            <div style="padding: 5px;">
                <table>
                    <tr style="border-bottom: 1px solid #C0C0C0; margin-bottom: 10px;">
                        <td>
                            <img src="../images/help.gif" />
                        </td>
                        <td style="width: 200px; color: Red; font-size: 13px;">
                            &nbsp; 缓存键
                        </td>
                        <td style="color: Red; font-size: 13px;">
                            类型
                        </td>
                        <td style="color: Red; font-size: 13px;">
                            缓存值
                        </td>
                    </tr>
                    <%
                        foreach (object obj2 in base.Cache)
                        {
                            var entry = (DictionaryEntry)obj2;
                    %>
                    <tr>
                        <td>
                            <img src="../images/help.gif" />
                        </td>
                        <td style="width: 200px;">
                            &nbsp;
                            <%=entry.Key %>：
                        </td>
                        <td>
                            <%=entry.Value.GetType().Name%>
                        </td>
                        <td>
                            <%=entry.Value%>
                        </td>
                    </tr>
                    <%
                        }
                    %>
                </table>
            </div>
        </fieldset>
        <div style="text-align: center; padding: 10px;">
            <a class="mini-button" onclick="onOk" style="width: 160px; margin-right: 20px;">清理系统缓存</a>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">

    function onOk() {

      

        var messageid = mini.loading("请稍等, 缓存清理中 ...", "Loading");
        $.post("/manage/system/ToolCache.aspx?act=clear", {}, function (data, state) {



            if (state == "success") {

                if (data == "yes") {
        

                    mini.alert("缓存清理成功！", "提示", function () {


                        mini.hideMessageBox(messageid);

                        window.location.reload();
                    });


                }
            }

        });



    
    }

</script>
