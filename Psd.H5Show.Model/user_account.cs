using System;
namespace Psd.H5Show.Model
{
	/// <summary>
	/// user_account:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class user_account
	{
		public user_account()
		{}
		#region Model
		private Int32 _id;
		private string _usercode;
		private string _username;
		private string _password;
		private string _state= "1";
		private DateTime _createtime= Convert.ToDateTime(DateTime.Now);
        private DateTime? _modifytime = Convert.ToDateTime(DateTime.Now);
        private DateTime? _logintime = Convert.ToDateTime(DateTime.Now);
		private string _loginip;
		private bool _delflag= false;
		private string _memo1;
		private string _memo2;
		/// <summary>
		/// auto_increment
		/// </summary>
		public Int32 ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserCode
		{
			set{ _usercode=value;}
			get{return _usercode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string State
		{
			set{ _state=value;}
			get{return _state;}
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
		public DateTime? ModifyTime
		{
			set{ _modifytime=value;}
			get{return _modifytime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LoginTime
		{
			set{ _logintime=value;}
			get{return _logintime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LoginIp
		{
			set{ _loginip=value;}
			get{return _loginip;}
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

