/**  版本信息模板在安装目录下，可自行修改。
* SendEmailRecord_Temp.cs
*
* 功 能： N/A
* 类 名： SendEmailRecord_Temp
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017-10-28 16:29:14   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace ND.FluentTaskScheduling.AddConfirmEmailTask.DAL
{
	/// <summary>
	/// 数据访问类:SendEmailRecord_Temp
	/// </summary>
	public  class SendEmailRecord_TempDAL
	{
        public SendEmailRecord_TempDAL()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "SendEmailRecord_Temp"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SendEmailRecord_Temp");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(ND.FluentTaskScheduling.AddConfirmEmailTask.SendEmailRecord_Temp model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SendEmailRecord_Temp(");
			strSql.Append("OrderId,HotelId,SendTime,ReplyTime,Content,SupplierId,SupplierName,SendPerson,Recipients,IsReply,ReplyResult,Type,IsDel,IsSend,EmailTitle,SendCount,EmailType)");
			strSql.Append(" values (");
			strSql.Append("@OrderId,@HotelId,@SendTime,@ReplyTime,@Content,@SupplierId,@SupplierName,@SendPerson,@Recipients,@IsReply,@ReplyResult,@Type,@IsDel,@IsSend,@EmailTitle,@SendCount,@EmailType)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt,8),
					new SqlParameter("@HotelId", SqlDbType.BigInt,8),
					new SqlParameter("@SendTime", SqlDbType.DateTime),
					new SqlParameter("@ReplyTime", SqlDbType.DateTime),
					new SqlParameter("@Content", SqlDbType.Text),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,100),
					new SqlParameter("@SendPerson", SqlDbType.NVarChar,100),
					new SqlParameter("@Recipients", SqlDbType.NVarChar,100),
					new SqlParameter("@IsReply", SqlDbType.Bit,1),
					new SqlParameter("@ReplyResult", SqlDbType.Bit,1),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@IsDel", SqlDbType.Bit,1),
					new SqlParameter("@IsSend", SqlDbType.Bit,1),
					new SqlParameter("@EmailTitle", SqlDbType.NVarChar,500),
					new SqlParameter("@SendCount", SqlDbType.Int,4),
					new SqlParameter("@EmailType", SqlDbType.Int,4)};
			parameters[0].Value = model.OrderId;
			parameters[1].Value = model.HotelId;
			parameters[2].Value = model.SendTime;
			parameters[3].Value = model.ReplyTime;
			parameters[4].Value = model.Content;
			parameters[5].Value = model.SupplierId;
			parameters[6].Value = model.SupplierName;
			parameters[7].Value = model.SendPerson;
			parameters[8].Value = model.Recipients;
			parameters[9].Value = model.IsReply;
			parameters[10].Value = model.ReplyResult;
			parameters[11].Value = model.Type;
			parameters[12].Value = model.IsDel;
			parameters[13].Value = model.IsSend;
			parameters[14].Value = model.EmailTitle;
			parameters[15].Value = model.SendCount;
			parameters[16].Value = model.EmailType;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		/// 更新一条数据
		/// </summary>
		public bool Update(ND.FluentTaskScheduling.AddConfirmEmailTask.SendEmailRecord_Temp model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SendEmailRecord_Temp set ");
			strSql.Append("OrderId=@OrderId,");
			strSql.Append("HotelId=@HotelId,");
			strSql.Append("SendTime=@SendTime,");
			strSql.Append("ReplyTime=@ReplyTime,");
			strSql.Append("Content=@Content,");
			strSql.Append("SupplierId=@SupplierId,");
			strSql.Append("SupplierName=@SupplierName,");
			strSql.Append("SendPerson=@SendPerson,");
			strSql.Append("Recipients=@Recipients,");
			strSql.Append("IsReply=@IsReply,");
			strSql.Append("ReplyResult=@ReplyResult,");
			strSql.Append("Type=@Type,");
			strSql.Append("IsDel=@IsDel,");
			strSql.Append("IsSend=@IsSend,");
			strSql.Append("EmailTitle=@EmailTitle,");
			strSql.Append("SendCount=@SendCount,");
			strSql.Append("EmailType=@EmailType");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@OrderId", SqlDbType.BigInt,8),
					new SqlParameter("@HotelId", SqlDbType.BigInt,8),
					new SqlParameter("@SendTime", SqlDbType.DateTime),
					new SqlParameter("@ReplyTime", SqlDbType.DateTime),
					new SqlParameter("@Content", SqlDbType.Text),
					new SqlParameter("@SupplierId", SqlDbType.Int,4),
					new SqlParameter("@SupplierName", SqlDbType.NVarChar,100),
					new SqlParameter("@SendPerson", SqlDbType.NVarChar,100),
					new SqlParameter("@Recipients", SqlDbType.NVarChar,100),
					new SqlParameter("@IsReply", SqlDbType.Bit,1),
					new SqlParameter("@ReplyResult", SqlDbType.Bit,1),
					new SqlParameter("@Type", SqlDbType.Int,4),
					new SqlParameter("@IsDel", SqlDbType.Bit,1),
					new SqlParameter("@IsSend", SqlDbType.Bit,1),
					new SqlParameter("@EmailTitle", SqlDbType.NVarChar,500),
					new SqlParameter("@SendCount", SqlDbType.Int,4),
					new SqlParameter("@EmailType", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.OrderId;
			parameters[1].Value = model.HotelId;
			parameters[2].Value = model.SendTime;
			parameters[3].Value = model.ReplyTime;
			parameters[4].Value = model.Content;
			parameters[5].Value = model.SupplierId;
			parameters[6].Value = model.SupplierName;
			parameters[7].Value = model.SendPerson;
			parameters[8].Value = model.Recipients;
			parameters[9].Value = model.IsReply;
			parameters[10].Value = model.ReplyResult;
			parameters[11].Value = model.Type;
			parameters[12].Value = model.IsDel;
			parameters[13].Value = model.IsSend;
			parameters[14].Value = model.EmailTitle;
			parameters[15].Value = model.SendCount;
			parameters[16].Value = model.EmailType;
			parameters[17].Value = model.ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SendEmailRecord_Temp ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			strSql.Append("delete from SendEmailRecord_Temp ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public ND.FluentTaskScheduling.AddConfirmEmailTask.SendEmailRecord_Temp GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,OrderId,HotelId,SendTime,ReplyTime,Content,SupplierId,SupplierName,SendPerson,Recipients,IsReply,ReplyResult,Type,IsDel,IsSend,EmailTitle,SendCount,EmailType from SendEmailRecord_Temp ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			ND.FluentTaskScheduling.AddConfirmEmailTask.SendEmailRecord_Temp model=new ND.FluentTaskScheduling.AddConfirmEmailTask.SendEmailRecord_Temp();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
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
		public ND.FluentTaskScheduling.AddConfirmEmailTask.SendEmailRecord_Temp DataRowToModel(DataRow row)
		{
			ND.FluentTaskScheduling.AddConfirmEmailTask.SendEmailRecord_Temp model=new ND.FluentTaskScheduling.AddConfirmEmailTask.SendEmailRecord_Temp();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["OrderId"]!=null && row["OrderId"].ToString()!="")
				{
					model.OrderId=long.Parse(row["OrderId"].ToString());
				}
				if(row["HotelId"]!=null && row["HotelId"].ToString()!="")
				{
					model.HotelId=long.Parse(row["HotelId"].ToString());
				}
				if(row["SendTime"]!=null && row["SendTime"].ToString()!="")
				{
					model.SendTime=DateTime.Parse(row["SendTime"].ToString());
				}
				if(row["ReplyTime"]!=null && row["ReplyTime"].ToString()!="")
				{
					model.ReplyTime=DateTime.Parse(row["ReplyTime"].ToString());
				}
				if(row["Content"]!=null)
				{
					model.Content=row["Content"].ToString();
				}
				if(row["SupplierId"]!=null && row["SupplierId"].ToString()!="")
				{
					model.SupplierId=int.Parse(row["SupplierId"].ToString());
				}
				if(row["SupplierName"]!=null)
				{
					model.SupplierName=row["SupplierName"].ToString();
				}
				if(row["SendPerson"]!=null)
				{
					model.SendPerson=row["SendPerson"].ToString();
				}
				if(row["Recipients"]!=null)
				{
					model.Recipients=row["Recipients"].ToString();
				}
				if(row["IsReply"]!=null && row["IsReply"].ToString()!="")
				{
					if((row["IsReply"].ToString()=="1")||(row["IsReply"].ToString().ToLower()=="true"))
					{
						model.IsReply=true;
					}
					else
					{
						model.IsReply=false;
					}
				}
				if(row["ReplyResult"]!=null && row["ReplyResult"].ToString()!="")
				{
					if((row["ReplyResult"].ToString()=="1")||(row["ReplyResult"].ToString().ToLower()=="true"))
					{
						model.ReplyResult=true;
					}
					else
					{
						model.ReplyResult=false;
					}
				}
				if(row["Type"]!=null && row["Type"].ToString()!="")
				{
					model.Type=int.Parse(row["Type"].ToString());
				}
				if(row["IsDel"]!=null && row["IsDel"].ToString()!="")
				{
					if((row["IsDel"].ToString()=="1")||(row["IsDel"].ToString().ToLower()=="true"))
					{
						model.IsDel=true;
					}
					else
					{
						model.IsDel=false;
					}
				}
				if(row["IsSend"]!=null && row["IsSend"].ToString()!="")
				{
					if((row["IsSend"].ToString()=="1")||(row["IsSend"].ToString().ToLower()=="true"))
					{
						model.IsSend=true;
					}
					else
					{
						model.IsSend=false;
					}
				}
				if(row["EmailTitle"]!=null)
				{
					model.EmailTitle=row["EmailTitle"].ToString();
				}
				if(row["SendCount"]!=null && row["SendCount"].ToString()!="")
				{
					model.SendCount=int.Parse(row["SendCount"].ToString());
				}
				if(row["EmailType"]!=null && row["EmailType"].ToString()!="")
				{
					model.EmailType=int.Parse(row["EmailType"].ToString());
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
			strSql.Append("select ID,OrderId,HotelId,SendTime,ReplyTime,Content,SupplierId,SupplierName,SendPerson,Recipients,IsReply,ReplyResult,Type,IsDel,IsSend,EmailTitle,SendCount,EmailType ");
			strSql.Append(" FROM SendEmailRecord_Temp ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ID,OrderId,HotelId,SendTime,ReplyTime,Content,SupplierId,SupplierName,SendPerson,Recipients,IsReply,ReplyResult,Type,IsDel,IsSend,EmailTitle,SendCount,EmailType ");
			strSql.Append(" FROM SendEmailRecord_Temp ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM SendEmailRecord_Temp ");
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
			strSql.Append(")AS Row, T.*  from SendEmailRecord_Temp T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "SendEmailRecord_Temp";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

