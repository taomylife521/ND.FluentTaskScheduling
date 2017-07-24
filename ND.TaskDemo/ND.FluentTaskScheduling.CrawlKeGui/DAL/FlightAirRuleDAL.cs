using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using ND.FluentTaskScheduling.CrawlKeGui.DAL;
namespace Maticsoft.DAL
{
	/// <summary>
	/// 数据访问类:FlightAirRule
	/// </summary>
	public partial class FlightAirRuleDAL
	{
		public FlightAirRuleDAL()
		{}
		#region  Method

		


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(FlightAirRule model)
		{
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into FlightAirRule(");
                strSql.Append("id,airline,seatclass,returnn,endorsement,returnndis,isdel,createby,createtime,change)");
                strSql.Append(" values (");
                strSql.Append("@id,@airline,@seatclass,@returnn,@endorsement,@returnndis,@isdel,@createby,@createtime,@change)");
                SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.VarChar,50),
					new SqlParameter("@airline", SqlDbType.VarChar,50),
					new SqlParameter("@seatclass", SqlDbType.VarChar,50),
					new SqlParameter("@returnn", SqlDbType.NVarChar),
					new SqlParameter("@endorsement", SqlDbType.NVarChar),
					new SqlParameter("@returnndis", SqlDbType.VarChar,50),
					new SqlParameter("@isdel", SqlDbType.Bit,1),
					new SqlParameter("@createby", SqlDbType.Int,4),
					new SqlParameter("@createtime", SqlDbType.DateTime),
                    new SqlParameter("@change", SqlDbType.NVarChar)};
                parameters[0].Value = model.id;
                parameters[1].Value = model.airline;
                parameters[2].Value = model.seatclass;
                parameters[3].Value = model.returnn;
                parameters[4].Value = model.endorsement;
                parameters[5].Value = model.returnndis;
                parameters[6].Value = model.isdel;
                parameters[7].Value = model.createby;
                parameters[8].Value = model.createtime;
                parameters[9].Value = model.change;

                int r = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                return r;
            }
            catch(Exception ex)
            {
                return 1;
            }
		}
		
		
		


		

		
	

		#endregion  Method

        #region extend method
        public bool deleteAllData()
        {
            string strSql = "update FlightAirRule set isDel=1  ";
           int r = DbHelperSQL.ExecuteSql(strSql);
            if(r <= 0)
            {
                return false;
            }
            return true;
        }
        public void deleteAllDataForever()
        {
             string strSql = "delete from  FlightAirRule where isDel=1  ";
           int r = DbHelperSQL.ExecuteSql(strSql);
            
        }
        public bool recoveryAllData()
        {
            string strSql2 = "delete from FlightAirRule where isDel=0";
             DbHelperSQL.ExecuteSql(strSql2);
            string strSql = " update FlightAirRule set isDel=0 where isDel=1 ";
            int r = DbHelperSQL.ExecuteSql(strSql);
            if (r <= 0)
            {
                return false;
            }
            return true;
        }
        #endregion
	}
}

