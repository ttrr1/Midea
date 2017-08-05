using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sys.BLL;
using Sys.Common;

public partial class data_Ajax_Table : System.Web.UI.Page
{



    protected void Page_Load(object sender, EventArgs e)
    {

        var methodName = PageRequest.GetString("method");
        var type = this.GetType();
        var method = type.GetMethod(methodName);
        if (method == null) throw new Exception("method is null");

        method.Invoke(this, null);
    }
    /// <summary>
    /// 查询具体的信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public void GetView()
    {

        var tableName = PageRequest.GetString("tableName");

        var bll = new Common();
        var dt = bll.DatabaseTable(tableName).Tables[0];
     
        //var data = MyDBUtils.DataTable2ArrayList(dt);
        //var user = data.Count > 0 ? (Hashtable)data[0] : null;
        var json = PluSoft.Utils.JSON.Encode(dt);
        Response.Write(json);
    }




    public void SearchData()
    {
        var bll = new Common();
        var dt = bll.DatabaseTableNew().Tables[0];
        //JSON
        var json = PluSoft.Utils.JSON.Encode(dt);
        Response.Write(json);
    }

}