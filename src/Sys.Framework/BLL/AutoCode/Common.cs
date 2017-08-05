using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.IO;
using Sys.Common;

namespace Sys.BLL
{


    public class Common
    {
        private readonly DAL.Common dal = new DAL.Common();
        public Common()
        { }

        


        /// <summary>
        /// 下拉列表
        /// </summary>
        /// <param name="intTmp">反选参数</param>
        /// <param name="dt">DataTable</param>
        /// <param name="noSelected">没有选择的名称，默认“—请选择—”</param>
        /// <returns></returns>
        public static string GetSelectList(int intTmp, DataTable dt, string noSelected)
        {
            if (noSelected == "")
                noSelected = "—请选择—";
            var xs = "";
            if (dt.Rows.Count == 0)
                xs = xs + "<option value=''>当前没有选项</option>";
            else
            {
                xs = xs + "<option value=''>" + noSelected + "</option>";
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(intTmp) == dt.Rows[i][0].ToString())//反选
                    {
                        xs = xs + "<option value='" + dt.Rows[i][0].ToString() + "' selected>" + dt.Rows[i][1].ToString() + "</option>";
                    }
                    else
                    {
                        xs = xs + "<option value='" + dt.Rows[i][0].ToString() + "' >" + dt.Rows[i][1].ToString() + "</option>";
                    }
                }
            }
            return xs;
        }


        /// <summary>
        /// 【程序集内部使用】执行SQL语句，使用的时候用在此备注。目前已经使用：ContentItem
        /// </summary>
        /// <param name="sql">SQL语句</param>
        public static void ExecuteSQL(string sql)
        {
            DAL.Common.ExecuteSQL(sql);
        }

        public static string GetSelectList(int intTmp, DataTable dt)
        {
            return GetSelectList(intTmp, dt, "");
        }
        public static string GetSelectList(int intTmp, DataSet ds, string noSelected)
        {
            return GetSelectList(intTmp, ds.Tables[0], noSelected);
        }
        public static string GetSelectList(int intTmp, DataSet ds)
        {
            return GetSelectList(intTmp, ds.Tables[0], "");
        }



        /// <summary>
        /// 下拉列表
        /// </summary>
        /// <param name="strTmp">反选参数</param>
        /// <param name="dt">DataTable</param>
        /// <param name="noSelected">没有选择的名称，默认“—请选择—”</param>
        /// <returns></returns>
        public static string GetSelectList(string strTmp, DataTable dt, string noSelected)
        {
            if (noSelected == "")
                noSelected = "—请选择—";

            if (strTmp == null)
            {
                strTmp = "";
            }

            string xs = "";
            if (dt.Rows.Count == 0)
                xs = xs + "<option value=''>当前没有分类</option>";
            else
            {
                xs = xs + "<option value=''>" + noSelected + "</option>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (strTmp.ToString().IndexOf(dt.Rows[i][0].ToString()) >= 0)//反选
                    {
                        xs = xs + "<option value='" + dt.Rows[i][0].ToString() + "' selected>" + dt.Rows[i][1].ToString() + "</option>";
                    }
                    else
                    {
                        xs = xs + "<option value='" + dt.Rows[i][0].ToString() + "' >" + dt.Rows[i][1].ToString() + "</option>";
                    }
                }
            }
            return xs;
        }

        public static string GetSelectList(string strTmp, DataTable dt)
        {
            return GetSelectList(strTmp, dt, "");
        }
        public static string GetSelectList(string strTmp, DataSet ds, string noSelected)
        {
            return GetSelectList(strTmp, ds.Tables[0], noSelected);
        }
        public static string GetSelectList(string strTmp, DataSet ds)
        {
            return GetSelectList(strTmp, ds.Tables[0], "");
        }

        public DataSet GetDataTableBySql(string sql)
        {
            return dal.GetDataTableBySql(sql);
        }

        /// <summary>
        /// 获得数据库名称
        /// </summary>
        /// <returns></returns>
        public string DatabaseName()
        {
            return dal.DatabaseName();
        }

        /// <summary>
        /// 获得数据库结构
        /// </summary>
        /// <returns></returns>
        public DataSet DatabaseTable()
        {
            return dal.DatabaseTable();
        }



        /// <summary>
        /// 获得数据库结构
        /// </summary>
        /// <returns></returns>
        public DataSet DatabaseTableNew()
        {
            return dal.DatabaseTableNew();

        }


        /// <summary>
        /// 获得指定数据表结构
        /// </summary>
        /// <returns></returns>
        public DataSet DatabaseTable(string tblName)
        {
            return dal.DatabaseTable(tblName);
        }

        /// <summary>
        /// 数据库备份
        /// </summary>
        /// <param name="BakPath">备份路径</param>
        /// <returns></returns>
        public string DatabaseBackup(string BakPath)
        {
            return dal.DatabaseBackup(BakPath);
        }

        /// <summary>
        /// 数据库日志删除
        /// </summary>
        public void DatabaseLogDel()
        {
            dal.DatabaseLogDel();
        }


        /// <summary>
        /// 多选框
        /// </summary>
        /// <param name="intTmp">反选参数</param>
        /// <param name="ds">ds</param>
        /// <returns></returns>
        public static string GetCheckBoxListForRole(string chkName, string intTmp, DataSet ds, int userID)
        {
            DataTable dt = ds.Tables[0];
            string xs = "";
            intTmp = "," + intTmp + ",";
            if (dt.Rows.Count == 0)
                xs = xs + "当前没有角色分类";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (intTmp.IndexOf("," + dt.Rows[i][0].ToString() + ",") >= 0)//反选
                    {
                        xs = xs + "<li><input type=\"checkbox\" name=" + chkName + " value='" + dt.Rows[i][0].ToString() + "' checked onclick=\"getRoleChk(" + userID + ")\"/>" + dt.Rows[i][1].ToString() + "</li>";//onclick=\"getRoleChk()\"
                    }
                    else
                    {
                        xs = xs + "<li><input type=\"checkbox\" name=" + chkName + " value='" + dt.Rows[i][0].ToString() + "' onclick=\"getRoleChk(" + userID + ")\" />" + dt.Rows[i][1].ToString() + "</li>";
                    }
                }
            }
            return xs;
        }

        /// <summary>
        /// 多选框  管理员管理隐藏没有权限的角色 
        /// </summary>
        /// <param name="intTmp">反选参数</param>
        /// <param name="ds">ds</param>
        /// <returns></returns>
        public static string GetCheckBoxListForRole2(string chkName, string intTmp, DataSet ds, int userID)
        {
            var dt = ds.Tables[0];
            var xs = "";
            intTmp = "," + intTmp + ",";
            if (dt.Rows.Count == 0)
                xs = xs + "当前没有角色分类";
            else
            {
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["RoleID"].ToString() == "-1")
                        continue;
                    if (intTmp.IndexOf("," + dt.Rows[i][0] + ",") >= 0)//反选
                    {
                        xs = xs + "<li><input type=\"checkbox\" name=" + chkName + " value='" + dt.Rows[i][0].ToString() + "' checked onclick=\"getRoleChk(" + userID + ")\"/>" + dt.Rows[i][1] + "</li>";//onclick=\"getRoleChk()\"
                    }
                    else
                    {
                        xs = xs + "<li><input type=\"checkbox\" name=" + chkName + " value='" + dt.Rows[i][0].ToString() + "' onclick=\"getRoleChk2(" + userID + ")\" />" + dt.Rows[i][1] + "</li>";
                    }
                }
            }
            return xs;
        }

        /// <summary>
        /// 商户应用模块多选框，
        /// </summary>
        /// <param name="intTmp">反选参数</param>
        /// <param name="ds">ds</param>
        /// <returns></returns>
        public static string GetCheckBoxListForModule(string chkName, string intTmp, DataSet ds, bool isAdd)
        {
            DataTable dt = ds.Tables[0];
            string xs = "";
            intTmp = "," + intTmp + ",";
            if (dt.Rows.Count == 0)
                xs = xs + "当前没有应用模块";
            else
            {
                string CanRead = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][2].ToString() == "0")
                    {
                        CanRead = "disabled";

                        if (isAdd && dt.Rows[i][3].ToString() == "1")
                        {
                            xs = xs + "<input type=\"hidden\" name=\"" + chkName + "\" value=\"" + dt.Rows[i][0].ToString() + "\" />";
                        }
                    }
                    else
                    {
                        CanRead = "";
                    }

                    if (isAdd && dt.Rows[i][3].ToString() == "1")
                    {
                        xs = xs + "<li><input type=\"checkbox\" name=" + chkName + " value='" + dt.Rows[i][0].ToString() + "' checked " + CanRead + "/>" + dt.Rows[i][1].ToString() + "</li>";
                    }
                    else if (intTmp.IndexOf("," + dt.Rows[i][0].ToString() + ",") >= 0)//反选
                    {
                        xs = xs + "<li><input type=\"checkbox\" name=\"" + chkName + "\" value=\"" + dt.Rows[i][0].ToString() + "\" checked " + CanRead + "/>" + dt.Rows[i][1].ToString() + "</li>";
                    }
                    else
                    {
                        xs = xs + "<li><input type=\"checkbox\" name=" + chkName + " value='" + dt.Rows[i][0].ToString() + "'  " + CanRead + "/>" + dt.Rows[i][1].ToString() + "</li>";
                    }
                }
            }
            return xs;
        }

        /// <summary>
        /// 多选框
        /// </summary>
        /// <param name="intTmp">反选参数</param>
        /// <param name="ds">ds</param>
        /// <returns></returns>
        public static string GetCheckBoxListForSite(string chkName, string strTmp, DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            string xs = "";
            strTmp = "," + strTmp + ",";
            if (dt.Rows.Count == 0)
                xs = xs + "当前没有站点";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (strTmp.IndexOf(",site_" + dt.Rows[i][0].ToString() + ",") >= 0)//反选
                    {
                        xs = xs + "<li><input type=\"checkbox\" name=" + chkName + " value='site_" + dt.Rows[i][0].ToString() + "' checked />" + dt.Rows[i][1].ToString() + "</li>";
                    }
                    else
                    {
                        xs = xs + "<li><input type=\"checkbox\" name=" + chkName + " value='site_" + dt.Rows[i][0].ToString() + "' />" + dt.Rows[i][1].ToString() + "</li>";
                    }
                }
            }
            return xs;
        }


        /// <summary>
        /// 多选框
        /// </summary>
        /// <param name="chkName">多选框的Name</param>
        /// <param name="strTmp_disabled">反选参数1 不能编辑的</param>
        /// <param name="strTmp">反选参数2 可以编辑的</param>
        /// <param name="ds">ds</param>
        /// <returns>ds</returns>
        public static string GetCheckBoxListForSite(string chkName, string strTmp_disabled, string strTmp, DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            string xs = "";
            if (dt.Rows.Count == 0)
                xs = xs + "当前没有站点";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string CheckState = "";
                    if (strTmp_disabled.IndexOf(",site_" + dt.Rows[i][0].ToString() + ",") != -1)//反选
                    {
                        CheckState = " checked disabled";
                    }
                    else
                    {
                        if (strTmp.IndexOf(",site_" + dt.Rows[i][0].ToString() + ",") != -1)
                        {
                            CheckState = " checked ";
                        }
                    }
                    xs = xs + "<li><input type=\"checkbox\" name=" + chkName + " value='site_" + dt.Rows[i][0].ToString() + "' " + CheckState + " />" + dt.Rows[i][1].ToString() + "</li>";
                }
            }
            return xs;
        }
        /// <summary>
        /// 多选框 管理员管理隐藏没有权限的站点 刘施洁
        /// </summary>
        /// <param name="chkName">多选框的Name</param>
        /// <param name="strTmp_disabled">反选参数1 不能编辑的</param>
        /// <param name="strTmp">反选参数2 可以编辑的</param>
        /// <param name="ds">ds</param>
        /// <returns>ds</returns>
        public static string GetCheckBoxListForSite2(string chkName, string strTmp_disabled, string strTmp, DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            string xs = "";
            if (dt.Rows.Count == 0)
                xs = xs + "当前没有站点";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["SiteID"].ToString() == "-1")
                        continue;
                    string CheckState = "";
                    if (strTmp_disabled.IndexOf(",site_" + dt.Rows[i][0].ToString() + ",") != -1)//反选
                    {
                        CheckState = " checked disabled";
                    }
                    else
                    {
                        if (strTmp.IndexOf(",site_" + dt.Rows[i][0].ToString() + ",") != -1)
                        {
                            CheckState = " checked ";
                        }
                    }
                    xs = xs + "<li><input type=\"checkbox\" name=" + chkName + " value='site_" + dt.Rows[i][0].ToString() + "' " + CheckState + " />" + dt.Rows[i][1].ToString() + "</li>";
                }
            }
            return xs;
        }


        /// <summary>
        /// 多选框 
        /// </summary>
        /// <param name="intTmp">反选参数</param>
        /// <param name="ds">ds</param>
        /// <returns></returns>
        public static string GetCheckBoxList(string chkName, string intTmp, DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            string xs = "";
            intTmp = "," + intTmp + ",";
            if (dt.Rows.Count == 0)
                xs = xs + "当前没有站点";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (intTmp.IndexOf("," + dt.Rows[i][0].ToString() + ",") >= 0)//反选
                    {
                        xs = xs + "<li><input type=\"checkbox\" name=" + chkName + " value='" + dt.Rows[i][0].ToString() + "' checked />" + dt.Rows[i][1].ToString() + "</li>";
                    }
                    else
                    {
                        xs = xs + "<li><input type=\"checkbox\" name=" + chkName + " value='" + dt.Rows[i][0].ToString() + "' />" + dt.Rows[i][1].ToString() + "</li>";
                    }
                }
            }
            return xs;
        }







        //最后修改人HYC
        /// <summary>
        /// 下拉列表
        /// </summary>
        /// <param name="strTmp">反选参数</param>
        /// <param name="dt">DataTable</param>
        /// <param name="noSelected">没有选择的名称</param>
        /// <returns></returns>
        public static string GetSelectListForTemplate(string strTmp, DataTable dt)
        {
            string xs = "";
            if (dt.Rows.Count == 0)
                xs = xs + "<option value=''>－－当前没有模板－－</option>";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (strTmp.ToString() == dt.Rows[i][0].ToString())//反选
                    {
                        xs = xs + "<option value='" + dt.Rows[i][0].ToString() + "' selected>" + dt.Rows[i][1].ToString() + "</option>";
                    }
                    else
                    {
                        xs = xs + "<option value='" + dt.Rows[i][0].ToString() + "' >" + dt.Rows[i][1].ToString() + "</option>";
                    }
                }
            }
            return xs;
        }
        public static string GetSelectListForTemplate(string strTmp, DataSet ds)
        {
            return GetSelectListForTemplate(strTmp, ds.Tables[0]);
        }


        public static string GetSelectListForAD(int strTmp, DataTable dt)
        {
            string xs = "";
            if (dt.Rows.Count == 0)
                xs = xs + "<option value=''>－－当前没有广告位－－</option>";
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (strTmp.ToString().IndexOf(dt.Rows[i][0].ToString()) >= 0)//反选
                    {
                        xs = xs + "<option value='" + dt.Rows[i][0].ToString() + "' selected>" + dt.Rows[i][1].ToString() + "</option>";
                    }
                    else
                    {
                        xs = xs + "<option value='" + dt.Rows[i][0].ToString() + "' >" + dt.Rows[i][1].ToString() + "</option>";
                    }
                }
            }
            return xs;
        }
        public static string GetSelectListForAD(int strTmp, DataSet ds)
        {
            return GetSelectListForAD(strTmp, ds.Tables[0]);
        }


        /// <summary>
        /// 获得数据总数（使用原则：程序内设定数据，非客户端提交数据。使用前提，保证SQL字符串安全，Utils.SqlStringFormat）
        /// </summary>
        /// <param name="tblName">表的名字</param>
        /// <param name="strWhere">搜索条件</param>
        /// <returns></returns>
        public int GetCount(string tblName, string strWhere)
        {
            return dal.GetCount(tblName, strWhere);
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
            return dal.GetList(tblName, PageSize, PageIndex, strWhere, strOrder);
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
            return dal.GetList(tblName, PageSize, PageIndex, strWhere, strOrder, fldName);
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
            return dal.GetListForMoreTable(tblName, PageSize, PageIndex, strWhere, strOrder, fldName);
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
            return dal.GetList(tblName, strWhere, strOrder, fldName, rowStart, rowEnd);
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
            return dal.GetList(tblName, PageSize, PageIndex, strWhere, strOrder, tblLink, fldLink, conLink);
        }
       
    }
}
