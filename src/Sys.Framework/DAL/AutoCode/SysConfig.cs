using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;//工具类
namespace Sys.DAL
{
    /// <summary>
    /// 数据访问类SysConfig。
    /// </summary>
    public class SysConfig
    {
        public SysConfig()
        { }
        #region  成员方法
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="Key">键名</param>
        /// <returns></returns>
        public Sys.Model.SysConfig GetModel(string Item, string Key)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@Item", SqlDbType.VarChar,50),
					new SqlParameter("@Key", SqlDbType.VarChar,50)};
            parameters[0].Value = Item;
            parameters[1].Value = Key;

            Sys.Model.SysConfig model = new Sys.Model.SysConfig();
            DataSet ds = DbHelperSQL.RunProcedure("SysConfig_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                model.Item = ds.Tables[0].Rows[0]["Item"].ToString();
                model.Key = ds.Tables[0].Rows[0]["Key"].ToString();
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                model.Value = ds.Tables[0].Rows[0]["Value"].ToString();
                if (ds.Tables[0].Rows[0]["OrderID"].ToString() != "")
                {
                    model.OrderID = int.Parse(ds.Tables[0].Rows[0]["OrderID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsUsing"].ToString() != "")
                {
                    model.IsUsing = int.Parse(ds.Tables[0].Rows[0]["IsUsing"].ToString());
                }
                model.Note = ds.Tables[0].Rows[0]["Note"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }


        
        /// <summary>
        /// 数据列表
        /// </summary>
        /// <param name="Item">项目类别</param>
        /// <param name="IsUsing">是否启用 0不启用 1启用 -1全部</param>
        /// <returns></returns>
        public DataSet GetList(string Item,int IsUsing)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@Item", SqlDbType.VarChar,50),
                    new SqlParameter("@IsUsing", SqlDbType.Int,4),
                    };
            parameters[0].Value = Item;
            parameters[1].Value = IsUsing;
            return DbHelperSQL.RunProcedure("SysConfig_GetList", parameters, "ds");
        }


        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(Sys.Model.SysConfig model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@Item", SqlDbType.VarChar,50),
					new SqlParameter("@Key", SqlDbType.VarChar,50),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@Value", SqlDbType.NText),
					new SqlParameter("@OrderID", SqlDbType.Int,4),
					new SqlParameter("@IsUsing", SqlDbType.Int,4),
					new SqlParameter("@Note", SqlDbType.NVarChar,255)};
            parameters[0].Value = model.Item;
            parameters[1].Value = model.Key;
            parameters[2].Value = model.Name;
            parameters[3].Value = model.Value;
            parameters[4].Value = model.OrderID;
            parameters[5].Value = model.IsUsing;
            parameters[6].Value = model.Note;

            DbHelperSQL.RunProcedure("SysConfig_Update", parameters, out rowsAffected);
        }

        #endregion  成员方法
    }
}

