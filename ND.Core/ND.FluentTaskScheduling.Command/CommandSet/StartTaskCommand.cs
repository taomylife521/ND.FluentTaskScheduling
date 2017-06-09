using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Core.CommandHandler;
using ND.FluentTaskScheduling.Core.TaskHandler;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using ND.FluentTaskScheduling.TaskInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：StartTaskCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-05 10:59:56         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-05 10:59:56          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandSet
{
    /// <summary>
    /// 开启任务命令 创建NodeTaskRunTimeInfo信息,为当前任务加载AppDomain, 并添加到任务池等待执行
    /// </summary>
   public class StartTaskCommand:AbstractCommand
    {

       public override string CommandDisplayName
       {
           get
           {
               return "开启任务";
           }
           
       }
       public override string CommandDescription
       {
           get
           {
               return "该命令用于通知各个节点执行命令";
           }
           
       }
       /// <summary>
       /// 执行
       /// </summary>
       public override RunCommandResult Execute()
       {
           string taskid = CommandQueue.taskid.ToString();
           string taskversionid = CommandQueue.taskversionid.ToString();
           try
           {
              
              
               var taskruntimeinfo = TaskPoolManager.CreateInstance().Get(taskid.ToString());
               if (taskruntimeinfo != null)
               {
                   ShowCommandLog("任务已在运行中");
                   UploadStatus(taskid, taskversionid, TaskScheduleStatus.Scheduling, "");
                   return new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteFailed, Message = "任务已在运行中" };
               }
               taskruntimeinfo = new NodeTaskRunTimeInfo();
               taskruntimeinfo.TaskLock = new TaskLock();
               //读取任务版本信息
                var taskversionreq=new LoadTaskVersionRequest() {TaskId=CommandQueue.taskid,TaskVersionId=CommandQueue.taskversionid, Source = Source.Node};
                var r = NodeProxy.PostToServer<LoadTaskVersionResponse, LoadTaskVersionRequest>(ProxyUrl.TaskVersionDetail_Url, taskversionreq);
                if (r.Status != ResponesStatus.Success)
                {
                    ShowCommandLog("获取任务版本号详情失败,请求Url：" + ProxyUrl.TaskVersionDetail_Url + ",请求参数:" + JsonConvert.SerializeObject(taskversionreq)+",返回参数:"+JsonConvert.SerializeObject(r));
                    return new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteFailed, Message = "获取任务版本号详情失败" };
                }
                taskruntimeinfo.TaskVersionModel = r.Data.TaskVersionDetail;
                taskruntimeinfo.TaskModel = r.Data.TaskDetail;
                ShowCommandLog("开始创建缓存目录,域安装目录,拷贝共享程序集...");
               string filelocalcachepath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + GlobalNodeConfig.TaskDllCompressFileCacheDir + @"\" + taskid + @"\" +r.Data.TaskVersionDetail.version + @"\"+r.Data.TaskVersionDetail.zipfilename;
               string domaininstallpath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + GlobalNodeConfig.TaskDllDir + "\\" + taskid+"\\";
              // string domaininstallmainclassdllpath = domaininstallpath + @"\" + taskruntimeinfo.TaskModel.taskclassname;
               string taskshareddlldir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + GlobalNodeConfig.TaskSharedDllsDir;
               //通知节点TaskProvider任务执行
               IOHelper.CreateDirectory(filelocalcachepath);
               IOHelper.CreateDirectory(domaininstallpath);
               File.WriteAllBytes(filelocalcachepath, taskruntimeinfo.TaskVersionModel.zipfile);
               CompressHelper.UnCompress(filelocalcachepath, domaininstallpath);
               IOHelper.CopyDirectory(taskshareddlldir, domaininstallpath);//拷贝共享程序集到域安装路径
               ShowCommandLog("目录操作完成,拷贝共享程序集完成,开始创建任务的AppDomain域");
               try
               {
                   var dllpath=Path.Combine(domaininstallpath, taskruntimeinfo.TaskVersionModel.zipfilename + ".dll");
                   AbstractTask dllTask = new AppDomainLoaderHelper<AbstractTask>().CreateDomain(dllpath.Replace(".rar","").Replace("zip",""), taskruntimeinfo.TaskModel.taskclassname, out taskruntimeinfo.Domain);
                   dllTask.SetAlarmList(r.Data.AlarmEmailList,r.Data.AlarmMobileList);//设置任务报警信息
                   tb_task cloneTaskModel=taskruntimeinfo.TaskModel.CloneObj<tb_task>();
                   tb_taskversion cloneTaskVersionModel = taskruntimeinfo.TaskVersionModel.CloneObj<tb_taskversion>();
                   dllTask.TaskDetail = cloneTaskModel;
                   dllTask.TaskVersionDetail = cloneTaskVersionModel;
                   dllTask.AppConfig = new TaskAppConfigInfo();
                   
                  //CommandParams cmdparams= JsonConvert.DeserializeObject<CommandParams>(CommandQueue.commandparams);
                  //if (!string.IsNullOrEmpty(cmdparams.TaskParams))
                  //{
                  //    dllTask.AppConfig = JsonConvert.DeserializeObject<TaskAppConfigInfo>(cmdparams.TaskParams);
                  //}
                  //else
                  //{
                      if (!string.IsNullOrEmpty(taskruntimeinfo.TaskVersionModel.taskparams))
                      {
                          dllTask.AppConfig = JsonConvert.DeserializeObject<TaskAppConfigInfo>(taskruntimeinfo.TaskVersionModel.taskparams);
                      }
                  //}
                   taskruntimeinfo.DllTask = dllTask;
                   string nextruntime = "2099-12-30";
                   TaskPoolManager.CreateInstance().Add(taskid.ToString(), taskruntimeinfo, ref nextruntime);
                   ShowCommandLog("加载AppDomain域成功,开始添加到任务池等待执行");
                   //上报任务执行日志，并更新调度状态为调度中
                   UploadStatus(taskid, taskversionid, TaskScheduleStatus.Scheduling,nextruntime);
                   ShowCommandLog("添加到任务池成功,开启任务成功");
                   return new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteSucess };
               }
               catch(Exception ex)
               {
                   ShowCommandLog("加载任务应用程序域异常,异常信息:"+JsonConvert.SerializeObject(ex));
                   UploadStatus(taskid, taskversionid, TaskScheduleStatus.StopSchedule);
                   return new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteException, Message = ex.Message, Ex = ex };
               }
              
           }
           catch(Exception ex)
           {
               ShowCommandLog("开启任务命令异常,异常信息:"+JsonConvert.SerializeObject(ex));
               UploadStatus(taskid, taskversionid, TaskScheduleStatus.StopSchedule);
               return new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteException, Message = ex.Message, Ex = ex };
           }
       }

       /// <summary>
       /// 上报任务调度状态
       /// </summary>
       /// <param name="taskid"></param>
       /// <param name="taskversionid"></param>
       /// <param name="status"></param>
       private void UploadStatus(string taskid,string taskversionid,TaskScheduleStatus status,string nextruntime="")
       {
           var req2 = new UpdateTaskScheduleStatusRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Node, ScheduleStatus = status, TaskId = int.Parse(taskid), TaskVersionId = int.Parse(taskversionid),NextRunTime=nextruntime };
           var r3 = NodeProxy.PostToServer<EmptyResponse, UpdateTaskScheduleStatusRequest>(ProxyUrl.UpdateTaskScheduleStatus_Url, req2);
           if (r3.Status != ResponesStatus.Success)
           {
               ShowCommandLog("更新任务调度状态("+status.description()+")失败,请求Url：" + ProxyUrl.UpdateTaskScheduleStatus_Url + ",请求参数:" + JsonConvert.SerializeObject(req2) + ",返回参数:" + JsonConvert.SerializeObject(r3));
           }
       }

       /// <summary>
       /// 初始化命令所需参数
       /// </summary>
       public override void InitAppConfig()
       {
         
       }
    }
}
