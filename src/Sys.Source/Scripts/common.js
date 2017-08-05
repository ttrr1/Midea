function onOrderStatusRenderer(e) {
    var render = '未处理';
    var value = e.value;
    if (value == 1) {
        render = '预约';
    }
    if (value == 2) {
        render = '现场勘查定位';
    } if (value == 3) {
        render = '设备安装';
    }
    if (value == 4) {
        render = '设备调试';
    }
    return render;
}

function onCreateTimeRenderer(e) {
    var value = e.value;
    if (value) return mini.formatDate(value, 'yyyy-MM-dd HH:mm');
    return "";
}

//图片绘制
function drawCell(grid, columnName, src, w, h) {
    grid.on("drawcell", function (e) {
        var record = e.record,
            column = e.column,
            field = e.field,
            value = e.value, row = e.row;

        if (column.field == columnName) {
            e.cellHtml = "<img src='" + src + "' width='" + w + "' height='" + h + "' />";
        }

    });
}

