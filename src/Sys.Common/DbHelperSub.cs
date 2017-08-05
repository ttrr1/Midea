using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace Sys.Common
{
    public class DbHelperSub
    {
        /// <summary>
        /// 创建 DataTable 对象
        /// </summary>
        /// <param name="tblName">表名</param>
        /// <param name="priKeyName">主键列</param>
        /// <param name="fldNames">要取值的字段名,多个字段通过逗号分割</param>
        /// <param name="pageSize">页尺寸，0表示不需要分页</param>
        /// <param name="pageIndex">页码，从1开始</param>
        /// <param name="strWhere">查询条件 (注意: 不要加 where)</param>
        /// <param name="totalItem">总的记录数</param>
        /// <param name="totalPage">总的页数</param>
        /// <param name="dataTable">返回 DataTable 对象</param>
       public static void GetPagedData(string tblName, string priKeyName, string fldNames, int pageSize, int pageIndex, string strWhere, out int totalItem, out int totalPage, out DataTable dataTable)
        {
            GetPagedData(tblName, priKeyName, fldNames, pageSize, pageIndex, "1", strWhere, "", out totalItem, out totalPage, out dataTable);
        }

        /// <summary>
        /// 创建 DataTable 对象
        /// </summary>
        /// <param name="tblName">表名</param>
        /// <param name="priKeyName">主键列</param>
        /// <param name="fldNames">要取值的字段名,多个字段通过逗号分割</param>
        /// <param name="pageSize">页尺寸，0表示不需要分页</param>
        /// <param name="pageIndex">页码，从1开始</param>
        /// <param name="orderType">设置排序，'':没有排序要求 0：主键升序 1：主键降序 else：用户自定义排序规则</param>
        /// <param name="strWhere">查询条件 (注意: 不要加 where)</param>
        /// <param name="strJoin">连接表</param>
        /// <param name="totalItem">总的记录数</param>
        /// <param name="totalPage">总的页数</param>
        /// <param name="dataReader">返回 DataTable 对象</param>
        /// <param name="connectionString">数据库连接字符串</param>
        public static void GetPagedData(string tblName, string priKeyName, string fldNames, int pageSize, int pageIndex, string orderType, string strWhere, string strJoin, out int totalItem, out int totalPage, out DataTable dataTable)
        {
            totalItem = 0;
            totalPage = 0;
            dataTable = null;
            
            //创建参数列表
            DbParameter[] paramsArr = 
            { 
                DbHelperSQL.MakeInParam("@tblName", SqlDbType.VarChar, 255, tblName),
                DbHelperSQL.MakeInParam("@priKeyName", SqlDbType.VarChar, 50, priKeyName),
                DbHelperSQL.MakeInParam("@fldNames", SqlDbType.VarChar, 1000, fldNames),
                DbHelperSQL.MakeInParam("@pageSize", SqlDbType.Int, 4, pageSize),
                DbHelperSQL.MakeInParam("@pageIndex", SqlDbType.Int, 4, pageIndex),
                DbHelperSQL.MakeInParam("@orderType", SqlDbType.VarChar, 200, orderType),
                DbHelperSQL.MakeInParam("@strWhere", SqlDbType.VarChar, 2000, strWhere),
                DbHelperSQL.MakeInParam("@strJoin", SqlDbType.VarChar, 1000, strJoin),

                DbHelperSQL.MakeOutParam("@totalPage", SqlDbType.Int, 4),
                DbHelperSQL.MakeOutParam("@totalItem", SqlDbType.Int, 4)
            };

            try
            {
                //填充 DataSet
                var dataSet = DbHelperSQL.RunProcedure("Z_LeftOuterJoinByPage", paramsArr, "newDataSet");

                //获取存储过程返回值 totalPage, totalItem
                totalPage = (int)paramsArr[8].Value;
                totalItem = (int)paramsArr[9].Value;

                if (dataSet != null)
                    dataTable = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 分页存储过程读取数据
        /// </summary>
        /// <param name="strTables">表名</param>
        /// <param name="strPrimaryKey">主键名</param>
        /// <param name="strFields">字段名</param>
        /// <param name="strFilter">条件</param>
        /// <param name="pageSize">页尺寸</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="totalItem">总记录</param>
        /// <param name="totalPage">总页数</param>
        /// <param name="dataTable">数据表</param>
        public static void GetPagedData_SP2(string strTables, string strPrimaryKey, string strFields, string strFilter, int pageSize, int currentPage, out int totalItem, out int totalPage, out DataTable dataTable)
        {
            GetPagedData_SP2(strTables, strPrimaryKey, strFields, strFilter, "", "", pageSize, currentPage, out totalItem, out totalPage, out dataTable);
        }

        public static void GetPagedData_SP2(string strTables, string strPrimaryKey, string strFields, string strFilter, string strGroup, string strSort, int pageSize, int currentPage, out int totalItem, out int totalPage, out DataTable dataTable)
        {
            totalItem = 0;
            totalPage = 0;
            dataTable = null;
            //创建参数列表
            //@Tables(1000), @PrimaryKey(100), @Sort(200), @CurrentPage, @PageSize, @Fields(1000), @Filter(1000), @Group(1000), @RecordCount
            DbParameter[] paramsArr = 
            { 
                DbHelperSQL.MakeInParam("@Tables", SqlDbType.VarChar, 1000, strTables),
                DbHelperSQL.MakeInParam("@PrimaryKey", SqlDbType.VarChar, 100, strPrimaryKey),
                DbHelperSQL.MakeInParam("@Fields", SqlDbType.VarChar, 1000, strFields),
                DbHelperSQL.MakeInParam("@Filter", SqlDbType.VarChar, 1000, strFilter),
                DbHelperSQL.MakeInParam("@Group", SqlDbType.VarChar, 1000, strGroup),
                DbHelperSQL.MakeInParam("@Sort", SqlDbType.VarChar, 200, strSort),
                DbHelperSQL.MakeInParam("@PageSize", SqlDbType.Int, 4, pageSize),
                DbHelperSQL.MakeInParam("@CurrentPage", SqlDbType.Int, 4, currentPage),

                DbHelperSQL.MakeOutParam("@RecordCount", SqlDbType.Int, 4),
                DbHelperSQL.MakeOutParam("@TotalPage", SqlDbType.Int, 4)
            };

            try
            {
                //填充 DataSet
                var dataSet = DbHelperSQL.RunProcedure("CN5135_SP_Pagination", paramsArr, "newDataSet"); ;
                
                //获取存储过程返回值 totalPage, totalItem
                totalItem = (int)paramsArr[8].Value;
                totalPage = (int)paramsArr[9].Value;

                if (dataSet != null)
                    dataTable = dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
