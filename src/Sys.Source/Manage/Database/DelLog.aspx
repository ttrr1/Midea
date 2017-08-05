<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DelLog.aspx.cs" Inherits="Manage_Database_DelLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
    <div class="mini-fit">
        <fieldset style="border: solid 1px #aaa; padding: 3px; margin-bottom: 12px;">
            <legend>数据库日志清除日志</legend>
            <div style="padding: 5px;">
                <table>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" alt="数据库日志清除日志 " />
                            &nbsp;数据库日志 &nbsp;<img src="../images/tips.gif" />
                            &nbsp;清除日志：
                        </td>
                        <td>
                            <a class="mini-button" onclick="onDel" style="width: 100px; margin-right: 20px;">执行</a>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <div id="datagrid1" class="mini-datagrid" style="width: 100%; height: 100%;" url="/data/Ajax_toollog.aspx?method=SearchData&type=del"
            idfield="id" sizelist="[5,10,20,50]" pagesize="17">
            <div property="columns">
                <div type="indexcolumn">
                </div>
                <div field="Username" width="50" headeralign="center" allowsort="true">
                    用户名</div>
                <div field="CreateIP" width="100" align="center" renderer="onGenderRenderer" headeralign="center">
                    操作IP</div>
                <div field="Log" width="400" align="center" renderer="onGenderRenderer" headeralign="center"
                    allowsort="true">
                    详情</div>
                <div field="CreateTime" width="100" align="center" renderer="onGenderRenderer" headeralign="center"
                    dateformat="yyyy-MM-dd" allowsort="true">
                    操作时间</div>
            </div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">

    mini.parse();
    var grid = mini.get("datagrid1");
    grid.load();

    function onDel() {

        var messageid = mini.loading("请稍等,数据库日志清除中 ...", "Loading");
        //发送邮件测试
        $.post("/manage/Database/DelLog.aspx?act=del", {}, function (data, state) {
            if (state == "success") {
                mini.hideMessageBox(messageid);
                if (data == "yes") {
                    mini.alert("日志清除成功！", "提示", function () {
                        window.location.reload();
                    });
                }

                else {
                    mini.alert("日志清除失败！<br/>" + data, "提示", function () {
                        window.location.reload();
                    });

                }
            }
        });
    }
</script>
