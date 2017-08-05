using System;
namespace Sys.Model
{
    /// <summary>
    /// 实体类SysConfig 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SysConfig
    {
        public SysConfig()
        { }
        #region Model
        private string _item;
        private string _key;
        private string _name;
        private string _value;
        private int _orderid;
        private int _isusing;
        private string _note;
        /// <summary>
        /// 说明
        /// </summary>
        public string Note
        {
            set { _note = value; }
            get { return _note; }
        }
        /// <summary>
        /// 项目类别
        /// </summary>
        public string Item
        {
            set { _item = value; }
            get { return _item; }
        }
        /// <summary>
        /// 键名
        /// </summary>
        public string Key
        {
            set { _key = value; }
            get { return _key; }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 数值
        /// </summary>
        public string Value
        {
            set { _value = value; }
            get { return _value; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsUsing
        {
            set { _isusing = value; }
            get { return _isusing; }
        }
        #endregion Model

    }
}

