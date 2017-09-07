using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using  Psd.H5Show.DBUtility;//Please add references
namespace Psd.H5Show.DAL
{
	/// <summary>
	/// 数据访问类:apppagebase
	/// </summary>
	public partial class apppagebase
	{
		public apppagebase()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Int32 ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from apppagebase");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,20)			};
			parameters[0].Value = ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Psd.H5Show.Model.apppagebase model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into apppagebase(");
			strSql.Append("ID,AppId,Title,OrderBy,CreateTime,DelFlag,Memo1,Memo2)");
			strSql.Append(" values (");
			strSql.Append("@ID,@AppId,@Title,@OrderBy,@CreateTime,@DelFlag,@Memo1,@Memo2)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,20),
					new MySqlParameter("@AppId", MySqlDbType.Int32,20),
					new MySqlParameter("@Title", MySqlDbType.VarChar,128),
					new MySqlParameter("@OrderBy", MySqlDbType.Int32,20),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@DelFlag", MySqlDbType.Bit),
					new MySqlParameter("@Memo1", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo2", MySqlDbType.VarChar,128)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.AppId;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.OrderBy;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.DelFlag;
			parameters[6].Value = model.Memo1;
			parameters[7].Value = model.Memo2;

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
		public bool Update(Psd.H5Show.Model.apppagebase model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update apppagebase set ");
			strSql.Append("AppId=@AppId,");
			strSql.Append("Title=@Title,");
			strSql.Append("OrderBy=@OrderBy,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("DelFlag=@DelFlag,");
			strSql.Append("Memo1=@Memo1,");
			strSql.Append("Memo2=@Memo2");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@AppId", MySqlDbType.Int32,20),
					new MySqlParameter("@Title", MySqlDbType.VarChar,128),
					new MySqlParameter("@OrderBy", MySqlDbType.Int32,20),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@DelFlag", MySqlDbType.Bit),
					new MySqlParameter("@Memo1", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo2", MySqlDbType.VarChar,128),
					new MySqlParameter("@ID", MySqlDbType.Int32,20)};
			parameters[0].Value = model.AppId;
			parameters[1].Value = model.Title;
			parameters[2].Value = model.OrderBy;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.DelFlag;
			parameters[5].Value = model.Memo1;
			parameters[6].Value = model.Memo2;
			parameters[7].Value = model.ID;

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
			strSql.Append("delete from apppagebase ");
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
			strSql.Append("delete from apppagebase ");
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
		public Psd.H5Show.Model.apppagebase GetModel(Int32 ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,AppId,Title,OrderBy,CreateTime,DelFlag,Memo1,Memo2 from apppagebase ");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,20)			};
			parameters[0].Value = ID;

			Psd.H5Show.Model.apppagebase model=new Psd.H5Show.Model.apppagebase();
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
		public Psd.H5Show.Model.apppagebase DataRowToModel(DataRow row)
		{
			Psd.H5Show.Model.apppagebase model=new Psd.H5Show.Model.apppagebase();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=Int32.Parse(row["ID"].ToString());
				}
				if(row["AppId"]!=null && row["AppId"].ToString()!="")
				{
					model.AppId=Int32.Parse(row["AppId"].ToString());
				}
				if(row["Title"]!=null)
				{
					model.Title=row["Title"].ToString();
				}
				if(row["OrderBy"]!=null && row["OrderBy"].ToString()!="")
				{
					model.OrderBy=Int32.Parse(row["OrderBy"].ToString());
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
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
				if(row["Memo1"]!=null)
				{
					model.Memo1=row["Memo1"].ToString();
				}
				if(row["Memo2"]!=null)
				{
					model.Memo2=row["Memo2"].ToString();
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
			strSql.Append("select ID,AppId,Title,OrderBy,CreateTime,DelFlag,Memo1,Memo2 ");
			strSql.Append(" FROM apppagebase ");
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
			strSql.Append("select count(1) FROM apppagebase ");
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
			strSql.Append(")AS Row, T.*  from apppagebase T ");
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
			parameters[0].Value = "apppagebase";
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

