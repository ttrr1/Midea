using System;
namespace Sys.Model
{
	/// <summary>
	/// 内容信息表
	/// </summary>
	[Serializable]
	public partial class News
	{
		public News()
		{}
		#region Model
		private int _id;
		private DateTime _createtime= DateTime.Now;
		private DateTime _updatetime= DateTime.Now;
		private string _createip="";
		private string _updateip="";
		private string _newstitle="";
		private string _abstract="";
		private string _content="";
		private int _typeid=0;
		private string _guid= "newid";
		private string _publisher="";
		private string _source="";
		private int _totalclick=0;
		private string _pic="";
		private int _isaudit=0;
		private int _isaddname=0;
		private int _userid;
		/// <summary>
		/// 自动增长编号,程序用
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
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
		/// 最新时间
		/// </summary>
		public DateTime UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 创建Ip
		/// </summary>
		public string CreateIp
		{
			set{ _createip=value;}
			get{return _createip;}
		}
		/// <summary>
		/// 更新IP
		/// </summary>
		public string UpdateIp
		{
			set{ _updateip=value;}
			get{return _updateip;}
		}
		/// <summary>
		/// 信息标题
		/// </summary>
		public string NewsTitle
		{
			set{ _newstitle=value;}
			get{return _newstitle;}
		}
		/// <summary>
		/// 信息摘要
		/// </summary>
		public string Abstract
		{
			set{ _abstract=value;}
			get{return _abstract;}
		}
		/// <summary>
		/// 信息内容
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 信息分类编号
		/// </summary>
		public int TypeId
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// GUID
		/// </summary>
		public string GUID
		{
			set{ _guid=value;}
			get{return _guid;}
		}
		/// <summary>
		/// 发布人
		/// </summary>
		public string Publisher
		{
			set{ _publisher=value;}
			get{return _publisher;}
		}
		/// <summary>
		/// 信息来源
		/// </summary>
		public string Source
		{
			set{ _source=value;}
			get{return _source;}
		}
		/// <summary>
		/// 点击率统计
		/// </summary>
		public int TotalClick
		{
			set{ _totalclick=value;}
			get{return _totalclick;}
		}
		/// <summary>
		/// 图片地址
		/// </summary>
		public string Pic
		{
			set{ _pic=value;}
			get{return _pic;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsAudit
		{
			set{ _isaudit=value;}
			get{return _isaudit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsAddName
		{
			set{ _isaddname=value;}
			get{return _isaddname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		#endregion Model

	}
}

