using System;
using System.Collections;

using Sys.BLL;
using Sys.Common;

public partial class data_Ajax_message : System.Web.UI.Page
{
    protected News BllNews = new News();
    Sys.Model.News model = new Sys.Model.News();
    protected void Page_Load(object sender, EventArgs e)
    {
        var methodName = PageRequest.GetString("method");
        var type = this.GetType();
        var method = type.GetMethod(methodName);
        if (method == null) throw new Exception("method is null");
        method.Invoke(this, null);
    }

    //=============================================================

    /// <summary>
    /// 查询具体的信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public void GetView()
    {

        var id = PageRequest.GetInt("ID", 0);
        var dt = BllNews.GetTableModel(id);
        var data = MyDBUtils.DataTable2ArrayList(dt);
        var user = data.Count > 0 ? (Hashtable)data[0] : null;
        var json = PluSoft.Utils.JSON.Encode(user);
        Response.Write(json);
    }

    /// <summary>
    /// 删除操作
    /// </summary>
    public void Remove()
    {
        var idStr = Request["ID"];
        BllNews.Delete(Utils.StrToInt(Request["ID"], 0));

    }



    /// <summary>
    /// 更新信息
    /// </summary>
    public void SaveData()
    {


        var json = Request["data"];
        var rows = (ArrayList)PluSoft.Utils.JSON.Decode(json);

        foreach (Hashtable row in rows)
        {
            var id = row["ID"] != null ? row["ID"].ToString() : "";

            //根据记录状态，进行不同的增加、删除、修改操作
            var state = row["_state"] != null ? row["_state"].ToString() : "";
            if (state == "added" || id == "")           //新增：id为空，或_state为added
            {

                model.Abstract = row["Abstract"] == null ? "" : row["Abstract"].ToString();
                model.Content = PageRequest.GetString("Content");
                model.CreateIp = PageRequest.GetIP();
                model.CreateTime = DateTime.Now;
                model.NewsTitle = row["NewsTitle"] == null ? "" : row["NewsTitle"].ToString();
                model.Publisher = Admin.GetRealName();
                model.Source = row["Source"] == null ? "" : row["Source"].ToString();
                model.TotalClick = 0;
                model.TypeId = Utils.StrToInt(row["TypeId"], 0);
                model.UpdateIp = model.CreateIp;
                model.UpdateTime = model.CreateTime;

                BllNews.Add(model);

            }
            else if (state == "modified" || state == "") //更新：_state为空或modified
            {
                model = BllNews.GetModel(Utils.StrToInt(row["ID"], 0));
                if (model != null)
                {

                    #region 基础字段
                    model.Abstract = row["Abstract"] == null ? "" : row["Abstract"].ToString();
                    model.Content = PageRequest.GetString("Content");
                    model.NewsTitle = row["NewsTitle"] == null ? "" : row["NewsTitle"].ToString();
                    model.Publisher = row["Publisher"] == null ? "" : row["Publisher"].ToString();
                    model.Source = row["Source"] == null ? "" : row["Source"].ToString();

                    model.TypeId = Utils.StrToInt(row["TypeId"], 0);
                    model.UpdateIp = PageRequest.GetIP();
                    model.UpdateTime = DateTime.Now;
                    #endregion

                    BllNews.Update(model);
                }
            }

        }

    }



    /// <summary>
    /// 查询
    /// </summary>
    /// 
    public void SearchData()
    {
        //查询条件
        var key = Utils.SqlStringFormat(PageRequest.GetString("key"), 2);
        var begtime = Utils.SqlStringFormat(PageRequest.GetString("begtime"), 2);

        //分页
        var pageIndex = PageRequest.GetInt("pageIndex", 1);
        var pageSize = PageRequest.GetInt("pageSize", 1);
        //字段排序
        var sortField = PageRequest.GetString("sortField");
        var sortOrder = PageRequest.GetString("sortOrder");
        var order = "";
        if (String.IsNullOrEmpty(sortField) == false)
        {
            if (sortOrder != "desc") sortOrder = "asc";
            order = " " + sortField + " " + sortOrder;
        }
        else
        {
            order += " createtime desc";
        }
        var strWhere = "1=1";
        if (key != "")
        {
            strWhere += " and charIndex('" + key + "',NewsTitle) > 0";
        }


        if (begtime.Length > 0)
        {


            strWhere += " and DATEDIFF(day,'" + DateTime.Parse(begtime) + "',createtime)=0";

        }
        var dt = BllNews.GetTable(pageSize, pageIndex, strWhere, order);
        var dataAll = MyDBUtils.DataTable2ArrayList(dt);
        var result = new Hashtable();
        result["data"] = dataAll;

        var total = new Common().GetCount("News", strWhere);
        result["total"] = total;
        var json = PluSoft.Utils.JSON.Encode(dataAll);

        Response.Write(json);

    }
}