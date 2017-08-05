using System;
namespace Sys.Model
{
	/// <summary>
	/// 用户信息表
	/// </summary>
	[Serializable]
	public partial class UserInfo
	{
		public UserInfo()
		{}
		#region Model
		private int _id;
		private int _userid;
		private string _usercode;
		private string _username="";
		private string _realname="";
		private DateTime _createtime= DateTime.Now;
		private int _sex=1;
		private string _address="";
		private string _introduction="";
		private string _smsmobile="";
		private string _mobile="";
		private int _sid=0;
		private int _user_rid=1;
		private int _motionsend=0;
		private int _cid;
		private int _gid;
		private DateTime _enrollmenttime= DateTime.Now;
		private string _logo="";
		private string _companyname;
		private int? _provinceid;
		private string _provincename;
		private int? _cityid;
		private string _cityname;
		private int? _areaid;
		private string _areaname;
		private int? _sareaid;
		private string _sareaname;
		private string _contact;
		private string _typekey;
		private string _typevalue;
		private int? _roleid;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 用户名(打卡，卡号)
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 真实姓名
		/// </summary>
		public string RealName
		{
			set{ _realname=value;}
			get{return _realname;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// (1男:2女)
		/// </summary>
		public int Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 联系地址
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 自我介绍
		/// </summary>
		public string Introduction
		{
			set{ _introduction=value;}
			get{return _introduction;}
		}
		/// <summary>
		/// 短信接收号码
		/// </summary>
		public string SMSMobile
		{
			set{ _smsmobile=value;}
			get{return _smsmobile;}
		}
		/// <summary>
		/// 联系电话
		/// </summary>
		public string Mobile
		{
			set{ _mobile=value;}
			get{return _mobile;}
		}
		/// <summary>
		/// 学校编号
		/// </summary>
		public int SId
		{
			set{ _sid=value;}
			get{return _sid;}
		}
		/// <summary>
		/// 1.学校管理员2.老师3.学生
		/// </summary>
		public int User_RId
		{
			set{ _user_rid=value;}
			get{return _user_rid;}
		}
		/// <summary>
		/// 0不启用 1:启用
		/// </summary>
		public int MotionSend
		{
			set{ _motionsend=value;}
			get{return _motionsend;}
		}
		/// <summary>
		/// 班级名称
		/// </summary>
		public int CId
		{
			set{ _cid=value;}
			get{return _cid;}
		}
		/// <summary>
		/// 所属年级
		/// </summary>
		public int GId
		{
			set{ _gid=value;}
			get{return _gid;}
		}
		/// <summary>
		/// 入学时间
		/// </summary>
		public DateTime EnrollmentTime
		{
			set{ _enrollmenttime=value;}
			get{return _enrollmenttime;}
		}
		/// <summary>
		/// 头像
		/// </summary>
		public string Logo
		{
			set{ _logo=value;}
			get{return _logo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CompanyName
		{
			set{ _companyname=value;}
			get{return _companyname;}
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
		public string contact
		{
			set{ _contact=value;}
			get{return _contact;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeKey
		{
			set{ _typekey=value;}
			get{return _typekey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeValue
		{
			set{ _typevalue=value;}
			get{return _typevalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? RoleId
		{
			set{ _roleid=value;}
			get{return _roleid;}
		}
		#endregion Model

	}
}

