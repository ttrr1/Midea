using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.BLL;
using Sys.Common;

public partial class data_Ajax_database : System.Web.UI.Page
{

    protected Sys.BLL.AdminLog BllAdminLog = new Sys.BLL.AdminLog();

    protected void Page_Load(object sender, EventArgs e)
    {

        var methodName = PageRequest.GetString("method");
        var type = this.GetType();
        var method = type.GetMethod(methodName);
        if (method == null) throw new Exception("method is null");

        method.Invoke(this, null);
    }

    //=============================================================









    public void SearchData()
    {
        //查询条件
        var key = Utils.SqlStringFormat(PageRequest.GetString("key"), 2);
        var begtime = Utils.SqlStringFormat(PageRequest.GetString("begtime"), 2);

        //分页

        var pageIndex = PageRequest.GetInt("pageIndex", 1) + 1;
        int pageSize = PageRequest.GetInt("pageSize", 1);
        //字段排序
        var sortField = PageRequest.GetString("sortField");
        var sortOrder = PageRequest.GetString("sortOrder");
        var order = "";
        if (String.IsNullOrEmpty(sortField) == false)
        {
            if (sortOrder != "desc") sortOrder = "asc";
            order = " " + sortField + " " + sortOrder;
        }
        else
        {
            order += " createtime desc";
        }
        //业务层：数据库操作

        var strWhere = "1=1";

        if (key.Length > 0)
        {

            strWhere += " and charIndex('" + key + "',userName)>0";
        }


        if (begtime.Length > 0)
        {


            strWhere += " and DATEDIFF(day,'" + DateTime.Parse(begtime) + "',createtime)=0";

        }


        if (PageRequest.GetString("type") == "db")
        {
            strWhere += " and charIndex('system_databasebackup',Flag)>0";
        }
        if (PageRequest.GetString("type") == "del")
        {
            strWhere += " and charIndex('system_databasedellog',Flag)>0";
        }


        var dt = BllAdminLog.GetTable(pageSize, pageIndex, strWhere, order);
        var dataAll = MyDBUtils.DataTable2ArrayList(dt);
        var result = new Hashtable();
        result["data"] = dataAll;
        var total = new Common().GetCount("AdminLog", strWhere);
        result["total"] = total;

        //JSON
        var json = PluSoft.Utils.JSON.Encode(result);
        Response.Write(json);
    }




}