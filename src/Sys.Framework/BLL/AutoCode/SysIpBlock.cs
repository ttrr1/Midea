using System;
using System.Data;
using System.Collections.Generic;
using Sys.Common;
using Sys.Model;
namespace Sys.BLL
{
    /// <summary>
    /// 业务逻辑类SysIpBlock 的摘要说明。
    /// </summary>
    public class SysIpBlock
    {
        private readonly Sys.DAL.SysIpBlock dal = new Sys.DAL.SysIpBlock();
        public SysIpBlock()
        { }
        #region  成员方法

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {

            dal.Delete(ID);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Sys.Model.SysIpBlock model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(Sys.Model.SysIpBlock model)
        {
            dal.Update(model);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.SysIpBlock GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Sys.Model.SysIpBlock GetModelByCache(int ID)
        {

            string CacheKey = "SysIpBlockModel-" + ID;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("DataCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Sys.Model.SysIpBlock)objModel;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="BlockModule">限制模块 0系统 1用户</param>
        /// <param name="BlockType">限制方式 0白名单 1黑名单</param>
        /// <param name="IPstr">当前IP</param>
        /// <returns></returns>
        public bool Exists(int BlockModule, int BlockType, string IPstr)
        {
        
     
          
           
            long IP = Sys.Common.IP.BlockIP.CalcIP(IPstr);
            string strWhere = "BlockModule=" + BlockModule + " and BlockType=" + BlockType + " and (" + IP + " BETWEEN IpStart and IpEnd)";
            DAL.Common db = new DAL.Common();
            if (db.GetCount("SysIpBlock", strWhere) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 检查是否重复重名
        /// </summary>
        public bool CheckName(int ID, string Name, int BlockType, int BlockModule)
        {
            string strWhere = "BlockModule=" + BlockModule + " and BlockType=" + BlockType + " and Name='" + Utils.SqlStringFormat(Name,1) + "'";
            if (ID > 0)
                strWhere += "and ID<>" + ID;
            DAL.Common db = new DAL.Common();
            if (db.GetCount("SysIpBlock", strWhere) > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="BlockModule">限制模块 0系统 1用户</param>
        /// <param name="BlockType">限制方式 0白名单 1黑名单</param>
        /// <returns></returns>
        public List<Sys.Model.SysIpBlock> GetList(int BlockModule,int BlockType)
        {
            string strWhere = "BlockModule=" + BlockModule + " and BlockType=" + BlockType;
            string strOrder = "ID desc";
            int PageSize = -1;
            int PageIndex = -1;
            DAL.Common db = new DAL.Common();
            DataSet ds = db.GetList("SysIpBlock", PageSize, PageIndex, strWhere, strOrder);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表，从缓存中。
        /// </summary>
        /// <param name="BlockModule">限制模块 0系统 1用户</param>
        /// <param name="BlockType">限制方式 0白名单 1黑名单</param>
        /// <returns></returns>
        public List<Sys.Model.SysIpBlock> GetListByCache(int BlockModule, int BlockType)
        {

            string CacheKey = "SysIpBlockList-" + BlockModule + "-" + BlockType;
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetList(BlockModule, BlockType);
                    if (objModel != null)
                    {
                        int ModelCache = ConfigHelper.GetConfigInt("DataCache");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Sys.Model.SysIpBlock>)objModel;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Sys.Model.SysIpBlock> DataTableToList(DataTable dt)
        {
            List<Sys.Model.SysIpBlock> modelList = new List<Sys.Model.SysIpBlock>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Sys.Model.SysIpBlock model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Sys.Model.SysIpBlock();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    model.IpStart = long.Parse(dt.Rows[n]["IpStart"].ToString());
                    model.IpEnd = long.Parse(dt.Rows[n]["IpEnd"].ToString());
                    model.Name = dt.Rows[n]["Name"].ToString();
                    if (dt.Rows[n]["BlockType"].ToString() != "")
                    {
                        model.BlockType = int.Parse(dt.Rows[n]["BlockType"].ToString());
                    }
                    if (dt.Rows[n]["BlockModule"].ToString() != "")
                    {
                        model.BlockModule = int.Parse(dt.Rows[n]["BlockModule"].ToString());
                    }
                    if (dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }



        #endregion  成员方法


        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="blockModule">限制模块 0系统 1用户</param>
        /// <param name="blockType">限制方式 0白名单 1黑名单</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public DataTable GetTable(int pageSize, int pageIndex, string strWhere, string strOrder)
        {
            var db = new DAL.Common();
            var ds = db.GetList("SysIpBlock", pageSize, pageIndex, strWhere, strOrder);
            return ds.Tables[0];
        }
    }
}

