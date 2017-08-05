<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderList.aspx.cs" Inherits="Manage_Order_OrderList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单列表</title>
    <script src="../../Scripts/boot.js" type="text/javascript"></script>
    <script src="../../Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
    <script src="../../Scripts/common.js" type="text/javascript"></script>
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
    <input name="hidorderStatus" id="hidorderStatus" class="mini-hidden" value="<%=orderStatus %>" />
    <div style="width: 100%;">
        <div class="mini-toolbar" style="border-bottom: 0; padding: 0px;">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100%;">
                        <a class="mini-button" iconcls="icon-add" onclick="add()" style="display: none;">添加</a>
                    </td>
                    <td style="white-space: nowrap;">
                        <label for="ProjectName$text">项目名称：</label>
                        <input name="ProjectName" id="ProjectName" class="mini-textbox" width="100" />
                        <label for="cmbDate$text">时间段：</label>
                        <input id="cmbDate" name="cmbDate" class="mini-combobox" style="width: 100px;"
                            textfield="text" valuefield="id" url="/data_txt/orderDate.txt"
                            value="" shownullitem="true" nullitemtext="请选择..." />
                        <input id="orderStatus" name="orderStatus" class="mini-combobox" style="width: 100px;"
                            textfield="Value" valuefield="Key" url="/data/Ajax_Orders.aspx?method=GetOrderStatus"
                            value="" shownullitem="true" nullitemtext="请选择..." />
                        <a class="mini-button" onclick="search()">查询</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="mini-fit">
        <div id="datagrid1" class="mini-datagrid" style="width: 100%; height: 100%;" allowresize="true"
            allowalternating="true" pagesize="10" url="/data/Ajax_Orders.aspx?method=SearchData"
            idfield="OrderId">
            <div property="columns">
                <div name="ProjectName" field="ProjectName" width="60">
                    项目名称</div>
                <div name="CustomName" field="CustomName" headeralign="center" align="center" width="60">
                    用户名称</div>
                <div name="ProvinceName" field="ProvinceName" headeralign="center" align="center"
                    width="30">
                    省</div>
                <div name="CityName" field="CityName" headeralign="center" align="center" width="30">
                    市</div>
                <div name="AreaName" field="AreaName" headeralign="center" align="center" width="30">
                    区</div>
                <div name="Address" field="Address" headeralign="center" align="center" width="100">
                    详细地址</div>
                <div name="CreateDate" field="CreateDate" headeralign="center" align="center" width="60"
                    renderer="onCreateTimeRenderer">
                    订单时间</div>
                <div name="OrderStatus" field="OrderStatus" headeralign="center" align="center" width="60"
                    renderer="onOrderStatusRenderer">
                    订单状态</div>
                <div name="workerName" field="workerName" headeralign="center" align="center" width="60">
                    当前安装工人</div>
                <div name="action" headeralign="center" align="center" renderer="onActionRenderer">
                    操作</div>
            </div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">
    mini.parse();
    var grid = mini.get("datagrid1");
    //grid.load();
    var status = mini.get("hidorderStatus").getValue();
    grid.load({ orderStatus: status });
    //set status value
    mini.get("orderStatus").setValue(status);

    function onActionRenderer(e) {
        var grid = e.sender;
        var record = e.record;
        var id = record.OrderId;
        var rowIndex = e.rowIndex;
        var s = '<a style=\"text-decoration: none; color: #006699;\" href="javascript:orderView(\'' + id + '\')">查看</a>&nbsp;|&nbsp;';
        s += '<a style=\"text-decoration: none; color: #006699;\" href="javascript:orderAssign(\'' + id + '\')">订单分配</a>&nbsp;|&nbsp;<a style=\"text-decoration: none; color: #006699;\" href="javascript:del(\'' + id + '\')">删除</a>';
        return s;
    }

    //删除操作
    function del(id) {
        if (confirm("你确定要删除？")) {
            var messageid = mini.loading("Loading, Please wait ...", "Loading");
            $.post("/data/Ajax_Orders.aspx", { "method": "DelOrder", "id": id }, function (date, state) {
                if (state == "success") {
                    mini.hideMessageBox(messageid);
                    if (date == "0") {
                        mini.alert("执行失败！");
                    }
                    else {
                        grid.reload();
                    }
                }
            }, "html");
        }
    }

    function orderAssign(id) {
        mini.open({
            url: "/manage/Order/OrderAssign.aspx?r=" + Math.random(),
            title: "订单分配", width: 850, height: 620,
            onload: function () {
                var iframe = this.getIFrameEl();
                var data = { id: id };
                iframe.contentWindow.SetData(data);
            },
            ondestroy: function (action) {
                grid.reload();
            }
        });
    }

    function orderView(id) {
        mini.open({
            url: "/manage/Order/OrderView.aspx?r=" + Math.random(),
            title: "订单查看", width: 850, height: 620,
            onload: function () {
                var iframe = this.getIFrameEl();
                var data = { id: id };
                iframe.contentWindow.SetData(data);
            },
            ondestroy: function (action) {
                grid.reload();
            }
        });
    }

    function search() {
        var projectName = mini.get("ProjectName").getValue();
        var cmbDate = mini.get("cmbDate").getValue();
        var orderStatus = mini.get("orderStatus").getValue();
        grid.load({ orderStatus: orderStatus, projectName: projectName, cmbDate: cmbDate });
    }

</script>
