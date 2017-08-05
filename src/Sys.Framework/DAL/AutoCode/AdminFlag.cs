using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;//工具类
namespace Sys.DAL
{
    /// <summary>
    /// 数据访问类AdminFlag。
    /// </summary>
    public class AdminFlag
    {
        public AdminFlag()
        { }
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            int result = DbHelperSQL.RunProcedure("AdminFlag_Exists", parameters, out rowsAffected);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(Sys.Model.AdminFlag model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@IsNav", SqlDbType.Int,4),
					new SqlParameter("@HaveChildNav", SqlDbType.Bit,1),
					new SqlParameter("@Flag", SqlDbType.VarChar,50),
					new SqlParameter("@FlagName", SqlDbType.NVarChar,50),
					new SqlParameter("@FlagAction", SqlDbType.VarChar,50),
					new SqlParameter("@FlagGroup", SqlDbType.Int,4),
					new SqlParameter("@FlagType", SqlDbType.Int,4),
					new SqlParameter("@AppUrl", SqlDbType.VarChar,255),
					new SqlParameter("@OrderID", SqlDbType.Int,4),
					new SqlParameter("@Icon", SqlDbType.VarChar,50),
					new SqlParameter("@IsOpen", SqlDbType.Bit,1)};
            parameters[0].Value = model.ParentID;
            parameters[1].Value = model.IsNav;
            parameters[2].Value = model.HaveChildNav;
            parameters[3].Value = model.Flag;
            parameters[4].Value = model.FlagName;
            parameters[5].Value = model.FlagAction;
            parameters[6].Value = model.FlagGroup;
            parameters[7].Value = model.FlagType;
            parameters[8].Value = model.AppUrl;
            parameters[9].Value = model.OrderID;
            parameters[10].Value = model.Icon;
            parameters[11].Value = model.IsOpen;

            DbHelperSQL.RunProcedure("AdminFlag_ADD", parameters, out rowsAffected);
            return (int)parameters[0].Value;
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public int Update(Sys.Model.AdminFlag model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@IsNav", SqlDbType.Int,4),
					new SqlParameter("@HaveChildNav", SqlDbType.Bit,1),
					new SqlParameter("@Flag", SqlDbType.VarChar,50),
					new SqlParameter("@FlagName", SqlDbType.NVarChar,50),
					new SqlParameter("@FlagAction", SqlDbType.VarChar,50),
					new SqlParameter("@FlagGroup", SqlDbType.Int,4),
					new SqlParameter("@FlagType", SqlDbType.Int,4),
					new SqlParameter("@AppUrl", SqlDbType.VarChar,255),
					new SqlParameter("@OrderID", SqlDbType.Int,4),
					new SqlParameter("@Icon", SqlDbType.VarChar,50),
					new SqlParameter("@IsOpen", SqlDbType.Bit,1)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.ParentID;
            parameters[2].Value = model.IsNav;
            parameters[3].Value = model.HaveChildNav;
            parameters[4].Value = model.Flag;
            parameters[5].Value = model.FlagName;
            parameters[6].Value = model.FlagAction;
            parameters[7].Value = model.FlagGroup;
            parameters[8].Value = model.FlagType;
            parameters[9].Value = model.AppUrl;
            parameters[10].Value = model.OrderID;
            parameters[11].Value = model.Icon;
            parameters[12].Value = model.IsOpen;

            DbHelperSQL.RunProcedure("AdminFlag_Update", parameters, out rowsAffected);
            return model.ID;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int Delete(int ID)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DbHelperSQL.RunProcedure("AdminFlag_Delete", parameters, out rowsAffected);

            return ID;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.AdminFlag GetModel(int ID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            Sys.Model.AdminFlag model = new Sys.Model.AdminFlag();
            DataSet ds = DbHelperSQL.RunProcedure("AdminFlag_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParentID"].ToString() != "")
                {
                    model.ParentID = int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsNav"].ToString() != "")
                {
                    model.IsNav = int.Parse(ds.Tables[0].Rows[0]["IsNav"].ToString());
                }
                if (ds.Tables[0].Rows[0]["HaveChildNav"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["HaveChildNav"].ToString() == "1") || (ds.Tables[0].Rows[0]["HaveChildNav"].ToString().ToLower() == "true"))
                    {
                        model.HaveChildNav = true;
                    }
                    else
                    {
                        model.HaveChildNav = false;
                    }
                }
                model.Flag = ds.Tables[0].Rows[0]["Flag"].ToString();
                model.FlagName = ds.Tables[0].Rows[0]["FlagName"].ToString();
                model.FlagAction = ds.Tables[0].Rows[0]["FlagAction"].ToString();
                if (ds.Tables[0].Rows[0]["FlagGroup"].ToString() != "")
                {
                    model.FlagGroup = int.Parse(ds.Tables[0].Rows[0]["FlagGroup"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FlagType"].ToString() != "")
                {
                    model.FlagType = int.Parse(ds.Tables[0].Rows[0]["FlagType"].ToString());
                }
                model.AppUrl = ds.Tables[0].Rows[0]["AppUrl"].ToString();
                if (ds.Tables[0].Rows[0]["OrderID"].ToString() != "")
                {
                    model.OrderID = int.Parse(ds.Tables[0].Rows[0]["OrderID"].ToString());
                }
                model.Icon = ds.Tables[0].Rows[0]["Icon"].ToString();
                if (ds.Tables[0].Rows[0]["IsOpen"].ToString() != "")
                {
                    if ((ds.Tables[0].Rows[0]["IsOpen"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsOpen"].ToString().ToLower() == "true"))
                    {
                        model.IsOpen = true;
                    }
                    else
                    {
                        model.IsOpen = false;
                    }
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }



        #endregion  成员方法


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetTableModel(int FlagId)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
						};
            parameters[0].Value = FlagId;


            var ds = DbHelperSQL.RunProcedure("AdminFlag_GetModel", parameters, "ds");
            return ds.Tables[0];
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string FlagIdlist)
        {
            var strSql = new StringBuilder();
            strSql.Append("delete from AdminFlag ");
            strSql.Append(" where FlagId in (" + FlagIdlist + ")  ");
            var rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows > 0;
        }
    }
}

