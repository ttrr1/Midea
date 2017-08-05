using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Sys.Common;//工具类
namespace Sys.DAL
{
    /// <summary>
    /// 数据访问类SysIpBlock。
    /// </summary>
    public class SysIpBlock
    {
        public SysIpBlock()
        { }
        #region  成员方法

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            DbHelperSQL.RunProcedure("SysIpBlock_Delete", parameters, out rowsAffected);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Sys.Model.SysIpBlock GetModel(int ID)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = ID;

            var model = new Sys.Model.SysIpBlock();
            DataSet ds = DbHelperSQL.RunProcedure("SysIpBlock_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IpStart"].ToString() != "")
                {
                    model.IpStart = long.Parse(ds.Tables[0].Rows[0]["IpStart"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IpEnd"].ToString() != "")
                {
                    model.IpEnd = long.Parse(ds.Tables[0].Rows[0]["IpEnd"].ToString());
                }
                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                if (ds.Tables[0].Rows[0]["BlockType"].ToString() != "")
                {
                    model.BlockType = int.Parse(ds.Tables[0].Rows[0]["BlockType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BlockModule"].ToString() != "")
                {
                    model.BlockModule = int.Parse(ds.Tables[0].Rows[0]["BlockModule"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        ///  增加一条数据
        /// </summary>
        public int Add(Sys.Model.SysIpBlock model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@IpStart", SqlDbType.BigInt,8),
					new SqlParameter("@IpEnd", SqlDbType.BigInt,8),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@BlockType", SqlDbType.Int,4),
					new SqlParameter("@BlockModule", SqlDbType.Int,4)};
            parameters[0].Direction = ParameterDirection.Output;
            parameters[1].Value = model.IpStart;
            parameters[2].Value = model.IpEnd;
            parameters[3].Value = model.Name;
            parameters[4].Value = model.BlockType;
            parameters[5].Value = model.BlockModule;

            DbHelperSQL.RunProcedure("SysIpBlock_ADD", parameters, out rowsAffected);
            return (int)parameters[0].Value;
        }

        /// <summary>
        ///  更新一条数据
        /// </summary>
        public void Update(Sys.Model.SysIpBlock model)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@IpStart", SqlDbType.BigInt,8),
					new SqlParameter("@IpEnd", SqlDbType.BigInt,8),
					new SqlParameter("@Name", SqlDbType.NVarChar,50),
					new SqlParameter("@BlockType", SqlDbType.Int,4),
					new SqlParameter("@BlockModule", SqlDbType.Int,4)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.IpStart;
            parameters[2].Value = model.IpEnd;
            parameters[3].Value = model.Name;
            parameters[4].Value = model.BlockType;
            parameters[5].Value = model.BlockModule;

            DbHelperSQL.RunProcedure("SysIpBlock_Update", parameters, out rowsAffected);
        }


        
        #endregion  成员方法
    }
}

