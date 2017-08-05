<%@ Page Language="C#" AutoEventWireup="true" CodeFile="messageTypeList.aspx.cs"
    Inherits="Manage_message_messageTypeList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>权限模块管理</title>
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
                            iconcls="icon-remove" enabled="false" onclick="remove()">删除</a>
                    </td>
                    <td align="left">
                        <a class="mini-button" onclick="expandAll()">展开所有节点</a> <a class="mini-button" onclick="collapseAll()">
                            折叠所有节点</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="treegrid1" class="mini-treegrid" style="width: 100%; height: 100%;" showtreeicon="true"
        treecolumn="TypeName" idfield="ID" parentfield="PId" resultastree="false" allowresize="true"
        isexpandednode="true">
        <div property="columns">
            <div name="TypeName" field="TypeName" width="100">
                类别名称</div>
            <div name="TypeAction" field="TypeAction" headeralign="center" align="center" width="180">
                模块标记</div>
            <div name="Count" field="Count" headeralign="center" align="center">
                信息数量</div>
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
            url: "/manage/message/messageTypeItem.aspx",
            title: "新增类别模块", width: 330, height: 240,
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
        var ID = record.ID;
        var rowIndex = e.rowIndex;
        var s = '<a style=\"text-decoration: none; color: #006699;\" href="javascript:openEdit(\'' + ID + '\')">编辑</a>';
        return s;
    }


    function getSource() {

        var grid = mini.get("treegrid1");
        grid.load("/data/Ajax_messageType.aspx?method=SearchData");
        grid.expandAll();
    }

    function openEdit(ID) {
        mini.open({
            url: "/manage/message/messageTypeItem.aspx",
            title: "编辑类别信息", width: 330, height: 240,
            onload: function () {
                var iframe = this.getIFrameEl();
                var data = { action: "edit", ID: ID };
                iframe.contentWindow.SetData(data);
            },
            ondestroy: function (action) {
                getSource();
            }
        });
    }
    function onOpenRenderer(e) {
        if (e.value == 1) return "<span style=\"color:green;\">是</span>";
        else return "<span style=\"color:red;\">否</span>";
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
