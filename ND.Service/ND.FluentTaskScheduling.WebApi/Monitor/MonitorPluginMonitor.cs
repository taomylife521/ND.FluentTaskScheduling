using ND.FluentTaskScheduling.Domain.interfacelayer;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//**********************************************************************
//
// 文件名称(File Name)：MonitorPluginMonitor.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-27 15:47:40         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-27 15:47:40          
//             修改理由：         
//**********************************************************************

namespace ND.FluentTaskScheduling.WebApi.Monitor
{
   
    /// <summary>
    /// 监控所有节点下的监控插件运行情况
    /// </summary>
    public class MonitorPluginMonitor : AbsMonitor
    {
         private INodeRepository nodeRep;
        private IUserRepository userRep;
        private INodeMonitorRepository nodemonitorRep;
        private static object lockObj = new object();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public MonitorPluginMonitor(INodeRepository noderespostory, IUserRepository userrespostory,INodeMonitorRepository nodemonitorrepostory)
        {
            nodeRep = noderespostory;
            userRep = userrespostory;
            nodemonitorRep = nodemonitorrepostory;
        }
        /// <summary>
        /// 监控间隔15s时间(ms为单位)
        /// </summary>
        public override int Interval
        {
            get
            {
                return 15000;
            }
        }
        protected override void Run()
        {
            lock (lockObj)
            {
                int monitorstatus=(int)MonitorStatus.Monitoring;
                List<tb_nodemonitor> nodemonitorlist = nodemonitorRep.Find(x => x.isdel == 0 && x.monitorstatus == monitorstatus).ToList();
                List<tb_user> userList = userRep.Find(x => x.isdel == 0).ToList();
                StringBuilder strLog = new StringBuilder();
                nodemonitorlist.ForEach(x =>
                {
                    try
                    {
                        tb_node node=nodeRep.FindSingle(m => m.id == x.nodeid);
                        List<int> uidlist = node.alarmuserid.ConvertToList();
                        List<tb_user> userlist = userList.Where(m => uidlist.Contains(m.id)).ToList();
                        string alaramperson = node.alarmtype == (int)AlarmType.Email ? string.Join(",", userlist.Select(m => m.useremail).ToArray()) : string.Join(",", userlist.Select(m => m.usermobile).ToArray());
                        if (x.monitorstatus == (int)MonitorStatus.Monitoring)//运行中
                        {
                            #region 检测心跳
                            double totalsecond = Math.Abs(x.lastmonitortime.Subtract(DateTime.Now).TotalSeconds);
                            if (totalsecond > (20+x.interval/1000) || totalsecond < 0)//大于10s 说明心跳不正常
                            {
                                nodemonitorRep.UpdateById(new List<int>() { x.id }, new Dictionary<string, string>() { { "monitorstatus", ((int)NodeStatus.NoRun).ToString() } });//, { "refreshcommandqueuestatus", ((int)RefreshCommandQueueStatus.NoRefresh).ToString() }
                                if (node.nodestatus != (int) NodeStatus.NoRun)
                                {
                                    string title = "节点(编号):" + node.nodename + "(" + x.nodeid.ToString() + "),监控组件:" +
                                                   x.classname + ",心跳异常,已自动更新为未监控状态,请及时处理该节点下该监控组件!";
                                    StringBuilder strContent = new StringBuilder();
                                    strContent.AppendLine("节点(编号):" + node.nodename + "(" + node.id.ToString() +
                                                          ")<br/>");
                                    strContent.AppendLine("节点监控组件名称(编号):" + x.name + "(" + x.id.ToString() + ")<br/>");
                                    strContent.AppendLine("节点监控组件描述:" + x.discription + "<br/>");
                                    strContent.AppendLine("节点监控类名,命名空间:" + x.classname + "," +
                                                          x.classnamespace.ToString() + "<br/>");
                                    strContent.AppendLine("节点监控组件最后一次心跳时间:" + x.lastmonitortime + "<br/>");
                                    AlarmHelper.AlarmAsync(node.isenablealarm, (AlarmType) node.alarmtype, alaramperson,
                                        title, strContent.ToString());
                                }
                            }
                            
                            #endregion
                        }


                    }
                    catch (Exception ex)
                    {
                        strLog.AppendLine("监控节点监控组件异常:" + JsonConvert.SerializeObject(ex));
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