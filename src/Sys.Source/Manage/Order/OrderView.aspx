<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderView.aspx.cs" Inherits="Manage_Order_OrderView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>订单分配</title>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <script src="../../scripts/boot.js" type="text/javascript"></script>
    <script src="../../Scripts/common.js" type="text/javascript"></script>
    <style type="text/css">
        html, body
        {
            font-size: 12px;
            padding: 0;
            margin: 0;
            border: 0;
            height: 100%;
            overflow: hidden;
        }
        
        .searchbox .mini-buttonedit-icon
        {
            background: url(../../Scripts/miniui/themes/icons/search.gif) no-repeat 50% 50%;
        }
        
        .asLabel .mini-textbox-border, .asLabel .mini-textbox-input, .asLabel .mini-buttonedit-border, .asLabel .mini-buttonedit-input, .asLabel .mini-textboxlist-border
        {
            border: 0;
            background: none;
            cursor: default;
        }
        .asLabel .mini-buttonedit-button, .asLabel .mini-textboxlist-close
        {
            display: none;
        }
        .asLabel .mini-textboxlist-item
        {
            padding-right: 8px;
        }
    </style>
</head>
<body>
    <form id="form1" method="post">
    <input name="OrderId" id="OrderId" class="mini-hidden" />
    <input name="PicList" id="PicList" class="mini-hidden" />
    <fieldset style="border: solid 1px #aaa; padding: 3px;">
        <legend>订单基本信息</legend>
        <div style="padding: 5px;">
            <table>
                <tr>
                    <td style="width: 80px;">
                        项目名称：
                    </td>
                    <td style="width: 180px;">
                        <input name="ProjectName" id="ProjectName" class="mini-textbox" width="180" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">
                        用户名称：
                    </td>
                    <td style="width: 180px;">
                        <input name="CustomName" id="CustomName" class="mini-textbox" width="180" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">
                        用户地址：
                    </td>
                    <td style="width: 180px;">
                        <input name="ProvinceName" id="Text2" class="mini-textbox" width="50" />
                        <input name="CityName" id="Text5" class="mini-textbox" width="50" />
                        <input name="AreaName" id="Text6" class="mini-textbox" width="50" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">
                        详细地址：
                    </td>
                    <td style="width: 180px;">
                        <input name="Address" id="Address" class="mini-textbox" width="180" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">
                        联系人：
                    </td>
                    <td style="width: 180px;">
                        <input name="Contact" id="Contact" class="mini-textbox" width="180" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">
                        联系电话：
                    </td>
                    <td style="width: 180px;">
                        <input name="Tel" id="Tel" class="mini-textbox" width="180" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">
                        备注：
                    </td>
                    <td style="width: 180px;">
                        <input name="Remark" id="Remark" class="mini-textbox" width="180" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="padding: 5px;">
            <div id="orderTypeGrid" class="mini-datagrid" showpager="false" style="width: auto;
                height: auto;" url="/data/Ajax_Orders.aspx?method=GetOrderType">
                <div property="columns">
                    <div field="ProductTypeName" width="50">
                        产品类型
                    </div>
                    <div field="MainModelName" width="50">
                        主机型号
                    </div>
                    <div field="MainModelNum" width="50">
                        产品数量
                    </div>
                    <div field="SubModelName1" width="50">
                        内机型号1
                    </div>
                    <div field="SubModelNum1" width="30">
                        数量
                    </div>
                    <div field="SubModelName2" width="50">
                        内机型号2
                    </div>
                    <div field="SubModelNum2" width="30">
                        数量
                    </div>
                    <div field="SubModelName3" width="50">
                        内机型号3
                    </div>
                    <div field="SubModelNum3" width="30">
                        数量
                    </div>
                    <div field="SubModelName4" width="50">
                        内机型号4
                    </div>
                    <div field="SubModelNum4" width="30">
                        数量
                    </div>
                    <div field="ModelName" width="50">
                        产品型号
                    </div>
                    <div field="ModelNum" width="30">
                        数量
                    </div>
                </div>
            </div>
        </div>
    </fieldset>
    <fieldset style="border: solid 1px #aaa; padding: 3px;">
        <legend>处理流程</legend>
        <div style="padding: 5px;">
            <div id="flowGrid" class="mini-datagrid" showpager="false" style="width: auto; height: auto;"
                url="/data/Ajax_Orders.aspx?method=GetOrderFlow">
                <div property="columns">
                    <div field="CreateDate" width="50" renderer="onCreateTimeRenderer">
                        处理时间
                    </div>
                    <div field="OrderStatus" width="50" renderer="onOrderStatusRenderer">
                        订单状态
                    </div>
                    <div field="StatusFlag" width="50" renderer="onStatusFlagRenderer">
                        是否完成
                    </div>
                    <div field="StatusMessage" width="150">
                        备注
                    </div>
                    <div field="picList" width="200">
                        图片
                    </div>
                </div>
            </div>
        </div>
    </fieldset>
    </form>
    <script type="text/javascript">
        mini.parse();
        var form = new mini.Form("form1");
        var orderTypeGrid = mini.get("orderTypeGrid");
        var flowGrid = mini.get("flowGrid");

        ////////////////////
        //标准方法接口定义
        function SetData(data) {
            if (data.id > 0) {
                //跨页面传递的数据对象，克隆后才可以安全使用
                data = mini.clone(data);
                $.ajax({
                    url: "/data/Ajax_Orders.aspx?method=GetView&id=" + data.id,
                    cache: false,
                    success: function (text) {
                        var o = mini.decode(text);
                        form.setData(o);
                        form.setChanged(false);

                        orderTypeGrid.load({ orderId: data.id });

                        flowGrid.load({ orderId: data.id });

                        labelModel();

                        flowGrid.on("drawcell", function (e) {
                            var record = e.record,
                            column = e.column;
                            var status = record.OrderStatus;
                            var flag = record.StatusFlag;

                            if (column.field == "picList") {
                                if (status == 4 && flag == 1) {
                                    var img = mini.get("PicList").getValue();
                                    if (img != '') {
                                        var imgArr = img.split(',');
                                        for (var i = 0; i < imgArr.length; i++) {
                                            e.cellHtml += '<img src="../..' + imgArr[i] + '" width="50" height="50" style="padding-right:5px;" />';
                                        }
                                    }
                                }
                            }
                        });

                    }
                });

            }
        }

        function GetData() {
            var o = form.getData();
            return o;
        }
        function CloseWindow(action) {
            window.close();

            if (action == "close" && form.isChanged()) {
                if (confirm("数据被修改了，是否先保存？")) {
                    return false;
                }
            }
            if (window.CloseOwnerWindow) return window.CloseOwnerWindow(action);
            else window.close();
        }
  
        function onCancel(e) {
            CloseWindow("cancel");
        }
        //////////////////////////////////
        function labelModel() {
            var fields = form.getFields();
            for (var i = 0, l = fields.length; i < l; i++) {
                var c = fields[i];
                if (c.setReadOnly) c.setReadOnly(true);     //只读
                if (c.setIsValid) c.setIsValid(true);      //去除错误提示
                if (c.addCls) c.addCls("asLabel");          //增加asLabel外观
            }
        }

        function onStatusFlagRenderer(e) {
            var value = e.value;
            if (value == '0') {
                return "进行中";
            }
            else if (value == '1') {
                return "已完成";
            }
        }


    </script>
</body>
</html>
