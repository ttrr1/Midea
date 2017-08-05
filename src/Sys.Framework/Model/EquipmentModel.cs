using System;
namespace Sys.Model
{
	/// <summary>
	/// EquipmentModel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class EquipmentModel
	{
		public EquipmentModel()
		{}
        #region Model
        private int _modelid;
        private string _modelname;
        private int? _eqptype;
        private int? _parentmodelid;
        private string _createuserid;
        private DateTime? _createdate;
        private string _modifyuserid;
        private DateTime? _modifydate;
        private string _status;
        /// <summary>
        /// 
        /// </summary>
        public int ModelId
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
        /// 1主机,2内机
        /// </summary>
        public int? EqpType
        {
            set { _eqptype = value; }
            get { return _eqptype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ParentModelId
        {
            set { _parentmodelid = value; }
            get { return _parentmodelid; }
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
        /// <summary>
        /// 
        /// </summary>
        public string Status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model

	}
}

