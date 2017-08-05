using System;
namespace Sys.Model
{
	/// <summary>
	/// 帐户
	/// </summary>
	[Serializable]
	public partial class Account
	{
		public Account()
		{}
		#region Model
		private int _userid;
		private string _username="";
		private string _password="";
		private DateTime _createtime= DateTime.Now;
		private string _createip="";
		private string _guid= "newid";
	
		/// <summary>
		/// 帐户编号
		/// </summary>
		public int UserID
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
		/// 密码
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
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
		/// GUID
		/// </summary>
		public string Guid
		{
			set{ _guid=value;}
			get{return _guid;}
		}
		
		#endregion Model

	}
}

