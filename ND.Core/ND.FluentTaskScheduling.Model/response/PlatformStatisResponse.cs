using ND.FluentTaskScheduling.Model.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：PlatformStatisResponse.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-11 15:12:08         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-11 15:12:08          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.response
{
    /// <summary>
    /// 平台统计响应
    /// </summary>
    public class PlatformStatisResponse
    {
        public PlatformStatisResponse()
        {
            NodeInfo = new NodeStatisDto();
            TaskInfo = new TaskStatisDto();
            CommandQueueInfo = new CommandQueueStatisDto();
            UserInfo = new UserStatisDto();
            CommandInfo = new CommandStatisDto();

        }
        public NodeStatisDto NodeInfo { get; set; }

        public TaskStatisDto TaskInfo { get; set; }

        public CommandQueueStatisDto CommandQueueInfo { get; set; }

        public UserStatisDto UserInfo { get; set; }

        public CommandStatisDto CommandInfo { get; set; }
    }
    #region 节点统计
    /// <summary>
    /// 节点统计
    /// </summary>
    public class NodeStatisDto
    {
        public NodeStatisDto()
        {
            NodeDic = new Dictionary<string, int>();
        }
        /// <summary>
        /// 总节点数量
        /// </summary>
        public int TotalNodeCount;

        public Dictionary<string, int> NodeDic;

        ///// <summary>
        ///// 运行中的节点数量
        ///// </summary>
        //public int RunningNodeCount;

        ///// <summary>
        ///// 未运行中的节点数量
        ///// </summary>
        //public int NoRunNodeCount;

    } 
    #endregion

    #region 任务统计
    /// <summary>
    /// 任务统计
    /// </summary>
    public class TaskStatisDto
    {
        public TaskStatisDto()
        {
            ScheduleDic = new Dictionary<string, int>();
            TaskTypeDic = new Dictionary<string, int>();
        }
        /// <summary>
        /// 总任务数量
        /// </summary>
        public int TotalTaskCount;

        ///// <summary>
        ///// 总调度任务数量
        ///// </summary>
        //public int TotalScheduleTaskCount;

        ///// <summary>
        ///// 总单次任务数量
        ///// </summary>
        //public int TotalOnceTaskCount;

        /// <summary>
        /// 任务类型统计字典
        /// </summary>
        public Dictionary<string, int> TaskTypeDic;

        /// <summary>
        /// 任务调度状态字典
        /// </summary>
        public Dictionary<string, int> ScheduleDic;
        ///// <summary>
        ///// 未调度任务数量
        ///// </summary>
        //public int NoScheduleCount;

        ///// <summary>
        ///// 待调度任务数量
        ///// </summary>
        //public int WaitScheduleCount;

        ///// <summary>
        ///// 调度中的任务数量
        ///// </summary>
        //public int SchedulingTaskCount;

        ///// <summary>
        ///// 暂停调度中的任务数量
        ///// </summary>
        //public int PauseScheduleTaskCount;

        ///// <summary>
        ///// 停止调度中的任务数量
        ///// </summary>
        //public int StopScheduleTaskCount;

    } 
    #endregion

    #region 命令队列统计
    /// <summary>
    /// 命令队列统计
    /// </summary>
    public class CommandQueueStatisDto
    {
        public CommandQueueStatisDto()
        {
            CommandQueueDic = new Dictionary<string, int>();
        }
        /// <summary>
        /// 总命令队列数量
        /// </summary>
        public int TotalCommandQueueCount;

        /// <summary>
        /// 命令队列统计字典
        /// </summary>
        public Dictionary<string, int> CommandQueueDic;

        ///// <summary>
        ///// 未执行的命令队列数量
        ///// </summary>
        //public int NoExecuteCommandQueueCount;

        ///// <summary>
        ///// 执行中的命令队列数量
        ///// </summary>
        //public int ExecutingCommandQueueCount;

        ///// <summary>
        ///// 执行成功的命令队列数量
        ///// </summary>
        //public int ExecuteSucessCommandQueueCount;

        ///// <summary>
        ///// 执行成功的命令队列数量
        ///// </summary>
        //public int ExecuteFailedCommandQueueCount;

        ///// <summary>
        ///// 执行异常的命令队列数量
        ///// </summary>
        //public int ExecuteExceptionCommandQueueCount;
    }
    
    #endregion

    #region 用户统计
    public class UserStatisDto
    {
        public int TotalUserCount { get; set; }
    }
    #endregion

    #region 命令数量
    public class CommandStatisDto
    {
        public int CommandCount { get; set; }
    }
    #endregion
}
