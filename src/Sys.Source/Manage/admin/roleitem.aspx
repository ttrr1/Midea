<%@ Page Language="C#" AutoEventWireup="true" CodeFile="roleitem.aspx.cs" Inherits="UiDesk_role_roleitem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>角色编辑</title>
    <script src="../../Scripts/boot.js" type="text/javascript"></script>
    <style type="text/css">
        input
        {
            vertical-align: middle;
        }
        .inputcheckbox
        {
            vertical-align: middle;
        }
        body
        {
            font-family: tahoma;
            font-size: 12px;
            color: #666;
        }
        *
        {
            margin: 0;
            padding: 0;
            list-style-type: none;
        }
        .content
        {
            width: 700px;
            height: 500px;
            margin: 0 auto;
            padding-top: 5px;
        }
        .top
        {
            margin: 0px 0px 10px 0px;
            border: 1px solid #ccc;
            width: 600px;
        }
        .top h3
        {
            background: #b9dbfd;
            padding: 3px 0 3px 10px;
            color: #333;
            border-bottom: 1px solid #ccc;
            font-size: 14px;
        }
        .top ul
        {
            padding: 3px 0;
        }
        .top li
        {
            background: url(images/img_03_03.png) 15px center no-repeat;
            padding: 5px 0 5px 30px;
        }
        .top li input
        {
            border: 1px solid #ccc;
        }
        .top li span
        {
            display: block;
            width: 70px;
            float: left;
        }
        .ctn
        {
            float: left;
            width: 163px;
            height: 185px;
            overflow-y: scroll;
            border: 1px solid #ccc;
            margin: 5px 3px 10px 3px;
        }
        .ctn li
        {
            padding: 5px 22px 5px 20px;
            line-height: 16px;
        }
        .ctn li input
        {
            margin-right: 5px;
        }
        .ctn li.bgc
        {
            background-color: #b9dbfd;
            color: #333;
            padding: 5px 20px 5px 5px;
        }
        .btn
        {
            text-align: center;
            clear: both;
        }
        .btn input
        {
            background: url(images/img_03.jpg) repeat-x;
            border: 1px solid #ccc;
            padding: 0 5px;
        }
    </style>
    <script type="text/javascript">

        function checkall(obj, id) {

            if (obj.checked) {
                $("#u" + id + " [name='RoleFlag']").attr("checked", 'true'); //全选;
            } else {
                $("#u" + id + " [name='RoleFlag']").removeAttr("checked"); //全选;
            }
        }

        function selcheck(obj, pid) {
            if (obj.checked) {
                $("#u" + pid + " [name='RoleFlag']").first().attr("checked", 'true');
            } else {
                var count = 0;
                $("#u" + pid + " [name='RoleFlag']").each(function (i) {
                    if (i > 0) {
                        if ($(this).attr("checked") == "checked") {
                            count += 1;
                        }
                    }
                });

                if (count == 0) {
                    $("#u" + pid + " [name='RoleFlag']").removeAttr("checked");
                }
            }
        }


        function onOk(e) {
            var chk_value = "";
            $('input[name="RoleFlag"]:checked').each(function () {
                chk_value += ($(this).val()) + ",";
            });
            if ($("#RoleName").val() == "") {
                alert("角色名不能为空");
                return;
            }
            if ($("#Note").val() == "") {
                alert("角色说明不能为空");
                return;
            }
            var act = "addsave";
            if ($("#RoleId").val() > 0) {
                act = "editsave";
            }
            sub(act, $("#RoleId").val(), chk_value, $("#RoleName").val(), $("#Note").val());
        }


        //确定按钮
        function sub(act, RoleId, RoleFlag, RoleName, Note) {
            var messageid = mini.loading("角色权限,数据修改中, Please wait ...", "Loading");
            $.post("/manage/admin/roleitem.aspx", { "act": act, "RoleId": RoleId, "RoleFlag": RoleFlag, "RoleName": RoleName, "Note": Note }, function (date, state) {
                if (state == "success") {
                    if (date == "yes") {
                        var timer;
                        var i = 2;
                        var fn = function () {
                            i--;
                            if (i == 0) {
                                mini.hideMessageBox(messageid);
                                CloseWindow("cancel");
                            }
                        }
                        timer = setInterval(fn, 1000);
                    } else if (date == "nopower") {
                        mini.hideMessageBox(messageid);
                        mini.alert("对不起，你没有操作此功能的权限！");

                    }


                }
            }, "html");


        }




        function onCancel(e) {
            CloseWindow("cancel");
        }

        function CloseWindow(action) {
            if (action == "close" && form.isChanged()) {
                if (confirm("数据被修改了，是否先保存？")) {
                    return false;
                }
            }
            if (window.CloseOwnerWindow) return window.CloseOwnerWindow(action);
            else window.close();
        }

    </script>
</head>
<body>
    <div class="content">
        <div class="top">
            <h3>
                编辑角色</h3>
            <ul>
                <li><span>角色名称：</span><input type="text" id="RoleName" name="RoleName" value="<%=ModelRole.RoleName %>" />
                    <input type="hidden" id="RoleId" name="RoleId" value="<%=RoleId %>" />
                </li>
                <li><span>备注：</span><input type="text" id="Note" name="Note" value="<%=ModelRole.Note %>" /></li>
                <li><span>排序：</span><input type="text" /></li>
            </ul>
        </div>
        <%=GetRoleFlagList(ModelRole.RoleFlag)%>
    </div>
<br/><br/><br/><br/><br/>
    <div style="text-align: center; magrin-top:40px; padding: 10px; clear: left;">
        <a class="mini-button" onclick="onOk" style="width: 60px; margin-right: 20px;">确定</a>
        <a class="mini-button" onclick="onCancel" style="width: 60px;">取消</a>
    </div>
</body>
</html>
