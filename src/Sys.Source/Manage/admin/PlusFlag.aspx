<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlusFlag.aspx.cs" Inherits="UiDesk_admin_PlusFlag" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                $("#u" + id + " [name='RoleFlag']:enabled").attr("checked", 'true'); //全选;
            } else {
                $("#u" + id + " [name='RoleFlag']:enabled").removeAttr("checked"); //全选;
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
            $('input[name="RoleFlag"]:enabled:checked').each(function () {
                chk_value += ($(this).val()) + ",";
            });
            if ($("#UserId").val() > 0) {
                var messageid = mini.loading("附加权限,数据修改中, Please wait ...", "Loading");
                $.post("/manage/admin/PlusFlag.aspx", { "act": "editsave", "UserId": $("#UserId").val(), "RoleFlag": chk_value }, function (date, state) {
                    if (state == "success") {
                        if (date != "") {
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
                        }
                    }
                }, "html");
            }

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
        <%=Html%>
        <input type="hidden" id="UserId" name="UserId" value="<%=UserId %>" />
    </div>
    <div style="text-align: center; padding: 10px; clear: left;">
        <a class="mini-button" onclick="onOk" style="width: 60px; margin-right: 20px;">确定</a>
        <a class="mini-button" onclick="onCancel" style="width: 60px;">取消</a>
    </div>
</body>
</html>
