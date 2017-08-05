using System;
namespace Sys.Model
{
	/// <summary>
	/// Services:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Services
	{
		public Services()
		{}
		#region Model
		private int _id;
		private string _name="";
		private string _picpath="";
		private string _linkaddress="";
		private int? _orderid=1;
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Picpath
		{
			set{ _picpath=value;}
			get{return _picpath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string linkAddress
		{
			set{ _linkaddress=value;}
			get{return _linkaddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? OrderId
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		#endregion Model

	}
}

