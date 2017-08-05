<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rolelist.aspx.cs" Inherits="UiDesk_role_rolelist" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>角色管理</title>
    <script src="../../Scripts/boot.js" type="text/javascript"></script>
    <script src="../../Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
   
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
    <div style="width: 100%;">
        <div class="mini-toolbar" style="border-bottom: 0; padding: 0px;">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100%;">
                        <a class="mini-button" name="add" iconcls="icon-add" onclick="add()">增加</a> <a class="mini-button"
                            iconcls="icon-add" name="edit" onclick="edit('-1')">编辑</a>
                    </td>
                    <td style="white-space: nowrap;">
                        <input id="key" class="mini-textbox" emptytext="请输入角色名称" style="width: 150px;" onenter="onKeyEnter" />
                        <a class="mini-button" onclick="search()">查询</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!--撑满页面-->
    <div class="mini-fit">
        <div id="datagrid1" class="mini-datagrid" style="width: 100%; height: 100%;" allowresize="true"
            allowalternating="true" sizelist="[5,10,15,20,100]" pagesize="10" url="/data/Ajax_role.aspx?method=SearchData"
            idfield="RoleID"  >
            <div property="columns">
                <div type="indexcolumn">
                </div>
                <div type="checkcolumn">
                </div>
                <div field="RoleID" width="70" headeralign="center" allowsort="true">
                    角色编号
                </div>
                <div field="RoleName" width="100" headeralign="center" align="center" allowsort="true">
                    角色名称
                </div>
                <div field="AdminNum" width="100" cellstyle="color:red;" headeralign="center" align="center"
                    allowsort="true">
                    角色数量
                </div>
                <div field="CreateTime" width="100" headeralign="center" align="center" allowsort="true"
                    renderer="onCreateTimeRenderer">
                    创建时间
                </div>
                <div field="Note" width="100" headeralign="center" align="center" allowsort="true">
                    备注
                </div>
                <div name="action" width="50" headeralign="center" align="center" renderer="onActionRenderer"
                    cellstyle="padding:0;">
                    操作</div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        mini.parse();
        var grid = mini.get("datagrid1");
        grid.load();
        grid.sortBy("createtime", "desc");
        function add() {
            mini.open({
                url: "/manage/admin/roleitem.aspx",
                title: "新增角色信息", width: 750, height: 620,
                onload: function () {
                    var iframe = this.getIFrameEl();
                    var data = { action: "new" };
                    //   iframe.contentWindow.SetData(data);
                },
                ondestroy: function (action) {
                    grid.reload();
                }
            });
        }


        function onActionRenderer(e) {
            var grid = e.sender;
            var record = e.record;
            var rid = record.RoleID;
            var rowIndex = e.rowIndex;
            var s = '<a style=\"text-decoration: none; color: #006699;\" href="javascript:edit(\'' + rid + '\')">配置权限</a>';
            return s;
        }


        function edit(id) {
            if (id > 0) {
                if (true) {
                    mini.open({
                        url: "/manage/admin/roleitem.aspx?act=edit&RoleId=" + id + "&r=" + Math.random(),
                        title: "角色权限编辑", width: 750, height: 620,
                        onload: function () {
                            var iframe = this.getIFrameEl();
                            var data = { action: "edit" };
                            //  iframe.contentWindow.SetData(data);
                        },
                        ondestroy: function (action) {
                            grid.reload();
                        }
                    });

                } else {
                    myAlert("对不起，请选择一个职位！", "error", 10);
                }

            } else {


          
                var row = grid.getSelected();
                if (row) {
                    mini.open({
                        url: "/manage/admin/roleitem.aspx?act=edit&RoleId=" + row.RoleID + "&r=" + Math.random(),
                        title: "角色权限编辑", width: 750, height: 620,
                        onload: function () {
                            var iframe = this.getIFrameEl();
                            var data = { action: "edit" };
                            //  iframe.contentWindow.SetData(data);
                        },
                        ondestroy: function (action) {
                            grid.reload();
                        }
                    });

                } else {
                    myAlert("对不起，请选择一个角色！", "error", 10);
                }
            }
        }

        /////////////////////////////////////////////////
        function onCreateTimeRenderer(e) {
            var value = e.value;
            if (value) return mini.formatDate(value, 'yyyy-MM-dd   HH:mm');
            return "";
        }
        function remove() {
            var rows = grid.getSelecteds();
            if (rows.length > 0) {
                if (confirm("确定删除选中记录？")) {
                    var ids = [];
                    for (var i = 0, l = rows.length; i < l; i++) {
                        var r = rows[i];
                        ids.push(r.NewsId);
                    }
                    var id = ids.join(',');
                    grid.loading("操作中，请稍后......");
                    $.ajax({
                        url: "/data/Ajax_NewsInfo.aspx?method=Remove&NewsId=" + NewsId,
                        success: function (text) {
                            grid.reload();

                        },
                        error: function () {
                        }
                    });
                }
            } else {
                myAlert("对不起，请选择一条记录！", "error", 10);
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
                opacity:0.01,
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
        function search() {
            var key = mini.get("key").getValue();
            grid.load({ key: key });



            grid.load({ key: key });
        }
        function onKeyEnter(e) {
            search();
        }

     



    </script>
</body>
</html>
