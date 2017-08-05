<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EqpModelItem.aspx.cs" Inherits="Manage_Order_EqpModelItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>型号编辑</title>
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
    </style>
</head>
<body>
    <form id="form1" method="post">
    <input name="ModelId" id="ModelId" class="mini-hidden" />
    <fieldset style="border: solid 1px #aaa; padding: 3px;">
        <legend>基本信息</legend>
        <div style="padding: 5px;">
            <table>
                <tr>
                    <td style="color: red;" style="width: 120px;">
                        <img src="../images/help.gif" />&nbsp; 产品型号：
                    </td>
                    <td style="width: 180px;">
                        <input name="ModelName" id="ModelName" class="mini-textbox" width="180" required="true" />
                    </td>
                </tr>

                <tr>
                    <td style="color: red;">
                        <img src="../images/help.gif" />
                        &nbsp;父级型号：
                    </td>
                    <td>
                        <input id="ParentModelId" name="ParentModelId" class="mini-buttonedit searchbox" textname="parentModelName"
                            onbuttonclick="onButtonEdit" allowinput="false" width="180" required="true" />
                    </td>
                </tr>

                <tr id="trEqpType" style="display: none;">
                    <td>
                        <img src="../images/help.gif" />&nbsp;主机/内机：
                    </td>
                    <td>
                        <input id="EqpType" name="EqpType" class="mini-combobox" style="width: 180px;" textfield="text"
                            valuefield="id" url="/data_txt/eqpType.txt" value="" required="true" allowinput="true"
                            shownullitem="true" nullitemtext="请选择..." />
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <div style="text-align: center; padding: 10px;">
        <a class="mini-button" onclick="onOk" style="width: 60px; margin-right: 20px;">确定</a>
        <a class="mini-button" onclick="onCancel" style="width: 60px;">取消</a>
    </div>
    </form>
    <script type="text/javascript">
        mini.parse();
        var form = new mini.Form("form1");
        function SaveData() {
            var o = form.getData();
            form.validate();
            if (form.isValid() == false) return;
            var json = mini.encode([o]);
            $.ajax({
                url: "/data/Ajax_EquipmentModel.aspx?method=SaveData",
                data: { data: json },
                cache: false,
                type: "POST",
                success: function (text) {
                    CloseWindow("save");
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
            if (data.action == "edit") {
                //跨页面传递的数据对象，克隆后才可以安全使用
                data = mini.clone(data);
                $.ajax({
                    url: "/data/Ajax_EquipmentModel.aspx?method=GetView&FlagId=" + data.FlagId,
                    cache: false,
                    success: function (text) {
                        var o = mini.decode(text);
                        form.setData(o);
                        form.setChanged(false);

                        initPage();
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



        function onButtonEdit(e) {
            var btnEdit = this;
            mini.open({
                url: "/manage/Order/selParentModel.htm",
                showMaxButton: false,
                title: "上级标记选择树",
                width: 350,
                height: 350,
                ondestroy: function (action) {
                    if (action == "ok") {
                        var iframe = this.getIFrameEl();
                        var data = iframe.contentWindow.GetData();
                        data = mini.clone(data);
                        if (data) {
                            //alert(data.FlagId);
                            btnEdit.setValue(data.ModelId);
                            btnEdit.setText(data.ModelName);

                            if (data.ModelId != 1 && data.ModelId != 263) {
                                $("#trEqpType").css("display", "none");
                            } else {
                                $("#trEqpType").css("display", "");
                            }
                        }
                    }
                }
            });

        }
        
        function initPage() {
            if ($('input[name="ParentModelId"]').val() == "1" || $('input[name="ParentModelId"]').val() == "263") {
                $("#trEqpType").css("display", "");
            }
        }
    </script>
</body>
</html>
