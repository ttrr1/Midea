<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminitem.aspx.cs" Inherits="UiDesk_admin_adminitem" %>

<%@ Import Namespace="Sys.BLL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>模块标记面板</title>
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
    <input name="UserID" id="UserID" class="mini-hidden" />
    <fieldset style="border: solid 1px #aaa; padding: 3px;">
        <legend>用户基本信息</legend>
        <div style="padding: 5px;">
            <table>
                <tr>
                    <td style="width: 120px;">
                        <img src="../images/help.gif" />&nbsp; 模块类别：
                    </td>
                    <td style="width: 280px;">
                        <input id="FlagType" class="mini-combobox" style="width: 180px;" textfield="text"
                            valuefield="id" url="/data_txt/flagType.txt" value="1" required="true" allowinput="true"
                            shownullitem="true" nullitemtext="请选择..." />
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px;">
                        <img src="../images/help.gif" />
                        &nbsp;用户名：
                    </td>
                    <td style="width: 180px;">
                        <input name="Username" class="mini-textbox" width="180" required="true" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px;">
                        <img src="../images/help.gif" />
                        &nbsp;用户密码：
                    </td>
                    <td style="width: 180px;">
                        <input name="Password" class="mini-password" width="180" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px;">
                        <img src="../images/help.gif" />
                        &nbsp;真实姓名：
                    </td>
                    <td style="width: 180px;">
                        <input name="RealName" class="mini-textbox" width="180" required="true" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px;">
                        <img src="../images/help.gif" />
                        &nbsp;办公电话：
                    </td>
                    <td style="width: 180px;">
                        <input name="OfficeTel" class="mini-textbox" width="180" required="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <img src="../images/help.gif" />&nbsp; 帐户状态：
                    </td>
                    <td>
                        <div id="State" name="State" class="mini-radiobuttonlist" repeatitems="2" repeatlayout="table"
                            repeatdirection="horizontal" textfield="text" valuefield="id" value="1" url="/data_txt/userstate.txt">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">
                        <img src="../images/help.gif" />&nbsp; 是否公开：
                    </td>
                    <td style="width: 180px;">
                        <input name="IsPublic" class="mini-checkbox" text="是否公开？" value="1" truevalue="1"
                            falsevalue="0" />
                    </td>
                </tr>
                <tr>
                    <td style="color: red;">
                        <img src="../images/help.gif" />
                        &nbsp;上级账号：
                    </td>
                    <td>
                        <input id="ParentUserID" allowinput="false" name="ParentUserID" class="mini-buttonedit searchbox"
                            allowinput="false" emptytext="上一级账号..." textname="ParentName" onbuttonclick="onButtonEdit"
                            width="180" />
                    </td>
                </tr>
                <%
                     
                    
                %>
                <tr>
                    <td style="color: red;">
                        <img src="../images/help.gif" />
                        &nbsp;分配角色：
                    </td>
                    <td>
                        <div id="RoleIDs" name="RoleIDs" class="mini-checkboxlist" repeatitems="3" repeatlayout="table"
                            textfield="RoleName" valuefield="RoleID" url="../../data/Ajax_role.aspx?method=SearchSelRole">
                        </div>
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
        function disable() {
            var obj = mini.get("RoleIDs");
            obj.disable();
        }
        <%if(!Flag){%>disable(); <%}%>
        var form = new mini.Form("form1");

        //保存数据
        function SaveData() {
        
            var o = form.getData();
            form.validate();
            if (o.UserId=="") {                
                if (o.Password=="") {                        
                      mini.alert("密码不能为空！");
                }    
            }            
            if (form.isValid() == false) return;    

            if (mini.get("ParentUserID").getValue()==""&&mini.get("ParentUserID").getValue()!="0") {
                  mini.alert("请先设置账号层次！");
                  return;
            }
           
            var json = mini.encode([o]);

         //   alert(json);return;
            var messageid = mini.loading("数据更新中, Please wait ...", "Loading");
            $.ajax({
                url: "../../data/Ajax_Admin.aspx?method=SaveData",
                data: { data: json },
                cache: false,
                success: function (text) { 
                        if (text == "yes") {
                            var timer;
                            var i = 2;
                            var fn = function () {
                                i--;
                                if (i == 0) {
                                    mini.hideMessageBox(messageid);
                                    CloseWindow("save");
                                }
                            }
                            timer = setInterval(fn, 1000);
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
            if (data.action == "edit") {
                //跨页面传递的数据对象，克隆后才可以安全使用

              
                data = mini.clone(data);
                $.ajax({
                    url: "../../data/Ajax_Admin.aspx?method=GetView&UserId=" + data.UserId,
                    cache: false,
                    success: function (text) {

                 
                        var o = mini.decode(text);
                        form.setData(o);
                        form.setChanged(false);

                        //onDeptChanged();
                        // mini.getbyName("position").setValue(o.position);
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
           if (mini.get("UserID").getValue()==<%=Account.GetLoginUserID() %>) {
            mini.alert("不能修改自己的账号层次!"); return;            
            }
            var btnEdit = this;
            mini.open({
                url:"/manage/admin/SelParentUserId.aspx?SelectUserId="+mini.get("UserID").getValue(),
                showMaxButton: false,
                title: "父级账号选择树",
                width: 350,
                height: 350,
                ondestroy: function (action) {
                    if (action == "ok") {
                        var iframe = this.getIFrameEl();
                        var data = iframe.contentWindow.GetData();
                        data = mini.clone(data);
                        if (data) {                        
                            btnEdit.setValue(data.UserID);
                            btnEdit.setText(data.RealName);
                        }
                    }
                }
            });
            
        } 
    </script>
</body>
</html>
