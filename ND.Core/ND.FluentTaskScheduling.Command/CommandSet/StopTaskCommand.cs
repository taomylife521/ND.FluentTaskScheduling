using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Core.CommandSet;
using ND.FluentTaskScheduling.Core.TaskHandler;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.TaskInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：StopTaskCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-10 11:03:48         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-10 11:03:48          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandSet
{
    public class StopTaskCommand : AbstractCommand
    {
        public override string CommandDisplayName
        {
            get
            {
                return "停止任务";
            }

        }
        public override string CommandDescription
        {
            get
            {
                return "该命令用于通知各个节点停止任务";
            }

        }
        /// <summary>
        /// 执行
        /// </summary>
        public override RunCommandResult Execute()
        {
            try
            {
                string taskid = CommandQueue.taskid.ToString();
                int taskversionid = CommandQueue.taskversionid;
                var taskruntimeinfo = TaskPoolManager.CreateInstance().Get(taskid.ToString());
                if (taskruntimeinfo == null)
                {
                    ShowCommandLog("任务不在运行中");
                    //return new RunCommandResult() { RunStatus = CommandRunStatus.Normal };
                }
                else
                {

                    var r = TaskDisposer.DisposeTask(CommandQueue.taskid, taskruntimeinfo, false, ShowCommandLog);
                }
               //上报任务已停止日志,并更新任务执行状态和调度状态(待调度)
                var req = new UpdateTaskScheduleStatusRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Node, ScheduleStatus = Model.enums.TaskScheduleStatus.StopSchedule, TaskId = int.Parse(taskid), TaskVersionId = taskversionid };
                var r2 = NodeProxy.PostToServer<EmptyResponse, UpdateTaskScheduleStatusRequest>(ProxyUrl.UpdateTaskScheduleStatus_Url, req);
                if (r2.Status != ResponesStatus.Success)
                {
                    ShowCommandLog("更新任务调度状态(停止调度)失败,请求Url：" + ProxyUrl.UpdateTaskScheduleStatus_Url + ",请求参数:" + JsonConvert.SerializeObject(req) + ",返回参数:" + JsonConvert.SerializeObject(r2));
                }
                return new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteSucess };
            }
            catch(Exception ex)
            {
                ShowCommandLog("停止执行节点任务失败,异常信息:" + JsonConvert.SerializeObject(ex));
                return new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteException, Ex = ex, Message = ex.Message };
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
