using System;
namespace Psd.H5Show.Model
{
	/// <summary>
	/// appbase:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class appbase
	{
		public appbase()
		{}
		#region Model
		private Int32 _id;
		private Int32 _uid;
		private string _appname;
		private string _bgmusic;
		private string _bgimg;
		private DateTime _createtime;
		private string _appdescribe;
		private bool _delflag;
		private string _appurl;
		private string _memo1;
		private string _memo6;
		private string _memo5;
		private string _memo4;
		private string _memo2;
		private string _memo3;
		/// <summary>
		/// 
		/// </summary>
		public Int32 ID
		{
			set{ _id=value;}
			get{return _id;}
		}
        /// <summary>
        /// 用户id
        /// </summary>
        public Int32 Uid
		{
			set{ _uid=value;}
			get{return _uid;}
		}
        /// <summary>
        /// 场景名称
        /// </summary>
        public string AppName
		{
			set{ _appname=value;}
			get{return _appname;}
		}
        /// <summary>
        /// 歌曲 空：表示无音乐
        /// </summary>
        public string BgMusic
		{
			set{ _bgmusic=value;}
			get{return _bgmusic;}
		}
        /// <summary>
        /// 背景图片 空：表示无背景
        /// </summary>
        public string BgImg
		{
			set{ _bgimg=value;}
			get{return _bgimg;}
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
        /// 场景说明
        /// </summary>
        public string AppDescribe
		{
			set{ _appdescribe=value;}
			get{return _appdescribe;}
		}
        /// <summary>
        /// 删除标识 0：正常 1：删除 默认：0
        /// </summary>
        public bool DelFlag
		{
			set{ _delflag=value;}
			get{return _delflag;}
		}
        /// <summary>
        /// 当前场景网络路径 如：http://*.5umovie.com/m/****
        /// </summary>
        public string AppUrl
		{
			set{ _appurl=value;}
			get{return _appurl;}
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
		public string Memo6
		{
			set{ _memo6=value;}
			get{return _memo6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo5
		{
			set{ _memo5=value;}
			get{return _memo5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo4
		{
			set{ _memo4=value;}
			get{return _memo4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo2
		{
			set{ _memo2=value;}
			get{return _memo2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo3
		{
			set{ _memo3=value;}
			get{return _memo3;}
		}
		#endregion Model

	}
}

