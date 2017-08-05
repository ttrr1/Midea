using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;//请先添加引用
namespace Sys.DAL
{




    /// <summary>
    /// 数据访问类Common。
    /// </summary>
    public class Common
    {
        public Common()
        { }
        #region  成员方法

        /// <summary>
        /// 获得模型（使用原则：程序内设定数据，非客户端提交数据。使用前提，保证SQL字符串安全，Utils.SqlStringFormat）
        /// </summary>
        /// <param name="tblName">表的名字</param>
        /// <param name="strWhere">搜索条件</param>
        /// <returns></returns>
        public DataSet GetModel(string tblName, string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.NText),
                    new SqlParameter("@strWhere", SqlDbType.NText)
                    };
            parameters[0].Value = tblName;
            parameters[1].Value = strWhere;
            return DbHelperSQL.RunProcedure("sp_GetModel", parameters, "ds");
        }

        /// <summary>
        /// 获得数据总数（使用原则：程序内设定数据，非客户端提交数据。使用前提，保证SQL字符串安全，Utils.SqlStringFormat）
        /// </summary>
        /// <param name="tblName">表的名字</param>
        /// <param name="strWhere">搜索条件</param>
        /// <returns></returns>
        public int GetCount(string tblName, string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.NText),
                    new SqlParameter("@strWhere", SqlDbType.NText)
                    };
            parameters[0].Value = tblName;
            parameters[1].Value = strWhere;
            DataSet ds = DbHelperSQL.RunProcedure("sp_GetCount", parameters, "ds");
            return int.Parse(ds.Tables[0].Rows[0][0].ToString());
        }

        /// <summary>
        /// 分页获取数据列表（使用原则：程序内设定数据，非客户端提交数据。使用前提，保证SQL字符串安全，Utils.SqlStringFormat）
        /// </summary>
        /// <param name="tblName">表的名字</param>
        /// <param name="PageSize">每页数据条数，-1：全部</param>
        /// <param name="PageIndex">第几页，-1：全部</param>
        /// <param name="strWhere">搜索条件</param>
        /// <param name="strOrder">排序</param>
        /// <returns></returns>
        public DataSet GetList(string tblName, int PageSize, int PageIndex, string strWhere, string strOrder)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.NText),
                    new SqlParameter("@strWhere", SqlDbType.NText),
                    new SqlParameter("@fldName", SqlDbType.NText),
                    new SqlParameter("@strOrder", SqlDbType.NText),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int)
                    };
            parameters[0].Value = tblName;
            parameters[1].Value = strWhere;
            parameters[2].Value = "*";
            parameters[3].Value = strOrder;
            parameters[4].Value = PageSize;
            parameters[5].Value = PageIndex;
            return DbHelperSQL.RunProcedure("sp_GetList", parameters, "ds");
        }

        /// <summary>
        /// 多表连接查询
        /// </summary>
        /// <param name="tblName">主表名</param>
        /// <param name="priKeyName">主</param>
        /// <param name="fldNames"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="orderType"></param>
        /// <param name="strWhere"></param>
        /// <param name="strJoin"></param>
        /// <param name="TotalItem"></param>
        /// <param name="totalPage"></param>
        /// <returns></returns>
        public DataSet GetList(string tblName, string priKeyName, string fldNames, int pageSize, int pageIndex, string orderType, string strWhere, string strJoin, out int totalItem, out int totalPage)
        {
            totalItem = 0;
            totalPage = 0;
            SqlParameter[] parameters = {
                      new SqlParameter("@tblName", SqlDbType.NVarChar,255),
                      new SqlParameter("@priKeyName", SqlDbType.NVarChar,50),
                      new SqlParameter("@fldNames", SqlDbType.NVarChar,200),
                      new SqlParameter("@PageSize", SqlDbType.Int),
                      new SqlParameter("@PageIndex", SqlDbType.Int),
                      new SqlParameter("@OrderType", SqlDbType.NVarChar,200),
                      new SqlParameter("@strWhere", SqlDbType.NText),
                      new SqlParameter("@strJoin", SqlDbType.NText),
                      new SqlParameter("@TotalItem", SqlDbType.Int,4),
                      new SqlParameter("@TotalPage", SqlDbType.Int,4)
                    };
            parameters[0].Value = tblName;
            parameters[1].Value = priKeyName;
            parameters[2].Value = fldNames;
            parameters[3].Value = pageSize;
            parameters[4].Value = pageIndex;
            parameters[5].Value = orderType;
            parameters[6].Value = strWhere;
            parameters[7].Value = strJoin;
            parameters[8].Value = totalItem;
            parameters[9].Value = totalPage;

            var dataSet = DbHelperSQL.RunProcedure("Z_LeftOuterJoinByPage", parameters, "ds");
            totalPage = (int)parameters[8].Value;
            totalItem = (int)parameters[9].Value;

            return dataSet;

        }


        /// <summary>
        /// GetList远程方法重载
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="strWhere"></param>
        /// <param name="strOrder"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet GetList(string tblName, int PageSize, int PageIndex, string strWhere, string strOrder, int type)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.NText),
                    new SqlParameter("@strWhere", SqlDbType.NText),
                    new SqlParameter("@fldName", SqlDbType.NText),
                    new SqlParameter("@strOrder", SqlDbType.NText),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int)
                    };
            parameters[0].Value = tblName;
            parameters[1].Value = strWhere;
            parameters[2].Value = "*";
            parameters[3].Value = strOrder;
            parameters[4].Value = PageSize;
            parameters[5].Value = PageIndex;
            return DbHelperSQLRemote.RunProcedure("sp_GetList", parameters, "ds");
        }


        /// <summary>
        /// 分页获取数据列表（使用原则：程序内设定数据，非客户端提交数据。使用前提，保证SQL字符串安全，Utils.SqlStringFormat）
        /// </summary>
        /// <param name="tblName">表的名字</param>
        /// <param name="PageSize">每页数据条数，-1：全部</param>
        /// <param name="PageIndex">第几页，-1：全部</param>
        /// <param name="strWhere">搜索条件</param>
        /// <param name="strOrder">排序</param>
        /// <param name="fldName">输出字段,空字符串表示*</param>
        /// <returns></returns>
        public DataSet GetList(string tblName, int PageSize, int PageIndex, string strWhere, string strOrder, string fldName)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.NText),
                    new SqlParameter("@strWhere", SqlDbType.NText),
                    new SqlParameter("@fldName", SqlDbType.NText),
                    new SqlParameter("@strOrder", SqlDbType.NText),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int)
                    };
            parameters[0].Value = tblName;
            parameters[1].Value = strWhere;
            parameters[2].Value = fldName;
            parameters[3].Value = strOrder;
            parameters[4].Value = PageSize;
            parameters[5].Value = PageIndex;
            return DbHelperSQL.RunProcedure("sp_GetList", parameters, "ds");
        }

        /// <summary>
        /// 分页获取数据列表（使用原则：程序内设定数据，非客户端提交数据。使用前提，保证SQL字符串安全，Utils.SqlStringFormat；适合多表连时使用）-吴菁,20100813
        /// </summary>
        /// <param name="tblName">表的名字</param>
        /// <param name="PageSize">每页数据条数，-1：全部</param>
        /// <param name="PageIndex">第几页，-1：全部</param>
        /// <param name="strWhere">搜索条件</param>
        /// <param name="strOrder">排序</param>
        /// <param name="fldName">输出字段,空字符串表示*</param>
        /// <returns></returns>
        public DataSet GetListForMoreTable(string tblName, int PageSize, int PageIndex, string strWhere, string strOrder, string fldName)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.NText),
                    new SqlParameter("@strWhere", SqlDbType.NText),
                    new SqlParameter("@fldName", SqlDbType.NText),
                    new SqlParameter("@strOrder", SqlDbType.NText),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int)
                    };
            parameters[0].Value = tblName;
            parameters[1].Value = strWhere;
            parameters[2].Value = fldName;
            parameters[3].Value = strOrder;
            parameters[4].Value = PageSize;
            parameters[5].Value = PageIndex;
            return DbHelperSQL.RunProcedure("sp_GetListForMoreTable", parameters, "ds");
        }

        /// <summary>
        /// 分页获取数据列表，通过行号获得（使用原则：程序内设定数据，非客户端提交数据。使用前提，保证SQL字符串安全，Utils.SqlStringFormat）
        /// </summary>
        /// <param name="tblName">表的名字</param>
        /// <param name="strWhere">搜索条件</param>
        /// <param name="strOrder">排序</param>
        /// <param name="fldName">输出字段,空字符串表示*</param>
        /// <param name="rowStart">开始行号，比如：从第2条开始</param>
        /// <param name="rowEnd">截止行号，比如：从第7条结束</param>
        /// <returns></returns>
        public DataSet GetList(string tblName, string strWhere, string strOrder, string fldName, int rowStart, int rowEnd)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.NText),
                    new SqlParameter("@strWhere", SqlDbType.NText),
                    new SqlParameter("@fldName", SqlDbType.NText),
                    new SqlParameter("@strOrder", SqlDbType.NText),
                    new SqlParameter("@rowStart", SqlDbType.Int),
                    new SqlParameter("@rowEnd", SqlDbType.Int)
                    };
            parameters[0].Value = tblName;
            parameters[1].Value = strWhere;
            parameters[2].Value = fldName;
            parameters[3].Value = strOrder;
            parameters[4].Value = rowStart;
            parameters[5].Value = rowEnd;
            return DbHelperSQL.RunProcedure("sp_GetListByRow", parameters, "ds");
        }

        /// <summary>
        /// 分页获取数据列表，连表（使用原则：程序内设定数据，非客户端提交数据。使用前提，保证SQL字符串安全，Utils.SqlStringFormat）
        /// </summary>
        /// <param name="tblName">表的名字</param>
        /// <param name="PageSize">每页数据条数，-1：全部</param>
        /// <param name="PageIndex">第几页，，-1：全部</param>
        /// <param name="strWhere">搜索条件</param>
        /// <param name="strOrder">排序</param>
        /// <param name="tblLink">链接表名。Link举例 'Users','ActiveTime','VisitorID = Users.UserID'</param>
        /// <param name="fldLink">字段名，不能用*。Link举例 'Users','ActiveTime','VisitorID = Users.UserID'</param>
        /// <param name="conLink">连接关系内容。Link举例 'Users','ActiveTime','VisitorID = Users.UserID'</param>
        /// <returns></returns>
        public DataSet GetList(string tblName, int PageSize, int PageIndex, string strWhere, string strOrder, string tblLink, string fldLink, string conLink)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.NText),
                    new SqlParameter("@strWhere", SqlDbType.NText),
                    new SqlParameter("@fldName", SqlDbType.NText),
                    new SqlParameter("@strOrder", SqlDbType.NText),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@tblLink", SqlDbType.NText),
                    new SqlParameter("@fldLink", SqlDbType.NText),
                    new SqlParameter("@conLink", SqlDbType.NText)
                    };
            parameters[0].Value = tblName;
            parameters[1].Value = strWhere;
            parameters[2].Value = "*";
            parameters[3].Value = strOrder;
            parameters[4].Value = PageSize;
            parameters[5].Value = PageIndex;
            parameters[6].Value = tblLink;
            parameters[7].Value = fldLink;
            parameters[8].Value = conLink;
            return DbHelperSQL.RunProcedure("sp_GetListForLink", parameters, "ds");
        }


        /// <summary>
        /// 获取最大排序（使用原则：程序内设定数据，非客户端提交数据。使用前提，保证SQL字符串安全，Utils.SqlStringFormat）
        /// </summary>
        /// <param name="tblName">表的名字</param>
        /// <param name="fldName">输出字段</param>
        /// <returns></returns>
        public int GetMaxOrderID(string tblName, string fldName)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.NText),
                    new SqlParameter("@fldName", SqlDbType.NText)};
            parameters[0].Value = tblName;
            parameters[1].Value = fldName;
            return DbHelperSQL.RunProcedure("sp_GetMaxOrderID", parameters, out rowsAffected);
        }

        /// <summary>
        /// 获取排序（使用原则：程序内设定数据，非客户端提交数据。使用前提，保证SQL字符串安全，Utils.SqlStringFormat）
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="NowID"></param>
        /// <param name="ModelID"></param>
        /// <param name="tblName">表的名字</param>
        public void ChageOrderID(int flag, int NowID, string ModelID, string tblName)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@flag", SqlDbType.Int),
                    new SqlParameter("@NowID", SqlDbType.Int),
                    new SqlParameter("@ModelID", SqlDbType.NText),
                    new SqlParameter("@tblName", SqlDbType.NText)};
            parameters[0].Value = flag;
            parameters[1].Value = NowID;
            parameters[2].Value = ModelID;
            parameters[3].Value = tblName;
            DbHelperSQL.RunProcedure("sp_ChangeOrderID", parameters, out rowsAffected);
        }

        /// <summary>
        /// 获得数据库名称
        /// </summary>
        /// <returns></returns>
        public string DatabaseName()
        {
            SqlParameter[] parameters = { };
            DataSet ds = DbHelperSQL.RunProcedure("sp_DatabaseName", parameters, "ds");
            return ds.Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 获得数据库结构
        /// </summary>
        /// <returns></returns>
        public DataSet DatabaseTable()
        {
            SqlParameter[] parameters = { };
            return DbHelperSQL.RunProcedure("sp_DatabaseTable", parameters, "ds");
        }


        /// <summary>
        /// 获得数据库结构,只获得表名和描述
        /// </summary>
        /// <returns></returns>
        public DataSet DatabaseTableNew()
        {
            SqlParameter[] parameters = { };
            return DbHelperSQL.RunProcedure("sp_DatabaseTable_New", parameters, "ds");
        }








        /// <summary>
        /// 获得指定数据表结构
        /// </summary>
        /// <returns></returns>
        public DataSet DatabaseTable(string tblName)
        {
            SqlParameter[] parameters = { new SqlParameter("@tblName", SqlDbType.VarChar) };
            parameters[0].Value = tblName;
            return DbHelperSQL.RunProcedure("TableColumnDescription", parameters, "ds");
        }

        /// <summary>
        /// 数据库备份
        /// </summary>
        /// <param name="BakPath">备份路径</param>
        /// <returns></returns>
        public string DatabaseBackup(string BakPath)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@BakPath", SqlDbType.NText)};
            parameters[0].Value = BakPath;
            DataSet ds = DbHelperSQL.RunProcedure("sp_DatabaseBackup", parameters, "ds");
            return ds.Tables[0].Rows[0][0].ToString();
        }

        /// <summary>
        /// 数据库日志删除
        /// </summary>
        public void DatabaseLogDel()
        {
            SqlParameter[] parameters = { };
            DbHelperSQL.RunProcedure("sp_DatabaseLogDel", parameters, "ds");
        }

        /// <summary>
        /// 获取Select列表（使用原则：程序内设定数据，非客户端提交数据。使用前提，保证SQL字符串安全，Utils.SqlStringFormat）
        /// </summary>
        /// <param name="tblName">表的名字</param>
        /// <param name="strWhere">搜索条件</param>
        /// <param name="strOrder">排序</param>
        /// <param name="fldName">输出字段</param>
        /// <returns></returns>
        public DataSet GetSelect(string tblName, string strWhere, string strOrder, string fldName)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.NText),
                    new SqlParameter("@strWhere", SqlDbType.NText),
                    new SqlParameter("@fldName", SqlDbType.NText),
                    new SqlParameter("@strOrder", SqlDbType.NText)};
            parameters[0].Value = tblName;
            parameters[1].Value = strWhere;
            parameters[2].Value = fldName;
            parameters[3].Value = strOrder;
            return DbHelperSQL.RunProcedure("sp_GetSelect", parameters, "ds");
        }


        /// <summary>
        /// 【程序集内部使用】执行SQL语句，使用的时候用在此备注。目前已经使用：ContentItem,iPortal.Upgrader
        /// </summary>
        /// <param name="sql">SQL语句</param>
        public static void ExecuteSQL(string sql)
        {
            SqlParameter[] parameters = { new SqlParameter("@StrSQL", SqlDbType.NText) };
            parameters[0].Value = sql;
            DbHelperSQL.RunProcedure("sp_ExecSQL", parameters);
        }

        public DataSet GetDataTableBySql(string sql)
        {
            SqlParameter[] parameters = { new SqlParameter("@StrSQL", SqlDbType.NText) };
            parameters[0].Value = sql;
            return DbHelperSQL.RunProcedure("sp_ExecSQL", parameters, "ds");
        }
        #endregion  成员方法
    }
}

