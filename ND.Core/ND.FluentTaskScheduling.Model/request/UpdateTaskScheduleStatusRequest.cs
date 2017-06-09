using ND.FluentTaskScheduling.Model.enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：UpdateTaskScheduleStatusRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-14 13:57:18         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-14 13:57:18          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
   public class UpdateTaskScheduleStatusRequest:RequestBase
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
        /// 任务调度状态
        /// </summary>
        [JsonProperty("schedulestatus")]
        public TaskScheduleStatus ScheduleStatus { get; set; }

        /// <summary>
        /// 任务下次调度时间
        /// </summary>
        [JsonProperty("nextruntime")]
        public string NextRunTime { get; set; }

    }
}
