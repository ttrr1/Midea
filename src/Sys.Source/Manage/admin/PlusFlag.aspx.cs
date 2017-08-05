using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.BLL;
using Sys.Common;

public partial class UiDesk_admin_PlusFlag : System.Web.UI.Page
{


    protected int UserId = PageRequest.GetInt("UserId", -1);
    protected Admin BllAdmin = new Admin();
    protected Sys.Model.Admin Modeladmin = new Sys.Model.Admin();
    protected Sys.Model.Admin Loginadmin = new Sys.Model.Admin();
    protected string act = PageRequest.GetString("act");
    protected AdminRole BllAdminRole = new AdminRole();
    protected AdminFlag BllSysFlag = new AdminFlag();
    protected List<Sys.Model.AdminFlag> Listflag = new List<Sys.Model.AdminFlag>();
    protected string Html = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        ManageHelper.CheckAdminLogin();
        

        switch (act)
        {
            case "editsave":
                Save();
                break;
            default:
                break;
        }
        Loginadmin = BllAdmin.GetModel(Account.GetLoginUserID());
        Modeladmin = BllAdmin.GetModel(UserId);

        Html = GetRoleFlagList(Modeladmin.RoleFlags, Modeladmin.PlusFlag);
    }




    #region 添加
    private void Save()
    {
        var model = BllAdmin.GetModel(PageRequest.GetInt("UserId", 0));
        if (model != null)
        {
            model.PlusFlag = PageRequest.GetString("RoleFlag");//获取新的权限
            model.UserFlag = model.RoleFlags + "," + model.PlusFlag;
            model.UserFlag = model.UserFlag.Replace(",,", ",");
            BllAdmin.UpdateForPlusSysRoleFlag(model);
            Response.Write("yes");
        }
        else
        {
            Response.Write("");
        }

        Response.End();


    }
    #endregion




    #region 权限列表
    int _emptyNum = 0;
    protected string GetRoleFlagList(string RoleFlags, string PlusFlag)
    {
        RoleFlags = "," + RoleFlags + ",";
        PlusFlag = "," + PlusFlag + ",";
        var xs = new StringBuilder();
        var dt = BllSysFlag.GetRoleChildFlag(0, 0).Tables[0];
        for (var i = 0; i < dt.Rows.Count; i++)
        {
            xs.Append("<div class=\"ctn\" id=\"div" + dt.Rows[i]["ID"] + "\"><ul id=\"u" + dt.Rows[i]["ID"] + "\">");

            var checkflag = "";
            var visString = "";//可见性
            if (RoleFlags.IndexOf("," + dt.Rows[i][4] + ",") != -1)
            {
                checkflag = "checked='checked'  disabled='disabled'";

            }
            else
            {
                if (PlusFlag.IndexOf("," + dt.Rows[i][4] + ",") != -1)
                {
                    checkflag = "checked";
                }
            }

            if (Loginadmin.UserFlag.IndexOf(dt.Rows[i][4].ToString()) == -1)
            {
                visString = " disabled='disabled' ";
            }
            else
            {
                visString = "";
            }

            xs.Append("<li class=\"bgc\" ><input type=\"checkbox\"  " + checkflag + "    " + visString + " value=\"" + dt.Rows[i]["Flag"] + "\" onclick=\"checkall(this,'" + dt.Rows[i]["ID"] + "');\" name=\"RoleFlag\"><img class=\"inputcheckbox\" src=\"../images/img_03.png\" /> " + dt.Rows[i]["FlagName"] + "</li>");

            xs.Append(GetChildRoleFlagList(Convert.ToInt32(dt.Rows[i][0]), RoleFlags, PlusFlag));
            xs.Append("</ul></div>");



            _emptyNum = 0;

            //xs.AppendFormat("<div id=\"p{0}\"><ul class=\"ultitle\" id=\"t{0}\"><input type=\"checkbox\" name=\"RoleFlag\" id=\"{0}\"  value=\"{3}\" " + checkflag + " onclick=\"selAll(this.id)\">" +
            //    "<img align=\"absmiddle\" src=\"/Manage/pic/menu/{2}\"/>&nbsp;<span id=\"s{0}\"onclick=\"showchildnode(this.id)\" style=\"cursor:pointer;\">{1}</span></ul>",
            //    dt.Rows[i][0], dt.Rows[i][1], dt.Rows[i][3], dt.Rows[i][4]);
            //xs.Append(GetChildRoleFlagList(Convert.ToInt32(dt.Rows[i][0]), strTmp));
            //xs.Append("</div>");




        }
        return xs.ToString();
    }
    private string GetChildRoleFlagList(int pID, string RoleFlags, string PlusFlag)
    {
        var xs = "";
        for (var n = 0; n < _emptyNum; n++)
        {
            xs += "　";
        }
        var strRFlaglist = new StringBuilder();
        //if (EmptyNum == 0)
        //{
        //    strRFlaglist.Append("<ul class=\"divurl\" id=\"c" + pID + "\" style=\"border:1px solid #99CCFF;\">");
        //}
        //else
        //{
        //    strRFlaglist.Append("<ul class=\"divurl\" id=\"c" + pID + "\">");
        //}
        DataTable dtChild = BllSysFlag.GetRoleChildFlag(pID, 0).Tables[0];


        for (int m = 0; m < dtChild.Rows.Count; m++)
        {
            var checkflag = "";
            var visString = "";//可见性
            if (RoleFlags.IndexOf("," + dtChild.Rows[m][4] + ",") != -1)
            {
                checkflag = "checked='checked'  disabled='disabled'";

            }
            else
            {
                if (PlusFlag.IndexOf("," + dtChild.Rows[m][4] + ",") != -1)
                {
                    checkflag = "checked";
                }
            }

            if (Loginadmin.UserFlag.IndexOf(dtChild.Rows[m][4].ToString()) == -1)
            {
                visString = " disabled='disabled' ";
            }
            else
            {
                visString = "";
            }


            //strRFlaglist.AppendFormat("<li>" + xs + "<input type=\"checkbox\"  name=\"RoleFlag\" id=\"{0}\" value=\"{3}\" " + checkflag + " onclick=\"conclick(this.id," + pID + ")\">" +
            //    "<img align=\"absmiddle\" src=\"/Manage/pic/menu/{2}\"/>&nbsp;{1}</li>", dtChild.Rows[m][0].ToString(), dtChild.Rows[m][1].ToString(),
            //    dtChild.Rows[m][3].ToString().ToLower() == "" ? "item.gif" : dtChild.Rows[m][3].ToString(), dtChild.Rows[m][4].ToString());



            strRFlaglist.Append("<li ><input type=\"checkbox\" " + checkflag + "  " + visString + "  value=\"" + dtChild.Rows[m]["Flag"] + "\"  name=\"RoleFlag\" onclick=\"selcheck(this,'" + pID + "');\" />" + dtChild.Rows[m]["FlagName"] + "</li>");
            if (dtChild.Rows[m]["HaveChildNav"].ToString().ToLower() == "true")
            {
                _emptyNum++;
                strRFlaglist.Append(GetChildRoleFlagList(Convert.ToInt32(dtChild.Rows[m][0]), RoleFlags, PlusFlag));
            }
        }
        // strRFlaglist.Append("</ul>");
        return strRFlaglist.ToString();


        //strTmp = "," + strTmp + ",";
        //var xs = "";
        //for (var n = 0; n < EmptyNum; n++)
        //{
        //    xs += "　";
        //}
        //var strRFlaglist = new StringBuilder();
        //if (EmptyNum == 0)
        //{
        //    strRFlaglist.Append("<ul class=\"divurl\" id=\"c" + pID + "\" style=\"border:1px solid #99CCFF;\">");
        //}
        //else
        //{
        //    strRFlaglist.Append("<ul class=\"divurl\" id=\"c" + pID + "\">");
        //}
        // DataTable dtChild = BllSysFlag.GetRoleChildFlag(pID, 0).Tables[0];


        //for (int m = 0; m < dtChild.Rows.Count; m++)
        //{
        //    string checkflag = "";
        //    if (strTmp.IndexOf("," + dtChild.Rows[m][4].ToString() + ",") != -1)
        //    {
        //        checkflag = "checked";
        //    }
        //    strRFlaglist.AppendFormat("<li>" + xs + "<input type=\"checkbox\"  name=\"RoleFlag\" id=\"{0}\" value=\"{3}\" " + checkflag + " onclick=\"conclick(this.id," + pID + ")\">" +
        //        "<img align=\"absmiddle\" src=\"/Manage/pic/menu/{2}\"/>&nbsp;{1}</li>", dtChild.Rows[m][0].ToString(), dtChild.Rows[m][1].ToString(),
        //        dtChild.Rows[m][3].ToString().ToLower() == "" ? "item.gif" : dtChild.Rows[m][3].ToString(), dtChild.Rows[m][4].ToString());
        //    if (dtChild.Rows[m]["HaveChildNav"].ToString().ToLower() == "true")
        //    {
        //        EmptyNum++;
        //        strRFlaglist.Append(GetChildRoleFlagList(Convert.ToInt32(dtChild.Rows[m][0]), strTmp));
        //    }
        //}
        //strRFlaglist.Append("</ul>");
        //return strRFlaglist.ToString();
    }
    #endregion
}