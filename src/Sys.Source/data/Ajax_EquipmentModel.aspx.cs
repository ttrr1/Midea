using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Midea.Common;
using Sys.BLL;
using Sys.Common;

public partial class data_Ajax_EquipmentModel : System.Web.UI.Page
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
    /// 更新信息
    /// </summary>
    public void SaveData()
    {
        var json = Request["data"];
        UtilLog.WriteTextLog("Eqp Model Add", json);
        var rows = (ArrayList)PluSoft.Utils.JSON.Decode(json);
        var bllEquipmentModel = new EquipmentModel();
        Sys.Model.EquipmentModel model;
        foreach (Hashtable row in rows)
        {
            var id = row["ModelId"] != null ? row["ModelId"].ToString() : "";
            //根据记录状态，进行不同的增加、删除、修改操作
            var state = row["_state"] != null ? row["_state"].ToString() : "";
            if (state == "added" || id == "")           //新增：id为空，或_state为added
            {
                model = new Sys.Model.EquipmentModel();
                model.ModelName = row["ModelName"].ToString();
                if (!string.IsNullOrEmpty(row["EqpType"].ToString()))
                {
                    model.EqpType = Convert.ToInt32(row["EqpType"]);
                }
                model.ParentModelId = Utils.StrToInt(row["ParentModelId"], 0);
                model.Status = "1";
                model.CreateDate = DateTime.Now;

                bllEquipmentModel.Add(model);
            }
            else if (state == "modified" || state == "") //更新：_state为空或modified
            {
                model = bllEquipmentModel.GetModel(Utils.StrToInt(row["ModelId"], 0));
                if (model != null)
                {
                    model.ModelName = row["ModelName"].ToString();
                    if (!string.IsNullOrEmpty(row["EqpType"].ToString()))
                    {
                        model.EqpType = Convert.ToInt32(row["EqpType"]);
                    }
                    else
                    {
                        model.EqpType = null;
                    }
                    model.ParentModelId = Utils.StrToInt(row["ParentModelId"], 0);
                    model.ModifyDate = DateTime.Now;
                    bllEquipmentModel.Update(model);
                }

            }
        }

    }

    /// <summary>
    /// 删除
    /// </summary>
    public void DelFlag()
    {
        var flagId = PageRequest.GetInt("FlagId", 0);
        if (flagId > 0)
        {
            var dt = new Sys.BLL.EquipmentModel().GetList("ParentModelId=" + flagId).Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                Response.Write("0");
            }
            else
            {
                var result = new Sys.BLL.EquipmentModel().Delete(flagId);
                if (result)
                {
                    Response.Write("1");
                }
            }
        }
    }

    /// <summary>
    /// 查询
    /// </summary>
    public void SearchData()
    {
        //查询条件
        var key = Utils.SqlStringFormat(PageRequest.GetString("key"), 2);
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
            order += " ModelId desc";
        }

        var strWhere = "";

        if (pageSize == 1)
        {
            pageSize = 10000;
        }
        var dt = new Sys.BLL.EquipmentModel().GetList(pageSize, pageIndex, strWhere, order);
        var json = Midea.Common.JsonHelper.ObjectToJson(dt);

        Response.Write(json);
        Response.End();

    }

    /// <summary>
    /// 查询具体的信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public void GetView()
    {
        var id = PageRequest.GetInt("FlagId", 0);
        var dt = new Sys.BLL.EquipmentModel().GetEqpModelInfo("e1.ModelId=" + id);
        var data = MyDBUtils.DataTable2ArrayList(dt);
        var user = data.Count > 0 ? (Hashtable)data[0] : null;
        var json = PluSoft.Utils.JSON.Encode(user);
        Response.Write(json);
        Response.End();
    }

}