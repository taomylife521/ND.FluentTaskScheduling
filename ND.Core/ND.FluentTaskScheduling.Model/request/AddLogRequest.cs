using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AddLogRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-10 17:09:53         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-10 17:09:53          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
     /// <summary>
    /// 任务日志类型
    /// </summary>
    public enum LogType
    {
       
        [Description("常用任务日志")]
        CommonLog=1,
        [Description("节点系统日志")]
        SystemLog=2,
        [Description("节点系统错误日志")]
        SystemError = 3,
        [Description("常用任务错误日志")]
        CommonError = 4,
        [Description("节点启动日志")]
        NodeInitLog = 5,

        [Description("节点停止日志")]
        NodeStopLog = 6,

        [Description("刷新命令队列日志")]
        RefreshCommandQueueLog = 7,
    }
    
    /// <summary>
    /// 添加日志请求
    /// </summary>
    public class AddLogRequest:RequestBase
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
        /// 日志消息
        /// </summary>
        [JsonProperty("msg")]
        public string Msg { get; set; }

        /// <summary>
        /// 日志类型
        /// </summary>
         [JsonProperty("logtype")]
        public LogType LogType { get; set; }
    }
}
