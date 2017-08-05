using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.BLL;
using Sys.Common;

public partial class data_Ajax_flag : System.Web.UI.Page
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

        var id = PageRequest.GetInt("FlagId", 0);
        var dt = new AdminFlag().GetTableModel(id);
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
        var bllAdminFlag = new AdminFlag();
        Sys.Model.AdminFlag model;
        foreach (Hashtable row in rows)
        {
            var id = row["ID"] != null ? row["ID"].ToString() : "";
            //根据记录状态，进行不同的增加、删除、修改操作
            var state = row["_state"] != null ? row["_state"].ToString() : "";
            if (state == "added" || id == "")           //新增：id为空，或_state为added
            {
                model = new Sys.Model.AdminFlag();
                model.AppUrl = row["AppUrl"] == null ? "" : row["AppUrl"].ToString();
                model.CreateTime = DateTime.Now;
                model.Flag = row["Flag"] == null ? "" : row["Flag"].ToString();
                model.FlagAction = row["FlagAction"] == null ? "" : row["FlagAction"].ToString();
                model.FlagGroup = Utils.StrToInt(row["FlagGroup"], 0);
                model.FlagName = row["FlagName"] == null ? "" : row["FlagName"].ToString();
                model.FlagType = Utils.StrToInt(row["FlagType"], 0);
                model.HaveChildNav = Utils.StrToInt(row["HaveChildNav"], 0) == 1 ? true : false;
                model.Icon = "";
                model.IsNav = Utils.StrToInt(row["IsNav"], 0);
                model.IsOpen = Utils.StrToInt(row["IsOpen"], 0) == 1 ? true : false;
                model.OrderID = bllAdminFlag.GetMaxId();
                model.ParentID = Utils.StrToInt(row["ParentID"], 0);
                bllAdminFlag.Add(model);
            }
            else if (state == "modified" || state == "") //更新：_state为空或modified
            {
                model = bllAdminFlag.GetModel(Utils.StrToInt(row["ID"], 0));
                if (model != null)
                {
                    model.AppUrl = row["AppUrl"] == null ? "" : row["AppUrl"].ToString();
                    model.Flag = row["Flag"] == null ? "" : row["Flag"].ToString();
                    model.FlagAction = row["FlagAction"] == null ? "" : row["FlagAction"].ToString();
                    model.FlagGroup = Utils.StrToInt(row["FlagGroup"], 0);
                    model.FlagName = row["FlagName"] == null ? "" : row["FlagName"].ToString();
                    model.FlagType = Utils.StrToInt(row["FlagType"], 0);
                    model.HaveChildNav = Utils.StrToInt(row["HaveChildNav"], 0) == 1 ? true : false;
                    model.Icon = "";
                    model.IsNav = Utils.StrToInt(row["IsNav"], 0);
                    model.IsOpen = Utils.StrToInt(row["IsOpen"], 0) == 1 ? true : false;
                    model.OrderID = bllAdminFlag.GetMaxId();
                    model.ParentID = Utils.StrToInt(row["ParentID"], 0);
                    bllAdminFlag.Update(model);
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
        var dt = new Sys.BLL.AdminFlag().GetTable(-1, -1, strWhere, order);
        var dataAll = MyDBUtils.DataTable2ArrayList(dt);
        var result = new Hashtable();
        result["data"] = dataAll;
        //var total = new Sys.BLL.Common().GetCount("Sys_Flag", strWhere);
        //result["total"] = total;
        //JSON
        var json = PluSoft.Utils.JSON.Encode(dataAll);

        Response.Write(json);

        //var tempstring = "[{\"FlagId\": \"1\",\"FlagName\": \"项目范围规划\",\"Duration\": 8,\"Start\": \"2007-01-01T00:00:00\",\"Finish\": \"2007-01-10T00:00:00\",\"PercentComplete\": 0,\"Summary\": 1,\"Critical\": 0,\"Milestone\": 0,\"PredecessorLink\": [],\"ParentId\": -1},{\"FlagId\": \"2\",\"FlagName\": \"确定项目范围\",\"Duration\": 1,\"Start\": \"2007-01-01T00:00:00\",\"Finish\": \"2007-01-01T23:23:59\",\"PercentComplete\": 30,\"Summary\": 0,\"Critical\": 0,\"Milestone\": 0,\"PredecessorLink\": [],\"ParentId\": \"1\"}]";
        //Response.Write(tempstring);
    }


    /// <summary>
    /// 查询
    /// </summary>
    public void SearchDatat()
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
            order += " OrderID";
        }



        const string strWhere = "IsNav=1";
        var dt = new AdminFlag().GetTable(-1, -1, strWhere, order);

        var tempDt = dt.Clone();
        foreach (DataRow model in dt.Rows)
        {
            if (ManageHelper.CheckAdminHavePower(model["Flag"].ToString()))
            {

                tempDt.Rows.Add(model.ItemArray);
            }
        }


        var dataAll = MyDBUtils.DataTable2ArrayList(tempDt);
        var result = new Hashtable();
        result["data"] = dataAll;
        var json = PluSoft.Utils.JSON.Encode(dataAll);
        Response.Write(json);


    }




    public void DelFlag()
    {
        var flagId = PageRequest.GetInt("FlagId", 0);
        if (flagId > 0)
        {
            var tem = new AdminFlag().Delete(flagId);

            if (tem <= 0)
            {
                Response.Write("");
            }
            else
            {
                Response.Write(tem);
            }
        }
    }



    public void Remove()
    {
        var idStr = Request["FlagId"];
        new Sys.BLL.AdminFlag().DeleteList(idStr);

    }
}