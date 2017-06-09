using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Core.TaskHandler;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.Model.request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：RecoverScheduleTaskCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-25 15:32:29         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-25 15:32:29          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandSet
{
    /// <summary>
    /// 恢复调度任务命令
    /// </summary>
    public class RecoverScheduleTaskCommand:AbstractCommand
    {
        public override string CommandDisplayName
        {
            get
            {
                return "恢复任务调度";
            }

        }
        public override string CommandDescription
        {
            get
            {
                return "该命令用于通知各个节点的恢复当前任务的调度";
            }

        }
        public override Model.RunCommandResult Execute()
        {
            try
            {
                string taskid = CommandQueue.taskid.ToString();
                var taskruntimeinfo = TaskPoolManager.CreateInstance().Get(taskid.ToString());
               
                if (taskruntimeinfo == null)
                {
                    ShowCommandLog("当前任务不存在于本地节点中,恢复任务调度失败");
                    return new Model.RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteFailed, Message = "当前任务不存在于本地节点中,暂停任务失败" };
                }
               
                TaskPoolManager.CreateInstance().UpdateTaskSchduleStatus(taskid, TaskScheduleStatus.RecoverSchedule);
                var req = new UpdateTaskScheduleStatusRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Node, ScheduleStatus = Model.enums.TaskScheduleStatus.RecoverSchedule, TaskId = int.Parse(taskid), TaskVersionId = taskruntimeinfo.TaskVersionModel.id };
                var r2 = NodeProxy.PostToServer<EmptyResponse, UpdateTaskScheduleStatusRequest>(ProxyUrl.UpdateTaskScheduleStatus_Url, req);
                if (r2.Status != ResponesStatus.Success)
                {
                    ShowCommandLog("更新任务调度状态(恢复调度)失败,请求Url：" + ProxyUrl.UpdateTaskScheduleStatus_Url + ",请求参数:" + JsonConvert.SerializeObject(req) + ",返回参数:" + JsonConvert.SerializeObject(r2));
                }
                return new Model.RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteSucess };
            }
            catch (Exception ex)
            {
                return new Model.RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteException, Message = ex.Message, Ex = ex };
            }
        }
        public override void InitAppConfig()
        {

        }
    }
}
