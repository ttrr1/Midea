using System;
namespace Sys.Model
{

    /// <summary>
    /// 实体类SysIpBlock 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SysIpBlock
    {
        public SysIpBlock()
        { }
        #region Model
        private int _id;
        private long _ipstart;
        private long _ipend;
        private string _name;
        private int _blocktype;
        private int _blockmodule;
        private DateTime _createtime;
        /// <summary>
        /// 自动编号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// IP开始值
        /// </summary>
        public long IpStart
        {
            set { _ipstart = value; }
            get { return _ipstart; }
        }
        /// <summary>
        /// IP结束值
        /// </summary>
        public long IpEnd
        {
            set { _ipend = value; }
            get { return _ipend; }
        }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 限制方式 0白名单 1黑名单
        /// </summary>
        public int BlockType
        {
            set { _blocktype = value; }
            get { return _blocktype; }
        }
        /// <summary>
        /// 限制模块 0系统 1用户
        /// </summary>
        public int BlockModule
        {
            set { _blockmodule = value; }
            get { return _blockmodule; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        #endregion Model

    }
}

