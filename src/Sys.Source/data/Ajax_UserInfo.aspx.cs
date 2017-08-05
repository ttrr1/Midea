using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.Common;

public partial class data_Ajax_UserInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var methodName = PageRequest.GetString("method");
        var type = this.GetType();
        var method = type.GetMethod(methodName);
        if (method == null) throw new Exception("method is null");
        method.Invoke(this, null);
    }

    /// <summary>
    /// 更新信息
    /// </summary>
    public void SaveData()
    {
        var json = Request["data"];
        var areaData = Request["areaData"];
        var roleValue = Request["roleValue"];
        var provinceData = areaData.Split('|')[0];
        var cityData = areaData.Split('|')[1];
        var area = areaData.Split('|')[2];
        var rows = (ArrayList)PluSoft.Utils.JSON.Decode(json);
        var bll = new Sys.BLL.UserInfo();
        Sys.Model.UserInfo model;
        foreach (Hashtable row in rows)
        {
            var id = row["ID"] != null ? row["ID"].ToString() : "";
            //根据记录状态，进行不同的增加、删除、修改操作
            var state = row["_state"] != null ? row["_state"].ToString() : "";
            if (state == "added" || id == "")           //新增：id为空，或_state为added
            {
                model = new Sys.Model.UserInfo();
                model.UserName = row["UserName"].ToString();
                model.RealName = row["RealName"].ToString();
                model.CompanyName = row["CompanyName"].ToString();
                model.ProvinceId = Utils.StrToInt(row["ProvinceId"], 0);
                model.ProvinceName = provinceData.Split(':')[1];
                model.CityId = Utils.StrToInt(row["CityId"], 0);
                model.CityName = cityData.Split(':')[1];
                model.AreaId = Utils.StrToInt(row["AreaId"], 0);
                model.AreaName = area.Split(':')[1];
                model.Address = row["Address"].ToString();
                model.RoleId = Utils.StrToInt(row["RoleId"], 0);
                model.TypeKey = row["TypeKey"].ToString();
                model.TypeValue = roleValue.Split(':')[1];
                model.CreateTime = DateTime.Now;

                var result = new Sys.BLL.UserInfo().Add(model, Utils.MD5(row["Password"].ToString()));

            }
            else if (state == "modified" || state == "") //更新：_state为空或modified
            {
                model = bll.GetModel(Utils.StrToInt(row["ID"], 0));
                if (model != null)
                {
                    model.UserName = row["UserName"].ToString();
                    model.RealName = row["RealName"].ToString();
                    model.CompanyName = row["CompanyName"].ToString();
                    model.ProvinceId = Utils.StrToInt(row["ProvinceId"], 0);
                    model.ProvinceName = provinceData.Split(':')[1];
                    model.CityId = Utils.StrToInt(row["CityId"], 0);
                    model.CityName = cityData.Split(':')[1];
                    model.AreaId = Utils.StrToInt(row["AreaId"], 0);
                    model.AreaName = area.Split(':')[1];
                    model.Address = row["Address"].ToString();
                    model.RoleId = Utils.StrToInt(row["RoleId"], 0);
                    model.TypeKey = row["TypeKey"].ToString();
                    model.TypeValue = roleValue.Split(':')[1];
                    bll.Update(model);
                }

            }
        }

    }

    /// <summary>
    /// 查询具体的信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public void GetView()
    {
        var id = PageRequest.GetInt("id", 0);
        var dt = new Sys.BLL.UserInfo().GetUserInfoById(id);
        var data = MyDBUtils.DataTable2ArrayList(dt);
        var user = data.Count > 0 ? (Hashtable)data[0] : null;
        var json = PluSoft.Utils.JSON.Encode(user);
        Response.Write(json);
        Response.End();
    }

    /// <summary>
    /// 区域信息
    /// </summary>
    public void GetAreaInfo()
    {
        var dataType = PageRequest.GetQueryString("dataType");
        List<Sys.Model.DicModel> lisProvice, lisCity, lisArea;
        new Sys.BLL.UserInfo().AreaInit(out lisProvice, out lisCity, out lisArea);

        var json = string.Empty;
        if (dataType.Equals("p"))
        {
            json = PluSoft.Utils.JSON.Encode(lisProvice);
        }
        else if (dataType.Equals("c"))
        {
            json = PluSoft.Utils.JSON.Encode(lisCity);
        }
        else if (dataType.Equals("a"))
        {
            json = PluSoft.Utils.JSON.Encode(lisArea);
        }
        Response.Write(json);
    }

    /// <summary>
    /// 用户类型值
    /// </summary>
    public void GetUserTypeInfo()
    {
        var roleId = PageRequest.GetQueryString("RoleId");
        List<Sys.Model.DicModel> lisAgentType, lisInstallerTypeList;
        new Sys.BLL.Account().RegisterInit(out lisAgentType, out lisInstallerTypeList);

        var json = string.Empty;
        if (roleId.Equals("1"))
        {
            json = PluSoft.Utils.JSON.Encode(lisAgentType);
        }
        else if (roleId.Equals("2"))
        {
            json = PluSoft.Utils.JSON.Encode(lisInstallerTypeList);
        }
        Response.Write(json);
    }


    /// <summary>
    /// 查询
    /// </summary>
    public void SearchData()
    {
        //查询条件
        var key = Utils.SqlStringFormat(PageRequest.GetString("key"), 2);
        var userType = PageRequest.GetString("userType");
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
            order = " u." + sortField + " " + sortOrder;
        }
        else
        {
            order += " u.createtime desc";
        }
        var strWhere = "1=1";
        if (!string.IsNullOrEmpty(userType))
        {
            strWhere += " and  u.RoleId=" + userType;
        }
        if (!string.IsNullOrEmpty(key))
        {
            strWhere += " and  charIndex('" + key + "',u.UserName) > 0";
        }

        if (pageSize == 1)
        {
            pageSize = 100;
        }
        var dt = new Sys.BLL.UserInfo().GetListForMoreTable(pageSize, pageIndex, strWhere, order);
        var dataAll = MyDBUtils.DataTable2ArrayList(dt);
        var result = new Hashtable();
        result["data"] = dataAll;
        var total = new Sys.BLL.Common().GetCount("UserInfo u", strWhere);
        result["total"] = total;
        //JSON
        var json = PluSoft.Utils.JSON.Encode(result);
        Response.Write(json);

    }


    public void StatusInit()
    {
        var id = PageRequest.GetInt("id", 0);
        var model = new Sys.BLL.UserInfo().GetModel(id);
        var json = string.Empty;
        if (model != null)
        {
            var userId = model.UserId;
            var memberModel = new Sys.BLL.Member().GetModel(userId);
            json = PluSoft.Utils.JSON.Encode(memberModel);
        }
        Response.Write(json);
    }

    //会员状态更新
    public void StatusUpdate()
    {
        var json = Request["data"];
        var rows = (ArrayList)PluSoft.Utils.JSON.Decode(json);
        foreach (Hashtable row in rows)
        {
            var userId = Convert.ToInt32(row["UserID"]);
            var memberModel = new Sys.BLL.Member().GetModel(userId);
            if (memberModel != null)
            {
                memberModel.State = Convert.ToInt32(row["State"]);
                var result = new Sys.BLL.Member().Update_AllInfo(memberModel);
                if (result > 0)
                {
                    Response.Write("1");
                }
            }
        }

        Response.Write("0");
    }


    public void PasswordUpdateInit()
    {
        var id = PageRequest.GetInt("id", 0);
        var model = new Sys.BLL.UserInfo().GetModel(id);
        var json = string.Empty;
        if (model != null)
        {
            json = PluSoft.Utils.JSON.Encode(model);
        }
        Response.Write(json);
    }

    public void PasswordUpdate()
    {
        var json = Request["data"];
        var rows = (ArrayList)PluSoft.Utils.JSON.Decode(json);
        foreach (Hashtable row in rows)
        {
            var userId = Convert.ToInt32(row["UserId"]);
            //var password = Utils.MD5(row["Password"].ToString());
            var passwordNew = Utils.MD5(row["PasswordNew"].ToString());
            var result = new Sys.BLL.Account().AccountUpdate(userId, passwordNew);
            if (result > 0)
            {
                Response.Write("1");
            }
        }

        Response.Write("0");
    }

}