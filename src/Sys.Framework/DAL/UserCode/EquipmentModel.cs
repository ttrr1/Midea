using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Sys.Common;

namespace Sys.DAL
{
    public partial class EquipmentModel
    {
        /// <summary>
        /// 获取型号
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataTable GetEqpModelInfo(string strWhere)
        {
            var sqlStr = @"SELECT  e1.* ,
        e2.ModelName as parentModelName
FROM    dbo.EquipmentModel e1
        LEFT JOIN dbo.EquipmentModel e2 ON e1.ParentModelId = e2.ModelId
WHERE   1 = 1";
            if (!string.IsNullOrEmpty(strWhere))
            {
                sqlStr += " AND " + strWhere;
            }
            return DbHelperSQL.Query(sqlStr).Tables[0];
        }
    }
}
