using System;
namespace Sys.Model
{
    /// <summary>
    /// 实体类AdminLog 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class AdminLog
    {
        public AdminLog()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _username;
        private string _flag;
        private string _log;
        private DateTime _createtime;
        private string _createip;
        /// <summary>
        /// 自动编号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 管理员ＩＤ
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 管理员姓名
        /// </summary>
        public string Username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 模块标记
        /// </summary>
        public string Flag
        {
            set { _flag = value; }
            get { return _flag; }
        }
        /// <summary>
        /// 日志内容
        /// </summary>
        public string Log
        {
            set { _log = value; }
            get { return _log; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 操作ＩＰ
        /// </summary>
        public string CreateIP
        {
            set { _createip = value; }
            get { return _createip; }
        }
        #endregion Model

    }
}

