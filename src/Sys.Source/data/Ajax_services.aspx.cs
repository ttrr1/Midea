using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.BLL;
using Sys.Common;

public partial class data_Ajax_services : System.Web.UI.Page
{
    protected Services BllServices = new Services();

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
    /// 更新信息
    /// </summary>
    public void SaveData()
    {
        var json = Request["data"];
        var rows = (ArrayList)PluSoft.Utils.JSON.Decode(json);
        var bllAdminFlag = new AdminFlag();
        Sys.Model.Services model;
        foreach (Hashtable row in rows)
        {
            var id = row["ID"] != null ? row["ID"].ToString() : "";
            //根据记录状态，进行不同的增加、删除、修改操作
            var state = row["_state"] != null ? row["_state"].ToString() : "";
            if (state == "added" || id == "")           //新增：id为空，或_state为added
            {
                model = new Sys.Model.Services
                {
                    linkAddress = row["linkAddress"] == null ? "" : row["linkAddress"].ToString(),
                    OrderId = Utils.StrToInt(row["OrderId"], 0),
                    Name = row["Name"] == null ? "" : row["Name"].ToString(),

                    Picpath = row["Picpath"] == null ? "" : row["Picpath"].ToString()
                };

                BllServices.Add(model);
            }
            else if (state == "modified" || state == "") //更新：_state为空或modified
            {
                model = BllServices.GetModel(Utils.StrToInt(row["ID"], 0));
                if (model != null)
                {
                    model.linkAddress = row["linkAddress"] == null ? "" : row["linkAddress"].ToString();
                    model.OrderId = Utils.StrToInt(row["OrderId"], 0);
                    model.Name = row["Name"] == null ? "" : row["Name"].ToString();

                    model.Picpath = row["Picpath"] == null ? "" : row["Picpath"].ToString();

                    BllServices.Update(model);
                }

            }
        }

    }





    /// <summary>
    /// 删除操作
    /// </summary>
    public void Remove()
    {

        BllServices.Delete(Utils.StrToInt(Request["ID"], 0));

    }



    /// <summary>
    /// 查询具体的信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public void GetView()
    {

        var id = PageRequest.GetInt("ID", 0);
        var dt = BllServices.GetModel(id);
        var json = PluSoft.Utils.JSON.Encode(dt);
        Response.Write(json);
    }






    public void SearchData()
    {

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
            order += " Id desc";
        }
        //业务层：数据库操作

        var strWhere = "1=1";

        var dt = BllServices.GetTable(pageSize, pageIndex, strWhere, order);
        var dataAll = MyDBUtils.DataTable2ArrayList(dt);
        var result = new Hashtable();
        result["data"] = dataAll;
        var total = new Common().GetCount("Services", strWhere);
        result["total"] = total;

        //JSON
        var json = PluSoft.Utils.JSON.Encode(result);
        Response.Write(json);
    }

}