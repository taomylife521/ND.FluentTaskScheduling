using ND.FluentTaskScheduling.AddConfirmEmailTask.DAL;
using ND.FluentTaskScheduling.TaskInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.FluentTaskScheduling.AddConfirmEmailTask
{
    public class AddConfirmEmailTask : AbstractTask
    {
        public override void InitTaskAppConfig()
        {
            if (!this.AppConfig.ContainsKey("connstr"))
            {
                this.AppConfig.Add("connstr", "Server=192.168.1.104;Database=BaseDB;uid=FlightDBUser;pwd=FLIGHT456db!@#f;");
            }
            if (!this.AppConfig.ContainsKey("emailtitle"))
            {
                this.AppConfig.Add("emailtitle", "你定旅行酒店支付成功");
            }
            if (!this.AppConfig.ContainsKey("isprintsql"))
            {
                this.AppConfig.Add("isprintsql", "0");
            }
            if (!this.AppConfig.ContainsKey("topcount"))
            {
                this.AppConfig.Add("topcount", "1");
            }
            if (!this.AppConfig.ContainsKey("wherecondition"))
            {
                this.AppConfig.Add("wherecondition", @" and oh.orderstatus=3 AND GETDATE()>=DATEADD(MINUTE,10,oh.paytime) AND oh.orderid NOT IN(
    SELECT sr.orderid FROM BaseDB.dbo.SendEmailRecord_Temp sr WHERE sr.orderid=od.orderid AND EmailType=1
    )");
            }
           
        }

        public override RunTaskResult RunTask()
        {
            List<SendEmailRecord_Temp> emailRecordList = new List<SendEmailRecord_Temp>();
            List<long?> sucessList = new List<long?>();
            List<long?> failedList = new List<long?>();
            string allcontent = "";
            string hotelname = "";
            string roomname = "";
            string roomcount = "";
            string isbreakfast = "";
            string checkin = "";
            string checkman = "";
            string checkout = "";
            string remark = "";
            try
            {
                ShowProcessIngLog("开始遍历已经支付超过10分钟的订单表");
                string select =" SELECT top "+this.AppConfig["topcount"].ToString()+" ";
                string transsql =select+ @" oh.orderid AS oid,od.hotelid AS hid,oh.hotelname AS hname,od.roomname AS roomname,od.roomcount AS roomcount,
(CASE od.breakfasttype WHEN 0 THEN '无早' ELSE '有早'END) AS 'isbreakfast'
,(select passengername+',' from HotelDB.dbo.OrderCheckMan where isdel=0 and orderid=oh.orderid for xml path('')) AS passengername
,od.checkindate AS checkin,od.checkoutdate AS checkout
 ,oh.supplierid AS sid,st.SupplierName AS sname,st.ContactEmail AS semail,'niding@126.com' AS sendperson,oh.customerdesc as remark 
 FROM HotelDB.dbo.OrderHotel oh 
    INNER JOIN HotelDB.dbo.OrderHotelDetail od ON oh.orderid=od.orderid 
    INNER JOIN BaseDB.dbo.Supplier_Temp st ON oh.supplierid=st.ID 
	INNER JOIN HotelDB.dbo.OrderCheckMan oc ON oh.orderid=oc.orderid
    WHERE oh.isdel=0 " + this.AppConfig["wherecondition"].ToString();
                if (this.AppConfig["isprintsql"].ToString() == "1")
                {
                    ShowProcessIngLog("ReadSql:" + transsql);
                }
                DbHelperSQL db = new DbHelperSQL(this.AppConfig["connstr"]);
                DataTable dt = DbHelperSQL.Query(transsql).Tables[0];
                ShowProcessIngLog("遍历到(" + dt.Rows.Count.ToString() + ")条记录");
                SendEmailRecord_TempDAL dal = new SendEmailRecord_TempDAL();
                string content = "";
                string tb = @"<table border='1' cellpadding='0' cellspacing='0' style='border-collapse: collapse; width: 318pt; margin-top: 10px;'>
                                  {0}
                           </table>";
                string tr = @"<tr height='28' style='mso-height-source:userset;height:21.0pt'>
                                          <td height='28' class='xl67' align='right' width='78' style='height: 21pt; width: 74pt; border-top:solid windowtext 1.0pt; border-left:solid windowtext 1.0pt; border-bottom:solid windowtext 1.0pt; border-right:solid windowtext 1.0pt; color: windowtext; font-size: 11pt; font-family: 宋体; vertical-align: middle; white-space: nowrap; background-color: rgb(222, 235, 246);'>{0}</td>
                                          <td class='xl66' width='285' style='border-top:solid windowtext 1.0pt; border-left:solid windowtext 1.0pt; border-bottom:solid windowtext 1.0pt; border-right:solid windowtext 1.0pt; width: 214pt;  color: windowtext; font-size: 11pt; font-family: 宋体; vertical-align: middle; white-space: nowrap; padding-left:4px;'>{1}</td>
                                  </tr> ";
                
                
                

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    
                    try
                    {
                        SendEmailRecord_Temp emaillist = new SendEmailRecord_Temp();
                        #region 拼接邮件内容
                        hotelname = dt.Rows[i]["hname"] == DBNull.Value ? "" : dt.Rows[i]["hname"].ToString();
                        roomname = dt.Rows[i]["roomname"] == DBNull.Value ? "" : dt.Rows[i]["roomname"].ToString();
                        roomcount = dt.Rows[i]["roomcount"] == DBNull.Value ? "" : dt.Rows[i]["roomcount"].ToString();
                        isbreakfast = dt.Rows[i]["isbreakfast"] == DBNull.Value ? "" : dt.Rows[i]["isbreakfast"].ToString();
                        checkin = dt.Rows[i]["checkin"] == DBNull.Value ? "" : dt.Rows[i]["checkin"].ToString();
                        checkout = dt.Rows[i]["checkout"] == DBNull.Value ? "" : dt.Rows[i]["checkout"].ToString();
                        checkman = dt.Rows[i]["passengername"] == DBNull.Value ? "" : dt.Rows[i]["passengername"].ToString();
                        remark = dt.Rows[i]["remark"] == DBNull.Value ? "" : dt.Rows[i]["remark"].ToString();
                        content += string.Format(tr, "邮件标题", this.AppConfig["emailtitle"]);
                        content += string.Format(tr, "酒店名称", string.Format("{0}", hotelname));
                        content += string.Format(tr, "房间类型", string.Format("{0}", roomname));
                        content += string.Format(tr, "房间数量", string.Format("{0}", roomcount));
                        content += string.Format(tr, "是否含早", string.Format("{0}", isbreakfast));
                        content += string.Format(tr, "客人姓名", string.Format("{0}", checkman));
                        content += string.Format(tr, "入住日期", string.Format("{0}", checkin));
                        content += string.Format(tr, "离店日期", string.Format("{0}", checkout));
                        content += string.Format(tr, "特殊备注", string.Format("{0}", remark));
                        content += string.Format(tr, "你定联系人", "联系电话");
                        content += string.Format(tr, "崔彩晓", "15301063759");
                        content += string.Format(tr, "刘  峰", "15101687097");
                        content += string.Format(tr, "张  瑶", "18210232324");
                        content += string.Format(tr, "朱  芬", "13550090301");
                        content += string.Format(tr, "赵聪颖", "13366453726");
                        content += string.Format(tr, "胡  剑", "13522000398");
                        content += string.Format(tr, "杨  晶", "15910917165");
                        content += string.Format(tr, "王宁宁", "15321462675");
                        content += string.Format(tr, "陆钰平", "18600330096");
                        allcontent = string.Format(tb, content); 
                        #endregion
                        emaillist.Content = allcontent;
                        emaillist.EmailTitle = this.AppConfig["emailtitle"];
                        emaillist.OrderId = dt.Rows[i]["oid"] == DBNull.Value ? 0 : long.Parse(dt.Rows[i]["oid"].ToString());
                        emaillist.HotelId = dt.Rows[i]["hid"] == DBNull.Value ? 0 : long.Parse(dt.Rows[i]["hid"].ToString());
                        emaillist.SupplierId = dt.Rows[i]["sid"] == DBNull.Value ? 0 : int.Parse(dt.Rows[i]["sid"].ToString());
                        emaillist.SupplierName = dt.Rows[i]["sname"] == DBNull.Value ? "" : dt.Rows[i]["sname"].ToString();
                        emaillist.Recipients = dt.Rows[i]["semail"] == DBNull.Value ? "" : dt.Rows[i]["semail"].ToString();
                        emaillist.SendPerson = dt.Rows[i]["sendperson"] == DBNull.Value ? "" : dt.Rows[i]["sendperson"].ToString();
                        emailRecordList.Add(emaillist);
                    }
                    catch (Exception ex)
                    {
                        ShowProcessIngLog("orderid:(" + dt.Rows[i]["oid"].ToString() + "),构造邮件对象异常,异常信息:" + JsonConvert.SerializeObject(ex));
                    }
                }
                foreach (var item in emailRecordList)//入邮件记录库
                {
                    int r = dal.Add(item);
                    if (r <= 0)
                    {
                        failedList.Add(item.OrderId);
                    }
                    else
                    {
                        sucessList.Add(item.OrderId);
                    }
                }
                if (emailRecordList.Count > 0)
                {
                    if (failedList.Count > 0)
                    {
                        ShowProcessIngLog("失败记录(" + string.Join(",", failedList) + ")");
                    }
                    ShowProcessIngLog("入邮件队列完成,成功(" + sucessList.Count + ")条,失败(" + failedList.Count + ")条!");

                }
                
                dt = null;
               
            }
            catch(Exception ex)
            {
                ShowProcessIngLog("操作异常:"+JsonConvert.SerializeObject(ex));
                return new RunTaskResult() { RunStatus = (int)RunStatus.Exception, Message = "" };
            }
            finally
            {
                #region Dispose
                emailRecordList.Clear();
                emailRecordList = null;
                sucessList.Clear();
                failedList.Clear();
                sucessList = null;
                failedList = null;
                allcontent = null;
                hotelname = null;
                roomname = null;
                roomcount = null;
                isbreakfast = null;
                checkin = null;
                checkman = null;
                checkout = null;
                remark = null; 
                #endregion
            }
             ShowProcessIngLog("操作完成");
             return new RunTaskResult() { RunStatus = (int)RunStatus.Normal, Message = "" };
        }
    }
}
