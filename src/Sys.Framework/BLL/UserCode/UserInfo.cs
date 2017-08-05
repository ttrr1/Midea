using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using Sys.Common;
using Sys.Model;

namespace Sys.BLL
{
    public partial class UserInfo
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys.Model.UserInfo model, string password)
        {
            return dal.Add(model, password);
        }

        /// <summary>
        /// 区域信息初始化
        /// </summary>
        /// <param name="lisProvice"></param>
        /// <param name="lisCity"></param>
        /// <param name="lisArea"></param>
        public void AreaInit(out List<Sys.Model.DicModel> lisProvice, out List<Sys.Model.DicModel> lisCity, out List<Sys.Model.DicModel> lisArea)
        {
            lisProvice = new List<DicModel>();
            lisCity = new List<DicModel>();
            lisArea = new List<DicModel>();

            XmlNodeList Provice = XmlHelper.GetXmlNodeList(HttpContext.Current.Server.MapPath("../ConfigData/CityData.xml"), "/items/module[@key='Province_js']");
            foreach (XmlNode node in Provice)
            {
                lisProvice.Add(new DicModel() { Key = node.Attributes["id"].Value, Value = node.Attributes["value"].Value });
            }
            XmlNodeList City = XmlHelper.GetXmlNodeList(HttpContext.Current.Server.MapPath("../ConfigData/CityData.xml"), "/items/module/subModule[@key='City_sz']");
            foreach (XmlNode node in City)
            {
                lisCity.Add(new DicModel() { Key = node.Attributes["id"].Value, Value = node.Attributes["value"].Value });
            }
            XmlNodeList Area = XmlHelper.GetXmlNodeList(HttpContext.Current.Server.MapPath("../ConfigData/CityData.xml"), "/items/module/subModule[@key='City_sz']/item");
            foreach (XmlNode node in Area)
            {
                lisArea.Add(new DicModel() { Key = node.Attributes["id"].Value, Value = node.InnerText });
            }
        }

        public DataTable GetUserInfoById(int id)
        {
            return dal.GetUserInfoById(id);
        }

        /// <summary>
        /// 返回一个数据表  List的前身
        /// </summary>
        public DataTable GetListForMoreTable(int pageSize, int pageIndex, string strWhere, string order)
        {
            var db = new DAL.Common();
            var condition = "u.userid=m.userid";
            if (!string.IsNullOrEmpty(strWhere))
                condition += " and " + strWhere;
            var ds = db.GetListForMoreTable("UserInfo u,member m", pageSize, pageIndex, condition, order, "u.*,m.State");
            return ds.Tables[0];
        }
    }
}
