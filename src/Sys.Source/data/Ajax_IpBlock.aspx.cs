using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.BLL;
using Sys.Common;

public partial class data_Ajax_IpBlock : System.Web.UI.Page
{
    protected Sys.BLL.SysIpBlock BllIpBlock = new Sys.BLL.SysIpBlock();

    protected void Page_Load(object sender, EventArgs e)
    {

        var methodName = PageRequest.GetString("method");
        var type = this.GetType();
        var method = type.GetMethod(methodName);
        if (method == null) throw new Exception("method is null");

        method.Invoke(this, null);
    }

    //=============================================================



    /// <summary>
    /// 删除操作
    /// </summary>
    public void Remove()
    {
        var idStr = Request["ContactId"];
        BllIpBlock.Delete(Utils.StrToInt(Request["ID"], 0));

    }



    /// <summary>
    /// 查询具体的信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public void GetView()
    {

        var id = PageRequest.GetInt("ID", 0);
        var dt = new SysIpBlock().GetModel(id);
        //var data = MyDBUtils.DataTable2ArrayList(dt);
        //var user = data.Count > 0 ? (Hashtable)data[0] : null;
        var json = PluSoft.Utils.JSON.Encode(dt);
        Response.Write(json);
    }




    public void SearchData()
    {
        //查询条件
        int BlockType = PageRequest.GetInt("BlockType", 0);//限制方式 0白名单 1黑名单

        //分页

        var pageIndex = PageRequest.GetInt("pageIndex", 1) + 1;
        var pageSize = PageRequest.GetInt("pageSize", 1);
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

        strWhere += " and BlockType=" + BlockType;


        var dt = BllIpBlock.GetTable(pageSize, pageIndex, strWhere, order);
        var dataAll = MyDBUtils.DataTable2ArrayList(dt);
        var result = new Hashtable();
        result["data"] = dataAll;
        var total = new Common().GetCount("SysIpBlock", strWhere);
        result["total"] = total;

        //JSON
        var json = PluSoft.Utils.JSON.Encode(result);
        Response.Write(json);
    }


}