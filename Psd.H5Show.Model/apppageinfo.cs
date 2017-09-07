using System;
namespace Psd.H5Show.Model
{
	/// <summary>
	/// apppageinfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class apppageinfo
	{
		public apppageinfo()
		{}
		#region Model
		private Int32 _id;
		private Int32 _pageid;
		private string _kittype;
		private Int32 _kitlinktype;
		private string _kitlinkurl;
		private double _kitanduration;
		private double _kitandelay;
		private string _kitaneffect;
		private string _kitanglez;
		private string _kitstyle;
		private string _kitleft;
		private string _kittop;
		private string _kitwidth;
		private string _kitheight;
		private string _kitcontent;
		/// <summary>
		/// 唯一编号
		/// </summary>
		public Int32 ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 页面编号
		/// </summary>
		public Int32 PageId
		{
			set{ _pageid=value;}
			get{return _pageid;}
		}
		/// <summary>
		/// 组件类型
		/// </summary>
		public string KitType
		{
			set{ _kittype=value;}
			get{return _kittype;}
		}
		/// <summary>
		/// 组件连接类型 0：内连接  1：外连接
		/// </summary>
		public Int32 KitLinkType
		{
			set{ _kitlinktype=value;}
			get{return _kitlinktype;}
		}
        /// <summary>
        /// 连接地址  如果：KitLinkType=0 该值为pageid 如果
        /// </summary>
        public string KitLinkUrl
		{
			set{ _kitlinkurl=value;}
			get{return _kitlinkurl;}
		}
		/// <summary>
		/// 动画持续时间
		/// </summary>
		public double KitAnDuration
		{
			set{ _kitanduration=value;}
			get{return _kitanduration;}
		}
        /// <summary>
        /// 动画延迟执行时间
        /// </summary>
        public double KitAnDelay
		{
			set{ _kitandelay=value;}
			get{return _kitandelay;}
		}
        /// <summary>
        /// 动画方式
        /// </summary>
        public string KitAnEffect
		{
			set{ _kitaneffect=value;}
			get{return _kitaneffect;}
		}
        /// <summary>
        /// 元素旋转角度
        /// </summary>
        public string KitAngleZ
		{
			set{ _kitanglez=value;}
			get{return _kitanglez;}
		}
        /// <summary>
        /// 元素样式
        /// </summary>
        public string KitStyle
		{
			set{ _kitstyle=value;}
			get{return _kitstyle;}
		}
        /// <summary>
        /// 元素左间距
        /// </summary>
        public string KitLeft
		{
			set{ _kitleft=value;}
			get{return _kitleft;}
		}
        /// <summary>
        /// 元素上间距
        /// </summary>
        public string KitTop
		{
			set{ _kittop=value;}
			get{return _kittop;}
		}
        /// <summary>
        /// 元素宽度
        /// </summary>
        public string KitWidth
		{
			set{ _kitwidth=value;}
			get{return _kitwidth;}
		}
        /// <summary>
        /// 元素高度
        /// </summary>
        public string KitHeight
		{
			set{ _kitheight=value;}
			get{return _kitheight;}
		}
        /// <summary>
        /// 元素html代码
        /// </summary>
        public string KitContent
		{
			set{ _kitcontent=value;}
			get{return _kitcontent;}
		}
		#endregion Model

	}
}

