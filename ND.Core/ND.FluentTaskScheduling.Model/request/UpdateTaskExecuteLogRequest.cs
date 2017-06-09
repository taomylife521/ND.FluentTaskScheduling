using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.TaskInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AddTaskExecuteLogRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-14 13:14:58         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-14 13:14:58          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    /// <summary>
    /// 更新任务执行日志请求
    /// </summary>
    public class UpdateTaskExecuteLogRequest:RequestBase
    {

        /// <summary>
        /// 日志id
        /// </summary>
        [JsonProperty("logid")]
        public int LogId { get; set; }

        /// <summary>
        /// 日志消息
        /// </summary>
        [JsonProperty("logmsg")]
        public string LogMsg { get; set; }

        /// <summary>
        /// 节点id
        /// </summary>
        [JsonProperty("nodeid")]
        public int NodeId { get; set; }

        /// <summary>
        /// 任务id
        /// </summary>
        [JsonProperty("taskid")]
        public int TaskId { get; set; }


        /// <summary>
        /// 任务版本id
        /// </summary>
        [JsonProperty("taskversionid")]
        public int TaskVersionId { get; set; }

        /// <summary>
        /// 任务执行结果
        /// </summary>
          [JsonProperty("taskresult")]
        public RunTaskResult TaskResult{ get; set; }


        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonProperty("endtime")]
        public string EndTime { get; set; }


        /// <summary>
        ///任务运行状态
        /// </summary>
        [JsonProperty("runstatus")]
        public ExecuteStatus RunStatus { get; set; }

        /// <summary>
        ///总共运行时间
        /// </summary>
        [JsonProperty("totalruntime")]
        public string TotalRunTime { get; set; }

       
    }
}
