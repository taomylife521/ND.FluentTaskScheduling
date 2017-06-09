using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AddCommandExecuteLogRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-12 16:23:40         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-12 16:23:40          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
   public class AddCommandExecuteLogRequest:RequestBase
    {
       public AddCommandExecuteLogRequest()
       {
           
       }
       public int CommandDetailId { get; set; }
        /// <summary>
        /// 命令开始时间
        /// </summary>
        public string CommandStartTime { get; set; }

        /// <summary>
        /// NodeId
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// 命令执行日志
        /// </summary>
        public string CommandExecuteLog { get; set; }

        /// <summary>
        /// 命令参数
        /// </summary>
        public string CommandParams { get; set; }

        /// <summary>
        /// 命令结束时间
        /// </summary>
        public string CommandEndTime { get; set; }

       /// <summary>
       /// 总共运行时间
       /// </summary>
       public string TotalTime { get; set; }

       /// <summary>
       /// 命令运行结果
       /// </summary>
       public string CommandResult { get; set; }

       /// <summary>
       /// 命令执行状态
       /// </summary>
       public int ExecuteStatus { get; set; }

       /// <summary>
       /// 命令队列id
       /// </summary>
       public int CommandQueueId { get; set; }

      
    }
}
