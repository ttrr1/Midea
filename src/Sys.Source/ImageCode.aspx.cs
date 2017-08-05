using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

public partial class ImageCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Drawer.DrawImage(70, 25, 2, 1, 4, 0, 16, Drawer.SetOut.Number, "ValidateCode");
    }
}