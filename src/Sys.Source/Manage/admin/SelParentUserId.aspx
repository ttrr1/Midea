﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelParentUserId.aspx.cs"
    Inherits="UiDesk_admin_SelParentUserId" %>

<%@ Import Namespace="Sys.BLL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <script src="../../scripts/boot.js" type="text/javascript"></script>
    <style type="text/css">
        html, body
        {
            padding: 0;
            margin: 0;
            border: 0;
            width: 100%;
            height: 100%;
            overflow: hidden;
        }
    </style>
</head>
<body>
    <div class="mini-toolbar" style="text-align: center; line-height: 30px;" borderstyle="border-left:0;border-top:0;border-right:0;">
        <label>
            名称：</label>
        <input id="key" class="mini-textbox" style="width: 150px;" onenter="onKeyEnter" />
        <a class="mini-button" style="width: 60px;" onclick="search()">查询</a>
    </div>
    <div class="mini-fit">
        <ul id="tree1" class="mini-tree" style="width: 100%; height: 100%;" textfield="RealName"
            idfield="UserID" parentfield="ParentUserID" resultastree="false" expandonload="true"
            onnodeclick="onNodeDblClick" expandondblclick="false">
        </ul>
    </div>
    <div class="mini-toolbar" style="text-align: center; padding-top: 8px; padding-bottom: 8px;"
        borderstyle="border-left:0;border-bottom:0;border-right:0;">
        <a class="mini-button" style="width: 60px;" onclick="onOk()">确定</a> <span style="display: inline-block;
            width: 25px;"></span><a class="mini-button" style="width: 60px;" onclick="onCancel()">
                取消</a>
    </div>
</body>
</html>
<script type="text/javascript">
    mini.parse();

    var tree = mini.get("tree1");
    tree.load("/data/Ajax_Admin.aspx?method=SearchData");
    function GetData() {
        var node = tree.getSelectedNode();
        return node;
    }
    function search() {
        var key = mini.get("key").getValue();
        if (key == "") {
            tree.clearFilter();
        } else {
            key = key.toLowerCase();
            tree.filter(function (node) {
                var text = node.ParentName ? node.ParentName.toLowerCase() : "";
                if (text.indexOf(key) != -1) {
                    return true;
                }
            });
        }
    }
    function onKeyEnter(e) {
        search();
    }
    function onNodeDblClick(e) {
        onOk();
    }
    //////////////////////////////////
    function CloseWindow(action) {
        if (window.CloseOwnerWindow) return window.CloseOwnerWindow(action);
        else window.close();
    }

    function onOk() {

        var node = tree.getSelectedNode();
        if (node && tree.isLeaf(node) == false) {
           
        }

        if ( node.UserID==<%=SelectUserId %>) {
             alert("编辑的账号的上级编号，不能被选择自身！");return;
        }

      

        CloseWindow("ok");
    }
    function onCancel() {
        CloseWindow("cancel");
    }

    
</script>
