<%@ Page Language="C#" AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="UiDesk_main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>订单管理系统</title>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <meta name="keywords" content="下单_管理系统" />
    <meta name="description" content="下单_管理系统" />
    <link rel="Shortcut Icon" href="/manage/images/favicon.ico" />
    <script src="/Scripts/boot.js" type="text/javascript"></script>
    <script src="../Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
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
        .header
        {
            background: url(header.gif) repeat-x;
        }
        .header div
        {
            font-family: 'Trebuchet MS' ,Arial,sans-serif;
            font-size: 25px;
            line-height: 60px;
            font-weight: bold;
            color: #333;
        }
        body .header .topNav
        {
            position: absolute;
            right: 8px;
            top: 10px;
            font-size: 12px;
            line-height: 25px;
        }
        .header .topNav a
        {
            text-decoration: none;
            color: #222;
            font-weight: normal;
            font-size: 12px;
            line-height: 25px;
            margin-left: 3px;
            margin-right: 3px;
        }
        .header .topNav a:hover
        {
            text-decoration: underline;
            color: Blue;
        }
        .mini-layout-region-south img
        {
            vertical-align: top;
        }
        
        .main
        {
            width: 500px;
            margin: 10px auto;
            position: relative;
            height: 380px;
        }
        .l
        {
            background: url(images/img_01.gif) repeat-x;
            width: 50%;
            float: left;
            padding: 80px 0;
            filter: alpha(opacity:50);
            opacity: 0.5;
        }
        .r
        {
            background: url(images/img_03.gif) repeat-x;
            width: 50%;
            float: right;
            padding: 80px 0;
            filter: alpha(opacity:50);
            opacity: 0.5;
        }
        .btn1
        {
            background: url(images/img_06.gif) no-repeat;
        }
        .btn2
        {
            background: url(images/img_05.gif) no-repeat;
        }
        .btn
        {
            width: 139px;
            height: 41px;
            line-height: 41px;
            margin: 55px 55px 10px;
            border: 0;
            cursor: pointer;
        }
        
        .main_a
        {
            cursor: pointer;
        }
        
        
        .divimg
        {
            width: 219px;
            margin: 0 auto;
        }
        
        .headerimg
        {
            background: url("/manage/images/top.jpg") repeat-x;
        }
        
        #cname
        {
            font-family: "微软雅黑";
            color: #005b84;
            margin-left: 25px;
        }
    </style>
</head>
<body>
    <div class="mini-layout" style="width: 100%; height: 100%;">
        <div title="north" region="north" class="header headerimg" bodystyle="overflow:hidden;"
            height="80" showheader="false" showsplit="false">
            <div id="cname">
                空调安装订单 管理系统</div>
            <div style="position: absolute; width: 210px; background: url(/manage/images/img_005.jpg) top right no-repeat;
                right: 0; top: 0; font-size: 12px; line-height: 25px; font-weight: normal;">
                <p style="text-align: right; padding: 0 70px 25px 0; margin: 0; line-height: 20px;">
                    <a style="color: #333; text-decoration: none; font-size: 12px;" href="javascript:logout();">
                        退出系统</a></p>
                <a style="text-decoration: none;"></a>&nbsp; &nbsp; &nbsp; <a id="changeStyle"><span
                    style="color: #fffd; font-family: 微软雅黑">登录人信息:</span> </a><span style="color: #fffd;
                        font-family: 微软雅黑">&nbsp;&nbsp;
                        <%=Sys.BLL.Admin.GetRealName() %></span>
            </div>
        </div>
        <div showheader="false" region="south" style="border: 0; text-align: center;" height="25"
            showsplit="false">
            Copyright © 版权 2013~2023 空调安装
        </div>
        <div region="west" title="系统菜单" showheader="true" bodystyle="padding-left:1px;" showspliticon="true"
            width="230" minwidth="100" maxwidth="350">
            <!--Tree-->
            <ul id="leftTree" class="mini-tree" url="../../data/Ajax_flag.aspx?method=SearchDatat"
                style="width: 100%; height: 100%;" showtreeicon="true" textfield="FlagName" idfield="ID"
                resultastree="false" parentfield="ParentID" onnodeselect="onNodeSelect">
            </ul>
        </div>
        <div title="center" region="center" style="border: 0;">
            <!--Tabs-->
            <div id="mainTabs" class="mini-tabs" activeindex="0" style="width: 100%; height: 100%;"
                plain="false" onactivechanged="onTabsActiveChanged">
               <div title="欢迎使用" url="/manage/welcome.aspx">
                </div> 
            </div>
        </div>
    </div>
</body>
</html>
<script type="text/javascript">


//    function document.onkeydown() {
//        if (event.keyCode == 116) {
//            alert("禁止F5");
//            event.keyCode = 0;
//            event.cancelBubble = true;
//            return false;
//        }
//    }




    function logout() {
        if (confirm("确定要退出系统?")) {

            window.location.href = "/manage/logout.aspx";

        }

    }


    mini.parse();
    var tree = mini.get("leftTree");

    function onBeforeExpand(e) {
        var tree = e.sender;
        var nowNode = e.node;
        var level = tree.getLevel(nowNode);

        var root = tree.getRootNode();
        tree.cascadeChild(root, function (node) {
            if (tree.isExpandedNode(node)) {
                var level2 = tree.getLevel(node);
                if (node != nowNode && !tree.isAncestor(node, nowNode) && level == level2) {
                    tree.collapseNode(node, true);
                }
            }
        });

    }


    function onTabsActiveChanged(e) {
        var tabs = e.sender;
        var tab = tabs.getActiveTab();
        if (tab && tab._nodeid) {

            var node = tree.getNode(tab._nodeid);
            if (node && !tree.isSelectedNode(node)) {
                tree.selectNode(node);
            }
        }
    }


    function onNodeSelect(e) {

        var node = e.node;
        var isLeaf = e.isLeaf;

        if (isLeaf) {
            showTab(node);
        }
    }


    function url(url) {
        window.location.href = url;
    }

    function showTab(node) {
        var tabs = mini.get("mainTabs");
        var id = node.FlagName;
        var tab = tabs.getTab(id);
        if (!tab) {
            tab = {};
            tab._nodeid = node.FlagName;
            tab.name = node.FlagName;
            tab.title = node.FlagName;
            tab.showCloseButton = true;
            tab.url = node.AppUrl;
            tabs.addTab(tab);
        }
        tabs.activeTab(tab);
    }






    //    function onDrawNode(e) {


    //        var node = e.node;
    //        if (node.AppUrl == "") {
    //            e.nodeHtml = node.FlagName;

    //        } else {
    //            // e.iconCls = "/aaa.ico";
    //            // e.nodeHtml = '<a href="' + node.AppUrl + '" target=\"main\">' + node.FlagName + '</a>';
    //            e.nodeHtml = '<a class=\"main_a\">' + node.FlagName + '</a>';
    //            // e.nodeHtml =""+ node.FlagName ;

    //        }

    //    }
       
</script>
