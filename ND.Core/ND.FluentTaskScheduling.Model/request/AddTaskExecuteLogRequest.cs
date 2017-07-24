using ND.FluentTaskScheduling.Model.enums;
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
    /// 添加任务执行日志请求
    /// </summary>
    public class AddTaskExecuteLogRequest:RequestBase
    {
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
        /// 任务执行参数
        /// </summary>
          [JsonProperty("taskparams")]
        public string TaskParams { get; set; }


        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonProperty("starttime")]
        public string StartTime { get; set; }


        /// <summary>
        ///任务运行状态
        /// </summary>
        [JsonProperty("runstatus")]
        public ExecuteStatus RunStatus { get; set; }

        /// <summary>
        ///下次运行时间
        /// </summary>
        [JsonProperty("nextruntime")]
        public string NextRunTime { get; set; }
    }
}
