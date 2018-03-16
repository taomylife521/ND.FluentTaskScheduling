using Maticsoft.DAL;
using ND.FluentTaskScheduling.CrawlKeGui.DAL;
using ND.FluentTaskScheduling.CrawlKeGui.getModifyAndRefundStipulates;
using ND.FluentTaskScheduling.TaskInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ND.FluentTaskScheduling.CrawlKeGui
{
    #region 记录日志方法
    public class LogContext
    {
        public void AddLogInfo(string strPath, string txt)
        {
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }

            var fs = File.AppendText(strPath);
            fs.WriteLine(txt);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }
        public void WriteDateTime(string strPath, string txt)
        {
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }

            var fs = File.CreateText(strPath);
            fs.WriteLine(txt);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }


        public void AddLogInfo(string strPath, string txt, bool isAppend)
        {
            string strDirecory = strPath.Substring(0, strPath.LastIndexOf('\\'));
            if (!Directory.Exists(strDirecory))
            {
                Directory.CreateDirectory(strDirecory);
            }
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }

            StreamWriter fs;
            if (isAppend) fs = File.AppendText(strPath);
            else fs = File.CreateText(strPath);

            fs.WriteLine(txt);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

        public void CreateFile(string strPath, string txt)
        {
            string strDirecory = strPath.Substring(0, strPath.LastIndexOf('\\'));
            if (!Directory.Exists(strDirecory))
            {
                Directory.CreateDirectory(strDirecory);
            }
            if (!File.Exists(strPath))
            {
                File.Create(strPath).Dispose();
            }



            FileStream fs = File.Create(strPath);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(txt);
            sw.Flush();
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }


        public string ReadDataLog(string strPath)
        {
            string strDirecory = strPath.Substring(0, strPath.LastIndexOf('\\'));
            if (!Directory.Exists(strDirecory))
            {
                Directory.CreateDirectory(strDirecory);
            }
            if (!File.Exists(strPath))
            {
                File.CreateText(strPath).Dispose();
            }

            string txt = File.ReadAllText(strPath, Encoding.UTF8);

            return txt;
        }
    }
    #endregion

    /// <summary>
    /// 抓取航空公司客规类
    /// </summary>
    public class CrawlKeGuiTask : AbstractTask
    {
        public override RunTaskResult RunTask()
        {
            DbHelperSQL sql = new DbHelperSQL(this.AppConfig["ConnStr"].ToString());
            _51bookHelper helper = new _51bookHelper(this.AppConfig["agencyCode"].ToString(), this.AppConfig["safetyCode"].ToString());
            ShowProcessIngLog("读取该任务的配置参数:" + JsonConvert.SerializeObject(this.AppConfig));
            RunTaskResult taskResult = new RunTaskResult() { RunStatus = (int)RunStatus.Normal,  Message = "执行完毕" };
            FlightAirRuleDAL ruleDal = new FlightAirRuleDAL();
            try
            {
                ShowProcessIngLog("开始清除现有数据");
                ruleDal.deleteAllData();//清空所有数据
                ShowProcessIngLog("清除现有数据完成");
                bool flag = true;
                int index = 0;
                while (flag)
                {
                    getModifyAndRefundStipulatesRequest req = new getModifyAndRefundStipulatesRequest();
                    req.rowPerPage = 1500;
                    req.rowPerPageSpecified = true;
                    string lastTimeAndId = GetLastUpTimeAndId("AirKGLog");
                    req.lastSeatId = Convert.ToInt32(lastTimeAndId.Split('|')[1]);
                    req.lastSeatIdSpecified = true;
                    req.lastModifiedAt = lastTimeAndId.Split('|')[0];
                    ShowProcessIngLog("读取本次要抓取的配置:"+lastTimeAndId);
                    getModifyAndRefundStipulatesReply rep = _51bookHelper.getModifyAndRefundStipulates(req) as getModifyAndRefundStipulatesReply;
                    if (rep.returnCode.ToLower() != "s")
                    {
                        ShowProcessIngLog(rep.returnMessage + "," + rep.returnStackTrace);
                        flag = false;
                        continue;
                    }
                    ShowProcessIngLog("收到退改签规定包数量:" + rep.modifyAndRefundStipulateList.Length + ",剩余页数:" + rep.leftPages);
                    if (index > 0)
                    {
                        if (rep.leftPages == 0)
                        {
                            ShowProcessIngLog("剩余页数为0，已经全部取完！");
                            flag = false;
                        }
                        else
                        {

                            addDb(rep);

                        }
                    }
                    else
                    {
                        addDb(rep);
                    }
                    index++;
                }
                SaveLastUpTimeAndId("2000-01-01 00:00:00|0", "AirKGLog");
                ruleDal.deleteAllDataForever();//清空所有数据

            }
            catch (Exception ex)
            {
                ShowProcessIngLog("添加航空公司客规失败：" + ex.Message+",异常:"+JsonConvert.SerializeObject(ex));
                ruleDal.recoveryAllData();
                taskResult = new RunTaskResult() { RunStatus = (int)RunStatus.Exception, Ex = ex, Message = ex.Message };
            }
            return taskResult;
        }

        public override void InitTaskAppConfig()
        {
            if (!this.AppConfig.ContainsKey("ConnStr"))
            {
                this.AppConfig.Add("ConnStr", "Server=.;Database=TestA;uid=sa;pwd=sa123456;");
            }
            if (!this.AppConfig.ContainsKey("agencyCode"))
            {
                this.AppConfig.Add("agencyCode", "NDLXS");
            }
            if (!this.AppConfig.ContainsKey("safetyCode"))
            {
                this.AppConfig.Add("safetyCode", "H&*WUgd2");
            }
        }

        #region 获取上次更新时间和id
        /// <summary>
        /// 获取上次更新时间和id
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public static string GetLastUpTimeAndId(string _name)
        {

            LogContext log = new LogContext();

            string logPath = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\ND.FlightKGService\\" + _name + ".txt";
            string lastUpTime = log.ReadDataLog(logPath).TrimEnd((char[])"\r\n".ToCharArray());
            if (lastUpTime.Trim() == "")
            {
                return "2015-10-10 08:00:00|0";

            }
            else
            {

                string lastUpdateTime = lastUpTime.Trim().Split('|')[0];
                string lastId = lastUpTime.Trim().Split('|')[1];
                return lastUpdateTime + "|" + lastId;

            }
        }
        #endregion

        #region 保存上次更新时间和id
        /// <summary>
        /// 保存最新政策更新时间和ID
        /// </summary>
        /// <param name="_timeAndId">时间|ID</param>
        public static void SaveLastUpTimeAndId(string _timeAndId, string _name)
        {
            LogContext log = new LogContext();
            //string logPath = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\ND.PolicyUploadService\\" + _name + ".txt";
            string logPath = System.IO.Directory.GetCurrentDirectory() + "\\LogContext\\ND.FlightKGService\\" + _name + ".txt";
            log.AddLogInfo(logPath, _timeAndId, false);
        }
        #endregion

        #region 添加到数据库
        public void addDb(getModifyAndRefundStipulatesReply rep)
        {
            try
            {
                string seatId = "0";
                string lastModifiedAt = "2000-01-01 00:00:00";
                int i = 0;
                FlightAirRuleDAL airRuleBll = new FlightAirRuleDAL();
                ShowProcessIngLog(DateTime.Now + ":开始添加退改签规定包：" + rep.modifyAndRefundStipulateList.Length);
                //获取记录并添加到数据库中
                for (i = 0; i < rep.modifyAndRefundStipulateList.Length; i++)
                {
                    FlightAirRule airRule = new FlightAirRule();
                    airRule.id = System.Guid.NewGuid().ToString();
                    airRule.returnn = HttpUtility.UrlDecode(rep.modifyAndRefundStipulateList[i].refundStipulate, System.Text.Encoding.UTF8);
                    airRule.endorsement = HttpUtility.UrlDecode(rep.modifyAndRefundStipulateList[i].modifyStipulate, System.Text.Encoding.UTF8);
                    lastModifiedAt = rep.modifyAndRefundStipulateList[i].modifiedAt;
                    seatId = rep.modifyAndRefundStipulateList[i].seatId.ToString();
                    airRule.seatclass = rep.modifyAndRefundStipulateList[i].seatCode;
                    airRule.change = HttpUtility.UrlDecode(rep.modifyAndRefundStipulateList[i].changeStipulate, System.Text.Encoding.UTF8);
                    airRule.airline = rep.modifyAndRefundStipulateList[i].airlineCode;
                    try
                    {
                        int r = airRuleBll.Add(airRule);
                        if (r == 0)
                        {
                            ShowProcessIngLog("添加航空公司客规失败：" + rep.modifyAndRefundStipulateList[i].airlineCode + rep.modifyAndRefundStipulateList[i].seatCode + rep.modifyAndRefundStipulateList[i].seatId);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
                if (i >= rep.modifyAndRefundStipulateList.Length)
                {
                    ShowProcessIngLog(DateTime.Now + ":添加完成：" + rep.modifyAndRefundStipulateList.Length + ",保存最后一次信息:Info=" + lastModifiedAt + "|" + seatId);
                    SaveLastUpTimeAndId(lastModifiedAt + "|" + seatId, "AirKGLog");
                }
            }
            catch (Exception ex)
            { }
        }
        #endregion
    }
}
