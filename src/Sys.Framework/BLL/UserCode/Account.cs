using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using Sys.Common;

namespace Sys.BLL
{
    public partial class Account
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public DataTable UserLogin(string userId, string password)
        {
            var ds = dal.GetUserInfo(userId, password);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 根据手机号获取用户信息
        /// </summary>
        /// <param name="userLoginId"></param>
        /// <returns></returns>
        public DataTable GetUserInfoByUserLoginId(string userLoginId)
        {
            var ds = dal.GetList(userLoginId);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 根据手机号获取用户编号
        /// </summary>
        /// <param name="userLoginId"></param>
        /// <returns></returns>
        public int? GetUserIdByUserLoginId(string userLoginId)
        {
            var ds = dal.GetList(userLoginId);
            if (ds != null && ds.Tables.Count > 0)
            {
                var dt = ds.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[0]["UserID"]);
                }
            }
            return null;
        }

        /// <summary>
        /// 获取经销商和安装工人类型
        /// </summary>
        /// <param name="lisAgentType"></param>
        /// <param name="lisInstallerTypeList"></param>
        public void RegisterInit(out List<Sys.Model.DicModel> lisAgentType, out List<Sys.Model.DicModel> lisInstallerTypeList)
        {
            lisAgentType = new List<Sys.Model.DicModel>();
            lisInstallerTypeList = new List<Sys.Model.DicModel>();
            XmlNodeList agentTypeList = XmlHelper.GetXmlNodeList(HttpContext.Current.Server.MapPath("../ConfigData/TypeData.xml"), "/items/module[@key='AgentType']/item");
            foreach (XmlNode node in agentTypeList)
            {
                lisAgentType.Add(new Sys.Model.DicModel() { Key = node.Attributes["key"].Value, Value = node.InnerText });
            }

            XmlNodeList installerTypeList = XmlHelper.GetXmlNodeList(HttpContext.Current.Server.MapPath("../ConfigData/TypeData.xml"), "/items/module[@key='InstallerType']/item");
            foreach (XmlNode node in installerTypeList)
            {
                lisInstallerTypeList.Add(new Sys.Model.DicModel() { Key = node.Attributes["key"].Value, Value = node.InnerText });
            }
        }

        /// <summary>
        /// token验证
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool CheckToken(string token)
        {
            if (ConfigurationSettings.AppSettings["tokenEnable"] == "1")
            {
                if (!string.IsNullOrEmpty(token))
                {
                    var userId = token.Split('|')[0];
                    var pwd = token.Split('|')[1];
                    var ds = dal.GetUserInfo(userId, pwd);
                    if (ds!=null && ds.Tables.Count>0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            return true;
        }


    }
}
