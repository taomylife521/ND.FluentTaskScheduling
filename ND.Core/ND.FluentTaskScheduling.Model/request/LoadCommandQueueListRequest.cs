using ND.FluentTaskScheduling.Model.enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadCommandQueueListRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-10 15:06:52         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-10 15:06:52          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    public class LoadCommandQueueListRequest:RequestBase
    {
       

        /// <summary>
        /// 节点id
        /// </summary>
        [JsonProperty("nodeid")]
        public int NodeId { get; set; }


       


        /// <summary>
        ///上次取到的最大id值
        /// </summary>
        [JsonProperty("lastmaxid")]
        public int LastMaxId { get; set; }

        /// <summary>
        ///命令队列执行状态 
        /// </summary>
        [JsonProperty("commandstatus")]
        public int CommandStatus { get; set; }
    }

    public class LoadWebCommandQueueListRequest : LoadCommandQueueListRequest
    {
        private int _commandQueueId = -1;
        /// <summary>
        /// 每页显示条数
        /// </summary>
        [JsonProperty("idisplaylength")]
        public int iDisplayLength { get; set; }

        /// <summary>
        /// 记录操作的次数
        /// </summary>
         [JsonProperty("secho")]
        public int sEcho { get; set; }

        /// <summary>
        /// 起始页
        /// </summary>
          [JsonProperty("idisplaystart")]
         public int iDisplayStart { get; set; }


        /// <summary>
        /// 命令队列创建起始时间
        /// </summary>
          [JsonProperty("commandqueuecreatetimerange")]
        public string CommandQueueCreateTimeRange { get; set; }

        /// <summary>
        /// 命令id
        /// </summary>
        [JsonProperty("commandid")]
        public int CommandId { get; set; }

       

        /// <summary>
        ///任务id
        /// </summary>
        [JsonProperty("taskid")]
        public int TaskId { get; set; }

          [JsonProperty("命令队列编号")]
        public int CommandQueueId { get { return _commandQueueId; } set { _commandQueueId = value; } }


     
    }
}
