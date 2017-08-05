using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Sys.Common;
using System.Collections.Generic;

public partial class Manage_System_IpBlockConfig : System.Web.UI.Page
{
    public List<Sys.Model.SysConfig> listIp;
    public int BlockModule = PageRequest.GetInt("BlockModule", 0);//限制模块 0系统 1用户
    public int BlockType = PageRequest.GetInt("BlockType", 0);//限制方式 0白名单 1黑名单
    public string CurPageFlag = "system_adminipblockconfig";//当前模块权限
    protected void Page_Load(object sender, EventArgs e)
    {
     
        ManageHelper.CheckAdminLogin();

        ManageHelper.CheckAdminPower(CurPageFlag);
        var bll = new Sys.BLL.SysConfig();

        if (ManageHelper.PageAct() == "save")
        {
            ManageHelper.CheckAdminPower("system_memberipblockconfigupdate");//修改权限
            var model = bll.GetModel("WebConfig", "AdminIpBlockType");
            model.Value = PageRequest.GetString("IpBlockType");
            bll.Update(model);
            //添加日志
            ManageHelper.AddLog(CurPageFlag, (BlockModule == 0 ? "后台" : "用户") + "访问限制选项修改为：“" + Sys.BLL.SysConfig.GetNameByCache("IpBlockType", PageRequest.GetString("IpBlockType"))+"”");

            Response.Write("yes");
            Response.End();
        }

      
    }
}
