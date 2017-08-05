using System;
namespace Sys.Model
{
    /// <summary>
    /// 实体类AdminRole 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class AdminRole
    {
        public AdminRole()
        { }
        #region Model
        private int _roleid;
        private string _rolename;
        private string _roleflag;
        private int _adminnum;
        private string _note;
        private int _orderid;
        private DateTime _createtime;
        private DateTime _updatetime;
        /// <summary>
        /// 角色ＩＤ
        /// </summary>
        public int RoleID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            set { _rolename = value; }
            get { return _rolename; }
        }
        /// <summary>
        /// 角色模块
        /// </summary>
        public string RoleFlag
        {
            set { _roleflag = value; }
            get { return _roleflag; }
        }
        /// <summary>
        /// 人数
        /// </summary>
        public int AdminNum
        {
            set { _adminnum = value; }
            get { return _adminnum; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note
        {
            set { _note = value; }
            get { return _note; }
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
        /// 添加时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        #endregion Model

    }
}

