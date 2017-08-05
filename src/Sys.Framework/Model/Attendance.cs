using System;
namespace Sys.Model
{
	/// <summary>
	/// 考勤表
	/// </summary>
	[Serializable]
	public partial class Attendance
	{
		public Attendance()
		{}
		#region Model
		private int _id;
		private string _iccode="";
		private int _sid=0;
		private DateTime _createtime= DateTime.Now;
		private string _createip="";
		private int _mendsign;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 考勤卡号
		/// </summary>
		public string ICcode
		{
			set{ _iccode=value;}
			get{return _iccode;}
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
		/// 
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// ''
		/// </summary>
		public string CreateIp
		{
			set{ _createip=value;}
			get{return _createip;}
		}
		/// <summary>
		/// 是否补签(0:正常打卡 1：补签)
		/// </summary>
		public int MendSign
		{
			set{ _mendsign=value;}
			get{return _mendsign;}
		}
		#endregion Model

	}
}

