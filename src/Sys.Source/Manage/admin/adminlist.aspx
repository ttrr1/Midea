<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminlist.aspx.cs" Inherits="UiDesk_admin_adminlist" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>管理员管理</title>
    <script src="../../Scripts/boot.js" type="text/javascript"></script>
    <script src="../../Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
</head>
<body>
    <div style="width: 100%;">
        <div class="mini-toolbar" style="border-bottom: 0; padding: 0px;">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 30%;">
                        <a class="mini-button" iconcls="icon-add" onclick="add()">添加</a> <a class="mini-button"
                            iconcls="icon-remove" enabled="false" onclick="remove()">删除</a> <a class="mini-button"
                                iconcls="icon-save" onclick="saveData()" enabled="false" plain="true">保存</a>
                    </td>
                    <td align="left">
                        <a class="mini-button" onclick="expandAll()">展开所有节点</a> <a class="mini-button" onclick="collapseAll()">
                            折叠所有节点</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!--撑满页面-->
    <div id="treegrid1" class="mini-treegrid" style="width: 100%; height: 100%;" treecolumn="Username"
        idfield="UserID" parentfield="ParentUserID" resultastree="false" allowresize="true">
        <div property="columns">
            <div name="Username" field="Username">
                用户名</div>
            <div name="UserID" field="UserID" width="30">
                用户编号</div>
            <div name="RealName" field="RealName" headeralign="center" align="center">
                真实姓名</div>
            <div name="OfficeTel" field="OfficeTel" headeralign="center" align="center">
                办公电话</div>
            <div name="State" field="State" headeralign="center" align="center" renderer="onOpenRenderer">
                账户状态</div>
            <div name="action" headeralign="center" align="center" renderer="onActionRenderer">
                操作</div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
    mini.parse();
    getSource();
    function add() {
        mini.open({
            url: "/manage/admin/adminitem.aspx",
            title: "添加用户", width: '80%', height: '80%',
            onload: function () {
                var iframe = this.getIFrameEl();
                var data = { action: "new" };
                iframe.contentWindow.SetData(data);
            },
            ondestroy: function (action) {
                getSource();
            }
        });
    }

    function onActionRenderer(e) {
        var grid = e.sender;
        var record = e.record;
        var userId = record.UserID;
        var s = '<a style=\"text-decoration: none; color: #006699;\" href="javascript:allot(\'' + userId + '\')">附加权限</a>&nbsp;|&nbsp;<a style=\"text-decoration: none; color: #006699;\" href="javascript:openEdit(\'' + userId + '\')">编辑</a>';

        return s;

    }

    function allot(id) {
        if (true) {
            mini.open({
                url: "/manage/admin/PlusFlag.aspx?act=edit&UserId=" + id + "&r=" + Math.random(),
                title: "附加权限", width: 750, height: 620,
                onload: function () {
                    var iframe = this.getIFrameEl();
                    var data = { action: "edit" };
                    //  iframe.contentWindow.SetData(data);
                },
                ondestroy: function (action) {
                    getSource();
                }
            });

        } else {
            myAlert("对不起，请选择一个职位！", "error", 10);
        }

    }

    function getSource() {

        var grid = mini.get("treegrid1");
        grid.load("/data/Ajax_Admin.aspx?method=SearchData");
        grid.expandLevel(0);

    }

    function onBeforeTreeLoad(e) {
        var tree = e.sender;    //树控件
        var node = e.node;      //当前节点
        var params = e.params;  //参数对象


    }

    function openEdit(userId) {
        mini.open({
            url: "/manage/admin/adminitem.aspx",
            title: "管理员信息编辑", width: '80%', height: '80%',
            onload: function () {
                var iframe = this.getIFrameEl();
                var data = { action: "edit", UserId: userId };
                iframe.contentWindow.SetData(data);
            },
            ondestroy: function (action) {
                getSource();
            }
        });
    }
    function onOpenRenderer(e) {
        if (e.value == 1) return "<span style=\"color:green;\">正常</span>";
        else return "<span style=\"color:red;\">锁定</span>";
    }
    function onNavRenderer(e) {
        if (e.value == 1) return "<span style=\"color:green;\">是</span>";
        else return "<span style=\"color:red;\">否</span>";
    }

    function collapseAll() {
        var tree = mini.get("treegrid1");
        tree.collapseAll();
    }
    function expandAll() {
        var tree = mini.get("treegrid1");
        tree.expandAll();
    }

</script>
