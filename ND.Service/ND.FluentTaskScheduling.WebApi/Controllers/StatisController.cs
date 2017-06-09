using ND.FluentTaskScheduling.Domain.interfacelayer;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using ND.FluentTaskScheduling.TaskInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ND.FluentTaskScheduling.WebApi.Controllers
{
     [RoutePrefix("api/statis")]
    public class StatisController : BaseController
    {
          ITaskRepository taskrep;
         ITaskGroupRepository taskgrouprep;
         ITaskVersionRepository taskversionrep;
         ICommandLibDetailRepository cmdrespository;
         ICommandQueueRepository cmdqueuerespository;
        
         public StatisController(ITaskRepository taskrepository,ICommandLibDetailRepository cmdrep, ITaskGroupRepository taskgrouprepository, ITaskVersionRepository taskversionrepository,  ICommandQueueRepository cmdqueuerep, INodeRepository nodeRep, IUserRepository userRep)
             : base(nodeRep, userRep)
         {
             taskrep = taskrepository;
             taskgrouprep = taskgrouprepository;
             taskversionrep = taskversionrepository;
             cmdqueuerespository = cmdqueuerep;
             cmdrespository = cmdrep;
         }


         #region 统计图表
         /// <summary>
         /// 统计图表
         /// </summary>
         /// <param name="req">统计图表请求类</param>
         /// <returns></returns>
         // [SignAuthorize]
         [ResponseType(typeof(ResponseBase<PlatformStatisResponse>))]
         [HttpPost, Route("platformstatis")]
         public ResponseBase<PlatformStatisResponse> PlatformStatis(PlatformStatisRequest req)
         {
             try
             {
                 PlatformStatisResponse platformrep = new PlatformStatisResponse();
                 #region 统计节点
                 List<tb_node> node = noderepository.Find(x => x.isdel == 0).ToList();
                 platformrep.NodeInfo.TotalNodeCount = node.Count;
                int RunningNodeCount = node.Where(x => x.nodestatus == (int)NodeStatus.Running).Count();
                int NoRunNodeCount = node.Where(x => x.nodestatus == (int)NodeStatus.NoRun).Count();
                 platformrep.NodeInfo.NodeDic.Add(NodeStatus.NoRun.description(), NoRunNodeCount);
                 platformrep.NodeInfo.NodeDic.Add(NodeStatus.Running.description(), RunningNodeCount);
                 #endregion

                 #region 统计任务
                 List<tb_task> task = taskrep.Find(x => x.isdel == 0).ToList();
                 platformrep.TaskInfo.TotalTaskCount = task.Count;
                 int TotalScheduleTaskCount = task.Where(x => x.tasktype == (int)TaskType.SchedulingTask).Count();
                 int TotalOnceTaskCount = task.Where(x => x.tasktype == (int)TaskType.OnceTask).Count();

                 platformrep.TaskInfo.TaskTypeDic.Add(TaskType.SchedulingTask.description() + "_#3c8dbc", TotalScheduleTaskCount);
                 platformrep.TaskInfo.TaskTypeDic.Add(TaskType.OnceTask.description() + "_#f39c12", TotalOnceTaskCount);

                 int WaitScheduleCount = task.Where(x => x.taskschedulestatus == (int)TaskScheduleStatus.WaitSchedule).Count();
                 int NoScheduleCount = task.Where(x => x.taskschedulestatus == (int)TaskScheduleStatus.NoSchedule).Count();
                 int PauseScheduleTaskCount = task.Where(x => x.taskschedulestatus == (int)TaskScheduleStatus.PauseSchedule).Count();
                 int SchedulingTaskCount = task.Where(x => x.taskschedulestatus == (int)TaskScheduleStatus.Scheduling).Count();
                 int StopScheduleTaskCount = task.Where(x => x.taskschedulestatus == (int)TaskScheduleStatus.StopSchedule).Count();

                 platformrep.TaskInfo.ScheduleDic.Add(TaskScheduleStatus.NoSchedule.description() + "_#f39c12", NoScheduleCount);
                 platformrep.TaskInfo.ScheduleDic.Add(TaskScheduleStatus.WaitSchedule.description() + "_#3c8dbc", WaitScheduleCount);
                 platformrep.TaskInfo.ScheduleDic.Add(TaskScheduleStatus.Scheduling.description() + "_#00c0ef", SchedulingTaskCount);
                 platformrep.TaskInfo.ScheduleDic.Add(TaskScheduleStatus.PauseSchedule.description() + "_#ff3bd9", PauseScheduleTaskCount);
                 platformrep.TaskInfo.ScheduleDic.Add(TaskScheduleStatus.StopSchedule.description() + "_#dd4b39", StopScheduleTaskCount);
                 
                 #endregion

                 #region 命令队列统计
                 List<tb_commandqueue> cmdqueue = cmdqueuerespository.Find(x => x.isdel == 0).ToList();
                 platformrep.CommandQueueInfo.TotalCommandQueueCount = cmdqueue.Count;
                 int NoExecuteCommandQueueCount = cmdqueue.Where(x => x.commandstate == (int)(ExecuteStatus.NoExecute)).Count();
                 int ExecuteExceptionCommandQueueCount = cmdqueue.Where(x => x.commandstate == (int)(ExecuteStatus.ExecuteException)).Count();
                 int ExecuteFailedCommandQueueCount = cmdqueue.Where(x => x.commandstate == (int)(ExecuteStatus.ExecuteFailed)).Count();
                 int ExecuteSucessCommandQueueCount = cmdqueue.Where(x => x.commandstate == (int)(ExecuteStatus.ExecuteSucess)).Count();
                 int ExecutingCommandQueueCount = cmdqueue.Where(x => x.commandstate == (int)(ExecuteStatus.Executing)).Count();

                 platformrep.CommandQueueInfo.CommandQueueDic.Add(ExecuteStatus.NoExecute.description() + "_#f39c12", NoExecuteCommandQueueCount);
                 platformrep.CommandQueueInfo.CommandQueueDic.Add(ExecuteStatus.Executing.description() + "_#00c0ef", ExecutingCommandQueueCount);
                 platformrep.CommandQueueInfo.CommandQueueDic.Add(ExecuteStatus.ExecuteSucess.description() + "_#00a65a", ExecuteSucessCommandQueueCount);
                 platformrep.CommandQueueInfo.CommandQueueDic.Add(ExecuteStatus.ExecuteFailed.description() + "_#dd4b39", ExecuteFailedCommandQueueCount);
                 platformrep.CommandQueueInfo.CommandQueueDic.Add(ExecuteStatus.ExecuteException.description() + "_#ff3bd9", ExecuteExceptionCommandQueueCount);
                 
                 #endregion

                 #region 用户统计

                 List<tb_user> user = userrepository.Find(x => x.isdel == 0).ToList();
                 platformrep.UserInfo.TotalUserCount = user.Count;
                 #endregion

                 #region 命令统计
                 List<tb_commandlibdetail> cmd = cmdrespository.Find(x => x.isdel == 0).ToList();
                 platformrep.CommandInfo.CommandCount = cmd.Count;
                 #endregion
                 return ResponseToClient<PlatformStatisResponse>(ResponesStatus.Success, "", platformrep);
             }
             catch(Exception ex)
             {
                 return ResponseToClient<PlatformStatisResponse>(ResponesStatus.Exception, ex.Message);
             }
         }
         #endregion
    }
}
