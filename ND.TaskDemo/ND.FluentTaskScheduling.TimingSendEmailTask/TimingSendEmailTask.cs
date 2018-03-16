using ND.FluentTaskScheduling.TaskInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TimingSendEmailTask.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-10-27 15:59:07         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-10-27 15:59:07          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.TimingSendEmailTask
{
    public class TimingSendEmailTask:AbstractTask
    {
        
        public override void InitTaskAppConfig()
        {
            if (!this.AppConfig.ContainsKey("connstr"))
            {
                this.AppConfig.Add("connstr", "Server=192.168.1.104;Database=BaseDB;uid=FlightDBUser;pwd=FLIGHT456db!@#f;");
            }
            if (!this.AppConfig.ContainsKey("mailfrom"))
            {
                this.AppConfig.Add("mailfrom", "niding@126.com");
            }
            if (!this.AppConfig.ContainsKey("mailpwd"))
            {
                this.AppConfig.Add("mailpwd", "QLND@2011*!?#kr");
            }
            if (!this.AppConfig.ContainsKey("host"))
            {
                this.AppConfig.Add("host", "smtp.126.com");
            }
            if (!this.AppConfig.ContainsKey("wherecondition"))//sql条件
            {
                this.AppConfig.Add("wherecondition", " AND IsSend=0 AND SendCount<=2 ");
            }
            if (!this.AppConfig.ContainsKey("topcount"))//sql条件
            {
                this.AppConfig.Add("topcount", "50");
            }
            if (!this.AppConfig.ContainsKey("isenablealarm"))//sql条件
            {
                this.AppConfig.Add("isenablealarm", "1");
            }
            
        }

        public override RunTaskResult RunTask()
        {
            try
            {
                ShowProcessIngLog("开始获取要发送邮件的记录");
                AlarmHelper2 helper = new AlarmHelper2();
                DbHelperSQL db = new DbHelperSQL(this.AppConfig["connstr"]);
                int isEnableAlaram = int.Parse(this.AppConfig["isenablealarm"]);
                string mailfrom = this.AppConfig["mailfrom"].ToString();
                string mailpwd = this.AppConfig["mailpwd"].ToString();
                string host = this.AppConfig["host"].ToString();
                int topcount = string.IsNullOrEmpty(this.AppConfig["topcount"].ToString())?50:int.Parse(this.AppConfig["topcount"].ToString());
                string wherecondition = string.IsNullOrEmpty(this.AppConfig["wherecondition"].ToString()) ? " AND IsSend=0 AND SendCount<=2 " : this.AppConfig["wherecondition"].ToString();
                List<int> sucessIdList = new List<int>();//发送成功的id列表
                List<int> faildIdList = new List<int>();//发送失败的id列表
                List<int> totalIdList = new List<int>();//发送失败的id列表
                string sendPerson = "";
                string recipients = "";
                string emailTitle = "";
                string emailContent = "";
                string strUpdateSql = "";
                string strSql = "SELECT top "+topcount.ToString()+" ID,SendPerson,Recipients,EmailTitle,Content FROM dbo.SendEmailRecord_Temp WHERE IsDel=0 " + wherecondition + " ORDER BY SendCount";
                ShowProcessIngLog("ReadSql:"+strSql);
                DataTable dt = DbHelperSQL.Query(strSql).Tables[0];
                string totalCount = dt.Rows.Count.ToString();
                ShowProcessIngLog("获取到要发送邮件的记录(" + totalCount + ")条!");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int mailId = int.Parse(dt.Rows[i]["ID"].ToString());
                    
                    try
                    {
                        totalIdList.Add(mailId);
                        if (dt.Rows[i]["ID"] != DBNull.Value)
                        {
                            sendPerson = dt.Rows[i]["SendPerson"] == DBNull.Value ? "" : dt.Rows[i]["SendPerson"].ToString();
                            recipients = dt.Rows[i]["Recipients"] == DBNull.Value ? "" : dt.Rows[i]["Recipients"].ToString();
                            emailTitle = dt.Rows[i]["EmailTitle"] == DBNull.Value ? "" : dt.Rows[i]["EmailTitle"].ToString();
                            emailContent = dt.Rows[i]["Content"] == DBNull.Value ? "" : dt.Rows[i]["Content"].ToString();

                            if (string.IsNullOrEmpty(recipients) || string.IsNullOrEmpty(emailTitle))
                            {
                                ShowProcessIngLog("mailid:" + mailId.ToString() + ",收件人信息或者邮件标题为空,发送失败!");
                                faildIdList.Add(mailId);
                            }
                            else
                            {
                                bool r = AlarmHelper2.Alarm(isEnableAlaram, recipients, emailTitle, emailContent, mailfrom, mailpwd, host);
                                if (r)
                                {
                                    sucessIdList.Add(mailId);
                                }
                                else
                                {
                                    faildIdList.Add(mailId);
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        ShowProcessIngLog("mailid:" + dt.Rows[i]["ID"].ToString() + ",Recipients:" + recipients + "，发送邮件异常:" + JsonConvert.SerializeObject(ex));
                        faildIdList.Add(mailId);
                    }

                }
                if (totalIdList.Count > 0)
                {
                    ShowProcessIngLog("成功详情:(" + string.Join(",", sucessIdList.ToArray()) + "),失败详情(" + string.Join(",", faildIdList.ToArray()) + ")");
                }
                ShowProcessIngLog("发送邮件完成,共读取到(" + totalCount + ")条,成功(" + sucessIdList.Count + ")条,失败(" + faildIdList.Count + ")");
                if (totalIdList.Count > 0)
                {
                    ShowProcessIngLog("开始更新发送记录");

                    //开始更新库中记录
                    if(sucessIdList.Count>0)
                    {
                         strUpdateSql = " UPDATE dbo.SendEmailRecord_Temp SET IsSend=1, SendTime=GETDATE(),SendCount=SendCount+1 WHERE ID IN(" + string.Join(",", sucessIdList.ToArray()) +")";
                    }
                    if(faildIdList.Count>0)
                    {
                        strUpdateSql=strUpdateSql+";UPDATE dbo.SendEmailRecord_Temp SET IsSend=0, SendTime=GETDATE(),SendCount=SendCount+1 WHERE ID IN(" + string.Join(",", faildIdList.ToArray()) + ")";
                    }
                   
                    ShowProcessIngLog("updateSql:" + strUpdateSql);
                    int r2 = DbHelperSQL.ExecuteSql(strUpdateSql);
                    ShowProcessIngLog("更新完成,影响行数:" + r2.ToString());
                }
                mailfrom = null;
                mailpwd = null;
                host = null;
                wherecondition = null;
                sendPerson = null;
                recipients = null;
                emailTitle = null;
                emailContent = null;
                strSql = null;
                strUpdateSql = null;
                return new RunTaskResult() { RunStatus = (int)RunStatus.Normal, Message = "发送邮件完成共读取到(" + totalCount + ")条,成功(" + sucessIdList.Count + ")条,失败(" + faildIdList.Count + ")" };
            }
            catch(Exception ex)
            {
               
                ShowProcessIngLog("发送邮件异常,异常信息:"+JsonConvert.SerializeObject(ex.Message));
                return new RunTaskResult() { RunStatus = (int)RunStatus.Exception, Message = "异常信息:"+JsonConvert.SerializeObject(ex) };
            }
            
              
        }


    }
}
