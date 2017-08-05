using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;
namespace Sys.DAL
{
    /// <summary>
    /// 数据访问类AdminLog。
    /// </summary>
    public class AdminLog
    {
        public AdminLog()
        { }
        #region  成员方法
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(Sys.Model.AdminLog model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Flag", SqlDbType.VarChar,50),
					new SqlParameter("@Log", SqlDbType.NVarChar,450),
					new SqlParameter("@CreateIP", SqlDbType.VarChar,15)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.Flag;
            parameters[3].Value = model.Log;
            parameters[4].Value = model.CreateIP;

            DbHelperSQL.RunProcedure("AdminLog_ADD", parameters, out rowsAffected);
            return (int)parameters[0].Value;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.AdminLog GetModel(int ID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            Sys.Model.AdminLog model = new Sys.Model.AdminLog();
            DataSet ds = DbHelperSQL.RunProcedure("AdminLog_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                model.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                model.Flag = ds.Tables[0].Rows[0]["Flag"].ToString();
                model.Log = ds.Tables[0].Rows[0]["Log"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.CreateIP = ds.Tables[0].Rows[0]["CreateIP"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }



        #endregion  成员方法
    }
}

