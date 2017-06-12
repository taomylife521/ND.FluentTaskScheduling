using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Command.Monitor;
using ND.FluentTaskScheduling.Core.TaskHandler;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.Model.request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskPerformanceMonitor.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-02 15:42:41         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-02 15:42:41          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.Monitor
{
   public class TaskPerformanceMonitor:AbsMonitor
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public override string Name
        {
            get
            {
                return "任务性能上报监控";
            }

        }

        public override string Discription
        {
            get
            {
                return "每隔5秒遍历下本地任务池列表并上报任务性能";
            }

        }
        /// <summary>
        /// 监控间隔时间(ms为单位)10s上报一次性能
        /// </summary>
        public override int Interval
        {
            get
            {
                return 5000;
            }
        }
        protected override void Run()
        {
            var res = NodeProxy.PostToServer<EmptyResponse, UpdateNodeMonitorRequest>(ProxyUrl.UpdateNodeMonitorRequest_Url, new UpdateNodeMonitorRequest() { MonitorClassName=this.GetType().Name,MonitorStatus=(int)MonitorStatus.Monitoring,NodeId=GlobalNodeConfig.NodeID,Source=Source.Node});
            if (res.Status != ResponesStatus.Success)
            {
                LogProxy.AddNodeErrorLog(this.GetType().Name+"监控组件上报监控信息失败,请求地址:" + ProxyUrl.UpdateNodeMonitorRequest_Url + ",返回结果:" + JsonConvert.SerializeObject(res));
            }
            foreach (var taskruntimeinfo in TaskPoolManager.CreateInstance().GetList())
            {
                try
                {
                    if (taskruntimeinfo == null)
                        continue;
                    string fileinstallpath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + GlobalNodeConfig.TaskDllDir + @"\" + taskruntimeinfo.TaskModel.id;
                    double dirsizeM = -1;
                    if (System.IO.Directory.Exists(fileinstallpath))
                    {
                        long dirsize = IOHelper.DirSize(new DirectoryInfo(fileinstallpath));
                        dirsizeM = (double)dirsize / 1024 / 1024;
                    }
                    try
                    {
                        
                        if (taskruntimeinfo.Domain != null)
                        {
                            //上报性能
                         AddPerformanceRequest addperformance= new AddPerformanceRequest() { 
                                NodeId = GlobalNodeConfig.NodeID,
                                Cpu = taskruntimeinfo.Domain.MonitoringTotalProcessorTime.TotalSeconds.ToString(),
                                Memory = ((double)taskruntimeinfo.Domain.MonitoringSurvivedMemorySize / 1024 / 1024).ToString(),//AppDomain当前正在使用的字节数（只保证在上一次垃圾回收时是准确的）
                                InstallDirsize = dirsizeM.ToString(),
                                TaskId = taskruntimeinfo.TaskModel.id,
                                Lastupdatetime = (DateTime.Now).ToString(),
                                Source = Source.Node,
                               
                            };
                           var r = NodeProxy.PostToServer<EmptyResponse, AddPerformanceRequest>(ProxyUrl.AddPerformance_Url, addperformance);
                            if(r.Status!= ResponesStatus.Success)
                            {
                                LogProxy.AddTaskErrorLog("任务性能上报出错,请求地址:" + ProxyUrl.AddPerformance_Url + ",请求内容:" + JsonConvert.SerializeObject(addperformance) + ",返回结果:" + JsonConvert.SerializeObject(r), taskruntimeinfo.TaskModel.id);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogProxy.AddTaskErrorLog("任务性能监控时出错:taskid=" + taskruntimeinfo.TaskModel.id + ",异常信息:" + JsonConvert.SerializeObject(ex), taskruntimeinfo.TaskModel.id);
                    }
                }
                catch (Exception exp)
                {
                    LogProxy.AddNodeErrorLog("任务性能监控异常,异常信息:"+JsonConvert.SerializeObject(exp));
                }
            }
        }
    }
}
