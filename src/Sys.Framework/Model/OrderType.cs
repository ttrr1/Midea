using System;
namespace Sys.Model
{
	/// <summary>
	/// OrderType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OrderType
	{
		public OrderType()
		{}
        #region Model
        private int _typeid;
        private int? _orderid;
        private int? _producttype;
        private string _producttypename;
        private int? _mainmodelid;
        private string _mainmodelname;
        private int? _mainmodelnum;
        private int? _submodelid1;
        private string _submodelname1;
        private int? _submodelnum1;
        private int? _submodelid2;
        private string _submodelname2;
        private int? _submodelnum2;
        private int? _submodelid3;
        private string _submodelname3;
        private int? _submodelnum3;
        private int? _submodelid4;
        private string _submodelname4;
        private int? _submodelnum4;
        private int? _modelid;
        private string _modelname;
        private int? _modelnum;
        private string _createuserid;
        private DateTime? _createdate;
        private string _modifyuserid;
        private DateTime? _modifydate;
        /// <summary>
        /// 
        /// </summary>
        public int TypeId
        {
            set { _typeid = value; }
            get { return _typeid; }
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
        public int? ProductType
        {
            set { _producttype = value; }
            get { return _producttype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ProductTypeName
        {
            set { _producttypename = value; }
            get { return _producttypename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MainModelId
        {
            set { _mainmodelid = value; }
            get { return _mainmodelid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MainModelName
        {
            set { _mainmodelname = value; }
            get { return _mainmodelname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? MainModelNum
        {
            set { _mainmodelnum = value; }
            get { return _mainmodelnum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SubModelId1
        {
            set { _submodelid1 = value; }
            get { return _submodelid1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubModelName1
        {
            set { _submodelname1 = value; }
            get { return _submodelname1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SubModelNum1
        {
            set { _submodelnum1 = value; }
            get { return _submodelnum1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SubModelId2
        {
            set { _submodelid2 = value; }
            get { return _submodelid2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubModelName2
        {
            set { _submodelname2 = value; }
            get { return _submodelname2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SubModelNum2
        {
            set { _submodelnum2 = value; }
            get { return _submodelnum2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SubModelId3
        {
            set { _submodelid3 = value; }
            get { return _submodelid3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubModelName3
        {
            set { _submodelname3 = value; }
            get { return _submodelname3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SubModelNum3
        {
            set { _submodelnum3 = value; }
            get { return _submodelnum3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SubModelId4
        {
            set { _submodelid4 = value; }
            get { return _submodelid4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SubModelName4
        {
            set { _submodelname4 = value; }
            get { return _submodelname4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SubModelNum4
        {
            set { _submodelnum4 = value; }
            get { return _submodelnum4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ModelId
        {
            set { _modelid = value; }
            get { return _modelid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ModelName
        {
            set { _modelname = value; }
            get { return _modelname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ModelNum
        {
            set { _modelnum = value; }
            get { return _modelnum; }
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

