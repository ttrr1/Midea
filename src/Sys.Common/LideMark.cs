using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using System.Data;
using Sys.Common;


namespace sz71096.Common
{
    public class LideMark
    {

        /// <summary>
        /// 返回流水号
        /// </summary>
        /// <param name="preFix">前缀</param>
        /// <param name="year">年 2位 还是 4位</param>
        /// <param name="month">0：不显示 2 显示2位 </param>
        /// <param name="dd">日期部分 >0 显示</param>
        /// <param name="filed">流水号字段名</param>
        /// <param name="tableName">表名</param>
        /// <param name="where">查询条件 可为空</param>
        /// <param name="numLength">流水号右边长度</param>
        /// <returns></returns>
        public static string GetSerialNumber(string preFix, int year, int month, int dd, string filed, string tableName, string where, int numLength)
        {
            var pars = new[]
                           {
                            new SqlParameter("@preFix",preFix),
                            new SqlParameter("@year",year),
                            new SqlParameter("@month",month),
                            new SqlParameter("@dd",dd),
                            new SqlParameter("@field",filed),
                            new SqlParameter("@table",tableName),
                            new SqlParameter("@where",where),
                            new SqlParameter("@num",numLength),
                            new SqlParameter("@result",SqlDbType.VarChar,20)
           
                           };

            pars[8].Direction = ParameterDirection.Output;
            DbHelperSQL.ExecuteScalar("CommonLideMark", CommandType.StoredProcedure, pars);
            return pars[8].Value.ToString();
        }

        /// <summary>
        /// 返回流水号
        /// </summary>
        public string GetSerialNumber()
        {
            const string sql = "select [dbo].[FSerialNumber]()";
            return (DbHelperSQL.GetSingle(sql)).ToString();
        }
    }
}
