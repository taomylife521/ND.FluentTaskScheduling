using ND.FluentTaskScheduling.Domain.interfacelayer;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.WebApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NodeHeartBeatMonitor.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-24 17:01:52         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-24 17:01:52          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.WebApi
{
    /// <summary>
    /// 节点心跳监控
    /// </summary>
    public class NodeHeartBeatMonitor:AbsMonitor
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private INodeRepository nodeRep;
        private IUserRepository userRep;
        private ITaskRepository taskRep;
        private ITaskVersionRepository taskVersionRep;
        private static object lockObj = new object();
        public NodeHeartBeatMonitor(INodeRepository noderespostory, IUserRepository userrespostory, ITaskRepository taskrespostory, ITaskVersionRepository taskVersionrepostory)
        {
            nodeRep = noderespostory;
            userRep = userrespostory;
            taskRep = taskrespostory;
            taskVersionRep = taskVersionrepostory;
        }
       
        /// <summary>
        /// 监控间隔时间(ms为单位)
        /// </summary>
        public override int Interval
        {
            get
            {
                return 10000;
            }
        }
        protected override void Run()
        {
            lock (lockObj)
            {
                List<tb_node> nodelist = nodeRep.Find(x => x.isdel == 0 && x.ifcheckstate == true).ToList();
                List<tb_user> userList = userRep.Find(x =>x.isdel==0).ToList();
                StringBuilder strLog = new StringBuilder();
                nodelist.ForEach(x =>
                {
                    try
                    {
                       List<int> uidlist=x.alarmuserid.ConvertToList();
                       List<tb_user> userlist=userList.Where(m => uidlist.Contains(m.id)).ToList();
                       string alaramperson = x.alarmtype == (int)AlarmType.Email ? string.Join(",", userlist.Select(m => m.useremail).ToArray()) : string.Join(",", userlist.Select(m => m.usermobile).ToArray());
                        if (x.nodestatus == (int)NodeStatus.Running)//运行中
                        {
                            #region 检测心跳
                            double totalsecond =Math.Abs(x.nodelastupdatetime.Subtract(DateTime.Now).TotalSeconds);
                            if (totalsecond > 20 || totalsecond < 0)//大于10s 说明心跳不正常
                            {
                                List<int> taskidlist = new List<int>();
                                taskVersionRep.Find(m => m.nodeid == x.id && m.isdel == 0).ToList().ForEach(m =>
                                {
                                    taskidlist.Add(m.taskid);
                                });
                                nodeRep.UpdateNodeById(x.id, new Dictionary<string, string>() { { "nodestatus", ((int)NodeStatus.NoRun).ToString() }, { "refreshcommandqueuestatus", ((int)RefreshCommandQueueStatus.NoRefresh).ToString() } });//, { "refreshcommandqueuestatus", ((int)RefreshCommandQueueStatus.NoRefresh).ToString() }
                                taskRep.UpdateById(taskidlist, new Dictionary<string, string>() { { "taskschedulestatus", ((int)TaskScheduleStatus.StopSchedule).ToString() } });
                                string title = "节点(编号):" + x.nodename + "(" + x.id.ToString() + "),心跳异常,已自动更新为未运行状态,请及时处理该节点下的任务!";
                                StringBuilder strContent = new StringBuilder();
                                strContent.AppendLine("节点(编号):" + x.nodename + "(" + x.id.ToString() + ")<br/>");
                                strContent.AppendLine("节点最后一次心跳时间:" + x.nodelastupdatetime +"<br/>");
                                //当前节点心跳不正常，已修改节点状态为未运行
                                AlarmHelper.AlarmAsync(x.isenablealarm, (AlarmType)x.alarmtype, alaramperson, title, strContent.ToString());
                            } else
                            {
                                if (x.refreshcommandqueuestatus == (int)RefreshCommandQueueStatus.Refreshing)
                                {
                                    double totalsecond2 = Math.Abs(x.lastrefreshcommandqueuetime.Subtract(DateTime.Now).TotalSeconds);
                                    if (totalsecond2 > 20 || totalsecond2 < 0)//大于10s 说明心跳不正常
                                    {
                                        nodeRep.UpdateNodeById(x.id, new Dictionary<string, string>() { { "refreshcommandqueuestatus", ((int)RefreshCommandQueueStatus.NoRefresh).ToString() } });//, 
                                        string title = "节点(编号):" + x.nodename + "(" + x.id.ToString() + "),监听队列异常,已自动更新为未监听状态,请及时处理该节点!";
                                        StringBuilder strContent = new StringBuilder();
                                        strContent.AppendLine("节点(编号):" + x.nodename + "(" + x.id.ToString() + ")<br/>");
                                        strContent.AppendLine("节点最后一次监听队列时间:" + x.lastrefreshcommandqueuetime + "<br/>");
                                        //当前节点心跳不正常，已修改节点状态为未运行
                                        AlarmHelper.AlarmAsync(x.isenablealarm, (AlarmType)x.alarmtype, alaramperson, title, strContent.ToString());
                                    }
                                }
                            }
                            #endregion
                        }
                       
                       
                    }
                    catch (Exception ex)
                    {
                        strLog.AppendLine("检测节点心跳异常:" + JsonConvert.SerializeObject(ex));
                    }
                });
                if (strLog.Length > 0)
                {
                    log.Error(strLog.ToString());
                }
            }
        }
    }
}
