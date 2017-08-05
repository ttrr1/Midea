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
using System.Management;
using Sys.BLL;
using Sys.Common;

public partial class Manage_login : System.Web.UI.Page
{

    public string errMsg = PageRequest.GetString("errmsg");
    public string act = PageRequest.GetString("act");
    public string username = PageRequest.GetString("username");
    public string password = PageRequest.GetString("password");
    public string validatecode = PageRequest.GetString("validatecode");
    public string url = PageRequest.GetString("url");

    protected void Page_Load(object sender, EventArgs e)
    {

        if (act == "checkLogin")
        {
            CheckLogin();
        }

    
        if (errMsg != "")
            errMsg = @"<img src=""pic/standard_msg_error.gif"" align=""absbottom"">&nbsp;" + errMsg;

        if (act == "login")
            UserLogin();

        if (GetLoginName() != "")
            username = GetLoginName();
    }


    /// <summary>
    /// 检查登录
    /// </summary>
    private void CheckLogin()
    {
        var msg = "yes";
        var userName = PageRequest.GetString("UserName");
        var passWord = PageRequest.GetString("PassWord");
    
        var validatecode = PageRequest.GetString("validatecode");

        //验证码检测
        try
        {
            if (validatecode.ToLower() != Session["ValidateCode"].ToString().ToLower())
            {
                msg = "验证码错误，请返回检查";
                Response.Write(msg);
                Response.End();
            }
        }
        catch
        {

            msg = "yes";
        }

        //帐户登陆
        var bllAccount = new Account();
        var userId = bllAccount.CheckLogin(userName,Utils.MD5(passWord));

        switch (userId)
        {
           
            case -2:
                msg = "帐户不存在！";
                break;
            case -1:
                msg = "密码不正确！";
                break;
        }

        if (userId < 0)
        {
            Response.Write(msg);
            Response.End();
        }

       
        

        //管理账户登录
        var bllAdmin = new Admin();
        var modelAdmin = bllAdmin.GetModel(userId);
        if (modelAdmin == null)
        {
            msg = "该账户无权登陆系统后台！";
            Response.Write(msg);
            Response.End();

        }
        else if (modelAdmin.State == 0)
        {

            msg = "该账户禁止登陆系统后台！";
            Response.Write(msg);
            Response.End();
        }

        //成功响应

        //Cookie保存-帐户
        //518400年 43200月 1440天
        Cookie.WriteUserCookie(userId, Utils.MD5(passWord), 518400);



        //添加日志
        ManageHelper.AddLog("managelogin", "登陆成功");

        //Cookie保存-帐户
        Sys.BLL.Admin.SetUsername(modelAdmin.Username);
        //Cookie保存-姓名
        Sys.BLL.Admin.SetRealName(modelAdmin.RealName);


        Response.Write(msg);
        Response.End();
    }



    /// <summary>
    /// 登陆
    /// </summary>
    private void UserLogin()
    {
        //URL登陆定向，涉及SSO登陆
        if (url.Length > 7 && url.Substring(0, 7) == "http://")
            url = "";
        else
            url = Utils.UrlEncode(url);

        //用户名字段判断
        if (username == "")
            Response.Redirect("login.aspx?url=" + url + "&errmsg=帐户为空，请重新输入！&username=" + username);

        //密码字段判断
        if (password == "")
            Response.Redirect("login.aspx?url=" + url + "&errmsg=密码为空，请重新输入！&username=" + username);

        ////验证码字段判断
        //if (validatecode == "")
        //    Response.Redirect("login.aspx?url=" + url + "&errmsg=验证码为空，请重新输入！&username=" + username);

        ////验证码判断
        //if (validatecode.ToLower() != Request.Cookies["ValidateCode"].Value.ToLower())
        //    Response.Redirect("login.aspx?url=" + url + "&errmsg=验证码不正确，请重新输入！&username=" + username);



        #region IP访问限制选项
        //IP访问限制选项
        var AdminIpBlockType = Sys.BLL.SysConfig.GetInt("WebConfig", "AdminIpBlockType", 0);
        switch (AdminIpBlockType)
        {
            case 0:
                break;
            case 1:
                {
                    var bllIpBlock = new Sys.BLL.SysIpBlock();
                    if (bllIpBlock.Exists(0, 1, Utils.GetRealIP()))
                        Response.Redirect("login.aspx?url=" + url + "&errmsg=当前IP被限制登录系统后台！&username=");
                }
                break;
            case 2:
                {
                    var bllIpBlock = new Sys.BLL.SysIpBlock();
                    if (!bllIpBlock.Exists(0, 0, Utils.GetRealIP()))
                        Response.Redirect("login.aspx?url=" + url + "&errmsg=当前IP被限制登录系统后台！&username=");
                }
                break;
        }

        #endregion



        //帐户登陆
        var bllAccount = new Sys.BLL.Account();
        int UserID = bllAccount.CheckLogin(username, Utils.MD5(password));
        if (UserID == -2)
            Response.Redirect("login.aspx?url=" + url + "&errmsg=帐户不存在！&username=");
        else if (UserID == -1)
            Response.Redirect("login.aspx?url=" + url + "&errmsg=密码不正确！&username=" + username);

        //用户登录
        var bllMember = new Sys.BLL.Member();
        Sys.Model.Member modelMember = bllMember.GetModel(UserID);
        if (modelMember == null)
            Response.Redirect("login.aspx?url=" + url + "&errmsg=该账户无权登陆系统！&username=" + username);
        else if (modelMember.State == 0)
            Response.Redirect("login.aspx?url=" + url + "&errmsg=该账户禁止登陆系统！&username=" + username);



        modelMember.LoginTimes += 1;




        modelMember.LastLoginTime = modelMember.ThisLoginTime;

        modelMember.LastLoginIP = modelMember.ThisLoginIP;


        modelMember.ThisLoginTime = DateTime.Now;

        modelMember.ThisLoginIP = PageRequest.GetIP();



        bllMember.Update_AllInfo(modelMember);


        //管理账户登录
        var bllAdmin = new Sys.BLL.Admin();
        Sys.Model.Admin modelAdmin = bllAdmin.GetModel(UserID);
        if (modelAdmin == null)
            Response.Redirect("login.aspx?url=" + url + "&errmsg=该账户无权登陆系统后台！&username=" + username);
        else if (modelAdmin.State == 0)
            Response.Redirect("login.aspx?url=" + url + "&errmsg=该账户禁止登陆系统后台！&username=" + username);

        //成功响应

        //Cookie保存-帐户
        //518400年 43200月 1440天
        Cookie.WriteUserCookie(UserID, Utils.MD5(password), 518400);



        //添加日志
        ManageHelper.AddLog("managelogin", "登陆成功");

        //Cookie保存-帐户
        Sys.BLL.Admin.SetUsername(modelAdmin.Username);
        //Cookie保存-姓名
        Sys.BLL.Admin.SetRealName(modelAdmin.RealName);

        //Cookie保存-rememberme
        if (PageRequest.GetInt("rememberme", 0) == 1)
            Utils.WriteCookie("manage_LoginName", Utils.UrlEncode(username), 518400, ConfigHelper.GetConfigString("CookieDomain"));

        if (modelAdmin.Username == modelAdmin.RealName)
            Response.Redirect("main.aspx?url=" + Utils.UrlEncode("user/profile.aspx"));
        else if (url != "")
            Response.Redirect("main.aspx?url=" + Utils.UrlEncode(url));
        else
            Response.Redirect("main.aspx");

    }

    /// <summary>
    /// 获得登陆名字
    /// </summary>
    /// <returns></returns>
    public static string GetLoginName()
    {
        return Utils.UrlDecode(Utils.GetCookie("manage_LoginName"));
    }
}
