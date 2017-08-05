<%@ Page Language="C#" AutoEventWireup="true" CodeFile="toollog.aspx.cs" Inherits="Manage_system_toollog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <div class="mini-toolbar" style="padding: 2px; border-bottom: 0;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <a class="mini-button" iconcls="icon-reload" onclick="onSearch();">刷新</a>
                </td>
                <td style="white-space: nowrap;">
                    <label style="font-family: Verdana;">
                        操作日期:
                    </label>
                    <input id="begtime" name="begtime" class="mini-datepicker" format="yyyy-MM-dd" allowinput="false" />
                    <label style="font-family: Verdana;">
                        账号:
                    </label>
                    <input id="key" class="mini-textbox" emptytext="请输入用户名" style="width: 150px;" onenter="onKeyEnter" />
                    <a class="mini-button" iconcls="icon-search" onclick="onSearch()">查询</a>
                </td>
            </tr>
        </table>
    </div>
    <!--撑满页面-->
    <div class="mini-fit">
        <div id="datagrid1" class="mini-datagrid" style="width: 100%; height: 100%;" url="/data/Ajax_toollog.aspx?method=SearchData"
            idfield="id" sizelist="[5,10,20,50]" pagesize="17">
            <div property="columns">
                <div type="indexcolumn">
                </div>
                <div field="ID" width="60" headeralign="center" allowsort="true">
                    编号</div>
                <div field="Username" width="80" headeralign="center" allowsort="true">
                    用户名</div>
                <div field="CreateIP" width="100" align="center" renderer="onGenderRenderer" headeralign="center">
                    操作IP</div>
                <div field="Flag" width="100" align="center" renderer="onGenderRenderer" headeralign="center"
                    allowsort="true">
                    模块</div>
                <div field="Log" width="100" align="center" renderer="onGenderRenderer" headeralign="center"
                    allowsort="true">
                    日志</div>
                <div field="CreateTime" width="100" align="center" renderer="onGenderRenderer" headeralign="center"
                    dateformat="yyyy-MM-dd" allowsort="true">
                    操作时间</div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        mini.parse();
        var grid = mini.get("datagrid1");
        grid.load();
        function onSearch() {
            var key = mini.get("key").getValue();

            var begtime = mini.get("begtime").getFormValue();

            grid.load({ key: key, begtime: begtime });
        }


        function onKeyEnter() {
            onSearch();

        }
    </script>
</body>
</html>
