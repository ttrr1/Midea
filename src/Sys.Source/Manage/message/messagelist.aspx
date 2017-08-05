<%@ Page Language="C#" AutoEventWireup="true" CodeFile="messagelist.aspx.cs" Inherits="Manage_message_messagelist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../Scripts/boot.js" type="text/javascript"></script>
    <script src="../../Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
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
    <fieldset style="border: solid 1px #aaa; padding: 3px;">
        <legend>搜索条件</legend>
        <div style="padding: 5px;">
            <table>
                <tr>
                    <td style="white-space: nowrap;">
                        <label style="font-family: Verdana;">
                            发布日期:
                        </label>
                        <input id="begtime" name="begtime" class="mini-datepicker" format="yyyy-MM-dd" allowinput="false" />
                        <label style="font-family: Verdana;">
                            信息标题:
                        </label>
                        <input id="key" class="mini-textbox" emptytext="请输入信息标题" style="width: 150px;" onenter="onKeyEnter" />
                        <a class="mini-button" iconcls="icon-search" onclick="onSearch()">查询</a>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <div class="mini-toolbar" style="padding: 2px; margin-top:8px; border-bottom: 0;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                     <a class="mini-button"
                        iconcls="icon-add" onclick="add();">新增</a> &nbsp;<a class="mini-button" iconcls="icon-edit"
                            onclick="edit();">编辑</a> &nbsp;<a class="mini-button" iconcls="icon-remove" onclick="remove()">
                                删除</a>
                </td>
            </tr>
        </table>
    </div>
    <!--撑满页面-->
    <div class="mini-fit">
        <div id="datagrid1" class="mini-datagrid" style="width: 100%; height: 100%;" url="/data/Ajax_message.aspx?method=SearchData"
            idfield="ID" sizelist="[5,10,20,50]" pagesize="17">
            <div property="columns">
                <div type="checkcolumn">
                    选择
                </div>
                <div type="indexcolumn">
                </div>
                <div field="NewsTitle" width="160" headeralign="center" allowsort="true">
                    信息标题</div>
                <div field="Source" width="60" align="center" headeralign="center" allowsort="true">
                    信息来源</div>
                <div field="Publisher" width="60" align="center" headeralign="center" allowsort="true">
                    信息发布者</div>
                <div field="TotalClick" width="40" align="center" headeralign="center">
                    点击率</div>
                <div field="CreateTime" width="70" align="center" headeralign="center" dateformat="yyyy-MM-dd"
                    allowsort="true">
                    发布时间</div>
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




        function add() {
            mini.open({
                url: "/manage/message/messageitem.aspx",
                title: "新增信息", width: 820, height: 600,
                onload: function () {
                    var iframe = this.getIFrameEl();
                    var data = { action: "new" };
                    iframe.contentWindow.SetData(data);
                },
                ondestroy: function (action) {
                    onSearch();
                }
            });
        }

        //删除操作
        function remove() {
            var rows = grid.getSelecteds();
            if (rows.length > 0) {
                if (confirm("确定删除选中记录？")) {
                    var ids = [];
                    for (var i = 0, l = rows.length; i < l; i++) {
                        var r = rows[i];
                        ids.push(r.ID);
                    }
                    var id = ids.join(',');
                    grid.loading("操作中，请稍后......");
                    $.ajax({
                        url: "/data/Ajax_message.aspx?method=Remove&ID=" + id,
                        success: function (text) {
                            myAlert("记录删除成功！", "error", 2);
                            grid.reload();
                        },
                        error: function () {
                        }
                    });
                }
            } else {
                myAlert("对不起，请选择一条记录！", "succeed", 2);

            }
        }


        function edit() {
            var row = grid.getSelected();
            if (row) {
                mini.open({
                    url: "/manage/message/messageitem.aspx",
                    title: "信息编辑", width: 820, height: 600,
                    onload: function () {
                        var iframe = this.getIFrameEl();
                        var data = { action: "edit", ID: row.ID };
                        iframe.contentWindow.SetData(data);
                    },
                    ondestroy: function (action) {
                        grid.reload();
                    }
                });

            } else {

                myAlert("对不起，请选择一个信息项！", "error", 2);
            }

        }
        //倒计时关闭提醒
        //1=error,2=warning ,3=succeed
        function myAlert(content, ico, times) {
            var timer;
            art.dialog({
                content: '<span style=\"color:green;\">' + content + '..</span>',
                icon: ico,
                lock: true,
                opacity: 0.01,
                ok: function () {

                    return true;
                },
                init: function () {
                    var that = this, i = times;
                    var fn = function () {
                        that.title(i + '秒后关闭\t\t\t系统友情提示');
                        !i && that.close();
                        i--;
                    };
                    timer = setInterval(fn, 1000);
                    fn();
                },
                close: function () {
                    clearInterval(timer);
                }
            }).show();
        }

        function onKeyEnter() {
            onSearch();

        }
    </script>
</body>
</html>
