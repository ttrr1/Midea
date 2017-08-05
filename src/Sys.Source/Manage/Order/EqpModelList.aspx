<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EqpModelList.aspx.cs" Inherits="Manage_Order_EqpModelList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>型号列表</title>
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
    <div id="treegrid1" class="mini-treegrid" style="width: 100%; height: 100%;"
        showtreeicon="true" treecolumn="ModelName" idfield="ModelId" parentfield="ParentModelId"
        resultastree="false" allowresize="true" isexpandednode="true">
        <div property="columns">
            <div name="ModelName" field="ModelName" width="200">
                产品型号</div>
            <div name="EqpType" field="EqpType" headeralign="center" align="center" width="200" renderer="onEqpTypeRenderer">
                主机/内机</div>
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
            url: "/manage/Order/EqpModelItem.aspx",
            title: "新增", width: 500, height: 360,
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
        var flagId = record.ModelId;
        var rowIndex = e.rowIndex;
        var s = '<a style=\"text-decoration: none; color: #006699;\" href="javascript:openEdit(\'' + flagId + '\')">编辑</a>&nbsp;|&nbsp;<a style=\"text-decoration: none; color: #006699;\" href="javascript:openDel(\'' + flagId + '\')">删除</a>';
        return s;
    }



    //删除操作
    function openDel(flagId) {
        if (confirm("你确定要删除？")) {
            var messageid = mini.loading("Loading, Please wait ...", "Loading");
            $.post("/data/Ajax_EquipmentModel.aspx", { "method": "DelFlag", "FlagId": flagId }, function (date, state) {
                if (state == "success") {
                    mini.hideMessageBox(messageid);
                    if (date == "0") {
                        mini.alert("对不起，此标记下还有子项目，不能进行删除操作！~");
                    }
                    else {
                        getSource();
                    }
                }
            }, "html");
        }
    }
    function getSource() {

        var grid = mini.get("treegrid1");
        grid.load("/data/Ajax_EquipmentModel.aspx?method=SearchData");
        grid.expandLevel(0);
    }

    function openEdit(flagId) {
        mini.open({
            url: "/manage/Order/EqpModelItem.aspx",
            title: "编辑型号", width: 600, height: 360,
            onload: function () {
                var iframe = this.getIFrameEl();
                var data = { action: "edit", FlagId: flagId };
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
    function onEqpTypeRenderer(e) {
        if (e.value == 1) return "主机";
        else if (e.value == 2) return "内机";
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
