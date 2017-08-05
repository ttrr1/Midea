<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderAssign.aspx.cs" Inherits="Manage_Order_OrderAssign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单分配</title>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <script src="../../scripts/boot.js" type="text/javascript"></script>
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
        <legend>安装工人分配</legend>
        <div style="padding: 5px;">
            <table>
                <%--<tr>
                    <td style="width: 100px;">
                        所属区域：
                    </td>
                    <td style="width: 100px;">
                        <input name="ProvinceName" id="Text1" class="mini-textbox" width="50" />
                        <input name="CityName" id="Text3" class="mini-textbox" width="50" />
                        <input name="AreaName" id="Text4" class="mini-textbox" width="50" />
                    </td>
                </tr>--%>
                <tr>
                    <td style="width: 100px;">
                        当前人员：
                    </td>
                    <td style="width: 180px;">
                        <input name="workerName" id="curWorkerName" class="mini-textbox" width="180" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">
                        分配人员：
                    </td>
                    <td style="width: 180px;">
                        <input id="newWorkerId" name="newWorkerId" class="mini-combobox" style="width: 180px;" required="true"
                            textfield="Value" valuefield="Key" value="" shownullitem="true" nullitemtext="请选择..." />
                    </td>
                </tr>
            </table>
        </div>
        <div style="text-align: center; padding: 10px;">
            <a class="mini-button" onclick="onOk" style="width: 60px; margin-right: 20px;">确定</a>
            <a class="mini-button" onclick="onCancel" style="width: 60px;">取消</a>
        </div>
    </fieldset>
    </form>
    <script type="text/javascript">
        mini.parse();
        var form = new mini.Form("form1");
        var orderTypeGrid = mini.get("orderTypeGrid");

        function SaveData() {
            var o = form.getData();
            form.validate();
            if (form.isValid() == false) return;
            var json = mini.encode([o]);
            $.ajax({
                url: "/data/Ajax_Orders.aspx?method=InstallerAssign",
                data: { data: json },
                cache: false,
                success: function (text) {
                    if (text == '-1') {
                        mini.alert("订单进行中，不能更换安装工人！");
                    } else {
                        mini.alert("分配成功！");
                        CloseWindow("save");
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(jqXHR.responseText);
                    CloseWindow();
                }
            });
        }

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

                        //人员筛选
                        var WorkerId = o["WorkerId"] != null ? o["WorkerId"] : '';
                        var ProvinceId = o["ProvinceId"] != null ? o["ProvinceId"] : '';
                        var CityId = o["WorkerId"] != null ? o["CityId"] : '';
                        var AreaId = o["AreaId"] != null ? o["AreaId"] : '';
                        onWorkIdChanged(WorkerId, ProvinceId, CityId, AreaId);

                        labelModel();
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
        function onOk(e) {
            SaveData();
        }
        function onCancel(e) {
            CloseWindow("cancel");
        }
        //////////////////////////////////
        function labelModel() {
            var fields = form.getFields();
            for (var i = 0, l = fields.length; i < l; i++) {
                var c = fields[i];
                if (c.id != 'newWorkerId') {
                    if (c.setReadOnly) c.setReadOnly(true);     //只读
                    if (c.setIsValid) c.setIsValid(true);      //去除错误提示
                    if (c.addCls) c.addCls("asLabel");          //增加asLabel外观
                }
            }
        }

        function onWorkIdChanged(wId, proId, cityId, areaId) {
            var newWorkerId = mini.get("newWorkerId");
            newWorkerId.setValue("");
            var url = "/data/Ajax_Orders.aspx?method=GetUserInfoByArea&proId=" + proId + "&cityId=" + cityId + "&areaId=" + areaId;
            newWorkerId.setUrl(url);
        }
    </script>
</body>
</html>
