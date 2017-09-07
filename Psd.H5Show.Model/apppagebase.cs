using System;
namespace Psd.H5Show.Model
{
	/// <summary>
	/// apppagebase:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class apppagebase
	{
		public apppagebase()
		{}
		#region Model
		private Int32 _id;
		private Int32 _appid;
		private string _title;
		private Int32 _orderby;
		private DateTime _createtime;
		private bool _delflag;
		private string _memo1;
		private string _memo2;
		/// <summary>
		/// 
		/// </summary>
		public Int32 ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Int32 AppId
		{
			set{ _appid=value;}
			get{return _appid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Int32 OrderBy
		{
			set{ _orderby=value;}
			get{return _orderby;}
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
		public bool DelFlag
		{
			set{ _delflag=value;}
			get{return _delflag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo1
		{
			set{ _memo1=value;}
			get{return _memo1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo2
		{
			set{ _memo2=value;}
			get{return _memo2;}
		}
		#endregion Model

	}
}

