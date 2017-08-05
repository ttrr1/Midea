using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using Sys.Common;

/// <summary>
/// ManageHelper 的摘要说明
/// </summary>
public class ManageHelper
{
    public ManageHelper()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 获得管理端风格
    /// </summary>
    /// <returns></returns>
    public static string GetTheme()
    {
        string theme = Utils.GetCookie("manage_Theme");
        if (theme == "")
        {
            var bllConfig = new Sys.BLL.SysConfig();
            List<Sys.Model.SysConfig> listTheme = bllConfig.GetListByCache("ManageTheme", 1);
            if (listTheme.Count == 0)
                theme = "blue";
            else
                theme = listTheme[0].Value;
            //Cookie保存
            //518400年 43200月 1440天
            Utils.WriteCookie("manage_Theme", theme, 518400, ConfigHelper.GetConfigString("CookieDomain"));
        }
        return theme;
    }

    /// <summary>
    /// 设置管理端风格
    /// </summary>
    /// <param name="newTheme">新风格</param>
    public static void SetTheme(string newTheme)
    {
        //Cookie保存
        //518400年 43200月 1440天
        Utils.WriteCookie("manage_Theme", newTheme, 518400, ConfigHelper.GetConfigString("CookieDomain"));
    }

    /// <summary>
    /// 页面act请求参数值
    /// </summary>
    /// <returns></returns>
    public static string PageAct()
    {
        return PageRequest.GetString("act").ToLower().Trim();
    }

    /// <summary>
    /// 页面sucmsg请求参数值
    /// </summary>
    /// <returns></returns>
    public static string PageSucMsg()
    {
        return PageRequest.GetString("sucmsg").Trim();
    }

    /// <summary>
    /// 页面errmsg请求参数值
    /// </summary>
    /// <returns></returns>
    public static string PageErrMsg()
    {
        return PageRequest.GetString("errmsg").Trim();
    }

    /// <summary>
    /// 邮件发送
    /// </summary>
    /// <param name="resEmail">接收人邮件地址</param>
    /// <param name="Subject">主题</param>
    /// <param name="Body">内容</param>
    /// <returns></returns>
    public static string MailSend(string resEmail, string Subject, string Body)
    {
        return Sys.Common.MailSend.Single(resEmail, Subject, Body,
            WebConfig.GetString("EmailServer"),
            WebConfig.GetString("EmailAccount"),
            WebConfig.GetString("EmailPassword"),
            WebConfig.GetString("EmailSender"),
            WebConfig.GetInt("EmailPort", 25));

      //  return "";
    }

    /// <summary>
    /// 邮件发送(用于分类信息)
    /// </summary>
    /// <param name="resEmail">接收人邮件地址</param>
    /// <param name="Subject">主题</param>
    /// <param name="Body">内容</param>
    /// <param name="EmailSender">邮件发送者</param>
    /// <returns></returns>
    public static string MailSend4Info(string resEmail, string Subject, string Body, string EmailSender)
    {
        //return Sys.Common.MailSend.Single(resEmail, Subject, Body,
        //    WebConfig.GetString("EmailServer"),
        //    WebConfig.GetString("EmailAccount"),
        //    WebConfig.GetString("EmailPassword"),
        //    EmailSender,
        //    WebConfig.GetInt("EmailPort", 25));

        return "";
    }

    /// <summary>
    /// 添加系统日志
    /// </summary>
    /// <param name="Flag">模块</param>
    /// <param name="Log">日志</param>
    public static void AddLog(string Flag, string Log)
    {
        Sys.BLL.AdminLog.Add(Sys.BLL.Account.GetLoginAdminID(), Flag, Log);
    }

    /// <summary>
    /// 添加系统日志
    /// </summary>
    /// <param name="Flag">模块</param>
    /// <param name="Log">日志</param>
    public static void AddLog(string Flag, string Log,bool isUser)
    {
        //Sys.BLL.AdminLog.Add(Sys.BLL.Account.GetLoginUserIdNew(), Flag, Log);
    }



    /// <summary>
    /// 分页显示
    /// </summary>
    /// <param name="url"></param>
    /// <param name="PageIndex"></param>
    /// <param name="PageSize"></param>
    /// <param name="PageCS"></param>
    /// <returns></returns>
    public static string showSplitPage(string url, int PageIndex, int PageSize, int PageCS)
    {
        string str = "";
        int PageCounts = 0;//总页数

        if (PageCS % PageSize == 0)
            PageCounts = PageCS / PageSize;
        else
            PageCounts = PageCS / PageSize + 1;


        if (PageCounts <= 1)
        {
            return str;
        }

        int start = (PageIndex - 4) > 0 ? (PageIndex - 4) : 1;
        int end = (PageIndex + 4) < PageCounts ? (PageIndex + 4) : PageCounts;
        if ((PageIndex - 1) < 4)
        {
            start = 1;
            end = (start + 8) < PageCounts ? (start + 8) : PageCounts;
        }
        if ((PageCounts - PageIndex) < 4)
        {
            end = PageCounts;
            start = (end - 9) > 0 ? (end - 9) : 1;
        }
        str += "<div class='pagesplit'><ul>";
        if (PageIndex != 1)
            str += @"<li class=""pg_first""><a href='" + url.Replace("{0}", "1") + @"'><img align='absmiddle' src='/Manage/Pic/Icons/arrow/arrow_first.gif'>&nbsp;首页</a>&nbsp;</li><li class=""pg_prve""><a href='" + url.Replace("{0}", (PageIndex - 1).ToString()) + "'><img align='absmiddle' src='/Manage/Pic/Icons/arrow/arrow_prev.gif'>&nbsp;上一页</a>&nbsp;</li>";
        for (int i = start; i <= end; i++)
        {
            if (i == PageIndex)
                str += @"<li class=""curr-page"">" + i.ToString() + "</li>";
            else
                str += @"<li><a href='" + url.Replace("{0}", i.ToString()) + "'>" + i.ToString() + "</a></li>";
        }
        if (PageIndex < PageCounts)
        {
            str += @"<li class=""pg_next"">&nbsp;<a href='" + url.Replace("{0}", (PageIndex + 1).ToString()) + "'><img align='absmiddle' src='/Manage/Pic/Icons/arrow/arrow_next.gif'>&nbsp;下一页</a></li>";
            str += @"<li class=""pg_next"">&nbsp;<a href='" + url.Replace("{0}", (PageCounts).ToString()) + "'><img align='absmiddle' src='/Manage/Pic/Icons/arrow/arrow_last.gif'>&nbsp;末页</a></li>";
        }
        str += string.Format(
            "</ul><div style='float: right; margin-right:30px'>当前页：<select name='pageselector' id='pageselector'  onchange=\"window.location.href=this.value\" >");
        for (int i = 1; i < PageCounts + 1; i++)
        {
            if (i >= PageIndex - 50 && i <= PageIndex + 50)
            {
                if (PageIndex == i)
                {
                    str += string.Format("<option selected='selected' value='{0}'>{1} / {2}</option>", url.Replace("{0}", (i).ToString()), i, PageCounts);
                }
                else
                {
                    str += string.Format("<option value='{0}'>{1} / {2}</option>", url.Replace("{0}", (i).ToString()), i, PageCounts);
                }
            }
        }
        str += "</select></div></div>";
        return str;
    }



    /// <summary>
    /// 检测管理员登陆，如果失败并做相应
    /// </summary>
    /// <param name="frmName">main：主操作区，menu：左边导航，top：上面导航，frame：框架</param>
    public static void CheckAdminLogin(string frmName)
    {
        if (Sys.BLL.Account.GetLoginAdminID() < 1)
        {
            frmName = frmName.ToLower();

            string js = "";
            if (frmName == "frame")
                js = "alert('登陆已超时，请重新登录。');window.top.location.href = '/manage/login.aspx?url=" + Utils.UrlEncode(PageRequest.GetQueryString()) + "';";
            else if (frmName == "main")
                js = "alert('登陆已超时，请重新登录。');window.top.location.href = '/manage/login.aspx?url=" + Utils.UrlEncode(PageRequest.GetRawUrl()) + "';";
            else
                js = @"document.write('<input type=""button"" value=""登陆已超时，请重新登录。"" onclick=""window.top.location.href =\'/manage/login.aspx\'"" />');";

            HttpContext.Current.Response.Write("<html><head><script language='javascript' type='text/javascript'>" + js + "</script></head></html>");
            HttpContext.Current.Response.End();
        }
    }

    /// <summary>
    /// 检测管理员登陆，如果失败并做相应（默认：main，主操作区）
    /// </summary>
    public static void CheckAdminLogin()
    {
        CheckAdminLogin("main");
    }

    /// <summary>
    /// 检测管理员是否有当前模块权限，如果失败并做相应
    /// </summary>
    /// <param name="Flag">模块名称</param>
    public static void CheckAdminPower(string Flag)
    {
        if (!CheckAdminHavePower(Flag.ToLower()))
        {
            HttpContext.Current.Response.Write("你没有权限");
            HttpContext.Current.Response.End();
        }
    }

    /// <summary>
    /// 检测管理员是否有当前模块权限
    /// </summary>
    /// <param name="Flag"></param>
    /// <returns></returns>
    public static bool CheckAdminHavePower(string Flag)
    {
        bool flag = false;
         int userID = Sys.BLL.Account.GetLoginAdminID();
        var bll = new Sys.BLL.Admin();
        Sys.Model.Admin model = bll.GetModel(userID);
        model.UserFlag = "," + model.UserFlag + ",";

        //Flag = Flag.Substring(Flag.IndexOf("_") + 1, Flag.Length - Flag.IndexOf("_") - 1);

        //if (model.UserFlag.ToLower().IndexOf("_" + Flag + ",") != -1)
        //{
        //    flag = true;
        //}

        if (model.UserFlag.ToLower().IndexOf(Flag) != -1)//ZJY
        {
            flag = true;
        }
        return flag;
    }

    /// <summary>
    /// 检测管理员是否有当前模块权限
    /// </summary>
    /// <param name="Flag"></param>
    /// <returns></returns>
    public static bool CheckAdminHavePower1(string FlagAct)
    {
        bool flag = false;
        //admin
        int userID = Sys.BLL.Account.GetLoginAdminID();
        Sys.BLL.Admin bllAdmin = new Sys.BLL.Admin();
        Sys.Model.Admin modelAdmin = bllAdmin.GetModel(userID);
        //adminFlag
        Sys.BLL.AdminFlag bllFlag = new Sys.BLL.AdminFlag();
        List<Sys.Model.AdminFlag> list = bllFlag.GetList(0, -1, -1, FlagAct);
        foreach (Sys.Model.AdminFlag model in list)
        {
            modelAdmin.UserFlag = "," + modelAdmin.UserFlag + ",";
            if (modelAdmin.UserFlag.IndexOf("," + model.Flag + ",") != -1)
            {
                flag = true;
            }
        }
        return flag;
    }




}
