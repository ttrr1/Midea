using System;
namespace Sys.Model
{
    /// <summary>
    /// 实体类AdminFlag 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class AdminFlag
    {
        public AdminFlag()
        { }
        #region Model
        private int _id;
        private int _parentid;
        private int _isnav;
        private bool _havechildnav;
        private string _flag;
        private string _flagname;
        private string _flagaction;
        private int _flaggroup;
        private int _flagtype;
        private string _appurl;
        private int _orderid;
        private string _icon;
        private bool _isopen;
        private DateTime _createtime;
        /// <summary>
        /// 模块ＩＤ
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 上级ＩＤ
        /// </summary>
        public int ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 是否导航
        /// </summary>
        public int IsNav
        {
            set { _isnav = value; }
            get { return _isnav; }
        }
        /// <summary>
        /// 是否有子导航
        /// </summary>
        public bool HaveChildNav
        {
            set { _havechildnav = value; }
            get { return _havechildnav; }
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
        /// 模块名称
        /// </summary>
        public string FlagName
        {
            set { _flagname = value; }
            get { return _flagname; }
        }
        /// <summary>
        /// 模块行为
        /// </summary>
        public string FlagAction
        {
            set { _flagaction = value; }
            get { return _flagaction; }
        }
        /// <summary>
        /// 模块分组
        /// </summary>
        public int FlagGroup
        {
            set { _flaggroup = value; }
            get { return _flaggroup; }
        }
        /// <summary>
        /// 模块类别 0系统 1站点 2内容
        /// </summary>
        public int FlagType
        {
            set { _flagtype = value; }
            get { return _flagtype; }
        }
        /// <summary>
        /// 程序地址
        /// </summary>
        public string AppUrl
        {
            set { _appurl = value; }
            get { return _appurl; }
        }
        /// <summary>
        /// 排列顺序
        /// </summary>
        public int OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// ICON图标
        /// </summary>
        public string Icon
        {
            set { _icon = value; }
            get { return _icon; }
        }
        /// <summary>
        /// 是否展开子模块
        /// </summary>
        public bool IsOpen
        {
            set { _isopen = value; }
            get { return _isopen; }
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

