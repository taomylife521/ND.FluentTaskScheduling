using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Command.Monitor;
using ND.FluentTaskScheduling.Core.TaskHandler;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskSchedulingMonitor.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-26 10:33:17         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-26 10:33:17          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.Monitor
{
    /// <summary>
    /// 调度中的任务监控
    /// </summary>
    public class NodeTaskSchedulingMonitor : AbsMonitor
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public override string Name
        {
            get
            {
                return "监控调度中任务";
            }

        }

        public override string Discription
        {
            get
            {
                return "该监控主要是防止调度中的任务异常停止,如果调度中任务停止则报警";
            }

        }
        /// <summary>
        /// 监控间隔时间(ms为单位),
        /// </summary>
        public override int Interval
        {
            get
            {
                return 1000 * 60;//1分钟
            }
        }
        protected override void Run()
        {
            try
            {
                var res = NodeProxy.PostToServer<EmptyResponse, UpdateNodeMonitorRequest>(ProxyUrl.UpdateNodeMonitorRequest_Url, new UpdateNodeMonitorRequest() { MonitorClassName = this.GetType().Name, MonitorStatus = (int)MonitorStatus.Monitoring, NodeId = GlobalNodeConfig.NodeID, Source = Source.Node });
                if (res.Status != ResponesStatus.Success)
                {
                    LogProxy.AddNodeErrorLog(this.GetType().Name + "监控组件上报监控信息失败,请求地址:" + ProxyUrl.UpdateNodeMonitorRequest_Url + ",返回结果:" + JsonConvert.SerializeObject(res));
                }
                
                LoadTaskIdListRequest req = new LoadTaskIdListRequest() { Source = Source.Node, TaskScheduleStatus = (int)TaskScheduleStatus.Scheduling,NodeId=GlobalNodeConfig.NodeID };
                var r = NodeProxy.PostToServer<LoadTaskIdListResponse, LoadTaskIdListRequest>(ProxyUrl.LoadTaskIdList_Url, req);
                List<int> taskschdulinglist = TaskPoolManager.CreateInstance().GetList().Select(x => x.TaskModel.id).ToList();
                if (r.Status != ResponesStatus.Success)
                {
                    //如果服务端没有调度中的，则把本地在调度中的上报状态
                    if (taskschdulinglist.Count > 0)
                    {
                        UploadLocalTask(taskschdulinglist);
                    }
                    return;
                }
                
                List<int> taskidnotserver= taskschdulinglist.Where(x => !(r.Data.TaskIdList.Contains(x))).ToList();
                r.Data.TaskIdList.ForEach(x =>
                {
                    var taskruntimeInfo = TaskPoolManager.CreateInstance().Get(x.ToString());
                    if (taskruntimeInfo == null)//如果等于空值则报警,说明该任务再
                    {

                        string title = "节点(id):" + GlobalNodeConfig.NodeInfo.nodename + "(" + GlobalNodeConfig.NodeInfo.id.ToString() + "),任务id:(" + x + ")" + ",调度异常,请及时处理!";
                        StringBuilder strContent = new StringBuilder();
                        strContent.AppendLine("所在节点名称(编号):" + GlobalNodeConfig.NodeInfo.nodename + "(" + GlobalNodeConfig.NodeID + ")<br/>");
                        strContent.AppendLine("任务编号:" + x + "<br/>");
                        strContent.AppendLine("服务端任务状态:调度中<br/>");
                        strContent.AppendLine("节点端任务状态:任务池中已不存在该任务,调度异常<br/>");
                        AlarmHelper.AlarmAsync(GlobalNodeConfig.NodeInfo.isenablealarm, (AlarmType)GlobalNodeConfig.NodeInfo.alarmtype, GlobalNodeConfig.Alarm, title, strContent.ToString());
                        LogProxy.AddNodeErrorLog(strContent.ToString());
                    }
                });
                if (taskidnotserver.Count > 0)
                {
                    UploadLocalTask(taskidnotserver);
                }
            }
            catch(Exception ex)
            {
                
                LogProxy.AddNodeErrorLog("节点("+GlobalNodeConfig.NodeInfo.nodename+"("+GlobalNodeConfig.NodeInfo.id.ToString()+")"+")监控调度中任务异常:"+ex.Message+",异常:"+JsonConvert.SerializeObject(ex));
                AlarmHelper.AlarmAsync(GlobalNodeConfig.NodeInfo.isenablealarm, (AlarmType)GlobalNodeConfig.NodeInfo.alarmtype, GlobalNodeConfig.Alarm, "节点("+GlobalNodeConfig.NodeInfo.nodename+"("+GlobalNodeConfig.NodeInfo.id.ToString()+")"+")监控调度中任务异常", JsonConvert.SerializeObject(ex));
            }
            
        }

        private void UploadLocalTask(List<int> taskidnotserver)
        {
                taskidnotserver.ForEach(x =>
                {
                    var taskruntimeInfo = TaskPoolManager.CreateInstance().Get(x.ToString());
                    //上报所在在本地节点运行的任务,但是在服务端未在调度中,更新服务端的任务调度状态为调度中
                    var req2 = new UpdateTaskScheduleStatusRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Node, ScheduleStatus = TaskScheduleStatus.Scheduling, TaskId = x, TaskVersionId = taskruntimeInfo.TaskVersionModel.id, NextRunTime = Convert.ToString(taskruntimeInfo.TaskModel.nextruntime) };
                    var r3 = NodeProxy.PostToServer<EmptyResponse, UpdateTaskScheduleStatusRequest>(ProxyUrl.UpdateTaskScheduleStatus_Url, req2);
                    if (r3.Status != ResponesStatus.Success)
                    {
                        LogProxy.AddNodeErrorLog("任务id:" + x.ToString() + ",任务名称:" + taskruntimeInfo.TaskModel.taskname + ",在服务端为停止调度状态,在本地节点下为调度中。上报本地节点下的任务状态(调度中)失败");
                        // ShowCommandLog("更新任务调度状态(" + status.description() + ")失败,请求Url：" + ProxyUrl.UpdateTaskScheduleStatus_Url + ",请求参数:" + JsonConvert.SerializeObject(req2) + ",返回参数:" + JsonConvert.SerializeObject(r3));
                    }
                });
        }
    }
}
