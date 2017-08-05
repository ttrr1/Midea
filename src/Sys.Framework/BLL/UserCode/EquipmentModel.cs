using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sys.BLL
{
    public partial class EquipmentModel
    {
        /// <summary>
        /// 获取类型信息
        /// </summary>
        /// <param name="dtParent"></param>
        /// <param name="dtSub"></param>
        public void GetEqpInfo(out DataTable dtParent, out DataTable dtSub)
        {
            dtParent = dal.GetList("ParentModelId=0").Tables[0];
            dtSub = dal.GetList("ParentModelId<>0").Tables[0];
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.EquipmentModel> GetList(int pageSize, int pageIndex, string strWhere, string strOrder)
        {
            var db = new DAL.Common();
            var ds = db.GetList("EquipmentModel", pageSize, pageIndex, strWhere, strOrder);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获取型号
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetEqpModelInfo(string strWhere)
        {
            return dal.GetEqpModelInfo(strWhere);
        }

    }
}
