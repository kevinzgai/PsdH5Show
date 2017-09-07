using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using  Psd.H5Show.DBUtility;//Please add references
namespace Psd.H5Show.DAL
{
	/// <summary>
	/// 数据访问类:user_account
	/// </summary>
	public partial class user_account
	{
		public user_account()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(Int32 ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from user_account");
			strSql.Append(" where ID=@ID");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32)
			};
			parameters[0].Value = ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Psd.H5Show.Model.user_account model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into user_account(");
			strSql.Append("UserCode,UserName,Password,State,CreateTime,ModifyTime,LoginTime,LoginIp,DelFlag,Memo1,Memo2)");
			strSql.Append(" values (");
			strSql.Append("@UserCode,@UserName,@Password,@State,@CreateTime,@ModifyTime,@LoginTime,@LoginIp,@DelFlag,@Memo1,@Memo2)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@UserCode", MySqlDbType.VarChar,20),
					new MySqlParameter("@UserName", MySqlDbType.VarChar,32),
					new MySqlParameter("@Password", MySqlDbType.VarChar,128),
					new MySqlParameter("@State", MySqlDbType.VarChar,1),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@ModifyTime", MySqlDbType.DateTime),
					new MySqlParameter("@LoginTime", MySqlDbType.DateTime),
					new MySqlParameter("@LoginIp", MySqlDbType.VarChar,16),
					new MySqlParameter("@DelFlag", MySqlDbType.Bit),
					new MySqlParameter("@Memo1", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo2", MySqlDbType.VarChar,128)};
			parameters[0].Value = model.UserCode;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.Password;
			parameters[3].Value = model.State;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.ModifyTime;
			parameters[6].Value = model.LoginTime;
			parameters[7].Value = model.LoginIp;
			parameters[8].Value = model.DelFlag;
			parameters[9].Value = model.Memo1;
			parameters[10].Value = model.Memo2;

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
		public bool Update(Psd.H5Show.Model.user_account model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update user_account set ");
			strSql.Append("UserCode=@UserCode,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("Password=@Password,");
			strSql.Append("State=@State,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("ModifyTime=@ModifyTime,");
			strSql.Append("LoginTime=@LoginTime,");
			strSql.Append("LoginIp=@LoginIp,");
			strSql.Append("DelFlag=@DelFlag,");
			strSql.Append("Memo1=@Memo1,");
			strSql.Append("Memo2=@Memo2");
			strSql.Append(" where ID=@ID");
			MySqlParameter[] parameters = {
					new MySqlParameter("@UserCode", MySqlDbType.VarChar,20),
					new MySqlParameter("@UserName", MySqlDbType.VarChar,32),
					new MySqlParameter("@Password", MySqlDbType.VarChar,128),
					new MySqlParameter("@State", MySqlDbType.VarChar,1),
					new MySqlParameter("@CreateTime", MySqlDbType.DateTime),
					new MySqlParameter("@ModifyTime", MySqlDbType.DateTime),
					new MySqlParameter("@LoginTime", MySqlDbType.DateTime),
					new MySqlParameter("@LoginIp", MySqlDbType.VarChar,16),
					new MySqlParameter("@DelFlag", MySqlDbType.Bit),
					new MySqlParameter("@Memo1", MySqlDbType.VarChar,128),
					new MySqlParameter("@Memo2", MySqlDbType.VarChar,128),
					new MySqlParameter("@ID", MySqlDbType.Int32,20)};
			parameters[0].Value = model.UserCode;
			parameters[1].Value = model.UserName;
			parameters[2].Value = model.Password;
			parameters[3].Value = model.State;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.ModifyTime;
			parameters[6].Value = model.LoginTime;
			parameters[7].Value = model.LoginIp;
			parameters[8].Value = model.DelFlag;
			parameters[9].Value = model.Memo1;
			parameters[10].Value = model.Memo2;
			parameters[11].Value = model.ID;

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
			strSql.Append("delete from user_account ");
			strSql.Append(" where ID=@ID");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32)
			};
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
			strSql.Append("delete from user_account ");
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
		public Psd.H5Show.Model.user_account GetModel(Int32 ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,UserCode,UserName,Password,State,CreateTime,ModifyTime,LoginTime,LoginIp,DelFlag,Memo1,Memo2 from user_account ");
			strSql.Append(" where ID=@ID");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int32)
			};
			parameters[0].Value = ID;

			Psd.H5Show.Model.user_account model=new Psd.H5Show.Model.user_account();
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
		public Psd.H5Show.Model.user_account DataRowToModel(DataRow row)
		{
			Psd.H5Show.Model.user_account model=new Psd.H5Show.Model.user_account();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=Int32.Parse(row["ID"].ToString());
				}
				if(row["UserCode"]!=null)
				{
					model.UserCode=row["UserCode"].ToString();
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["Password"]!=null)
				{
					model.Password=row["Password"].ToString();
				}
				if(row["State"]!=null)
				{
					model.State=row["State"].ToString();
				}
				if(row["CreateTime"]!=null && row["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(row["CreateTime"].ToString());
				}
				if(row["ModifyTime"]!=null && row["ModifyTime"].ToString()!="")
				{
					model.ModifyTime=DateTime.Parse(row["ModifyTime"].ToString());
				}
				if(row["LoginTime"]!=null && row["LoginTime"].ToString()!="")
				{
					model.LoginTime=DateTime.Parse(row["LoginTime"].ToString());
				}
				if(row["LoginIp"]!=null)
				{
					model.LoginIp=row["LoginIp"].ToString();
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
			strSql.Append("select ID,UserCode,UserName,Password,State,CreateTime,ModifyTime,LoginTime,LoginIp,DelFlag,Memo1,Memo2 ");
			strSql.Append(" FROM user_account ");
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
			strSql.Append("select count(1) FROM user_account ");
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
			strSql.Append(")AS Row, T.*  from user_account T ");
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
			parameters[0].Value = "user_account";
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

