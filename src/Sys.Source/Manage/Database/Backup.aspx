<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Backup.aspx.cs" Inherits="Manage_Database_Backup" %>

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
            <legend>数据库备份</legend>
            <div style="padding: 5px;">
                <table>
                    <tr>
                        <td style="width: 200px;">
                            <img src="../images/help.gif" alt="备份路径 " />
                            &nbsp;备份路径：
                        </td>
                        <td>
                            <input name="DatabaseBackupPath" emptytext="备份路径" id="DatabaseBackupPath" class="mini-textbox"
                                width="200" required="true" value="D:\DB" />
                            <a class="mini-button" onclick="onBackup" style="width: 100px; margin-right: 20px;">
                                执行</a>
                        </td>
                    </tr>
                </table>
            </div>
        </fieldset>
        <div id="datagrid1" class="mini-datagrid" style="width: 100%; height: 100%;" url="/data/Ajax_toollog.aspx?method=SearchData&type=db"
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

    function onBackup() {
        var DatabaseBackupPath = mini.get("DatabaseBackupPath").getValue();
        var messageid = mini.loading("请稍等,数据库备份中 ...", "Loading");
        //发送邮件测试
        $.post("/manage/Database/Backup.aspx?act=bak", { "DatabaseBackupPath": DatabaseBackupPath }, function (data, state) {
            if (state == "success") {
                mini.hideMessageBox(messageid);
                if (data == "yes") {
                    mini.alert("数据库备份成功！", "提示", function () {
                        window.location.reload();
                    });
                }

                else {
                    mini.alert("数据库备份失败！<br/>" + data, "提示", function () {
                        window.location.reload();
                    });

                }
            }
        });
    }
</script>
