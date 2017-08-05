using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.BLL;
using Sys.Common;

public partial class data_Ajax_Admin : System.Web.UI.Page
{
    protected Sys.Model.Admin Loginadmin = new Sys.Model.Admin();
    protected Admin BllAdmin = new Admin();
    Sys.Model.Admin model = new Sys.Model.Admin();
    protected void Page_Load(object sender, EventArgs e)
    {
        var methodName = PageRequest.GetString("method");
        Loginadmin = BllAdmin.GetModel(Account.GetLoginUserID());
        if (Loginadmin == null)
        {
            return;
        }
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

        var id = PageRequest.GetInt("UserId", 0);
        var dt = new Admin().GetTableModel(id);
        var data = MyDBUtils.DataTable2ArrayList(dt);
        var user = data.Count > 0 ? (Hashtable)data[0] : null;
        var json = PluSoft.Utils.JSON.Encode(user);
        Response.Write(json);
    }
    /// <summary>
    /// 检查修改账号的层次是否是自己的账号
    /// </summary>
    public void Checkedself()
    {
        var selectUserId = PageRequest.GetInt("SelectUserId", 0);
        if (Account.GetLoginUserID() == selectUserId)
        {
            Response.Write("不能修改自己的层次!");
        }

    }
    /// <summary>
    /// 更新信息
    /// </summary>
    public void SaveData()
    {

        var msg = "yes";
        var json = Request["data"];
        var rows = (ArrayList)PluSoft.Utils.JSON.Decode(json);
        var bllAdmin = new Admin();
        foreach (Hashtable row in rows)
        {
            var id = row["UserID"] != null ? row["UserID"].ToString() : "";
            var password = row["Password"] == null ? "" : row["Password"].ToString();
            //根据记录状态，进行不同的增加、删除、修改操作
            var state = row["_state"] != null ? row["_state"].ToString() : "";
            if (state == "added" || id == "")           //新增：id为空，或_state为added
            {
                if (model != null)
                {
                    model.RealName = row["RealName"] == null ? "" : row["RealName"].ToString();
                    model.CreateIP = PageRequest.GetIP();
                    model.CreateTime = DateTime.Now;
                    model.Username = row["Username"] == null ? "" : row["Username"].ToString();
                    model.IsPublic = Utils.StrToInt(row["IsPublic"], 0);
                    model.RoleIDs = row["RoleIDs"] == null ? "" : row["RoleIDs"].ToString();
                    model.State = Utils.StrToInt(row["State"], 0);
                    model.OfficeTel = row["OfficeTel"] == null ? "" : row["OfficeTel"].ToString();

                    
                }

                var listrole = new List<Sys.Model.AdminRole>();
                if (!string.IsNullOrEmpty(model.RoleIDs))
                {
                    listrole = new AdminRole().GetList(-1, -1, "RoleId in (" + model.RoleIDs + ")", "createtime desc");

                }
                foreach (var sysRole in listrole)
                {
                    if (sysRole.RoleFlag != "")
                    {
                        model.RoleFlags += sysRole.RoleFlag + ",";
                    }
                    if (sysRole.RoleName != "")
                    {
                        model.RoleNames += sysRole.RoleName + ",";
                    }
                }
                if (model.RoleFlags != "")
                {
                    model.UserFlag = model.RoleFlags + "," + model.PlusFlag;
                }

                var modelPartent = BllAdmin.GetModel(Utils.StrToInt(row["ParentUserID"], 0));
                if (modelPartent != null)
                {
                    if (modelPartent.ParentUserIDs == "")
                    {
                        model.ParentUserIDs = modelPartent.UserId.ToString();
                    }
                    else
                    {
                        model.ParentUserIDs = modelPartent.ParentUserIDs + "," + modelPartent.UserId;
                    }
                    model.ParentUserID = modelPartent.UserId;
                }


                model.RoleNames = Utils.Strquotes(model.RoleNames);
                bllAdmin.Add(model, Utils.MD5(password));

                Response.Write(msg);


            }
            else if (state == "modified" || state == "") //更新：_state为空或modified
            {
                model = bllAdmin.GetModel(Utils.StrToInt(row["UserID"], 0));
                if (model != null)
                {
                    #region 基础字段
                    model.RealName = row["RealName"] == null ? "" : row["RealName"].ToString();
                    model.CreateIP = PageRequest.GetIP();
                    model.CreateTime = DateTime.Now;
                    model.Username = row["Username"] == null ? "" : row["Username"].ToString();
                    model.IsPublic = Utils.StrToInt(row["IsPublic"], 0);
                    model.RoleIDs = row["RoleIDs"] == null ? "" : row["RoleIDs"].ToString();
                    model.ParentUserID = Utils.StrToInt(row["ParentUserID"], 0);
                    model.State = Utils.StrToInt(row["State"], 0);
                    model.OfficeTel = row["OfficeTel"] == null ? "" : row["OfficeTel"].ToString();
                    #endregion
                    password = password == "" ? new Account().GetModel(model.UserId).Password : Utils.MD5(password);
                    #region 角色权限
                    var listrole = new List<Sys.Model.AdminRole>();
                    if (!string.IsNullOrEmpty(model.RoleIDs))
                    {
                        listrole = new AdminRole().GetList(-1, -1, "RoleId in (" + model.RoleIDs + ")", "createtime desc");
                    }

                    model.RoleFlags = "";
                    model.RoleNames = "";
                    foreach (var sysRole in listrole)
                    {
                        if (sysRole.RoleFlag != "")
                        {
                            model.RoleFlags += sysRole.RoleFlag + ",";
                        }
                        if (sysRole.RoleName != "")
                        {
                            model.RoleNames += sysRole.RoleName + ",";
                        }
                    }
                    if (model.RoleFlags != "")
                    {
                        model.UserFlag = model.RoleFlags + "," + model.PlusFlag;
                    }
                    #endregion



                    #region 账号层次修改
                    if (bllAdmin.GetList(-1, -1, "(CHARINDEX('" + Loginadmin.UserId + "',parentUserIDs)>0 or userid=" + Loginadmin.UserId + ") and parentUserID=" + model.UserId, "createtime desc").Count > 0)
                    {
                        if (model.ParentUserID > 0)
                        {
                            UpDtaeParentUserIds(model.UserId, model.ParentUserID);

                        }
                    }

                    #endregion

                    bllAdmin.Update(model, password);
                    Response.Write(msg);


                }

            }
        }

    }

    private void UpDtaeParentUserIds(int userId, int parentUserId)
    {
        UpDateLevels(userId, parentUserId, 0);
    }

    #region 递归移动
    private void UpDateLevels(int userId, int parentUserId, int i)
    {
        var mm = new Sys.Model.Admin();
        mm.UserId = userId;
        var partentMod = BllAdmin.GetModel(parentUserId);
        mm.ParentUserIDs = partentMod.ParentUserIDs + "," + parentUserId;
        mm.ParentUserID = parentUserId;
        //去除前后逗号 以防万一
        if (mm.ParentUserIDs.IndexOf(',') == 0)
        {
            mm.ParentUserIDs = mm.ParentUserIDs.Substring(1, mm.ParentUserIDs.Length - 1);
        }
        if (mm.ParentUserIDs.LastIndexOf(',') == (mm.ParentUserIDs.Length - 1))
        {
            mm.ParentUserIDs.Substring(0, mm.ParentUserIDs.Length - 1);
        }
        if (i == 0)
        {
            model.ParentUserID = mm.ParentUserID;
            model.ParentUserIDs = mm.ParentUserIDs;
        }
        BllAdmin.UpDateLeave(mm);
        var childAdminModel = BllAdmin.GetList("ParentUserID=" + mm.UserId);
        foreach (var mmm in childAdminModel)
        {
            UpDateLevels(mmm.UserId, mm.UserId, 1);
        }
    }
    #endregion

    /// <summary>
    /// 查询
    /// </summary>
    /// 
    public void SearchData()
    {
        Loginadmin = BllAdmin.GetModel(Account.GetLoginUserID());
        if (Loginadmin == null)
        {
            return;
        }

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
        var strWhere = "1=1";
        if (key != "")
        {
            strWhere += " and charIndex('" + key + "',UserName) > 0";
        }

        strWhere += "  and charIndex('" + Loginadmin.UserId + "',parentUserIDs) > 0  or UserId=" + Loginadmin.UserId + "";


        var dt = new Admin().GetTable(-1, -1, strWhere, order);
        var dataAll = MyDBUtils.DataTable2ArrayList(dt);
        var result = new Hashtable();
        result["data"] = dataAll;
        var json = PluSoft.Utils.JSON.Encode(dataAll);

        Response.Write(json);

    }


    /// <summary>
    /// 订单分配查询，
    /// </summary>
    /// 
    public void SearchDataDistrbution()
    {
        Loginadmin = BllAdmin.GetModel(Account.GetLoginUserID());
        if (Loginadmin == null)
        {
            return;
        }

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
        var strWhere = "1=1";
        if (key != "")
        {
            strWhere += " and charIndex('" + key + "',UserName) > 0";
        }

       // strWhere += "  and charIndex('" + Loginadmin.UserId + "',parentUserIDs) > 0  or UserId=" + Loginadmin.UserId + "";
        strWhere += " and RoleIDs='89'";

        var dt = new Admin().GetTable(-1, -1, strWhere, order);
        var dataAll = MyDBUtils.DataTable2ArrayList(dt);
        var result = new Hashtable();
        result["data"] = dataAll;
        var json = PluSoft.Utils.JSON.Encode(dataAll);

        Response.Write(json);

    }



  
}