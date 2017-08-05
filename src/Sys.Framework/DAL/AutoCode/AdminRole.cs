using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;//工具类
namespace Sys.DAL
{
    /// <summary>
    /// 数据访问类AdminRole。
    /// </summary>
    public class AdminRole
    {
        public AdminRole()
        { }
        #region  成员方法
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.AdminRole GetModel(int RoleID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.Int,4)};
            parameters[0].Value = RoleID;

            Sys.Model.AdminRole model = new Sys.Model.AdminRole();
            DataSet ds = DbHelperSQL.RunProcedure("AdminRole_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["RoleID"].ToString() != "")
                {
                    model.RoleID = int.Parse(ds.Tables[0].Rows[0]["RoleID"].ToString());
                }
                model.RoleName = ds.Tables[0].Rows[0]["RoleName"].ToString();
                model.RoleFlag = ds.Tables[0].Rows[0]["RoleFlag"].ToString();
                if (ds.Tables[0].Rows[0]["AdminNum"].ToString() != "")
                {
                    model.AdminNum = int.Parse(ds.Tables[0].Rows[0]["AdminNum"].ToString());
                }
                if (ds.Tables[0].Rows[0]["OrderID"].ToString() != "")
                {
                    model.OrderID = int.Parse(ds.Tables[0].Rows[0]["OrderID"].ToString());
                }
                model.Note = ds.Tables[0].Rows[0]["Note"].ToString();
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        public int Add(Sys.Model.AdminRole model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.Int,4),
					new SqlParameter("@RoleName", SqlDbType.NVarChar,20),
					new SqlParameter("@RoleFlag", SqlDbType.VarChar),
					new SqlParameter("@AdminNum", SqlDbType.Int,4),
					new SqlParameter("@Note", SqlDbType.NVarChar,255),
					new SqlParameter("@OrderID", SqlDbType.Int,4)};
            parameters[0].Value = model.RoleID;
            parameters[1].Value = model.RoleName;
            parameters[2].Value = model.RoleFlag;
            parameters[3].Value = model.AdminNum;
            parameters[4].Value = model.Note;
            parameters[5].Value = model.OrderID;
            return DbHelperSQL.RunProcedure("AdminRole_ADD", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public int Update(Sys.Model.AdminRole model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.Int,4),
					new SqlParameter("@RoleName", SqlDbType.NVarChar,20),
					new SqlParameter("@RoleFlag", SqlDbType.VarChar),
					new SqlParameter("@Note", SqlDbType.NVarChar,255),
					new SqlParameter("@OrderID", SqlDbType.Int,4)};
            parameters[0].Value = model.RoleID;
            parameters[1].Value = model.RoleName;
            parameters[2].Value = model.RoleFlag;
            parameters[3].Value = model.Note;
            parameters[4].Value = model.OrderID;

            return DbHelperSQL.RunProcedure("AdminRole_Update", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据：更新站点权限
        /// </summary>
        public int UpdateRoleFlagForSiteFlag(Sys.Model.AdminRole model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.Int,4),
					new SqlParameter("@RoleFlag", SqlDbType.VarChar)};
            parameters[0].Value = model.RoleID;
            parameters[1].Value = model.RoleFlag;
            return DbHelperSQL.RunProcedure("AdminRole_UpdateRoleFlagForSite", parameters, out rowsAffected);
        }
        
        public int GetMaxOrderID()
        {
            int rowsAffected;
            SqlParameter[] parameters = { };
            return DbHelperSQL.RunProcedure("AdminRole_GetMaxOrderID", parameters, out rowsAffected);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int RoleID)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.Int,4)};
            parameters[0].Value = RoleID;

            return DbHelperSQL.RunProcedure("AdminRole_Delete", parameters, out rowsAffected);
        }

        #endregion  成员方法


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetTableModel(int RoleId)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@RoleId", SqlDbType.Int,4)
						};
            parameters[0].Value = RoleId;

            var model = new Sys.Model.AdminRole();
            var ds = DbHelperSQL.RunProcedure("AdminRole_GetModel", parameters, "ds");
            return ds.Tables[0];
        }
    }
}

