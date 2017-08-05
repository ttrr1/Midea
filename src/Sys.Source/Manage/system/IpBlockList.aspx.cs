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
using Sys.BLL;
using Sys.Common;
using System.Collections.Generic;

public partial class Manage_System_IpBlockList : System.Web.UI.Page
{
    public List<Sys.Model.SysIpBlock> listIp;
    public int BlockModule = PageRequest.GetInt("BlockModule", 0);//限制模块 0系统 1用户 -----//(Int32)Request.Form["BlockModule"];
    public int BlockType = PageRequest.GetInt("BlockType", 0);//限制方式 0白名单 1黑名单

    protected Sys.BLL.SysIpBlock BllSysIpBlock = new Sys.BLL.SysIpBlock();
    protected string act = PageRequest.GetString("act");
    protected string strWhere = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //检查登录
        ManageHelper.CheckAdminLogin();
        //权限
        ManageHelper.CheckAdminPower("system_adminipblockblacklist");
        //这是业务层



        switch (act)
        {
            case "searchData":
                searchData();
                break;

        }


      
        var result = new Hashtable();
        result["data"] = listIp;
        var total = new Common().GetCount("AdminRole", strWhere);
        result["total"] = total;

    //    Response.Write(json);

        var json = PluSoft.Utils.JSON.Encode(listIp);



    }

    void searchData()
    {

        // string strWhere = "BlockModule=" + blockModule + " and BlockType=" + blockType;
        //从业务层加载数据
        listIp = BllSysIpBlock.GetList(BlockModule, BlockType);
    }
}
