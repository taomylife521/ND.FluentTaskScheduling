using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using ND.FluentTaskScheduling.TaskInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AbstractTaskExtentions.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-14 14:48:29         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-14 14:48:29          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.TaskHandler
{
    public static class AbstractTaskExtentions 
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region TryRunTask
        public static RunTaskResult TryRunTask(this AbstractTask task,string nextRunTime,NodeTaskRunTimeInfo taskruntimeinfo)
        {
           
            RunTaskResult result = new RunTaskResult() { RunStatus = (int)RunStatus.Failed };
            string logId = "";
            long times = 0;
            Model.enums.ExecuteStatus executestatus = Model.enums.ExecuteStatus.ExecuteSucess;
            try
            {
                task.ShowProcessIngLog("----------------------" + DateTime.Now + ":开始执行任务-----------------------------------------");
                DateTime startTime = DateTime.Now;
                //开始执行任务，上报开始执行日志，并更新任务版本状态为执行中
                #region 开始执行任务，上报开始执行日志，并更新任务版本状态为执行中
                AddTaskExecuteLogRequest req = new AddTaskExecuteLogRequest()
                            {
                                NodeId = GlobalNodeConfig.NodeID,
                                RunStatus = Model.enums.ExecuteStatus.Executing,
                                Source = Model.Source.Node,
                                StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                TaskId = taskruntimeinfo.TaskModel.id,
                                TaskParams = taskruntimeinfo.TaskVersionModel.taskparams,
                                TaskVersionId = taskruntimeinfo.TaskVersionModel.id,
                                NextRunTime = nextRunTime
                            };
                var r2 = NodeProxy.PostToServer<AddTaskExecuteLogResponse, AddTaskExecuteLogRequest>(ProxyUrl.AddTaskVersionExecuteLog_Url, req);
                if (r2.Status != ResponesStatus.Success)
                {
                    task.ShowProcessIngLog("上报任务(taskid=" + task.TaskDetail.id + "taskversionid=" + task.TaskVersionDetail.id + ")的执行日志失败,请求地址:" + ProxyUrl.AddTaskVersionExecuteLog_Url + ",请求参数:" + JsonConvert.SerializeObject(req) + ",服务器返回参数:" + JsonConvert.SerializeObject(r2));
                }
                logId = r2.Data.LogId;
                #endregion

                Stopwatch sw = new Stopwatch();
                sw.Start();
                try
                {
                    result = task.RunTask();
                }
                catch (Exception ex)
                {
                    result = new RunTaskResult() { RunStatus = (int)RunStatus.Exception, Message = ex.Message, Ex = ex };
                    task.ShowProcessIngLog("执行任务异常,异常信息:" + JsonConvert.SerializeObject(ex));
                }
                sw.Stop();

                TimeSpan ts = sw.Elapsed;
                times = sw.ElapsedMilliseconds / 1000;//秒 

                executestatus =result.RunStatus == (int)RunStatus.Normal? Model.enums.ExecuteStatus.ExecuteSucess:(result.RunStatus == (int)RunStatus.Failed?ExecuteStatus.ExecuteFailed:ExecuteStatus.ExecuteException);

            }
            catch (Exception exp)
            {
                executestatus = Model.enums.ExecuteStatus.ExecuteException;
                task.ShowProcessIngLog("执行任务异常,异常信息:" + JsonConvert.SerializeObject(exp));


            }
            finally
            {
                #region 执行任务完毕，上报开始执行日志，并更新任务版本状态为执行中
                task.ShowProcessIngLog("上报任务状态:" + executestatus);
                task.ShowProcessIngLog("----------------------" + DateTime.Now + ":执行任务完成-----------------------------------------");
         
                DateTime endTime = DateTime.Now;
                //上报任务执行日志和执行结果，并更新最后一次任务状态
                UpdateTaskExecuteLogRequest req2 = new UpdateTaskExecuteLogRequest()
                {
                    NodeId = GlobalNodeConfig.NodeID,
                    RunStatus = executestatus,
                    Source = Model.Source.Node,
                    EndTime = endTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    TaskId = taskruntimeinfo.TaskModel.id,
                    TaskResult = result,
                    LogId =string.IsNullOrEmpty(logId)?0: int.Parse(logId),
                    TotalRunTime = times.ToString(),
                    TaskVersionId = taskruntimeinfo.TaskVersionModel.id,
                    LogMsg = task.GetLog(),
                   
                };
                if(!string.IsNullOrEmpty(logId))
                {
                    var r3 = NodeProxy.PostToServer<EmptyResponse, UpdateTaskExecuteLogRequest>(ProxyUrl.UpdateTaskVersionExecuteLog_Url, req2);
                    if (r3.Status != ResponesStatus.Success)
                    {
                        task.ShowProcessIngLog("上报任务(taskid=" + taskruntimeinfo.TaskModel.id + "taskversionid=" + taskruntimeinfo.TaskVersionModel.id + ")的执行日志失败,请求地址:" + ProxyUrl.AddTaskVersionExecuteLog_Url + ",请求参数:" + JsonConvert.SerializeObject(req2) + ",服务器返回参数:" + JsonConvert.SerializeObject(r3));
                    }
                }
                log.Info(task.GetLog().ToString());
                Alaram(req2, taskruntimeinfo,task);
                task.ClearLog();
                #endregion
            }
            return result;
           
        } 
        #endregion

        #region 更新暂停调度状态
        public static bool UpdateTaskPauseStatus(this AbstractTask task,int isPauseStatus)
        {
            task.UpdateTaskPauseStatus(isPauseStatus);
            return true;
        } 
        #endregion

        #region 报警
        private static void Alaram(UpdateTaskExecuteLogRequest req2, NodeTaskRunTimeInfo task,AbstractTask abstracttask)
        {
            if (task.TaskModel.isenablealarm == 1)
            {
                string alarmperson = task.TaskModel.alarmtype == (int)AlarmType.Email ? abstracttask.GetAlarmEmailList() : abstracttask.GetAlarmMobileList();
                string title = "任务名称(" + task.TaskModel.taskname + ")" + req2.RunStatus.description() + ",请及时处理!";
                StringBuilder strContent = new StringBuilder();
                strContent.AppendLine("所在节点名称(编号):" + GlobalNodeConfig.NodeInfo.nodename + "(" + GlobalNodeConfig.NodeID + ")<br/>");
                strContent.AppendLine("任务名称(编号):" + task.TaskModel.taskname + "(" + task.TaskModel.id + ")<br/>");
                strContent.AppendLine("任务类型:" + ((TaskType)task.TaskModel.tasktype).description() + "<br/>");
                strContent.AppendLine("任务执行参数:" + task.TaskVersionModel.taskparams + "<br/>");
                strContent.AppendLine("任务执行日志:" + req2.LogMsg + "<br/>");
                strContent.AppendLine("任务执行结果,状态:" + req2.RunStatus.description() + ",结果:" + JsonConvert.SerializeObject(req2.TaskResult) + "<br/>");
                strContent.AppendLine("任务执行耗时(s):" + req2.TotalRunTime + "<br/>");
                AlarmHelper.AlarmAsync(task.TaskModel.isenablealarm, (AlarmType)task.TaskModel.alarmtype, alarmperson, title, strContent.ToString());
            }
        }
        #endregion

       
    }
}
