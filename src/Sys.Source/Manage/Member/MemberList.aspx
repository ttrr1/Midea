<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberList.aspx.cs" Inherits="Manage_Member_MemberList" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员管理</title>
    <script src="../../Scripts/boot.js" type="text/javascript"></script>
    <script src="../../Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
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
    <input name="hiduserType" id="hiduserType" class="mini-hidden" value="<%=userType %>" />
    <div style="width: 100%;">
        <div class="mini-toolbar" style="border-bottom: 0; padding: 0px;">
            <table style="width: 100%;">
                <tr>
                    <td style="width: 100%;">
                        <a class="mini-button" name="add" iconcls="icon-add" onclick="edit(0)">增加</a>
                    </td>
                    <td style="white-space: nowrap;">
                        <input id="userType" name="userType" class="mini-combobox" style="width: 100px;" textfield="text"
                            valuefield="id" url="/data_txt/userType.txt" value="" allowinput="true"
                            shownullitem="true" nullitemtext="请选择..." />
                        <input id="key" class="mini-textbox" emptytext="请输入用户名" style="width: 150px;" />
                        <a class="mini-button" onclick="search()">查询</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <!--撑满页面-->
    <div class="mini-fit">
        <div id="datagrid1" class="mini-datagrid" style="width: 100%; height: 100%;" allowresize="true"
            allowalternating="true" pagesize="10" url="/data/Ajax_UserInfo.aspx?method=SearchData"
            idfield="ID"  >
            <div property="columns">
                <div field="UserName" width="60" headeralign="center" allowsort="true">
                    用户名
                </div>
                <div field="RealName" width="50" headeralign="center" align="center" allowsort="true">
                    姓名
                </div>
                <div field="CompanyName" width="80" headeralign="center" align="center"
                    allowsort="true">
                    公司名称
                </div>
                <div field="ProvinceName" width="50" headeralign="center" align="center" allowsort="true">
                    省
                </div>
                <div field="CityName" width="50" headeralign="center" align="center" allowsort="true">
                    市
                </div>
                <div field="AreaName" width="50" headeralign="center" align="center" allowsort="true">
                    区
                </div>
                <div field="Address" width="100" headeralign="center" align="center" allowsort="true">
                    详细地址
                </div>
                
                <div field="TypeValue" width="60" headeralign="center" align="center" allowsort="true">
                    所属类别
                </div>
                <div field="RoleId" width="60" headeralign="center" align="center" allowsort="true" renderer="onRoleIdRenderer">
                    用户类型
                </div>
                <div field="CreateTime" width="100" headeralign="center" align="center" allowsort="true"
                    renderer="onCreateTimeRenderer">
                    注册时间
                </div>
                <div field="State" width="40" headeralign="center" align="center" allowsort="true" renderer="onStateRenderer">
                    状态
                </div>
                <div name="action" width="160" headeralign="center" align="center" renderer="onActionRenderer"
                    cellstyle="padding:0;">
                    操作</div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        mini.parse();
        var grid = mini.get("datagrid1");
        //grid.load();
        var userType = mini.get("hiduserType").getValue();
        grid.load({ userType: userType });
        mini.get("userType").setValue(userType);

        function edit(id) {
            mini.open({
                url: "/manage/Member/MemberItem.aspx?r=" + Math.random(),
                title: "会员编辑", width: 750, height: 420,
                onload: function () {
                    var iframe = this.getIFrameEl();
                    var data = { flagId: id };
                    iframe.contentWindow.SetData(data);
                },
                ondestroy: function (action) {
                    grid.reload();
                }
            });
        }

        function onActionRenderer(e) {
            var grid = e.sender;
            var record = e.record;
            var flagId = record.ID;
            var rowIndex = e.rowIndex;
            var s = '<a style=\"text-decoration: none; color: #006699;\" href="javascript:edit(\'' + flagId + '\')">编辑</a>&nbsp;|&nbsp;<a style=\"text-decoration: none; color: #006699;\" href="javascript:stateUpdate(\'' + flagId + '\')">用户审核</a>';
            s += '&nbsp;|&nbsp;<a style=\"text-decoration: none; color: #006699;\" href="javascript:passwordUpdate(\'' + flagId + '\')">密码修改</a>';
            return s;
        }

        //状态更新
        function stateUpdate(flagId) {
            mini.open({
                url: "/manage/Member/MemberStatusEdit.aspx?r=" + Math.random(),
                title: "状态更新", width: 550, height: 320,
                onload: function () {
                    var iframe = this.getIFrameEl();
                    var data = { id: flagId };
                    iframe.contentWindow.SetData(data);
                },
                ondestroy: function (action) {
                    grid.reload();
                }
            });
        }

        //修改密码
        function passwordUpdate(flagId) {
            mini.open({
                url: "/manage/Member/PasswordUpdate.aspx",
                title: "修改密码", width: 550, height: 320,
                onload: function () {
                    var iframe = this.getIFrameEl();
                    var data = { id: flagId };
                    iframe.contentWindow.SetData(data);
                },
                ondestroy: function (action) {
                    grid.reload();
                }
            });
        }

        /////////////////////////////////////////////////
        function onCreateTimeRenderer(e) {
            var value = e.value;
            if (value) return mini.formatDate(value, 'yyyy-MM-dd   HH:mm');
            return "";
        }

        function onRoleIdRenderer(e) {
            var value = e.value;
            if (value=="1") return "经销商";
            else if (value == "2") return "安装工人";
        }

        function onStateRenderer(e) {
            var value = e.value;
            if (value == "0") return "锁定";
            else if (value == "1") return "正常";
        }

        function search() {
            var userType = mini.get("userType").getValue();
            var key = mini.get("key").getValue();
            grid.load({ userType: userType, key: key });
        }
        function onKeyEnter(e) {
            search();
        }

    </script>
</body>
</html>
