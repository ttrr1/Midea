<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderDetail.aspx.cs" Inherits="MobilePages_OrderDetail" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="Sys.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0">
    <title>订单详情</title>
    <link rel="stylesheet" href="css/member.css">
</head>
<body>
    <div class="list3">
        <div class="xingming">
            <h3>
                <%=orderModel.ProjectName %></h3>
            <p>
                <span><em>
                    <img src="images/shijian.png" width="15" height="15" alt="" /></em>订单时间：<%=orderModel.CreateDate.Value.ToString("yyyy-MM-dd") %></span></p>
        </div>
    </div>
    <div class="list1">
        <p>
            <span>用户名称：</span><%=orderModel.CustomName %></p>
        <p>
            <span>用户地址：</span><%=orderModel.ProvinceName %><%=orderModel.CityName %><%=orderModel.AreaName %></p>
        <p>
            <span>详细地址：</span><%=orderModel.Address %></p>
        <p>
            <span>联 系 人：</span><%=orderModel.Contact %></p>
        <p>
            <span>联系电话：</span><%=orderModel.Tel %></p>
        <%
            var strSubInfo = string.Empty;
            foreach (OrderType item in orderTypeList)
            {
                if (!string.IsNullOrEmpty(item.MainModelName))
                {
                    strSubInfo += string.Format("<p><span>主机型号：</span>{0}</p>", item.MainModelName);
                    strSubInfo += string.Format("<p><span>产品数量：</span>{0}</p>", item.MainModelNum);
                }
                if (!string.IsNullOrEmpty(item.SubModelName1))
                {
                    strSubInfo += string.Format("<p><span>内机型号：</span>{0}</p>", item.SubModelName1);
                    strSubInfo += string.Format("<p><span>产品数量：</span>{0}</p>", item.SubModelNum1);
                }
                if (!string.IsNullOrEmpty(item.SubModelName2))
                {
                    strSubInfo += string.Format("<p><span>内机型号：</span>{0}</p>", item.SubModelName2);
                    strSubInfo += string.Format("<p><span>产品数量：</span>{0}</p>", item.SubModelNum2);
                }
                if (!string.IsNullOrEmpty(item.SubModelName3))
                {
                    strSubInfo += string.Format("<p><span>内机型号：</span>{0}</p>", item.SubModelName3);
                    strSubInfo += string.Format("<p><span>产品数量：</span>{0}</p>", item.SubModelNum3);
                }
                if (!string.IsNullOrEmpty(item.SubModelName4))
                {
                    strSubInfo += string.Format("<p><span>内机型号：</span>{0}</p>", item.SubModelName4);
                    strSubInfo += string.Format("<p><span>产品数量：</span>{0}</p>", item.SubModelNum4);
                }
                if (!string.IsNullOrEmpty(item.ModelName))
                {
                    strSubInfo += string.Format("<p><span>产品型号：</span>{0}</p>", item.ModelName);
                    strSubInfo += string.Format("<p><span>产品数量：</span>{0}</p>", item.ModelNum);
                }

            } %>
        <%=strSubInfo %>
        <p>
            <span>备 注：</span>
            <%=orderModel.Remark %></p>
    </div>
    <div class="content">
        <article>
	  <h5><span>下午</span></h5>
      <%
          //效果图处理
          var strPicList = string.Empty;
          var commentHtml = string.Empty;
          if (dtFlow != null && dtFlow.Rows.Count > 0)
          {
              DataRow drFirst = dtFlow.Rows[0];
              if (drFirst["OrderStatus"].ToString().Equals("4") && drFirst["StatusFlag"].ToString().Equals("1"))
              {
                  //图片处理
                  var pic = orderModel.PicList;
                  var imgList = string.Empty;
                  if (!string.IsNullOrEmpty(pic))
                  {
                      string[] arr = pic.Split(',');
                      for (int j = 0; j < arr.Length; j++)
                      {
                          imgList += string.Format("<img src=\"{0}\" width=\"325\" height=\"260\" alt=\"\"/>", arr[j]);
                      }
                  }

                  strPicList = string.Format("<p class=\"brief\">图片展示：<span>{0}</span></p>", imgList);
              }
          }


          var i = 0;
          foreach (DataRow row in dtFlow.Rows)
          {
              i += 1;
              var pointColor = string.Empty;
              var processImg = string.Empty;
              if (i == 1)
              {
                  pointColor = "point-blue";
                  processImg = GetCommentStar();
              }
              else
              {
                  pointColor = "point-white";
                  strPicList = string.Empty;
              }
              

      %>
             <section>
			<span class="point-time <%=pointColor %>"></span>
			
			<aside>
				<p class="things">处理时间：<span><%=Convert.ToDateTime(row["CreateDate"]).ToString("yyyy-MM-dd")%></span><b><%=processImg%></b></p>
                <p class="things">更新状态：<span><%=GetOrderStatus(row["OrderStatus"].ToString())%></span></p>
                <p class="things">备注信息：<span><%=row["StatusMessage"]%></span></p>
                <%=strPicList %>
				
			</aside>
		</section>
             
             <%
          }
           %>

	</article>
    </div>
</body>
</html>
