<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IpBlockList.aspx.cs" Inherits="Manage_System_IpBlockList" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../../Scripts/boot.js" type="text/javascript"></script>
    <script src="../../Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
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
    <div class="mini-toolbar" style="padding: 2px; border-bottom: 0;">
        <table style="width: 100%;">
            <tr>
                <td style="width: 100%;">
                    <a class="mini-button" iconcls="icon-reload" onclick="onSearch();">刷新</a> <a class="mini-button"
                        iconcls="icon-add" onclick="add()">添加</a> <a class="mini-button" iconcls="icon-edit"
                            onclick="edit();">编辑</a> <a class="mini-button" iconcls="icon-remove" onclick="remove()">
                                删除</a>
                </td>
            </tr>
        </table>
    </div>
    <!--撑满页面-->
    <div class="mini-fit">
        <div id="datagrid1" class="mini-datagrid" style="width: 100%; height: 100%;" url="/data/Ajax_IpBlock.aspx?method=SearchData&BlockType=<%=BlockType %>"
            idfield="ID" sizelist="[5,10,20,50]" pagesize="17">
            <div property="columns">
                <div type="checkcolumn">
                    选择
                </div>
                <div type="indexcolumn" align="center" headeralign="center">
                    序号
                </div>
                <div field="Name" width="80" headeralign="center" align="center" allowsort="true">
                    IP地址</div>
                <div field="CreateTime" dateformat="yyyy-MM-dd hh:mm:ss" width="100" align="center"
                    headeralign="center">
                    添加时间</div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        mini.parse();
        var grid = mini.get("datagrid1");
        grid.load();
        function onSearch() {
            grid.load({ BlockType: <%=BlockType %>});
        }


        //删除操作
         function remove() {
            var rows = grid.getSelecteds();
            if (rows.length > 0) {
                if (confirm("确定删除选中记录？")) {
                    var ids = [];
                    for (var i = 0, l = rows.length; i < l; i++) {
                        var r = rows[i];
                        ids.push(r.ID);
                    }
                      var id = ids.join(',');
                    grid.loading("操作中，请稍后......");
                    $.ajax({
                        url: "/data/Ajax_IpBlock.aspx?method=Remove&ID=" + id,
                        success: function (text) {
                           myAlert("记录删除成功！", "error", 3);
                            grid.reload();
                        },
                        error: function () {
                        }
                    });
                }
            } else {
                myAlert("对不起，请选择一条记录！", "succeed", 3);

            }
        }

         function edit() {
            var row = grid.getSelected();
            if (row) {
                mini.open({
                    url: "/Manage/system/IpBlockItem.aspx?BlockType=<%=BlockType %>&ID="+row.ID,
                    title: "编辑IP选项", width: 400, height: 250,
                    onload: function () {
                        var iframe = this.getIFrameEl();
                        var data = { action: "edit", ID: row.ID };
                        iframe.contentWindow.SetData(data);
                    },
                    ondestroy: function (action) {
                        grid.reload();
                    }
                });

            } else {
            
                myAlert("对不起，请选择一个IP项！", "error", 3);
            }

        }

        


      function add() {
        mini.open({
            url:"/manage/system/IpBlockItem.aspx?BlockType=<%=BlockType %>",
            title: "新增IP选项", width: 400, height: 250,
            onload: function () {
                var iframe = this.getIFrameEl();
                var data = { action: "new" };
                iframe.contentWindow.SetData(data);
            },
            ondestroy: function (action) {
                 grid.reload();
            }
        });
    }



        //倒计时关闭提醒
        //1=error,2=warning ,3=succeed
        function myAlert(content, ico, times) {
            var timer;
            art.dialog({
                content: '<span style=\"color:green;\">' + content + '..</span>',
                icon: ico,
                lock: true,
                opacity:0.01,
                ok: function () {

                    return true;
                },
                init: function () {
                    var that = this, i = times;
                    var fn = function () {
                        that.title(i + '秒后关闭\t\t\t系统友情提示');
                        !i && that.close();
                        i--;
                    };
                    timer = setInterval(fn, 1000);
                    fn();
                },
                close: function () {
                    clearInterval(timer);
                }
            }).show();
        }
      
    </script>
</body>
</html>
