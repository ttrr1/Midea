using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;
using Sys.Common;

public partial class Manage_System_ToolCache : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        ManageHelper.CheckAdminLogin();
        ManageHelper.CheckAdminPower("system_toolcache");
        if (ManageHelper.PageAct() == "clear")
        {
            foreach (var entry in base.Cache.Cast<DictionaryEntry>())
            {
                DataCache.RemoveCache(entry.Key.ToString());
            }
            Response.Write("yes");
            Response.End();
        }
    }


}
