using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.BLL;
using Sys.Common;

public partial class data_Ajax_role : System.Web.UI.Page
{
    protected Sys.Model.Admin Loginadmin = new Sys.Model.Admin();
    protected Admin BllAdmin = new Admin();
    Sys.Model.Admin model = new Sys.Model.Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        Loginadmin = BllAdmin.GetModel(Account.GetLoginUserID());
        if (Loginadmin == null)
        {
            return;
        }

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

        var id = PageRequest.GetInt("ContactId", 0);
        var dt = new Sys.BLL.AdminRole().GetTableModel(id);
        var data = MyDBUtils.DataTable2ArrayList(dt);
        var user = data.Count > 0 ? (Hashtable)data[0] : null;
        var json = PluSoft.Utils.JSON.Encode(user);
        Response.Write(json);
    }





    ///// <summary>
    ///// 更新角色信息
    ///// </summary>
    //public void SaveData()
    //{
    //    var json = Request["data"];
    //    var rows = (ArrayList)PluSoft.Utils.JSON.Decode(json);
    //    var bllContactInfo = new Sys.BLL.ContactInfo();
    //    Sys.Model.ContactInfo model;
    //    foreach (Hashtable row in rows)
    //    {
    //        var id = row["ContactId"] != null ? row["ContactId"].ToString() : "";
    //        //根据记录状态，进行不同的增加、删除、修改操作
    //        var state = row["_state"] != null ? row["_state"].ToString() : "";
    //        if (state == "added" || id == "")           //新增：id为空，或_state为added
    //        {
    //            model = new Sys.Model.ContactInfo();
    //            model.CompanyCode = Loginadmin.CompanyCode;
    //            model.CompanyId = 0;
    //            model.ContactName = row["ContactName"] == null ? "" : row["ContactName"].ToString();
    //            model.PositionName = row["PositionName"] == null ? "" : row["PositionName"].ToString();
    //            model.Remarks = row["Remarks"] == null ? "" : row["Remarks"].ToString();
    //            model.OfficeTel = row["OfficeTel"] == null ? "" : row["OfficeTel"].ToString();
    //            model.CreateIp = PageRequest.GetIP();
    //            model.CreateTime = DateTime.Now;
    //            model.CustomerId = Utils.StrToInt(row["CustomerId"], 0);//关联的客户编号
    //            model.Email = row["Email"] == null ? "" : row["Email"].ToString();
    //            model.IsMain = Utils.StrToInt(row["IsMain"], 0);
    //            bllContactInfo.Add(model);
    //        }
    //        else if (state == "modified" || state == "") //更新：_state为空或modified
    //        {
    //            model = bllContactInfo.GetModel(Utils.StrToInt(row["ContactId"], 0));
    //            if (model != null)
    //            {
    //                model.ContactName = row["ContactName"].ToString();
    //                model.ContactName = row["ContactName"].ToString();
    //                model.Email = row["Email"].ToString();
    //                model.Mobile = row["Mobile"].ToString();
    //                model.OfficeTel = row["OfficeTel"].ToString();
    //                model.PositionName = row["PositionName"].ToString();
    //                model.Remarks = row["Remarks"].ToString();
    //                bllContactInfo.Update(model);
    //            }

    //        }
    //    }

    //}


    /// <summary>
    /// 查询公司编码相关的角色数据
    /// </summary>
    public void SearchSelRole()
    {
        var strWhere = "";
        var dt = new AdminRole().GetTable(-1, -1, strWhere, " createtime desc");
        var dataAll = MyDBUtils.DataTable2ArrayList(dt);
        var result = new Hashtable();
        result["data"] = dataAll;
        var json = PluSoft.Utils.JSON.Encode(dataAll);
        Response.Write(json);

    }

    /// <summary>
    /// 角色查询
    /// </summary>
    public void SearchData()
    {
        //查询条件
        var key = Utils.SqlStringFormat(PageRequest.GetString("key"), 2);
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
        var strWhere = "1=1";
        if (key != "")
        {
            strWhere += " and  charIndex('" + key + "',RoleName) > 0";
        }


        if (pageSize == 1)
        {
            pageSize = 100;
        }
        var dt = new AdminRole().GetTable(pageSize, pageIndex, strWhere, order);
        var dataAll = MyDBUtils.DataTable2ArrayList(dt);
        var result = new Hashtable();
        result["data"] = dataAll;
        var total = new Common().GetCount("AdminRole", strWhere);
        result["total"] = total;
        //JSON
        var json = PluSoft.Utils.JSON.Encode(result);
        Response.Write(json);
    }






}