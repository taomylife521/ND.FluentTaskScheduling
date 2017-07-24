using ND.FluentTaskScheduling.Model.enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：UpdateCommandQueueStatusRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-10 16:32:37         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-10 16:32:37          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    public enum CommandQueueStatus
    {
        [Description("未执行")]
        NoExecute = 0,
        [Description("执行错误")]
        ExecuteError = 1,
        [Description("成功执行")]
        ExecuteSuccess = 2
    }
    public class UpdateCommandQueueStatusRequest:RequestBase
    {

        /// <summary>
        /// 命令队列id
        /// </summary>
        [JsonProperty("nodeid")]
        public int NodeId { get; set; }

        /// <summary>
        /// 命令队列id
        /// </summary>
          [JsonProperty("commandqueueid")]
        public int CommandQueueId { get; set; }


        /// <summary>
        /// 命令状态
        /// </summary>
         [JsonProperty("commandstatus")]
          public CommandQueueStatus CommandStatus { get; set; }
    }
}
