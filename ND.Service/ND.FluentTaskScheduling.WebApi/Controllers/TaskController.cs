using ND.FluentTaskScheduling.Domain.interfacelayer;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using ND.FluentTaskScheduling.TaskInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ND.FluentTaskScheduling.Helper;

namespace ND.FluentTaskScheduling.WebApi.Controllers
{
     [RoutePrefix("api/task")]
    public class TaskController : BaseController
    {
         ITaskRepository taskrep;
         ITaskGroupRepository taskgrouprep;
         ITaskVersionRepository taskversionrep;
         ITaskLogRepository tasklogrep;
         INodeMonitorRepository nodemonitorRep;
         public TaskController(INodeMonitorRepository nodeMonitorrepostory,ITaskRepository taskrepository,ITaskGroupRepository taskgrouprepository, ITaskVersionRepository taskversionrepository,ITaskLogRepository tasklogRep,INodeRepository nodeRep, IUserRepository userRep)
             : base(nodeRep, userRep)
         {
             taskrep = taskrepository;
             taskgrouprep = taskgrouprepository;
             taskversionrep = taskversionrepository;
             tasklogrep = tasklogRep;
             nodemonitorRep = nodeMonitorrepostory;
         }

         #region 添加任务
         /// <summary>
         /// 添加任务
         /// </summary>
         /// <param name="req">添加任务请求</param>
         /// <remarks></remarks>
         /// <returns></returns>
         // [SignAuthorize]
         [ResponseType(typeof(ResponseBase<AddTaskResponse>))]
         [HttpPost, Route("addtask")]
         public ResponseBase<AddTaskResponse> AddTaskInfo(AddTaskRequest req)
         {
            var task= taskrep.FindSingle(x => x.taskname == req.TaskName && x.isdel==0);
             if(task!=null)
             {
                 return ResponseToClient<AddTaskResponse>(ResponesStatus.Failed, "当前任务名称已存在");
             }
             DateTime dt = DateTime.Now; 
           int taskid= taskrep.AddTask(new tb_task()
             {
                 createby=int.Parse(string.IsNullOrEmpty(req.AdminId)?"0":req.AdminId),
                 createtime=dt,
                 groupid=req.GroupId,
                 isdel=0,
                 taskclassname=req.TaskMainClassName,
                 taskdescription=req.TaskDescription,
                 taskname=req.TaskName,
                 tasknamespace=req.TaskNameSpace,
                 taskremark=string.IsNullOrEmpty(req.TaskRemark)?"":req.TaskRemark,
                 taskschedulestatus = (int)TaskScheduleStatus.NoSchedule,
                 alarmtype=req.AlarmType,
                 alarmuserid=req.AlarmUserId,
                 isenablealarm=req.IsEnabledAlarm,
                 tasktype=req.TaskType
             });
             if(taskid <= 0)
             {
                 return ResponseToClient<AddTaskResponse>(ResponesStatus.Failed, "添加任务失败");
             }
             taskversionrep.Add(new tb_taskversion()
             {
                 nodeid=req.NodeId,
                 taskcreatetime=dt,
                 taskcron=(req.TaskType == (int)TaskType.SchedulingTask?req.TaskCorn:"[simple,,1,,]"),
                 taskerrorcount=0,
                 tasklastendtime=Convert.ToDateTime("2099-12-30"),
                 tasklasterrortime=Convert.ToDateTime("2099-12-30"),
                 tasklaststarttime=Convert.ToDateTime("2099-12-30"),
                 taskparams=string.IsNullOrEmpty(req.TaskParams)?"":req.TaskParams,
                 taskruncount=0,
                 taskrunstatus=(int)ExecuteStatus.NoExecute,
                 taskupdatetime=dt,
                 version=1,
                 versioncreatetime=dt,
                 zipfile=req.TaskDll,
                 zipfilename=req.TaskDllFileName,
                 taskid=taskid
                 
             });
             return ResponseToClient<AddTaskResponse>(ResponesStatus.Success, "添加任务成功");
         }
         #endregion

         #region 更新任务
           /// <summary>
         /// 更新任务
         /// </summary>
         /// <param name="req">更新任务请求</param>
         /// <remarks></remarks>
         /// <returns></returns>
         // [SignAuthorize]
         [ResponseType(typeof(ResponseBase<UpdateTaskInfoResponse>))]
         [HttpPost, Route("updatetask")]
         public ResponseBase<UpdateTaskInfoResponse> UpdateTaskInfo(UpdateTaskInfoRequest req)
         {
             var task=taskrep.FindSingle(x => x.id == req.TaskId);
             var taskVersion = taskversionrep.FindSingle(x => x.id == req.TaskVersionId);
             if(task==null || taskVersion==null)
             {
                 return ResponseToClient<UpdateTaskInfoResponse>(ResponesStatus.Failed, "任务不存在");
             }
             if(task.taskschedulestatus == (int)TaskScheduleStatus.Scheduling)
             {
                 return ResponseToClient<UpdateTaskInfoResponse>(ResponesStatus.Failed, "当前任务正在被调度中,请先停止再修改任务！");
             }
             if (!string.IsNullOrEmpty(req.TaskMainClassName)) { task.taskclassname = req.TaskMainClassName; }
             if (!string.IsNullOrEmpty(req.TaskDescription)) { task.taskdescription = req.TaskDescription; }
             if (!string.IsNullOrEmpty(req.TaskName)) { task.taskname = req.TaskName; }
             if (!string.IsNullOrEmpty(req.TaskNameSpace)) { task.tasknamespace = req.TaskNameSpace; }
             if (!string.IsNullOrEmpty(req.TaskRemark)) { task.taskremark = req.TaskRemark; }
             if (req.IsEnabledAlarm >= 0) { task.isenablealarm = req.IsEnabledAlarm; }
             if (req.AlarmType >= 0) { task.alarmtype = req.AlarmType; }
             if (!string.IsNullOrEmpty(req.AlarmUserId)) { task.alarmuserid = req.AlarmUserId; }
             if (req.GroupId > 0) { task.groupid = req.GroupId; }
             task.tasktype = req.TaskType;
             taskrep.Update(task);

             if (req.NodeId>0) { taskVersion.nodeid = req.NodeId; }

             if (!string.IsNullOrEmpty(req.TaskCorn)) { taskVersion.taskcron = (req.TaskType == (int)TaskType.SchedulingTask ? req.TaskCorn : "[simple,,1,,]"); }
             if (!string.IsNullOrEmpty(req.TaskParams)) { taskVersion.taskparams = req.TaskParams; }
             if (!string.IsNullOrEmpty(req.TaskDllFileName)) { taskVersion.zipfilename = req.TaskDllFileName; }
             if (req.TaskDll != null && req.TaskDll.Length>0) { taskVersion.zipfile = req.TaskDll; }
             taskVersion.taskupdatetime = DateTime.Now;
             taskversionrep.Update(taskVersion);
             return ResponseToClient<UpdateTaskInfoResponse>(ResponesStatus.Success, "更新任务成功");
         }
         #endregion

         #region 任务列表
          /// <summary>
         /// 获取任务列表
         /// </summary>
         /// <param name="req">获取任务列表请求</param>
         /// <remarks></remarks>
         /// <returns></returns>
         // [SignAuthorize]
         [ResponseType(typeof(ResponseBase<PageInfoResponse<List<TaskListDto>>>))]
         [HttpPost, Route("tasklist")]
         public ResponseBase<PageInfoResponse<List<TaskListDto>>> LoadTaskList(LoadTaskListRequest req)
         {
             #region old code
             //DateTime starttime = Convert.ToDateTime(req.TaskCreateTimeRange.Split('/')[0]);
             //DateTime endtime = Convert.ToDateTime(req.TaskCreateTimeRange.Split('/')[1]).AddDays(1);
             //int pageIndex = (req.iDisplayStart / req.iDisplayLength) + 1;
             //int totalCount = 0;
             //List<int> taskidlist = taskrep.Find(out totalCount, pageIndex, req.iDisplayLength, "id",
             //    x => x.taskschedulestatus == (req.TaskSchduleStatus < 0 ? x.taskschedulestatus : req.TaskSchduleStatus)
             //        && x.taskname.Contains(string.IsNullOrEmpty(req.TaskName) ? x.taskname : req.TaskName)
             //    ).Select(x => x.id).ToList();
             //var taskversionlist = taskversionrep.Find(x =>
             //                                x.nodeid == (req.NodeId <= 0 ? x.nodeid : req.NodeId)
             //                                && taskidlist.Contains(x.taskid)
             //                                && x.taskrunstatus == (req.TaskExecuteStatus < 0 ? x.taskrunstatus : req.TaskExecuteStatus)
             //    );

             //if (taskidlist.Count <= 0)
             //{
             //    return ResponseToClient<LoadTaskListResponse>(ResponesStatus.Failed, "任务列表为空");
             //}
             //List<TaskListDto> TaskList = new List<TaskListDto>();
             //taskidlist.ForEach(x =>
             //{
             //    var taskversion = taskversionlist.FirstOrDefault(m => m.taskid == x);
             //    TaskList.Add(new TaskListDto()
             //    {
             //        Task = taskrep.FindSingle(m => m.id == x),
             //        TaskVersion = taskversion,
             //        Node = noderepository.FindSingle(m => m.id == taskversion.nodeid)
             //    });
             //});
             //return ResponseToClient<LoadTaskListResponse>(ResponesStatus.Success, "", new LoadTaskListResponse() { TaskList = TaskList });
             #endregion
             int totalCount = 0;
            List<TaskListDto> TaskList= taskrep.LoadTaskPageList(out totalCount, req);
             if(TaskList.Count <= 0)
             {
                 return ResponseToClient<PageInfoResponse<List<TaskListDto>>>(ResponesStatus.Failed, "");
             }
            return ResponseToClient<PageInfoResponse<List<TaskListDto>>>(ResponesStatus.Success, "", new PageInfoResponse<List<TaskListDto>>() { aaData = TaskList,sEcho=req.sEcho,iTotalDisplayRecords=totalCount,iTotalRecords=totalCount });
             
         }
         #endregion

         #region 根据调度状态获取任务id列表
         /// <summary>
         /// 获取任务列表
         /// </summary>
         /// <param name="req">获取任务列表请求</param>
         /// <remarks></remarks>
         /// <returns></returns>
         // [SignAuthorize]
         [ResponseType(typeof(ResponseBase<LoadTaskIdListResponse>))]
         [HttpPost, Route("taskidlist")]
         public ResponseBase<LoadTaskIdListResponse> LoadTaskIdList(LoadTaskIdListRequest req)
         {
            
            List<int> taskidlist= taskrep.Find(x => x.taskschedulestatus == req.TaskScheduleStatus).Select(x => x.id).ToList();
            if (taskidlist.Count <= 0)
             {
                 return ResponseToClient<LoadTaskIdListResponse>(ResponesStatus.Failed, "");
             }
            return ResponseToClient<LoadTaskIdListResponse>(ResponesStatus.Success, "", new LoadTaskIdListResponse() { TaskIdList=taskidlist });

         }
         #endregion

         #region 获取任务版本详情
         /// <summary>
         /// 获取任务版本详情
         /// </summary>
         /// <param name="req">获取任务版本详情请求</param>
         /// <remarks></remarks>
         /// <returns></returns>
         // [SignAuthorize]
         [ResponseType(typeof(ResponseBase<LoadTaskVersionResponse>))]
         [HttpPost, Route("taskversiondetail")]
         public ResponseBase<LoadTaskVersionResponse> TaskVersionDetail(LoadTaskVersionRequest req)
         {

             var taskversion = taskversionrep.FindSingle(x => x.id == req.TaskVersionId && x.taskid == req.TaskId);
             if (taskversion != null)
             {
                 var task=taskrep.FindSingle(x => x.id == req.TaskId);
                List<int> useridlist= task.alarmuserid.ConvertToList();
                List<string> AlarmEmailList = new List<string>();
                List<string> AlarmMobileList = new List<string>();
                userrepository.Find(x => useridlist.Contains(x.id)).ToList().ForEach(x =>
                {
                    if (task.alarmtype == (int)AlarmType.Email)
                    {
                        if (!string.IsNullOrEmpty(x.useremail))
                        {
                            AlarmEmailList.Add(x.useremail);
                        }
                    }
                    else if (task.alarmtype == (int)AlarmType.SMS)
                    {
                        if (!string.IsNullOrEmpty(x.usermobile))
                        {
                            AlarmEmailList.Add(x.usermobile);
                        }
                    }
                });
                return ResponseToClient<LoadTaskVersionResponse>(ResponesStatus.Success, "", new LoadTaskVersionResponse() { TaskVersionDetail = taskversion, TaskDetail = task, AlarmEmailList = (AlarmEmailList.Count <= 0 ? "" : string.Join(",", AlarmEmailList)), AlarmMobileList = (AlarmMobileList.Count <= 0 ? "" : string.Join(",", AlarmMobileList)) });
             }
             return ResponseToClient<LoadTaskVersionResponse>(ResponesStatus.Failed, "未找到该任务版本号对应的信息");
         } 
         #endregion

         #region 更新任务调度状态
         /// <summary>
         /// 获取任务版本详情
         /// </summary>
         /// <param name="req">获取任务版本详情请求</param>
         /// <remarks></remarks>
         /// <returns></returns>
         // [SignAuthorize]
         [ResponseType(typeof(ResponseBase<EmptyResponse>))]
         [HttpPost, Route("updatetaskschedulestatus")]
         public ResponseBase<EmptyResponse> UpdateTaskScheduleStatus(UpdateTaskScheduleStatusRequest req)
         {
             
             var taskversion = taskrep.FindSingle(x =>  x.id == req.TaskId);
             if (taskversion == null)
             {
                 return ResponseToClient<EmptyResponse>(ResponesStatus.Failed, "未找到该任务版本号对应的信息");
                 
             }
             taskversion.taskschedulestatus = (int)req.ScheduleStatus;
             if(req.ScheduleStatus == TaskScheduleStatus.PauseSchedule)
             {
                 taskversion.ispauseschedule = 1;
             }
             if (req.ScheduleStatus == TaskScheduleStatus.RecoverSchedule)
             {
                 taskversion.ispauseschedule = 0;
                 taskversion.taskschedulestatus =(int)TaskScheduleStatus.Scheduling;
             }
             taskversion.nextruntime = string.IsNullOrEmpty(req.NextRunTime) ? taskversion.nextruntime : Convert.ToDateTime(req.NextRunTime);
             taskrep.Update(taskversion);
             return ResponseToClient<EmptyResponse>(ResponesStatus.Success, "");
         } 
         #endregion

         #region 添加任务执行日志并更新任务状态
         /// <summary>
         /// 添加任务执行日志信息
         /// </summary>
         /// <param name="req">添加任务执行日志请求类</param>
         /// <remarks></remarks>
         /// <returns></returns>
         // [SignAuthorize]
         [ResponseType(typeof(ResponseBase<AddTaskExecuteLogResponse>))]
         [HttpPost, Route("addtaskxecutelog")]
         public ResponseBase<AddTaskExecuteLogResponse> AddTaskExecuteLog(AddTaskExecuteLogRequest req)
         {
             try
             {
                 var taskmodel = taskversionrep.FindSingle(x => x.id == req.TaskVersionId && x.taskid == req.TaskId && x.nodeid == req.NodeId);
                 if (taskmodel == null)
                 {
                     return ResponseToClient<AddTaskExecuteLogResponse>(ResponesStatus.Failed, "任务已不存在");
                 }
                 DateTime dt=DateTime.Now;
                 int runstatus=(int)req.RunStatus;
                 //添加执行命令日志
                int r= tasklogrep.AddTaskLog(new tb_tasklog() { 
                    logcreatetime=dt,
                    logmsg="",
                    nodeid=req.NodeId,
                    taskid=req.TaskId,
                    taskversionid=req.TaskVersionId,
                    taskparams=req.TaskParams,
                    taskstarttime=Convert.ToDateTime(req.StartTime),
                    taskstatus = (int)req.RunStatus,
                    nextruntime=Convert.ToDateTime(req.NextRunTime)
                 });
                 
                 if(r<= 0)
                 {
                     return ResponseToClient<AddTaskExecuteLogResponse>(ResponesStatus.Failed, "添加任务执行日志失败");
                 }
                 taskmodel.taskrunstatus = (int)req.RunStatus;
                 taskversionrep.Update(taskmodel);//更新任务实体
                 taskrep.UpdateById(new List<int> { taskmodel.taskid }, new Dictionary<string, string>() { {"nextruntime", req.NextRunTime} });//更新任务下次运行时间
                 return ResponseToClient<AddTaskExecuteLogResponse>(ResponesStatus.Success, "", new AddTaskExecuteLogResponse() { LogId = r.ToString() });
             }
             catch (Exception ex)
             {
                 return ResponseToClient<AddTaskExecuteLogResponse>(ResponesStatus.Exception, JsonConvert.SerializeObject(ex));
             }
         }
         #endregion

         #region 更新任务执行日志并更新任务状态
         /// <summary>
         /// 更新任务执行日志信息
         /// </summary>
         /// <param name="req">更新任务执行日志请求类</param>
         /// <remarks></remarks>
         /// <returns></returns>
         // [SignAuthorize]
         [ResponseType(typeof(ResponseBase<EmptyResponse>))]
         [HttpPost, Route("updatetaskxecutelog")]
         public ResponseBase<EmptyResponse> UpdateTaskExecuteLog(UpdateTaskExecuteLogRequest req)
         {
             try
             {
              
                 var taskmodel = taskversionrep.FindSingle(x => x.id == req.TaskVersionId && x.taskid == req.TaskId && x.isdel==0 && x.nodeid == req.NodeId);
                 if (taskmodel == null)
                 {
                     return ResponseToClient<EmptyResponse>(ResponesStatus.Failed, "任务已不存在");
                 }
                
                 var tasklogModel=tasklogrep.FindSingle(x => x.id == req.LogId && x.taskversionid == req.TaskVersionId && x.nodeid == req.NodeId && x.taskid == req.TaskId);
                 tasklogModel.logmsg = req.LogMsg;
                 tasklogModel.taskendtime = Convert.ToDateTime(req.EndTime);
                 tasklogModel.taskrunresult =JsonConvert.SerializeObject(req.TaskResult);
                 tasklogModel.taskstatus = (int)req.RunStatus;
                 tasklogModel.totalruntime = decimal.Parse(req.TotalRunTime);
                 tasklogrep.Update(tasklogModel);//更新执行日志
                 taskmodel.taskrunstatus = (int)req.RunStatus;
                 switch(req.RunStatus)
                 {
                     case ExecuteStatus.ExecuteFailed:
                         taskmodel.taskfailedcount = taskmodel.taskfailedcount + 1;
                         break;
                     case ExecuteStatus.ExecuteException:
                         taskmodel.taskerrorcount = taskmodel.taskerrorcount + 1;
                         break;
                     case ExecuteStatus.ExecuteSucess:
                         taskmodel.tasksucesscount = taskmodel.tasksucesscount + 1;
                         break;
                     default:
                         break;
                 }
                 taskmodel.taskruncount = taskmodel.taskruncount + 1;
                 taskversionrep.Update(taskmodel);//更新任务实体
                 return ResponseToClient<EmptyResponse>(ResponesStatus.Success, "");
             }
             catch (Exception ex)
             {
                 return ResponseToClient<EmptyResponse>(ResponesStatus.Exception, JsonConvert.SerializeObject(ex));
             }
         }
         #endregion
    }
}
