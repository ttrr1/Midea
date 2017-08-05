using System;
namespace Sys.Model
{
	/// <summary>
	/// 学生表现信息表
	/// </summary>
	[Serializable]
	public partial class Expression
	{
		public Expression()
		{}
		#region Model
		private int _id;
		private DateTime _createtime= DateTime.Now;
		private int _sid=0;
		private int _stuid=0;
		private int _gid=0;
		private string _cid= "0";
		private string _content="";
		private string _f_userid= "0";
		private string _f_realname="";
		private string _createip="";
		private int _sendname=0;
		private int _sendstates=0;
		private int _sendsuccess=0;
		private int _typeid=0;
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
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SId
		{
			set{ _sid=value;}
			get{return _sid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int StuId
		{
			set{ _stuid=value;}
			get{return _stuid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int GId
		{
			set{ _gid=value;}
			get{return _gid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CId
		{
			set{ _cid=value;}
			get{return _cid;}
		}
		/// <summary>
		/// 表现内容
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 发布人编号
		/// </summary>
		public string F_UserId
		{
			set{ _f_userid=value;}
			get{return _f_userid;}
		}
		/// <summary>
		/// 发布人姓名
		/// </summary>
		public string F_RealName
		{
			set{ _f_realname=value;}
			get{return _f_realname;}
		}
		/// <summary>
		/// 创建IP
		/// </summary>
		public string CreateIp
		{
			set{ _createip=value;}
			get{return _createip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SendName
		{
			set{ _sendname=value;}
			get{return _sendname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SendStates
		{
			set{ _sendstates=value;}
			get{return _sendstates;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int SendSuccess
		{
			set{ _sendsuccess=value;}
			get{return _sendsuccess;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TypeID
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		#endregion Model

	}
}

