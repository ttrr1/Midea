<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Table.aspx.cs" Inherits="Manage_Database_Table" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script src="../../Scripts/boot.js" type="text/javascript"></script>
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
    <style type="text/css">
        .New_Button, .Edit_Button, .Delete_Button, .Update_Button, .Cancel_Button
        {
            font-size: 11px;
            color: #1B3F91;
            font-family: Verdana;
            margin-right: 5px;
        }
    </style>
</head>
<body>
    <h1>
        数据库表结构</h1>
    <div class="mini-fit">
        <div id="table_grid" class="mini-datagrid" showpager="false" style="width: 100%;
            height: 100%;" allowresize="true" url="/data/Ajax_Table.aspx?method=SearchData"
            idfield="id" onshowrowdetail="onShowRowDetail">
            <div property="columns">
                <div type="expandcolumn">
                </div>
                <div field="name" width="80" headeralign="center">
                    数据表名</div>
                <div field="value" width="320" headeralign="left">
                    描述</div>
            </div>
        </div>
        <div id="detailGrid_Form" style="display: none;">
            <div id="column_grid" class="mini-datagrid" showpager="false" style="width: auto;
                height: auto;" url="/data/Ajax_Table.aspx?method=GetView">
                <div property="columns">
                    <div field="字段名" width="120" align="center" headeralign="center" allowsort="true">
                        字段名
                    </div>
                    <div field="标识" width="50" allowsort="true" align="center" headeralign="center">
                        标识
                    </div>
                    <div field="主键" width="50" allowsort="true">
                        主键
                    </div>
                    <div field="类型" width="50" allowsort="true">
                        类型
                    </div>
                    <div field="字节数" width="30" allowsort="true">
                        字节数
                    </div>
                    <div field="长度" width="30" allowsort="true">
                        长度
                    </div>
                    <div field="小数位数" width="50" allowsort="true">
                        小数位数
                    </div>
                    <div field="允许空" width="50" allowsort="true">
                        允许空
                    </div>
                    <div field="默认值" width="70" allowsort="true">
                        默认值
                    </div>
                    <div field="字段说明" width="100" allowsort="true">
                        字段说明
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        mini.parse();
        var table_grid = mini.get("table_grid");
        var column_grid = mini.get("column_grid");
        var detailGrid_Form = document.getElementById("detailGrid_Form");

        table_grid.load();

        ///////////////////////////////////////////////////////       

        function onGenderRenderer(e) {
            for (var i = 0, l = Genders.length; i < l; i++) {
                var g = Genders[i];
                if (g.id == e.value) return g.text;
            }
            return "";
        }

        function onShowRowDetail(e) {
            var grid = e.sender;
            var row = e.record;

            var td = grid.getRowDetailCellEl(row);
            td.appendChild(detailGrid_Form);
            detailGrid_Form.style.display = "block";
            column_grid.load({ tableName: row.name });
        }
    
        
    </script>
</body>
</html>
