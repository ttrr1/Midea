using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Sys.Common;

/// <summary>
/// WebConfig 的摘要说明
/// </summary>
public class WebConfig
{
    public WebConfig()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    /// <summary>
    /// 得到一个string数值，从缓存中。
    /// </summary>
    /// <param name="Key">键名</param>
    /// <returns></returns>
    public static string GetString(string Key)
    {
        return Sys.BLL.SysConfig.GetStringByCache("WebConfig", Key);
    }

    /// <summary>
    /// 得到一个string数值，从缓存中。
    /// </summary>
    /// <param name="Item">项目类别</param>
    /// <param name="Key">键名</param>
    /// <returns></returns>
    public static string GetString(string Item, string Key)
    {
        return Sys.BLL.SysConfig.GetStringByCache(Item, Key);
    }

    /// <summary>
    /// 得到一个int数值，从缓存中。
    /// </summary>
    /// <param name="Key">键名</param>
    /// <param name="defValue">缺省值</param>
    /// <returns></returns>
    public static int GetInt(string Key, int defValue)
    {
        return Sys.BLL.SysConfig.GetIntByCache("WebConfig", Key, defValue);
    }

    /// <summary>
    /// 得到一个int数值，从缓存中。
    /// </summary>
    /// <param name="Item">项目类别</param>
    /// <param name="Key">键名</param>
    /// <param name="defValue">缺省值</param>
    /// <returns></returns>
    public static int GetInt(string Item, string Key, int defValue)
    {
        return Sys.BLL.SysConfig.GetIntByCache(Item, Key, defValue);
    }

    /// <summary>
    /// 得到名字，从缓存中。
    /// </summary>
    /// <param name="Key">键名</param>
    /// <returns></returns>
    public static string GetName(string Key)
    {
        return Sys.BLL.SysConfig.GetNameByCache("WebConfig", Key);
    }

    /// <summary>
    /// 得到名字，从缓存中。
    /// </summary>
    /// <param name="Item">项目类别</param>
    /// <param name="Key">键名</param>
    /// <returns></returns>
    public static string GetName(string Item, string Key)
    {
        return Sys.BLL.SysConfig.GetNameByCache(Item, Key);
    }

    /// <summary>
    /// 设置键值，并更新缓存
    /// </summary>
    /// <param name="Key"></param>
    /// <param name="Value"></param>
    public static void SetString(string Key, string Value)
    {
        var bll = new Sys.BLL.SysConfig();
        var model = new Sys.Model.SysConfig();

        model = bll.GetModel("WebConfig", Key);
        model.Value = Value;
        bll.Update(model);
        DataCache.RemoveCache("SysConfigValue-WebConfig-" + Key);
    }
}
