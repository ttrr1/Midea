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

public partial class Manage_System_IpBlockItem : System.Web.UI.Page
{
    public int ID = PageRequest.GetInt("ID", -1);
    public int BlockModule = PageRequest.GetInt("BlockModule", 0);//限制模块 0系统 1用户
    protected string BlockModuleName = "";//中文标记用于写日志
    public int BlockType = PageRequest.GetInt("BlockType", 0);//限制方式 0白名单 1黑名单
    protected string BlockTypeName = "";//中文标记用于写日志
    public string Name = PageRequest.GetString("Name");
    public ManageErrMsg err = new ManageErrMsg();

    protected void Page_Load(object sender, EventArgs e)
    {
        //获取BlockModule和BlockType的中文名
        if (BlockModule == 0)
        {
            BlockModuleName = "系统";
        }
        else if (BlockModule == 1)
        {
            BlockModuleName = "用户";
        }
        if (BlockType == 0)
        {
            BlockModuleName = "白名单";
        }
        else if (BlockType == 1)
        {
            BlockModuleName = "黑名单";
        }
        //检查登录
        ManageHelper.CheckAdminLogin();

        //管理权限
        //    ManageHelper.CheckAdminPower("system_adminipblockmanage");
        if (PageRequest.GetString("act") == "SaveData")
        {
            Save();
        }

    }

    private void Delete()
    {
        //删除权限                    
        ManageHelper.CheckAdminPower("system_memberipblockdelete");
        var bll = new Sys.BLL.SysIpBlock();
        bll.Delete(ID);
        ManageHelper.AddLog("system_memberipblockdelete", "删除了" + ID);
        Response.Redirect("ipblocklist.aspx?BlockModule=" + BlockModule + "&BlockType=" + BlockType + "&sucmsg=IP访问规则删除成功。");
    }

    private void Check()
    {
        Sys.Common.IP.RestrictionIPResult ip = Sys.Common.IP.BlockIP.GenerateIPList(Name);
        if (!ip.IsChecked)
        {
            Response.Write("-1");//IP错误
            Response.End();
        }

        var bllIP = new Sys.BLL.SysIpBlock();
        if (bllIP.CheckName(ID, Name, BlockType, BlockModule))
        {
            Response.Write("-2");//重名
            Response.End();
        }

        Response.Write("1");
        Response.End();
    }

    private void Save()
    {

        var msg = "yes";
        //修改权限
        //    ManageHelper.CheckAdminPower("system_memberipblockupdate");
        var ip = Sys.Common.IP.BlockIP.GenerateIPList(Name);
        if (!ip.IsChecked)
        {

            msg = "*IP访问规则格式错误";


            Response.Write(msg);
            Response.End();

        }




        var bll = new Sys.BLL.SysIpBlock();
        //if (bll.CheckName(ID, Name, BlockType, BlockModule))
        //    err.AddErr("*该IP访问规则已经存在");

        //err.ChkErr();

        var model = new Sys.Model.SysIpBlock();
        model.ID = ID;
        model.IpStart = ip.StartIP;
        model.IpEnd = ip.EndIP;
        model.Name = Name;
        model.BlockType = BlockType;
        model.BlockModule = BlockModule;

        if (ID == 0)
        {
            bll.Add(model);
            ManageHelper.AddLog("system_memberipblockadd", "添加了" + BlockModuleName + BlockTypeName + model.Name);//写日志

        }
        else
        {

            bll.Update(model);
            ManageHelper.AddLog("system_memberipblockadd", "修改" + Name + "成" + BlockModuleName + BlockTypeName + model.Name);//写日志


        }

        Response.Write(msg);

        Response.End();
    }

    private void Add()
    {
        //添加权限
        ManageHelper.CheckAdminPower("system_memberipblockadd");
        Sys.Common.IP.RestrictionIPResult ip = Sys.Common.IP.BlockIP.GenerateIPList(Name);
        if (!ip.IsChecked)
            err.AddErr("*IP访问规则格式错误");

        Sys.BLL.SysIpBlock bll = new Sys.BLL.SysIpBlock();
        if (bll.CheckName(ID, Name, BlockType, BlockModule))
            err.AddErr("*该IP访问规则已经存在");

        err.ChkErr();

        Sys.Model.SysIpBlock model = new Sys.Model.SysIpBlock();
        model.ID = 0;
        model.IpStart = ip.StartIP;
        model.IpEnd = ip.EndIP;
        model.Name = Name;
        model.BlockType = BlockType;
        model.BlockModule = BlockModule;
        int NewID = bll.Add(model);
        ManageHelper.AddLog("system_memberipblockadd", "添加了" + BlockModuleName + BlockTypeName + model.Name);//写日志
        Response.Write("<script>window.parent.location.href=\"ipblocklist.aspx?BlockModule=" + BlockModule + "&BlockType=" + BlockType + "&sucmsg=IP访问规则添加成功。\";</script>");
        Response.End();
    }
}
