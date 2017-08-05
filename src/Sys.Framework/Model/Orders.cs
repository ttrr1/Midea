using System;
namespace Sys.Model
{
	/// <summary>
	/// Orders:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Orders
	{
		public Orders()
		{}
		#region Model
		private int _orderid;
		private string _userloginid;
		private string _workerid;
		private string _projectname;
		private string _customname;
		private int? _provinceid;
		private string _provincename;
		private int? _cityid;
		private string _cityname;
		private int? _areaid;
		private string _areaname;
		private int? _sareaid;
		private string _sareaname;
		private string _address;
		private string _contact;
		private string _tel;
		private DateTime? _handledate;
		private string _orderstatus;
		private int? _statusflag;
		private string _remark;
		private string _piclist;
		private string _createuserid;
		private DateTime? _createdate;
		private string _modifyuserid;
		private DateTime? _modifydate;
		/// <summary>
		/// 经销商编号
		/// </summary>
		public int OrderId
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserLoginId
		{
			set{ _userloginid=value;}
			get{return _userloginid;}
		}
		/// <summary>
		/// 工人编号
		/// </summary>
		public string WorkerId
		{
			set{ _workerid=value;}
			get{return _workerid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProjectName
		{
			set{ _projectname=value;}
			get{return _projectname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CustomName
		{
			set{ _customname=value;}
			get{return _customname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ProvinceId
		{
			set{ _provinceid=value;}
			get{return _provinceid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProvinceName
		{
			set{ _provincename=value;}
			get{return _provincename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CityId
		{
			set{ _cityid=value;}
			get{return _cityid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CityName
		{
			set{ _cityname=value;}
			get{return _cityname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AreaId
		{
			set{ _areaid=value;}
			get{return _areaid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AreaName
		{
			set{ _areaname=value;}
			get{return _areaname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? sAreaId
		{
			set{ _sareaid=value;}
			get{return _sareaid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sAreaName
		{
			set{ _sareaname=value;}
			get{return _sareaname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Contact
		{
			set{ _contact=value;}
			get{return _contact;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tel
		{
			set{ _tel=value;}
			get{return _tel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? HandleDate
		{
			set{ _handledate=value;}
			get{return _handledate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OrderStatus
		{
			set{ _orderstatus=value;}
			get{return _orderstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? StatusFlag
		{
			set{ _statusflag=value;}
			get{return _statusflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PicList
		{
			set{ _piclist=value;}
			get{return _piclist;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreateUserId
		{
			set{ _createuserid=value;}
			get{return _createuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ModifyUserId
		{
			set{ _modifyuserid=value;}
			get{return _modifyuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ModifyDate
		{
			set{ _modifydate=value;}
			get{return _modifydate;}
		}
		#endregion Model

	}
}

