using System;
namespace Psd.H5Show.Model.Tools
{
	/// <summary>
	/// user_account:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Messge
    {
		public Messge()
		{}
		#region Model
		private int _result;
		private string _msg;
		/// <summary>
		/// 结果说明 1:成功 2：失败
		/// </summary>
		public int Result
		{
			set{ _result = value;}
			get{return _result; }
		}
        /// <summary>
        /// 提示说明 
        /// </summary>
        public string Msg
		{
			set{ _msg = value;}
			get{return _msg; }
		}
#endregion
    }
}
