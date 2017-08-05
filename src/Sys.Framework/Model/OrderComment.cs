using System;
namespace Sys.Model
{
	/// <summary>
	/// OrderComment:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OrderComment
	{
		public OrderComment()
		{}
        #region Model
        private int _commentid;
        private int? _orderid;
        private string _starlevel;
        private string _remark;
        private string _createuserid;
        private DateTime? _createdate;
        /// <summary>
        /// 
        /// </summary>
        public int CommentId
        {
            set { _commentid = value; }
            get { return _commentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StarLevel
        {
            set { _starlevel = value; }
            get { return _starlevel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateUserId
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        #endregion Model

	}
}

