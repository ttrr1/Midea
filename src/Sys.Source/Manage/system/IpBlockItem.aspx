<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IpBlockItem.aspx.cs" Inherits="Manage_System_IpBlockItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script src="../../Scripts/boot.js" type="text/javascript"></script>
    <script src="../../Scripts/artDialog/jquery.artDialog.js?skin=blue" type="text/javascript"></script>
    <style type="text/css">
        .tips
        {
            background: url(/manage/images/tips.gif) no-repeat;
            background-position: 4px 8px;
            line-height: 26px;
            margin: 2px 2px;
            padding-right: 2px;
            padding-left: 18px;
            padding-bottom: 2px;
            padding-top: 2px;
            text-align: left;
            border: 1px dotted #999999;
            background-color: #FFFFCC;</style>
</head>
<body>
    <div class="mycolumn" style="border: 0px">
        <form id="form1" name="form1" method="post">
        <input type="hidden" name="ID" id="ID" value="<%=ID%>" />
        <input type="hidden" id="BlockModule" name="BlockModule" value="<%=BlockModule%>" />
        <input type="hidden" id="BlockType" name="BlockType" value="<%=BlockType%>" />
      
        <table cellpadding="2" width="98%" align="center">
            <tr>
                <td colspan="2">
                    <div class="tips">
                        提示：<br />
                        xxx.xxx.xxx.xxx = 精确匹配<br />
                        xxx.xxx.xxx.xxx-xxx.xxx.xxx.xxx = 范围<br />
                        xxx.xxx.xxx.* = 任何匹配</div>
                </td>
            </tr>
            <tr>
                <td align="left" width="100">
                    <img src="/manage/images/help.gif" alt="IP访问规则" onclick="helpInf(this)" />
                    IP访问规则：
                </td>
                <td align="left">
                    <input name="Name" id="Name" class="mini-textbox" width="180" required="true" />
                </td>
            </tr>
        </table>
        <div style="text-align: center; padding: 10px;">
            <a class="mini-button" onclick="onOk" style="width: 60px; margin-right: 20px;">确定</a>
            <a class="mini-button" onclick="onCancel" style="width: 60px;">取消</a>
        </div>
        </form>
        <script type="text/javascript">
            mini.parse();
            var form = new mini.Form("form1");
            function SaveData() {
                var o = form.getData();
                form.validate();
                if (form.isValid() == false) return;

                var messageid = mini.loading("请稍等, 系统配置更新中 ...", "Loading");

                //保存数据
                $.post("/manage/system/IpBlockItem.aspx?act=SaveData", { "Name": mini.get("Name").getValue(), "ID": $("#ID").val(), "BlockType": $("#BlockType").val() }, function (data, state) {
                    if (state == "success") {
                        mini.hideMessageBox(messageid);
                        if (data == "yes") {

                            mini.alert("系统配置成功！", "提示", function () {
                                CloseWindow("save");

                            });
                        }
                        else {

                            mini.alert(data, "提示", function () {

                            });

                        }
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
                        url: "/data/Ajax_IpBlock.aspx?method=GetView&ID=" + data.ID,
                        cache: false,
                        success: function (text) {
                            var o = mini.decode(text);
                            form.setData(o);
                            form.setChanged(false);

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



           

          
        </script>
    </div>
</body>
</html>
