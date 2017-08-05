<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberItem.aspx.cs" Inherits="Manage_Member_MemberItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员编辑</title>
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
    <input name="ID" id="ID" class="mini-hidden" />
    <fieldset style="border: solid 1px #aaa; padding: 3px;">
        <legend>基本信息</legend>
        <div style="padding: 5px;">
            <table>
                <tr>
                    <td style="color: red;" style="width: 120px;">
                        <img src="../images/help.gif" />&nbsp; 手机号码：
                    </td>
                    <td style="width: 180px;">
                        <input name="UserName" id="UserName" class="mini-textbox" width="180" required="true" />
                    </td>
                </tr>
                <tr id="trPassword">
                    <td style="color: red;" style="width: 120px;">
                        <img src="../images/help.gif" />&nbsp; 密码：
                    </td>
                    <td style="width: 180px;">
                        <input  type="password" id="Password" class="mini-password" width="180" name="Password" maxlength="16" required="true" />
                    </td>
                </tr>
                <tr>
                    <td style="color: red;" style="width: 120px;">
                        <img src="../images/help.gif" />&nbsp; 姓名：
                    </td>
                    <td style="width: 180px;">
                        <input name="RealName" id="RealName" class="mini-textbox" width="180" required="true" />
                    </td>
                </tr>
                <tr>
                    <td style="color: red;" style="width: 120px;">
                        <img src="../images/help.gif" />&nbsp; 公司名称：
                    </td>
                    <td style="width: 180px;">
                        <input name="CompanyName" id="CompanyName" class="mini-textbox" width="180" required="true" />
                    </td>
                </tr>
                <tr>
                    <td style="color: red;" style="width: 120px;">
                        <img src="../images/help.gif" />&nbsp; 公司地址：
                    </td>
                    <td style="width: 360px;">
                        <input id="ProvinceId" name="ProvinceId" class="mini-combobox" style="width: 100px;"
                            textfield="Value" required="true" valuefield="Key" url="/data/Ajax_UserInfo.aspx?method=GetAreaInfo&dataType=p"
                            value="" allowinput="false" shownullitem="true" nullitemtext="请选择..." />
                        <input id="CityId" name="CityId" class="mini-combobox" style="width: 100px;" textfield="Value"
                            required="true" valuefield="Key" url="/data/Ajax_UserInfo.aspx?method=GetAreaInfo&dataType=c"
                            value="" allowinput="false" shownullitem="true" nullitemtext="请选择..." />
                        <input id="AreaId" name="AreaId" class="mini-combobox" style="width: 100px;" textfield="Value"
                            required="true" valuefield="Key" url="/data/Ajax_UserInfo.aspx?method=GetAreaInfo&dataType=a"
                            value="" allowinput="false" shownullitem="true" nullitemtext="请选择..." />
                    </td>
                </tr>
                <tr>
                    <td style="color: red;">
                        <img src="../images/help.gif" />&nbsp;详细地址：
                    </td>
                    <td>
                        <input name="Address" id="Address" class="mini-textbox" width="180" required="true" />
                    </td>
                </tr>
                <tr>
                    <td style="color: red;">
                        <img src="../images/help.gif" />&nbsp;用户类型：
                    </td>
                    <td>
                        <input id="RoleId" name="RoleId" class="mini-combobox" style="width: 180px;" textfield="text"
                            required="true" valuefield="id" url="/data_txt/userType.txt" onvaluechanged="onRoleIdChanged" 
                            value="" allowinput="false" shownullitem="true" nullitemtext="请选择..." />
                    </td>
                </tr>
                <tr>
                    <td style="color: red;">
                        <img src="../images/help.gif" />&nbsp;所属类别：
                    </td>
                    <td>
                        <input id="TypeKey" name="TypeKey" class="mini-combobox" style="width: 180px;" textfield="Value"
                            required="true" valuefield="Key"
                            value="" allowinput="false" shownullitem="true" nullitemtext="请选择..." />
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
            var ProvinceId = mini.get("ProvinceId");
            var CityId = mini.get("CityId");
            var AreaId = mini.get("AreaId");
            var dataStr = ProvinceId.getValue() + ":" + ProvinceId.getText() + "|";
            dataStr += CityId.getValue() + ":" + CityId.getText() + "|";
            dataStr += AreaId.getValue() + ":" + AreaId.getText();

            var TypeKey = mini.get("TypeKey");
            var roleValue = TypeKey.getValue() + ":" + TypeKey.getText();

            var o = form.getData();
            form.validate();
            if (form.isValid() == false) return;
            var json = mini.encode([o]);
            $.ajax({
                url: "/data/Ajax_UserInfo.aspx?method=SaveData",
                data: { data: json, areaData: dataStr, roleValue: roleValue },
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
            if (data.flagId > 0) {
                $("#trPassword").remove();
                //跨页面传递的数据对象，克隆后才可以安全使用
                data = mini.clone(data);
                $.ajax({
                    url: "/data/Ajax_UserInfo.aspx?method=GetView&id=" + data.flagId,
                    cache: false,
                    success: function (text) {
                        
                        var o = mini.decode(text);
                        form.setData(o);
                        form.setChanged(false);

                        onRoleIdChanged(mini.get("TypeKey").getValue());
                        
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


        function onRoleIdChanged(e) {
            var RoleId = mini.get("RoleId");
            var TypeKey = mini.get("TypeKey");

            var id = RoleId.getValue();
            TypeKey.setValue("");
            var url = "/data/Ajax_UserInfo.aspx?method=GetUserTypeInfo&RoleId=" + id;
            TypeKey.setUrl(url);
            if (typeof e == 'string') {
                TypeKey.setValue(e);
            }
            
        }

    </script>
</body>
</html>
