using System;
namespace Sys.Model
{
	/// <summary>
	/// OrderStatusFlow:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OrderStatusFlow
	{
		public OrderStatusFlow()
		{}
        #region Model
        private int _flowid;
        private int? _orderid;
        private string _orderstatus;
        private string _statusmessage;
        private int? _statusflag;
        private string _createuserid;
        private DateTime? _createdate;
        private string _modifyuserid;
        private DateTime? _modifydate;
        /// <summary>
        /// 
        /// </summary>
        public int FlowId
        {
            set { _flowid = value; }
            get { return _flowid; }
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
        public string OrderStatus
        {
            set { _orderstatus = value; }
            get { return _orderstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StatusMessage
        {
            set { _statusmessage = value; }
            get { return _statusmessage; }
        }
        /// <summary>
        /// 0进行中,1已完成
        /// </summary>
        public int? StatusFlag
        {
            set { _statusflag = value; }
            get { return _statusflag; }
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
        /// <summary>
        /// 
        /// </summary>
        public string ModifyUserId
        {
            set { _modifyuserid = value; }
            get { return _modifyuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ModifyDate
        {
            set { _modifydate = value; }
            get { return _modifydate; }
        }
        #endregion Model

	}
}

