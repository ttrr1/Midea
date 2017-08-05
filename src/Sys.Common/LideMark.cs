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
        /// ������ˮ��
        /// </summary>
        /// <param name="preFix">ǰ׺</param>
        /// <param name="year">�� 2λ ���� 4λ</param>
        /// <param name="month">0������ʾ 2 ��ʾ2λ </param>
        /// <param name="dd">���ڲ��� >0 ��ʾ</param>
        /// <param name="filed">��ˮ���ֶ���</param>
        /// <param name="tableName">����</param>
        /// <param name="where">��ѯ���� ��Ϊ��</param>
        /// <param name="numLength">��ˮ���ұ߳���</param>
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
        /// ������ˮ��
        /// </summary>
        public string GetSerialNumber()
        {
            const string sql = "select [dbo].[FSerialNumber]()";
            return (DbHelperSQL.GetSingle(sql)).ToString();
        }
    }
}
