using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Sys.Common;

namespace Sys.DAL
{
    public partial class UserInfo
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys.Model.UserInfo model, string password)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@UserName", SqlDbType.VarChar,50),
					new SqlParameter("@RealName", SqlDbType.NVarChar,50),
                    new SqlParameter("@Mobile", SqlDbType.VarChar,50),
					new SqlParameter("@SId", SqlDbType.Int,4),
	                new SqlParameter("@password", SqlDbType.VarChar,50),
                    new SqlParameter("@User_RId", SqlDbType.Int,4),

	                new SqlParameter("@CompanyName", SqlDbType.NVarChar,100),
					new SqlParameter("@ProvinceId", SqlDbType.Int,4),
					new SqlParameter("@ProvinceName", SqlDbType.NVarChar,50),
					new SqlParameter("@CityId", SqlDbType.Int,4),
					new SqlParameter("@CityName", SqlDbType.NVarChar,50),
					new SqlParameter("@AreaId", SqlDbType.Int,4),
					new SqlParameter("@AreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@sAreaId", SqlDbType.Int,4),
					new SqlParameter("@sAreaName", SqlDbType.NVarChar,50),
					new SqlParameter("@contact", SqlDbType.VarChar,50),
					new SqlParameter("@TypeKey", SqlDbType.Char,2),
					new SqlParameter("@TypeValue", SqlDbType.NVarChar,50),
					new SqlParameter("@RoleId", SqlDbType.Int,4),
					new SqlParameter("@Address", SqlDbType.NVarChar,150)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.UserName;
            parameters[2].Value = model.RealName;
            parameters[3].Value = model.Mobile;
            parameters[4].Value = model.SId;
            parameters[5].Value = password;
            parameters[6].Value = model.User_RId;

            parameters[7].Value = model.CompanyName;
            parameters[8].Value = model.ProvinceId;
            parameters[9].Value = model.ProvinceName;
            parameters[10].Value = model.CityId;
            parameters[11].Value = model.CityName;
            parameters[12].Value = model.AreaId;
            parameters[13].Value = model.AreaName;
            parameters[14].Value = model.sAreaId;
            parameters[15].Value = model.sAreaName;
            parameters[16].Value = model.contact;
            parameters[17].Value = model.TypeKey;
            parameters[18].Value = model.TypeValue;
            parameters[19].Value = model.RoleId;
            parameters[20].Value = model.Address;

            return DbHelperSQL.RunProcedure("UserInfo_ADD_New_Admin", parameters, out rowsAffected);
            // return (int)parameters[0].Value;
        }

        public DataTable GetUserInfoById(int id)
        {
            var sqlStr = @"SELECT  u.* ,
        m.State
FROM    dbo.UserInfo u
        INNER JOIN dbo.Member m ON u.UserId = m.UserID
WHERE   u.ID = @ID
";
            SqlParameter[] parameters =
            {
                new SqlParameter("@ID", SqlDbType.Int, 4)
            };
            parameters[0].Value = id;
            return DbHelperSQL.Query(sqlStr, parameters).Tables[0];
        }

    }
}
