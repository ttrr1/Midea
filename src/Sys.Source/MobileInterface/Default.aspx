<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="MobileInterface_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" method="post" action="OrderStatusHandle.aspx" enctype="multipart/form-data">
    <div>
        <input id="request_id" name="request_id" type="text" value="update_orderstatus" />
        <input id="ordersId" name="ordersId" type="text" value="1" />
        <input id="orderStatus" name="orderStatus" type="text" value="2" />
        <input id="staFlag" name="staFlag" type="text" value="0" />
        <input id="staMessage" name="staMessage" type="text" value="预约完成" />
        <input id="token" name="token" type="text" value="55555555" />
        <input id="File1" name="File1" type="file" />
        <br />
        <input id="Text1" type="text" datatype="s5-16" errormsg="昵称至少5个字符,最多16个字符！" />
        <br />
        <input id="Submit1" type="submit" value="submit" />
    </div>
    </form>
    
</body>
</html>
