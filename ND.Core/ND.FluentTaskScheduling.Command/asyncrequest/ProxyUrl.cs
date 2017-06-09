using ND.FluentTaskScheduling.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：ProxyUrl.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-10 16:10:01         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-10 16:10:01          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Command.asyncrequest
{
    public class ProxyUrl
    {
        #region Node
        /// <summary>
        /// 载入节点配置url
        /// </summary>
        public static readonly string LoadNodeConfig_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/node/" + "loadnodeconfig/";

        /// <summary>
        /// 更新节点心跳
        /// </summary>
        public static readonly string UpdateNodeHeatbeat_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/node/" + "updatenodeheatbeat/";

        /// <summary>
        /// 更新节点刷新命令状态
        /// </summary>

        public static readonly string UpdateNodeRefreshCmdQueueStatus_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/node/" + "updatenoderefreshcmdqueuestatus/";

        /// <summary>
        /// 更新节点运行状态
        /// </summary>

        public static readonly string UpdateNodeStatus_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/node/" + "updatenodestatus/";

        /// <summary>
        /// 添加节点请求
        /// </summary>
        public static readonly string AddNodeMonitorRequest_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/node/" + "addnodemonitor/";

        /// <summary>
        /// 更新节点监控状态
        /// </summary>
        public static readonly string UpdateNodeMonitorRequest_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/node/" + "updatenodemonitor/";
      
        #endregion

        #region Command
        /// <summary>
        /// 载入命令列表url
        /// </summary>
        public static readonly string LoadCommandList_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/command/" + "loadcommandlist/";
        #endregion



        #region CommandQueue
        /// <summary>
        /// 载入命令队列列表url
        /// </summary>
        public static readonly string LoadCommandQueueList_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/commandqueue/" + "loadcommandqueuelist/";

        /// <summary>
        /// 更新命令队列中的状态
        /// </summary>
        public static readonly string UpdateCommandQueueStatus_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/commandqueue/" + "updatecommandqueuestatus/"; 
        #endregion



        #region Log
        /// <summary>
        /// 添加日志
        /// </summary>
        public static readonly string AddLog_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/log/" + "addlog/";

        /// <summary>
        /// 添加命令执行日志
        /// </summary>
        public static readonly string AddCommandExecuteLog_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/log/" + "addcommandexecutelog/"; 
        #endregion


        #region Task
        /// <summary>
        /// 获取任务版本号详情
        /// </summary>
        public static readonly string TaskVersionDetail_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/task/" + "taskversiondetail/";

        /// <summary>
        /// 添加任务版本执行日志
        /// </summary>
        public static readonly string AddTaskVersionExecuteLog_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/task/" + "addtaskxecutelog/";

        /// <summary>
        /// 更新任务版本执行日志
        /// </summary>
        public static readonly string UpdateTaskVersionExecuteLog_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/task/" + "updatetaskxecutelog/";

        /// <summary>
        /// 更新任务调度状态
        /// </summary>
        public static readonly string UpdateTaskScheduleStatus_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/task/" + "updatetaskschedulestatus/";

        /// <summary>
        /// 载入任务id列表
        /// </summary>
        public static readonly string LoadTaskIdList_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/task/" + "taskidlist/";
        #endregion

        #region Performance
         public static readonly string AddPerformance_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/performance/" + "addperformance/";
         public static readonly string AddNodePerformance_Url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/performance/" + "addnodeperformance/";
        
        #endregion
        
    }
}
