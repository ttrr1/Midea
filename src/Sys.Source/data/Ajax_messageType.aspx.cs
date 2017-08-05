using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.Common;

public partial class data_Ajax_messageType : System.Web.UI.Page
{
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
    /// 查询具体的信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public void GetView()
    {

        var id = PageRequest.GetInt("ID", 0);
        var dt = new Sys.BLL.NewsType().GetTableModel(id);
        var data = MyDBUtils.DataTable2ArrayList(dt);
        var user = data.Count > 0 ? (Hashtable)data[0] : null;
        var json = PluSoft.Utils.JSON.Encode(user);
        Response.Write(json);
    }





    /// <summary>
    /// 更新信息
    /// </summary>
    public void SaveData()
    {
        var json = Request["data"];
        var rows = (ArrayList)PluSoft.Utils.JSON.Decode(json);
        var bllNewsType = new Sys.BLL.NewsType();
        Sys.Model.NewsType model;
        foreach (Hashtable row in rows)
        {
            var id = row["ID"] != null ? row["ID"].ToString() : "";
            //根据记录状态，进行不同的增加、删除、修改操作
            var state = row["_state"] != null ? row["_state"].ToString() : "";
            if (state == "added" || id == "")           //新增：id为空，或_state为added
            {
                model = new Sys.Model.NewsType
                            {
                                Count = 0,
                                CreateIp = PageRequest.GetIP(),
                                CreateTime = DateTime.Now,
                                PId = Utils.StrToInt(row["PId"], 0),
                                TypeAction = row["TypeAction"] == null ? "" : row["TypeAction"].ToString(),
                                TypeName = row["TypeName"] == null ? "" : row["TypeName"].ToString()
                            };


                bllNewsType.Add(model);
            }
            else if (state == "modified" || state == "") //更新：_state为空或modified
            {
                model = bllNewsType.GetModel(Utils.StrToInt(row["ID"], 0));
                if (model != null)
                {
                    model.PId = Utils.StrToInt(row["PId"], 0);
                    model.TypeAction = row["TypeAction"] == null ? "" : row["TypeAction"].ToString();
                    model.TypeName = row["TypeName"] == null ? "" : row["TypeName"].ToString();
                    bllNewsType.Update(model);
                }

            }
        }

    }



    /// <summary>
    /// 查询
    /// </summary>
    public void SearchData()
    {
        //查询条件
        var key = Utils.SqlStringFormat(PageRequest.GetString("key"), 2);
        //分页
        var pageIndex = PageRequest.GetInt("pageIndex", 1);
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
        var strWhere = "";
        if (key != "")
        {
            strWhere += "charIndex('" + key + "',FlagName) > 0";
        }


        if (pageSize == 1)
        {
            pageSize = 1000;
        }
        var dt = new Sys.BLL.NewsType().GetTable(-1, -1, strWhere, order);
        var dataAll = MyDBUtils.DataTable2ArrayList(dt);
        var result = new Hashtable();
        result["data"] = dataAll;
        //var total = new Sys.BLL.Common().GetCount("Sys_Flag", strWhere);
        //result["total"] = total;
        //JSON
        var json = PluSoft.Utils.JSON.Encode(dataAll);

        Response.Write(json);

    }

}