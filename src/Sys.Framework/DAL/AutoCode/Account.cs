using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;
namespace Sys.DAL
{
	/// <summary>
	/// 数据访问类Account。
	/// </summary>
	public partial class Account
	{
		public Account()
		{}
		#region  成员方法

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("UserID", "Account"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int UserID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
			parameters[0].Value = UserID;

			int result= DbHelperSQL.RunProcedure("Account_Exists",parameters,out rowsAffected);
			if(result==1)
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
		public int Add(Sys.Model.Account model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Username", SqlDbType.NVarChar,20),
					new SqlParameter("@Password", SqlDbType.VarChar,200),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateIP", SqlDbType.VarChar,15),
					new SqlParameter("@Guid", SqlDbType.VarChar,36)};
			parameters[0].Direction = ParameterDirection.Output;
			parameters[1].Value = model.Username;
			parameters[2].Value = model.Password;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.CreateIP;
			parameters[5].Value = model.Guid;

			DbHelperSQL.RunProcedure("Account_ADD",parameters,out rowsAffected);
			return (int)parameters[0].Value;
		}

		/// <summary>
		///  更新一条数据
		/// </summary>
		public void Update(Sys.Model.Account model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Username", SqlDbType.NVarChar,20),
					new SqlParameter("@Password", SqlDbType.VarChar,200),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateIP", SqlDbType.VarChar,15),
					new SqlParameter("@Guid", SqlDbType.VarChar,36)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.Username;
			parameters[2].Value = model.Password;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.CreateIP;
			parameters[5].Value = model.Guid;

			DbHelperSQL.RunProcedure("Account_Update",parameters,out rowsAffected);
		}


        /// <summary>
        ///  更新一条数据
        /// </summary>
        public int Update(Sys.Model.Account model, string oldPwd)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Password", SqlDbType.VarChar,50),
					new SqlParameter("@OldPassword", SqlDbType.VarChar,50)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Password;
            parameters[2].Value = oldPwd;
            return DbHelperSQL.RunProcedure("Account_Update", parameters, out rowsAffected);
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(int UserID)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
			parameters[0].Value = UserID;

			DbHelperSQL.RunProcedure("Account_Delete",parameters,out rowsAffected);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Sys.Model.Account GetModel(int UserID)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
			parameters[0].Value = UserID;

			Sys.Model.Account model=new Sys.Model.Account();
			DataSet ds= DbHelperSQL.RunProcedure("Account_GetModel",parameters,"ds");
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					model.UserID=int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				model.Username=ds.Tables[0].Rows[0]["Username"].ToString();
				model.Password=ds.Tables[0].Rows[0]["Password"].ToString();
				if(ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				model.CreateIP=ds.Tables[0].Rows[0]["CreateIP"].ToString();
				model.Guid=ds.Tables[0].Rows[0]["Guid"].ToString();
				return model;
			}
			else
			{
				return null;
			}
		}



		#endregion  成员方法

        /// <summary>
        /// 用户登录 -1失败 -2没有帐户 >0用户UserID
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码MD5</param>
        /// <returns></returns>
        public int CheckLogin(string username, string password)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@Username", SqlDbType.NVarChar,20),
                    new SqlParameter("@Password", SqlDbType.VarChar,50)
				};
            parameters[0].Value = username;
            parameters[1].Value = password;
            return DbHelperSQL.RunProcedure("Account_CheckLoginbyUsername", parameters, out rowsAffected);
        }




        /// <summary>
        /// 用户登录 -1失败 -2没有帐户 >0用户UserID
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码MD5</param>
        ///  <param name="companyCode">企业号</param>
        /// <returns></returns>
        public int CheckLogin(string username, string password, string companyCode)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@Username", SqlDbType.NVarChar,20),
                    new SqlParameter("@Password", SqlDbType.VarChar,50)
                    

				};
            parameters[0].Value = username;
            parameters[1].Value = password;
            parameters[2].Value = companyCode;

            return DbHelperSQL.RunProcedure("Account_CheckLoginbyUsername", parameters, out rowsAffected);
        }




        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int AccountUpdate(int userId, string password)
        {

            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@Password", SqlDbType.VarChar,50)
				};
            parameters[0].Value = userId;
            parameters[1].Value = password;
            return DbHelperSQL.RunProcedure("Account_Update_PWD", parameters, out rowsAffected);
        }

        /// <summary>
        /// 用户登录 -1失败 >0用户UserID
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="Password">密码MD5</param>
        /// <returns></returns>
        public int CheckLogin(int UserID, string Password)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@Password", SqlDbType.VarChar,50)
				};
            parameters[0].Value = UserID;
            parameters[1].Value = Password;
            return DbHelperSQL.RunProcedure("Account_CheckLoginbyUserID", parameters, out rowsAffected);
        }
	}
}

