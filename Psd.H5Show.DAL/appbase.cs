using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using  Psd.H5Show.DBUtility;//Please add references
namespace Psd.H5Show.DAL
{
	/// <summary>
	/// 数据访问类:appbase
	/// </summary>
	public partial class appbase
	{
		public appbase()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Int32 ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from appbase");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,20)			};
			parameters[0].Value = ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Psd.H5Show.Model.appbase model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into appbase(");
			strSql.Append("ID,Uid,AppName,BgMusic,BgImg,CreateTime,AppDescribe,DelFlag,AppUrl,Memo1,Memo6,Memo5,Memo4,Memo2,Memo3)");
			strSql.Append(" values (");
			strSql.Append("@ID,@Uid,@AppName,@BgMusic,@BgImg,@CreateTime,@AppDescribe,@DelFlag,@AppUrl,@Memo1,@Memo6,@Memo5,@Memo4,@Memo2,@Memo3)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,20),
					new MySqlParameter("@Uid", MySqlDbType.Int32,20),
					new MySqlParameter("@AppName", MySqlDbType.VarString,50),
					new MySqlParameter("@BgMusic", MySqlDbType.VarChar,128),
					new MySqlParameter("@BgImg", MySqlDbType.VarChar,128),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@AppDescribe", MySqlDbType.VarChar,500),
					new MySqlParameter("@DelFlag", MySqlDbType.Bit),
					new MySqlParameter("@AppUrl", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo1", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo6", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo5", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo4", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo2", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo3", MySqlDbType.VarChar,128)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.Uid;
			parameters[2].Value = model.AppName;
			parameters[3].Value = model.BgMusic;
			parameters[4].Value = model.BgImg;
			parameters[5].Value = DateTime.Now;
			parameters[6].Value = model.AppDescribe;
			parameters[7].Value = model.DelFlag;
			parameters[8].Value = model.AppUrl;
			parameters[9].Value = model.Memo1;
			parameters[10].Value = model.Memo6;
			parameters[11].Value = model.Memo5;
			parameters[12].Value = model.Memo4;
			parameters[13].Value = model.Memo2;
			parameters[14].Value = model.Memo3;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Psd.H5Show.Model.appbase model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update appbase set ");
			strSql.Append("Uid=@Uid,");
			strSql.Append("AppName=@AppName,");
			strSql.Append("BgMusic=@BgMusic,");
			strSql.Append("BgImg=@BgImg,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("AppDescribe=@AppDescribe,");
			strSql.Append("DelFlag=@DelFlag,");
			strSql.Append("AppUrl=@AppUrl,");
			strSql.Append("Memo1=@Memo1,");
			strSql.Append("Memo6=@Memo6,");
			strSql.Append("Memo5=@Memo5,");
			strSql.Append("Memo4=@Memo4,");
			strSql.Append("Memo2=@Memo2,");
			strSql.Append("Memo3=@Memo3");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@Uid", MySqlDbType.Int32,20),
					new MySqlParameter("@AppName", MySqlDbType.VarChar,50),
					new MySqlParameter("@BgMusic", MySqlDbType.VarChar,128),
					new MySqlParameter("@BgImg", MySqlDbType.VarChar,128),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@AppDescribe", MySqlDbType.VarChar,500),
					new MySqlParameter("@DelFlag", MySqlDbType.Bit),
					new MySqlParameter("@AppUrl", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo1", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo6", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo5", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo4", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo2", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo3", MySqlDbType.VarChar,128),
					new MySqlParameter("@ID", MySqlDbType.Int32,20)};
			parameters[0].Value = model.Uid;
			parameters[1].Value = model.AppName;
			parameters[2].Value = model.BgMusic;
			parameters[3].Value = model.BgImg;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.AppDescribe;
			parameters[6].Value = model.DelFlag;
			parameters[7].Value = model.AppUrl;
			parameters[8].Value = model.Memo1;
			parameters[9].Value = model.Memo6;
			parameters[10].Value = model.Memo5;
			parameters[11].Value = model.Memo4;
			parameters[12].Value = model.Memo2;
			parameters[13].Value = model.Memo3;
			parameters[14].Value = model.ID;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(Int32 ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from appbase ");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,20)			};
			parameters[0].Value = ID;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from appbase ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Psd.H5Show.Model.appbase GetModel(Int32 ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,Uid,AppName,BgMusic,BgImg,CreateTime,AppDescribe,DelFlag,AppUrl,Memo1,Memo6,Memo5,Memo4,Memo2,Memo3 from appbase ");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,20)			};
			parameters[0].Value = ID;

			Psd.H5Show.Model.appbase model=new Psd.H5Show.Model.appbase();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Psd.H5Show.Model.appbase DataRowToModel(DataRow row)
		{
			Psd.H5Show.Model.appbase model=new Psd.H5Show.Model.appbase();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=Int32.Parse(row["ID"].ToString());
				}
				if(row["Uid"]!=null && row["Uid"].ToString()!="")
				{
					model.Uid=Int32.Parse(row["Uid"].ToString());
				}
				if(row["AppName"]!=null)
				{
					model.AppName=row["AppName"].ToString();
				}
				if(row["BgMusic"]!=null)
				{
					model.BgMusic=row["BgMusic"].ToString();
				}
				if(row["BgImg"]!=null)
				{
					model.BgImg=row["BgImg"].ToString();
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
				}
				if(row["AppDescribe"]!=null)
				{
					model.AppDescribe=row["AppDescribe"].ToString();
				}
				if(row["DelFlag"]!=null && row["DelFlag"].ToString()!="")
				{
					if((row["DelFlag"].ToString()=="1")||(row["DelFlag"].ToString().ToLower()=="true"))
					{
						model.DelFlag=true;
					}
					else
					{
						model.DelFlag=false;
					}
				}
				if(row["AppUrl"]!=null)
				{
					model.AppUrl=row["AppUrl"].ToString();
				}
				if(row["Memo1"]!=null)
				{
					model.Memo1=row["Memo1"].ToString();
				}
				if(row["Memo6"]!=null)
				{
					model.Memo6=row["Memo6"].ToString();
				}
				if(row["Memo5"]!=null)
				{
					model.Memo5=row["Memo5"].ToString();
				}
				if(row["Memo4"]!=null)
				{
					model.Memo4=row["Memo4"].ToString();
				}
				if(row["Memo2"]!=null)
				{
					model.Memo2=row["Memo2"].ToString();
				}
				if(row["Memo3"]!=null)
				{
					model.Memo3=row["Memo3"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,Uid,AppName,BgMusic,BgImg,CreateTime,AppDescribe,DelFlag,AppUrl,Memo1,Memo6,Memo5,Memo4,Memo2,Memo3 ");
			strSql.Append(" FROM appbase ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM appbase ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from appbase T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			MySqlParameter[] parameters = {
					new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@PageSize", MySqlDbType.Int32),
					new MySqlParameter("@PageIndex", MySqlDbType.Int32),
					new MySqlParameter("@IsReCount", MySqlDbType.Bit),
					new MySqlParameter("@OrderType", MySqlDbType.Bit),
					new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
					};
			parameters[0].Value = "appbase";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

