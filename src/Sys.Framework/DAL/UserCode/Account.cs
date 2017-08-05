using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Sys.Common;

namespace Sys.DAL
{
    public partial class Account
    {
        public DataSet GetUserInfo(string userId, string password)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  u.UserID ,
        u.UserName AS USERLOGINID ,
        u.RealName AS USERNAME ,
        a.Password AS PASSWORD ,
        u.CompanyName ,
        u.ProvinceName ,
        u.CityName ,
        u.AreaName ,
        u.Address AS Address ,
        u.contact ,
        u.TypeKey ,
        u.TypeValue ,
        u.RoleId ,
        m.State
FROM    dbo.Account a
        INNER JOIN dbo.Member m ON m.UserID = a.UserID
        LEFT JOIN dbo.UserInfo u ON a.UserID = u.UserId
WHERE   a.Username = @USERLOGINID
        AND a.Password = @PASSWORD");
            SqlParameter[] parameters = {
					new SqlParameter("@USERLOGINID", SqlDbType.NVarChar,20)	,
                    new SqlParameter("@PASSWORD", SqlDbType.VarChar,200)};
            parameters[0].Value = userId;
            parameters[1].Value = password;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="userLoginId"></param>
        /// <returns></returns>
        public DataSet GetList(string userLoginId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  a.UserID ,
        u.UserName AS USERLOGINID ,
        u.RealName AS USERNAME ,
        a.Password AS PASSWORD ,
        u.CompanyName ,
        u.ProvinceName ,
        u.CityName ,
        u.Address AS Address ,
        u.contact ,
        u.TypeKey ,
        u.TypeValue ,
        u.RoleId
FROM    dbo.Account a
        LEFT JOIN dbo.UserInfo u ON a.UserID = u.UserId
WHERE   a.Username = @USERLOGINID");
            SqlParameter[] parameters = {
					new SqlParameter("@USERLOGINID", SqlDbType.NVarChar,20)};
            parameters[0].Value = userLoginId;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }


        
    }
}
