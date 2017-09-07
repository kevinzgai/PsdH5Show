using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using  Psd.H5Show.DBUtility;//Please add references
namespace Psd.H5Show.DAL
{
	/// <summary>
	/// 数据访问类:apppageinfo
	/// </summary>
	public partial class apppageinfo
	{
		public apppageinfo()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Int32 ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from apppageinfo");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,20)			};
			parameters[0].Value = ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Psd.H5Show.Model.apppageinfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into apppageinfo(");
			strSql.Append("ID,PageId,KitType,KitLinkType,KitLinkUrl,KitAnDuration,KitAnDelay,KitAnEffect,KitAngleZ,KitStyle,KitLeft,KitTop,KitWidth,KitHeight,KitContent)");
			strSql.Append(" values (");
			strSql.Append("@ID,@PageId,@KitType,@KitLinkType,@KitLinkUrl,@KitAnDuration,@KitAnDelay,@KitAnEffect,@KitAngleZ,@KitStyle,@KitLeft,@KitTop,@KitWidth,@KitHeight,@KitContent)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,20),
					new MySqlParameter("@PageId", MySqlDbType.Int32,20),
					new MySqlParameter("@KitType", MySqlDbType.VarChar,20),
					new MySqlParameter("@KitLinkType", MySqlDbType.Int32,20),
					new MySqlParameter("@KitLinkUrl", MySqlDbType.VarChar,128),
					new MySqlParameter("@KitAnDuration", MySqlDbType.Float),
					new MySqlParameter("@KitAnDelay", MySqlDbType.Float),
					new MySqlParameter("@KitAnEffect", MySqlDbType.VarChar,32),
					new MySqlParameter("@KitAngleZ", MySqlDbType.VarChar,32),
					new MySqlParameter("@KitStyle", MySqlDbType.VarChar,32),
					new MySqlParameter("@KitLeft", MySqlDbType.VarChar,32),
					new MySqlParameter("@KitTop", MySqlDbType.VarChar,32),
					new MySqlParameter("@KitWidth", MySqlDbType.VarChar,32),
					new MySqlParameter("@KitHeight", MySqlDbType.VarChar,32),
					new MySqlParameter("@KitContent", MySqlDbType.Text)};
			parameters[0].Value = model.ID;
			parameters[1].Value = model.PageId;
			parameters[2].Value = model.KitType;
			parameters[3].Value = model.KitLinkType;
			parameters[4].Value = model.KitLinkUrl;
			parameters[5].Value = model.KitAnDuration;
			parameters[6].Value = model.KitAnDelay;
			parameters[7].Value = model.KitAnEffect;
			parameters[8].Value = model.KitAngleZ;
			parameters[9].Value = model.KitStyle;
			parameters[10].Value = model.KitLeft;
			parameters[11].Value = model.KitTop;
			parameters[12].Value = model.KitWidth;
			parameters[13].Value = model.KitHeight;
			parameters[14].Value = model.KitContent;

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
		public bool Update(Psd.H5Show.Model.apppageinfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update apppageinfo set ");
			strSql.Append("PageId=@PageId,");
			strSql.Append("KitType=@KitType,");
			strSql.Append("KitLinkType=@KitLinkType,");
			strSql.Append("KitLinkUrl=@KitLinkUrl,");
			strSql.Append("KitAnDuration=@KitAnDuration,");
			strSql.Append("KitAnDelay=@KitAnDelay,");
			strSql.Append("KitAnEffect=@KitAnEffect,");
			strSql.Append("KitAngleZ=@KitAngleZ,");
			strSql.Append("KitStyle=@KitStyle,");
			strSql.Append("KitLeft=@KitLeft,");
			strSql.Append("KitTop=@KitTop,");
			strSql.Append("KitWidth=@KitWidth,");
			strSql.Append("KitHeight=@KitHeight,");
			strSql.Append("KitContent=@KitContent");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@PageId", MySqlDbType.Int32,20),
					new MySqlParameter("@KitType", MySqlDbType.VarChar,20),
					new MySqlParameter("@KitLinkType", MySqlDbType.Int32,20),
					new MySqlParameter("@KitLinkUrl", MySqlDbType.VarChar,128),
					new MySqlParameter("@KitAnDuration", MySqlDbType.Float),
					new MySqlParameter("@KitAnDelay", MySqlDbType.Float),
					new MySqlParameter("@KitAnEffect", MySqlDbType.VarChar,32),
					new MySqlParameter("@KitAngleZ", MySqlDbType.VarChar,32),
					new MySqlParameter("@KitStyle", MySqlDbType.VarChar,32),
					new MySqlParameter("@KitLeft", MySqlDbType.VarChar,32),
					new MySqlParameter("@KitTop", MySqlDbType.VarChar,32),
					new MySqlParameter("@KitWidth", MySqlDbType.VarChar,32),
					new MySqlParameter("@KitHeight", MySqlDbType.VarChar,32),
					new MySqlParameter("@KitContent", MySqlDbType.Text),
					new MySqlParameter("@ID", MySqlDbType.Int32,20)};
			parameters[0].Value = model.PageId;
			parameters[1].Value = model.KitType;
			parameters[2].Value = model.KitLinkType;
			parameters[3].Value = model.KitLinkUrl;
			parameters[4].Value = model.KitAnDuration;
			parameters[5].Value = model.KitAnDelay;
			parameters[6].Value = model.KitAnEffect;
			parameters[7].Value = model.KitAngleZ;
			parameters[8].Value = model.KitStyle;
			parameters[9].Value = model.KitLeft;
			parameters[10].Value = model.KitTop;
			parameters[11].Value = model.KitWidth;
			parameters[12].Value = model.KitHeight;
			parameters[13].Value = model.KitContent;
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
			strSql.Append("delete from apppageinfo ");
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
			strSql.Append("delete from apppageinfo ");
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
		public Psd.H5Show.Model.apppageinfo GetModel(Int32 ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,PageId,KitType,KitLinkType,KitLinkUrl,KitAnDuration,KitAnDelay,KitAnEffect,KitAngleZ,KitStyle,KitLeft,KitTop,KitWidth,KitHeight,KitContent from apppageinfo ");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32,20)			};
			parameters[0].Value = ID;

			Psd.H5Show.Model.apppageinfo model=new Psd.H5Show.Model.apppageinfo();
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
		public Psd.H5Show.Model.apppageinfo DataRowToModel(DataRow row)
		{
			Psd.H5Show.Model.apppageinfo model=new Psd.H5Show.Model.apppageinfo();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=Int32.Parse(row["ID"].ToString());
				}
				if(row["PageId"]!=null && row["PageId"].ToString()!="")
				{
					model.PageId=Int32.Parse(row["PageId"].ToString());
				}
				if(row["KitType"]!=null)
				{
					model.KitType=row["KitType"].ToString();
				}
				if(row["KitLinkType"]!=null && row["KitLinkType"].ToString()!="")
				{
					model.KitLinkType=Int32.Parse(row["KitLinkType"].ToString());
				}
				if(row["KitLinkUrl"]!=null)
				{
					model.KitLinkUrl=row["KitLinkUrl"].ToString();
				}
				if(row["KitAnDuration"]!=null && row["KitAnDuration"].ToString()!="")
				{
					model.KitAnDuration=double.Parse(row["KitAnDuration"].ToString());
				}
				if(row["KitAnDelay"]!=null && row["KitAnDelay"].ToString()!="")
				{
					model.KitAnDelay=double.Parse(row["KitAnDelay"].ToString());
				}
				if(row["KitAnEffect"]!=null)
				{
					model.KitAnEffect=row["KitAnEffect"].ToString();
				}
				if(row["KitAngleZ"]!=null)
				{
					model.KitAngleZ=row["KitAngleZ"].ToString();
				}
				if(row["KitStyle"]!=null)
				{
					model.KitStyle=row["KitStyle"].ToString();
				}
				if(row["KitLeft"]!=null)
				{
					model.KitLeft=row["KitLeft"].ToString();
				}
				if(row["KitTop"]!=null)
				{
					model.KitTop=row["KitTop"].ToString();
				}
				if(row["KitWidth"]!=null)
				{
					model.KitWidth=row["KitWidth"].ToString();
				}
				if(row["KitHeight"]!=null)
				{
					model.KitHeight=row["KitHeight"].ToString();
				}
				if(row["KitContent"]!=null)
				{
					model.KitContent=row["KitContent"].ToString();
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
			strSql.Append("select ID,PageId,KitType,KitLinkType,KitLinkUrl,KitAnDuration,KitAnDelay,KitAnEffect,KitAngleZ,KitStyle,KitLeft,KitTop,KitWidth,KitHeight,KitContent ");
			strSql.Append(" FROM apppageinfo ");
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
			strSql.Append("select count(1) FROM apppageinfo ");
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
			strSql.Append(")AS Row, T.*  from apppageinfo T ");
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
			parameters[0].Value = "apppageinfo";
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

