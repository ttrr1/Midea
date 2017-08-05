using System;
namespace Sys.Model
{
	/// <summary>
	/// 信息分类信息表
	/// </summary>
	[Serializable]
	public partial class NewsType
	{
		public NewsType()
		{}
		#region Model
		private int _id;
		private int _pid=0;
		private string _typename="";
		private DateTime _createtime= DateTime.Now;
		private string _createip="";
		private int _count=0;
		private string _typeaction="";
		/// <summary>
		/// 自增长编号
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 分类上级编号
		/// </summary>
		public int PId
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 信息分类名称
		/// </summary>
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
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
		public string CreateIp
		{
			set{ _createip=value;}
			get{return _createip;}
		}
		/// <summary>
		/// 该分类下面的信息数统计
		/// </summary>
		public int Count
		{
			set{ _count=value;}
			get{return _count;}
		}
		/// <summary>
		/// 分组标记
		/// </summary>
		public string TypeAction
		{
			set{ _typeaction=value;}
			get{return _typeaction;}
		}
		#endregion Model

	}
}

