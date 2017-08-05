using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;//工具类
namespace Sys.DAL
{
    /// <summary>
    /// 数据访问类Member。
    /// </summary>
    public class Member
    {
        public Member()
        { }
        #region  成员方法
        /// <summary>
		///  增加一条数据
		/// </summary>
		public void Add(Sys.Model.Member model)
		{
			int rowsAffected;
			SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Username", SqlDbType.NVarChar,20),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,50),
					new SqlParameter("@Email", SqlDbType.VarChar,50),
					new SqlParameter("@EmailState", SqlDbType.Int,4),
					new SqlParameter("@RealName", SqlDbType.NVarChar,20),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Sign", SqlDbType.NVarChar,255),
					new SqlParameter("@QQ", SqlDbType.VarChar,20),
					new SqlParameter("@MSN", SqlDbType.VarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateIP", SqlDbType.VarChar,50),
					new SqlParameter("@Question", SqlDbType.NVarChar,50),
					new SqlParameter("@Answer", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginTimes", SqlDbType.Int,4),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@LastLoginIP", SqlDbType.VarChar,50),
					new SqlParameter("@ThisLoginTime", SqlDbType.DateTime),
					new SqlParameter("@ThisLoginIP", SqlDbType.VarChar,50),
					new SqlParameter("@ActiveTime", SqlDbType.DateTime),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@IsVIP", SqlDbType.Int,4),
					new SqlParameter("@VIPStartTime", SqlDbType.DateTime),
					new SqlParameter("@VIPEndTime", SqlDbType.DateTime)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.Username;
			parameters[2].Value = model.GroupID;
			parameters[3].Value = model.GroupName;
			parameters[4].Value = model.Email;
			parameters[5].Value = model.EmailState;
			parameters[6].Value = model.RealName;
			parameters[7].Value = model.Birthday;
			parameters[8].Value = model.Sign;
			parameters[9].Value = model.QQ;
			parameters[10].Value = model.MSN;
			parameters[11].Value = model.CreateTime;
			parameters[12].Value = model.CreateIP;
			parameters[13].Value = model.Question;
			parameters[14].Value = model.Answer;
			parameters[15].Value = model.LoginTimes;
			parameters[16].Value = model.LastLoginTime;
			parameters[17].Value = model.LastLoginIP;
			parameters[18].Value = model.ThisLoginTime;
			parameters[19].Value = model.ThisLoginIP;
			parameters[20].Value = model.ActiveTime;
			parameters[21].Value = model.State;
			parameters[22].Value = model.IsVIP;
			parameters[23].Value = model.VIPStartTime;
			parameters[24].Value = model.VIPEndTime;
			parameters[25].Value = model.Guid;

			DbHelperSQL.RunProcedure("Member_ADD",parameters,out rowsAffected);
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.Member GetModel(int UserID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4)};
            parameters[0].Value = UserID;

            Sys.Model.Member model = new Sys.Model.Member();
            DataSet ds = DbHelperSQL.RunProcedure("Member_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["UserID"].ToString() != "")
                {
                    model.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
                }
                model.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                if (ds.Tables[0].Rows[0]["GroupID"].ToString() != "")
                {
                    model.GroupID = int.Parse(ds.Tables[0].Rows[0]["GroupID"].ToString());
                }
                model.GroupName = ds.Tables[0].Rows[0]["GroupName"].ToString();
                model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                if (ds.Tables[0].Rows[0]["EmailState"].ToString() != "")
                {
                    model.EmailState = int.Parse(ds.Tables[0].Rows[0]["EmailState"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                model.CreateIP = ds.Tables[0].Rows[0]["CreateIP"].ToString();
                model.Question = ds.Tables[0].Rows[0]["Question"].ToString();
                model.Answer = ds.Tables[0].Rows[0]["Answer"].ToString();
                if (ds.Tables[0].Rows[0]["LoginTimes"].ToString() != "")
                {
                    model.LoginTimes = int.Parse(ds.Tables[0].Rows[0]["LoginTimes"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LastLoginTime"].ToString() != "")
                {
                    model.LastLoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["LastLoginTime"].ToString());
                }
                model.LastLoginIP = ds.Tables[0].Rows[0]["LastLoginIP"].ToString();
                if (ds.Tables[0].Rows[0]["ThisLoginTime"].ToString() != "")
                {
                    model.ThisLoginTime = DateTime.Parse(ds.Tables[0].Rows[0]["ThisLoginTime"].ToString());
                }
                model.ThisLoginIP = ds.Tables[0].Rows[0]["ThisLoginIP"].ToString();
                if (ds.Tables[0].Rows[0]["ActiveTime"].ToString() != "")
                {
                    model.ActiveTime = DateTime.Parse(ds.Tables[0].Rows[0]["ActiveTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IsVIP"].ToString() != "")
                {
                    model.IsVIP = int.Parse(ds.Tables[0].Rows[0]["IsVIP"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VIPStartTime"].ToString() != "")
                {
                    model.VIPStartTime = DateTime.Parse(ds.Tables[0].Rows[0]["VIPStartTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["VIPEndTime"].ToString() != "")
                {
                    model.VIPEndTime = DateTime.Parse(ds.Tables[0].Rows[0]["VIPEndTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Birthday"].ToString() != "")
                {
                    model.Birthday = DateTime.Parse(ds.Tables[0].Rows[0]["Birthday"].ToString());
                }
                model.RealName = ds.Tables[0].Rows[0]["RealName"].ToString();
                model.Sign = ds.Tables[0].Rows[0]["Sign"].ToString();
                model.QQ = ds.Tables[0].Rows[0]["QQ"].ToString();
                model.MSN = ds.Tables[0].Rows[0]["MSN"].ToString();
                model.Guid = ds.Tables[0].Rows[0]["Guid"].ToString();
                model.Integral = int.Parse(ds.Tables[0].Rows[0]["Integral"].ToString());
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(Sys.Model.Member model, string Password)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@Username", SqlDbType.NVarChar,20),
					new SqlParameter("@CreateIP", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
                    new SqlParameter("@Email",SqlDbType.NVarChar,50)};
            parameters[0].Value = model.Username;
            parameters[1].Value = model.CreateIP;
            parameters[2].Value = Password;
            parameters[3].Value = model.Email;
            return DbHelperSQL.RunProcedure("Member_ADD", parameters, out rowsAffected);
        }

        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int AddReturnUserID(Sys.Model.Member model, string Password)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Username", SqlDbType.NVarChar,20),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@CreateIP", SqlDbType.NVarChar,50),
					new SqlParameter("@Password", SqlDbType.NVarChar,50),
					new SqlParameter("@State", SqlDbType.Int,4)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.Username;
            parameters[2].Value = model.GroupID;
            parameters[3].Value = model.CreateIP;
            parameters[4].Value = Password;
            parameters[5].Value = model.State;
            DbHelperSQL.RunProcedure("Member_ADDReturnUserID", parameters, out rowsAffected);

            if (rowsAffected ==1)
            {
                return rowsAffected;
            }
            else
            {
                return (int)parameters[0].Value;;
            }
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public int Update(Sys.Model.Member model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Username", SqlDbType.NVarChar,20),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@State", SqlDbType.Int,4)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Username;
            parameters[2].Value = model.GroupID;
            parameters[3].Value = model.State;
            return DbHelperSQL.RunProcedure("Member_Update", parameters, out rowsAffected);
        }


        /// <summary>
        ///  更新一条数据
        /// </summary>
        public int UpdateBasicInfo(Sys.Model.Member model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Username", SqlDbType.NVarChar,20),
					new SqlParameter("@Email", SqlDbType.VarChar,50),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@QQ", SqlDbType.VarChar,20),
					new SqlParameter("@MSN", SqlDbType.VarChar,50),
                    new SqlParameter("@Question", SqlDbType.NVarChar,100),
                    new SqlParameter("@Answer", SqlDbType.NVarChar,100),
                    new SqlParameter("@RealName", SqlDbType.NVarChar,20),
                    new SqlParameter("@EmailState",SqlDbType.Int,4)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Username;
            parameters[2].Value = model.Email;
            parameters[3].Value = model.Birthday;
            parameters[4].Value = model.QQ;
            parameters[5].Value = model.MSN;
            parameters[6].Value = model.Question;
            parameters[7].Value = model.Answer;
            parameters[8].Value = model.RealName;
            parameters[9].Value = model.EmailState;

            return DbHelperSQL.RunProcedure("Member_UpdateBasicInfo", parameters, out rowsAffected);
        }

        /// <summary>
        ///  更新一条数据(包含所有字段)
        /// </summary>
        public int Update_AllInfo(Sys.Model.Member model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
					new SqlParameter("@Username", SqlDbType.NVarChar,20),
					new SqlParameter("@GroupID", SqlDbType.Int,4),
					new SqlParameter("@GroupName", SqlDbType.NVarChar,50),
					new SqlParameter("@Email", SqlDbType.VarChar,50),
					new SqlParameter("@EmailState", SqlDbType.Int,4),
					new SqlParameter("@RealName", SqlDbType.NVarChar,20),
					new SqlParameter("@Birthday", SqlDbType.DateTime),
					new SqlParameter("@Sign", SqlDbType.NVarChar,255),
					new SqlParameter("@QQ", SqlDbType.VarChar,20),
					new SqlParameter("@MSN", SqlDbType.VarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@CreateIP", SqlDbType.VarChar,50),
					new SqlParameter("@Question", SqlDbType.NVarChar,50),
					new SqlParameter("@Answer", SqlDbType.NVarChar,50),
					new SqlParameter("@LoginTimes", SqlDbType.Int,4),
					new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
					new SqlParameter("@LastLoginIP", SqlDbType.VarChar,50),
					new SqlParameter("@ThisLoginTime", SqlDbType.DateTime),
					new SqlParameter("@ThisLoginIP", SqlDbType.VarChar,50),
					new SqlParameter("@ActiveTime", SqlDbType.DateTime),
					new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@IsVIP", SqlDbType.Int,4),
					new SqlParameter("@VIPStartTime", SqlDbType.DateTime),
					new SqlParameter("@VIPEndTime", SqlDbType.DateTime),
					new SqlParameter("@Guid", SqlDbType.VarChar,36),
					new SqlParameter("@Integral", SqlDbType.Int,4)};
            parameters[0].Value = model.UserID;
            parameters[1].Value = model.Username;
            parameters[2].Value = model.GroupID;
            parameters[3].Value = model.GroupName;
            parameters[4].Value = model.Email;
            parameters[5].Value = model.EmailState;
            parameters[6].Value = model.RealName;
            parameters[7].Value = model.Birthday;
            parameters[8].Value = model.Sign;
            parameters[9].Value = model.QQ;
            parameters[10].Value = model.MSN;
            parameters[11].Value = model.CreateTime;
            parameters[12].Value = model.CreateIP;
            parameters[13].Value = model.Question;
            parameters[14].Value = model.Answer;
            parameters[15].Value = model.LoginTimes;
            parameters[16].Value = model.LastLoginTime;
            parameters[17].Value = model.LastLoginIP;
            parameters[18].Value = model.ThisLoginTime;
            parameters[19].Value = model.ThisLoginIP;
            parameters[20].Value = model.ActiveTime;
            parameters[21].Value = model.State;
            parameters[22].Value = model.IsVIP;
            parameters[23].Value = model.VIPStartTime;
            parameters[24].Value = model.VIPEndTime;
            parameters[25].Value = model.Guid;
            parameters[26].Value = model.Integral;

            return DbHelperSQL.RunProcedure("Member_Update_AllInfo", parameters, out rowsAffected);
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

            DbHelperSQL.RunProcedure("Member_Delete", parameters, out rowsAffected);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int ChageState(int UserID, int IsAdmin)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@UserID", SqlDbType.Int,4),
                    new SqlParameter("@IsAdmin", SqlDbType.Int,4)};
            parameters[0].Value = UserID;
            parameters[1].Value = IsAdmin;
            return DbHelperSQL.RunProcedure("Member_ChangeState", parameters, out rowsAffected);
        }

        /// <summary>
        /// 检测用户注册的Email是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>是否存在</returns>
        public bool CheckEmailExist(string userName)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@Email", SqlDbType.NVarChar,20),
                    };
            parameters[0].Value = userName;
            return DbHelperSQL.RunProcedure("Member_CheckEmailExist", parameters, out rowsAffected) > 0;
        }

        /// <summary>
        /// 根据用户名、找回密码问题、找回密码答案找回密码
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="question">找回密码问题</param>
        /// <param name="answer">找回密码答案</param>
        /// <returns>密码</returns>
        public int UpdatePwd(string name, string question, string answer, string newpwd)
        {
            int rowsAffected; 
            SqlParameter[] parameters = {
					new SqlParameter("@Username", SqlDbType.NVarChar,20),
                    new SqlParameter("@Question", SqlDbType.NVarChar,20),
                    new SqlParameter("@Answer", SqlDbType.NVarChar,20),
                    new SqlParameter("@Password", SqlDbType.VarChar,200)};
            parameters[0].Value = name;
            parameters[1].Value = question;
            parameters[2].Value = answer;
            parameters[3].Value = newpwd;
            return DbHelperSQL.RunProcedure("Member_UpdatePwdByUID", parameters, out rowsAffected);
            
        }
        #endregion  成员方法
    }
}

