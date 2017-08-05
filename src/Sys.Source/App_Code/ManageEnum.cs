using System;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// AdminFlag的FlagType
/// </summary>
public class ManageEnum
{
    public enum AdminFlagType
    {
        [Description("系统通用管理")]
        SystemFlag = 0,

        [Description("站点管理")]
        Site = 1,

        [Description("内容管理")]
        Content = 2,
    }

    public enum CmsFieldType
    {
        [Description("单行文本")]
        单行文本 = 1,

        [Description("多行文本")]
        多行文本 = 2,

        [Description("多行文本(HTML)")]
        多行文本HTML = 3,

        [Description("单选按钮")]
        单选按钮 = 4,

        [Description("下拉列表")]
        下拉列表 = 7,

        [Description("数字型")]
        数字型 = 8,

        [Description("日期和时间")]
        日期和时间 = 9,

        [Description("图片")]
        图片 = 10,

        [Description("附加字段属性")]
        附加字段属性 = 11,
    }

}
