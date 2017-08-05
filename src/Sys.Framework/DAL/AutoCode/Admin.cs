using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;//工具类
namespace Sys.DAL
{
    /// <summary>
    /// 数据访问类:Admin
    /// 作者：王星海
    /// 日期：2013/1/9 8:40:42
    /// </summary>
    public partial class Admin
    {
        public Admin()
        { }
        #region  工具生成常用底层数据访问方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("UserId", "Admin");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UserId)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4)						};
            parameters[0].Value = UserId;

            var result = DbHelperSQL.RunProcedure("Admin_Exists", parameters, out rowsAffected);
            return result == 1;
        }

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(Sys.Model.Admin model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@Username", SqlDbType.NVarChar,20),
					new SqlParameter("@UserFlag", SqlDbType.VarChar),
					new SqlParameter("@RoleIDs", SqlDbType.VarChar),
					new SqlParameter("@RoleNames", SqlDbType.NVarChar),
					new SqlParameter("@RoleFlags", SqlDbType.VarChar),
					new SqlParameter("@PlusFlag", SqlDbType.VarChar),
					new SqlParameter("@RealName", SqlDbType.NVarChar,20),
					new SqlParameter("@JobTitle", SqlDbType.NVarChar,50),
					new SqlParameter("@JobDept", SqlDbType.NVarChar,50),
					new SqlParameter("@QQ", SqlDbType.VarChar,50),
					new SqlParameter("@MSN", SqlDbType.VarChar,50),
					new SqlParameter("@Email", SqlDbType.VarChar,50),
					new SqlParameter("@Mobile", SqlDbType.VarChar,50),
					new SqlParameter("@HomeTel", SqlDbType.VarChar,50),
					new SqlParameter("@OfficeTel", SqlDbType.VarChar,50),
					new SqlParameter("@IsFounder", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@Guid", SqlDbType.VarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateIP", SqlDbType.VarChar,15),
					new SqlParameter("@ParentUserID", SqlDbType.Int,4),
					new SqlParameter("@ParentUserIDs", SqlDbType.VarChar),
					new SqlParameter("@IsPublic", SqlDbType.Int,4)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.Username;
            parameters[2].Value = model.UserFlag;
            parameters[3].Value = model.RoleIDs;
            parameters[4].Value = model.RoleNames;
            parameters[5].Value = model.RoleFlags;
            parameters[6].Value = model.PlusFlag;
            parameters[7].Value = model.RealName;
            parameters[8].Value = model.JobTitle;
            parameters[9].Value = model.JobDept;
            parameters[10].Value = model.QQ;
            parameters[11].Value = model.MSN;
            parameters[12].Value = model.Email;
            parameters[13].Value = model.Mobile;
            parameters[14].Value = model.HomeTel;
            parameters[15].Value = model.OfficeTel;
            parameters[16].Value = model.IsFounder;
            parameters[17].Value = model.State;
            parameters[18].Value = model.Guid;
            parameters[19].Value = model.CreateTime;
            parameters[20].Value = model.CreateIP;
            parameters[21].Value = model.ParentUserID;
            parameters[22].Value = model.ParentUserIDs;
            parameters[23].Value = model.IsPublic;

            return DbHelperSQL.RunProcedure("Admin_ADD", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public int Update(Sys.Model.Admin model, string password)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4),
					new SqlParameter("@Username", SqlDbType.NVarChar,20),
					new SqlParameter("@UserFlag", SqlDbType.VarChar),
					new SqlParameter("@RoleIDs", SqlDbType.VarChar),
					new SqlParameter("@RoleNames", SqlDbType.NVarChar),
					new SqlParameter("@RoleFlags", SqlDbType.VarChar),
					new SqlParameter("@PlusFlag", SqlDbType.VarChar),
					new SqlParameter("@RealName", SqlDbType.NVarChar,20),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@ParentUserID", SqlDbType.Int,4),
					new SqlParameter("@ParentUserIDs", SqlDbType.VarChar),
					new SqlParameter("@IsPublic", SqlDbType.Int,4),
                    new SqlParameter("@Password", SqlDbType.VarChar),
                    new SqlParameter("@OfficeTel", SqlDbType.VarChar) };
                                        
                                                



            parameters[0].Value = model.UserId;
            parameters[1].Value = model.Username;
            parameters[2].Value = model.UserFlag;
            parameters[3].Value = model.RoleIDs;
            parameters[4].Value = model.RoleNames;
            parameters[5].Value = model.RoleFlags;
            parameters[6].Value = model.PlusFlag;
            parameters[7].Value = model.RealName;
            parameters[8].Value = model.State;
            parameters[9].Value = model.ParentUserID;
            parameters[10].Value = model.ParentUserIDs;
            parameters[11].Value = model.IsPublic;
            parameters[12].Value = password;
            parameters[13].Value = model.OfficeTel;
            return DbHelperSQL.RunProcedure("Admin_UpdateNew", parameters, out rowsAffected);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int UserId)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4)						};
            parameters[0].Value = UserId;

            DbHelperSQL.RunProcedure("Admin_Delete", parameters, out rowsAffected);
            return rowsAffected > 0;
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string UserIdlist)
        {
            var strSql = new StringBuilder();
            strSql.Append("delete from Admin ");
            strSql.Append(" where UserId in (" + UserIdlist + ")  ");
            var rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows > 0;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.Admin GetModel(int UserId)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4)						};
            parameters[0].Value = UserId;

            var model = new Sys.Model.Admin();
            var ds = DbHelperSQL.RunProcedure("Admin_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserId"] != null && ds.Tables[0].Rows[0]["UserId"].ToString() != "")
                {
                    model.UserId = int.Parse(ds.Tables[0].Rows[0]["UserId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Username"] != null && ds.Tables[0].Rows[0]["Username"].ToString() != "")
                {
                    model.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                }
                if (ds.Tables[0].Rows[0]["UserFlag"] != null && ds.Tables[0].Rows[0]["UserFlag"].ToString() != "")
                {
                    model.UserFlag = ds.Tables[0].Rows[0]["UserFlag"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RoleIDs"] != null && ds.Tables[0].Rows[0]["RoleIDs"].ToString() != "")
                {
                    model.RoleIDs = ds.Tables[0].Rows[0]["RoleIDs"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RoleNames"] != null && ds.Tables[0].Rows[0]["RoleNames"].ToString() != "")
                {
                    model.RoleNames = ds.Tables[0].Rows[0]["RoleNames"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RoleFlags"] != null && ds.Tables[0].Rows[0]["RoleFlags"].ToString() != "")
                {
                    model.RoleFlags = ds.Tables[0].Rows[0]["RoleFlags"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PlusFlag"] != null && ds.Tables[0].Rows[0]["PlusFlag"].ToString() != "")
                {
                    model.PlusFlag = ds.Tables[0].Rows[0]["PlusFlag"].ToString();
                }
                if (ds.Tables[0].Rows[0]["RealName"] != null && ds.Tables[0].Rows[0]["RealName"].ToString() != "")
                {
                    model.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JobTitle"] != null && ds.Tables[0].Rows[0]["JobTitle"].ToString() != "")
                {
                    model.JobTitle = ds.Tables[0].Rows[0]["JobTitle"].ToString();
                }
                if (ds.Tables[0].Rows[0]["JobDept"] != null && ds.Tables[0].Rows[0]["JobDept"].ToString() != "")
                {
                    model.JobDept = ds.Tables[0].Rows[0]["JobDept"].ToString();
                }
                if (ds.Tables[0].Rows[0]["QQ"] != null && ds.Tables[0].Rows[0]["QQ"].ToString() != "")
                {
                    model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MSN"] != null && ds.Tables[0].Rows[0]["MSN"].ToString() != "")
                {
                    model.MSN = ds.Tables[0].Rows[0]["MSN"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Email"] != null && ds.Tables[0].Rows[0]["Email"].ToString() != "")
                {
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Mobile"] != null && ds.Tables[0].Rows[0]["Mobile"].ToString() != "")
                {
                    model.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HomeTel"] != null && ds.Tables[0].Rows[0]["HomeTel"].ToString() != "")
                {
                    model.HomeTel = ds.Tables[0].Rows[0]["HomeTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OfficeTel"] != null && ds.Tables[0].Rows[0]["OfficeTel"].ToString() != "")
                {
                    model.OfficeTel = ds.Tables[0].Rows[0]["OfficeTel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsFounder"] != null && ds.Tables[0].Rows[0]["IsFounder"].ToString() != "")
                {
                    model.IsFounder = int.Parse(ds.Tables[0].Rows[0]["IsFounder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"] != null && ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Guid"] != null && ds.Tables[0].Rows[0]["Guid"].ToString() != "")
                {
                    model.Guid = ds.Tables[0].Rows[0]["Guid"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateIP"] != null && ds.Tables[0].Rows[0]["CreateIP"].ToString() != "")
                {
                    model.CreateIP = ds.Tables[0].Rows[0]["CreateIP"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ParentUserID"] != null && ds.Tables[0].Rows[0]["ParentUserID"].ToString() != "")
                {
                    model.ParentUserID = int.Parse(ds.Tables[0].Rows[0]["ParentUserID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ParentUserIDs"] != null && ds.Tables[0].Rows[0]["ParentUserIDs"].ToString() != "")
                {
                    model.ParentUserIDs = ds.Tables[0].Rows[0]["ParentUserIDs"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IsPublic"] != null && ds.Tables[0].Rows[0]["IsPublic"].ToString() != "")
                {
                    model.IsPublic = int.Parse(ds.Tables[0].Rows[0]["IsPublic"].ToString());
                }



                return model;
            }
            return null;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public DataTable GetTableModel(int UserId)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@UserId", SqlDbType.Int,4)						};
            parameters[0].Value = UserId;

            var model = new Sys.Model.Admin();
            var ds = DbHelperSQL.RunProcedure("Admin_GetModel", parameters, "ds");
            return ds.Tables[0];
        }

        #endregion  Method





        /// <summary>
        /// 移动账号层次 
        /// </summary>
        /// <param name="model"></param>
        public int UpDateLeave(Model.Admin model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@ParentUserID", SqlDbType.Int),
					new SqlParameter("@ParentUserIDs", SqlDbType.VarChar)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.ParentUserID;
            parameters[2].Value = model.ParentUserIDs;
            return DbHelperSQL.RunProcedure("Admin_UpdateLeave", parameters, out rowsAffected);
        }





        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(Sys.Model.Admin model, string password)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@Username", SqlDbType.NVarChar,20),
					new SqlParameter("@RoleIDs", SqlDbType.VarChar),
					new SqlParameter("@Password", SqlDbType.VarChar),
					new SqlParameter("@CreateIP", SqlDbType.VarChar),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@RoleFlags", SqlDbType.VarChar),
					new SqlParameter("@RoleNames", SqlDbType.VarChar),
					new SqlParameter("@PlusFlag", SqlDbType.VarChar),
					new SqlParameter("@RealName", SqlDbType.VarChar),
					new SqlParameter("@UserFlag", SqlDbType.VarChar),
                    new SqlParameter("ParentUserID",SqlDbType.Int),
                    new SqlParameter("ParentUserIDs",SqlDbType.VarChar),
                    new SqlParameter("IsPublic",SqlDbType.Int)

                 
                  
            };
            parameters[0].Value = model.Username;
            parameters[1].Value = model.RoleIDs;
            parameters[2].Value = password;
            parameters[3].Value = model.CreateIP;
            parameters[4].Value = model.State;
            parameters[5].Value = model.RoleFlags;
            parameters[6].Value = model.RoleNames;
            parameters[7].Value = model.PlusFlag;
            parameters[8].Value = model.RealName;
            parameters[9].Value = model.UserFlag;
            parameters[10].Value = model.ParentUserID;
            parameters[11].Value = model.ParentUserIDs;
            parameters[12].Value = model.IsPublic;

            //return DbHelperSQL.RunProcedure("Admin_AddNew", parameters, out rowsAffected);
            DbHelperSQL.RunProcedure("Admin_AddNew", parameters, out rowsAffected);
            return rowsAffected;
        }


        /// <summary>
        /// 更新附加权限信息
        /// </summary>
        public int UpdateForPlusSysRoleFlag(Model.Admin model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@UserFlag", SqlDbType.VarChar),
					new SqlParameter("@PlusFlag", SqlDbType.VarChar)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.UserFlag;
            parameters[2].Value = model.PlusFlag;
            return DbHelperSQL.RunProcedure("Admin_UpdatePlusSysRoleFlag", parameters, out rowsAffected);
        }



        /// <summary>
        /// 更新当前登录的信息
        /// </summary>
        public int updateAdminInfo(Model.Admin model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@RealName", SqlDbType.NVarChar,20),
					new SqlParameter("@JobTitle", SqlDbType.VarChar),
					new SqlParameter("@JobDept", SqlDbType.VarChar),
					new SqlParameter("@QQ", SqlDbType.NVarChar),
					new SqlParameter("@MSN", SqlDbType.VarChar),
					new SqlParameter("@Email", SqlDbType.VarChar),
					new SqlParameter("@Mobile", SqlDbType.VarChar),
					new SqlParameter("@HomeTel", SqlDbType.VarChar),
					new SqlParameter("@OfficeTel", SqlDbType.VarChar)};
            parameters[0].Value = model.UserId;
            parameters[1].Value = model.RealName;
            parameters[2].Value = model.JobTitle;
            parameters[3].Value = model.JobDept;
            parameters[4].Value = model.QQ;
            parameters[5].Value = model.MSN;
            parameters[6].Value = model.Email;
            parameters[7].Value = model.Mobile;
            parameters[8].Value = model.HomeTel;
            parameters[9].Value = model.OfficeTel;
            return DbHelperSQL.RunProcedure("Admin_UpdateInfo", parameters, out rowsAffected);
        }

    }
}

