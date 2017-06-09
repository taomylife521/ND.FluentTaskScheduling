
using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using ND.FluentTaskScheduling.TaskInterface;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskJob.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 15:07:06         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 15:07:06          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.TaskHandler
{
    /// <summary>
    /// 任务回调Job
    /// </summary>
    public class TaskJob :IJob//MarshalByRefObject, IJob
    {
       // private static object lockobj = new object();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void Execute(JobExecutionContext context)
        {
            //lock (lockobj)
            //{
            int taskid = Convert.ToInt32(context.JobDetail.Name);
                try
                {
                   
                    var taskruntimeinfo = TaskPoolManager.CreateInstance().Get(taskid.ToString());
                    
                    taskruntimeinfo.DllTask.ClearLog();
                    if (taskruntimeinfo == null || taskruntimeinfo.DllTask == null)
                    {
                        // LogHelper.AddTaskError("当前任务信息为空引用", taskid, new Exception());
                        return;
                    }
                    taskruntimeinfo.TaskLock.Invoke(() =>
                    {
                        // int runCount = 0;
                        try
                        {
                            int taskid2 = taskruntimeinfo.TaskModel.id;
                            int taskversionid = taskruntimeinfo.TaskVersionModel.id;
                            string nextrunTime = Convert.ToDateTime(context.NextFireTimeUtc).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                            nextrunTime = nextrunTime.IndexOf("0001-01") > -1 ? "2099-12-30" : nextrunTime;
                            if (taskruntimeinfo.TaskModel.ispauseschedule == 0)//等于0 说明没有停止调度，否则停止调度
                            {
                                taskruntimeinfo.DllTask.TryRunTask(nextrunTime, taskruntimeinfo);//执行完，判断是否需要释放
                                try
                                {
                                  
                                    if (taskruntimeinfo.TaskModel.tasktype == (int)TaskType.OnceTask)
                                    {
                                       bool disposeflag= TaskDisposer.DisposeTask(taskid2, taskruntimeinfo, false, null);
                                       if (disposeflag)//如果释放成功则上报
                                       {
                                           var req = new UpdateTaskScheduleStatusRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Node, ScheduleStatus = Model.enums.TaskScheduleStatus.StopSchedule, TaskId = taskid2, TaskVersionId = taskversionid };
                                           var r2 = NodeProxy.PostToServer<EmptyResponse, UpdateTaskScheduleStatusRequest>(ProxyUrl.UpdateTaskScheduleStatus_Url, req);
                                           if (r2.Status != ResponesStatus.Success)
                                           {
                                               LogProxy.AddTaskErrorLog("更新任务调度状态(停止调度)失败,请求Url：" + ProxyUrl.UpdateTaskScheduleStatus_Url + ",请求参数:" + JsonConvert.SerializeObject(req) + ",返回参数:" + JsonConvert.SerializeObject(r2), taskid);
                                           }
                                       }else
                                       {
                                           LogProxy.AddTaskErrorLog("taskid=" + taskid + ",释放单次执行任务资源失败", taskid);
                                       }
                                       
                                    }
                                }
                                catch(Exception ex)
                                {
                                    LogProxy.AddTaskErrorLog("taskid=" + taskid + ",释放单次执行任务资源异常:" + JsonConvert.SerializeObject(ex), taskid);
                                }

                            }
                           
                        }
                        catch (Exception exp)
                        {
                            LogProxy.AddTaskErrorLog("任务:" + taskid + ",TaskJob回调时执行异常,异常信息:" + JsonConvert.SerializeObject(exp), taskid);
                        }
                    });

                }
                catch (Exception exp)
                {
                    LogProxy.AddTaskErrorLog("任务调度组件回调时发生严重异常,异常信息:" + JsonConvert.SerializeObject(exp), taskid);
                }
            }

        //public override object InitializeLifetimeService()
        //{
        //    //Remoting对象 无限生存期
        //    return null;
        //}
        //}
    }
}
