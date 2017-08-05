using System;
namespace Sys.Model
{
	/// <summary>
	/// 管理员
	/// </summary>
	[Serializable]
	public partial class Admin
	{
		public Admin()
		{}
		#region Model
		private int _userid=0;
		private string _username="";
		private string _userflag="";
		private string _roleids="";
		private string _rolenames="";
		private string _roleflags="";
		private string _plusflag="";
		private string _realname="";
		private string _jobtitle="";
		private string _jobdept="";
		private string _qq="";
		private string _msn="";
		private string _email="";
		private string _mobile="";
		private string _hometel="";
		private string _officetel="";
		private int _isfounder=0;
		private int _state=1;
		private string _guid= "newid";
		private DateTime _createtime= DateTime.Now;
		private string _createip="";
		private int _parentuserid=-1;
		private string _parentuserids="";
		private int _ispublic=0;

     
		/// <summary>
		/// 用户ID
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string Username
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 用户模块(角色模块+附加模块)
		/// </summary>
		public string UserFlag
		{
			set{ _userflag=value;}
			get{return _userflag;}
		}
		/// <summary>
		/// 角色ID
		/// </summary>
		public string RoleIDs
		{
			set{ _roleids=value;}
			get{return _roleids;}
		}
		/// <summary>
		/// 角色名
		/// </summary>
		public string RoleNames
		{
			set{ _rolenames=value;}
			get{return _rolenames;}
		}
		/// <summary>
		/// 角色模块
		/// </summary>
		public string RoleFlags
		{
			set{ _roleflags=value;}
			get{return _roleflags;}
		}
		/// <summary>
		/// 附加模块
		/// </summary>
		public string PlusFlag
		{
			set{ _plusflag=value;}
			get{return _plusflag;}
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
		/// 职位名称
		/// </summary>
		public string JobTitle
		{
			set{ _jobtitle=value;}
			get{return _jobtitle;}
		}
		/// <summary>
		/// 职位部门
		/// </summary>
		public string JobDept
		{
			set{ _jobdept=value;}
			get{return _jobdept;}
		}
		/// <summary>
		/// ＱＱ
		/// </summary>
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		/// <summary>
		/// ＭＳＮ
		/// </summary>
		public string MSN
		{
			set{ _msn=value;}
			get{return _msn;}
		}
		/// <summary>
		/// 电子邮箱
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 手机
		/// </summary>
		public string Mobile
		{
			set{ _mobile=value;}
			get{return _mobile;}
		}
		/// <summary>
		/// 家庭电话
		/// </summary>
		public string HomeTel
		{
			set{ _hometel=value;}
			get{return _hometel;}
		}
		/// <summary>
		/// 办公电话
		/// </summary>
		public string OfficeTel
		{
			set{ _officetel=value;}
			get{return _officetel;}
		}
		/// <summary>
		/// 是否是创始人
		/// </summary>
		public int IsFounder
		{
			set{ _isfounder=value;}
			get{return _isfounder;}
		}
		/// <summary>
		/// 帐户状态 0锁定 1正常
		/// </summary>
		public int State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// ＧＵＩＤ
		/// </summary>
		public string Guid
		{
			set{ _guid=value;}
			get{return _guid;}
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
		/// 创建IP
		/// </summary>
		public string CreateIP
		{
			set{ _createip=value;}
			get{return _createip;}
		}
		/// <summary>
		/// 父ID
		/// </summary>
		public int ParentUserID
		{
			set{ _parentuserid=value;}
			get{return _parentuserid;}
		}
		/// <summary>
		/// 父权限集合(上一级的全部集合)
		/// </summary>
		public string ParentUserIDs
		{
			set{ _parentuserids=value;}
			get{return _parentuserids;}
		}
		/// <summary>
		/// 是否公开(1公开，0不公开)
		/// </summary>
		public int IsPublic
		{
			set{ _ispublic=value;}
			get{return _ispublic;}
		}
		#endregion Model

	}
}

