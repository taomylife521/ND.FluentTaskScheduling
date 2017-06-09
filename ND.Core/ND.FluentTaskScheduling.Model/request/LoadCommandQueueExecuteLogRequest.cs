using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadCommandQueueExecuteLogRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-19 14:32:42         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-19 14:32:42          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    public class LoadCommandQueueExecuteLogRequest : PageRequestBase
    {
        /// <summary>
        /// 命令队列id
        /// </summary>
        public int CommandQueueId { get; set; }

        /// <summary>
        /// 命令队列id
        /// </summary>
        public string LogCreateTimeRange { get; set; }

        /// <summary>
        /// 节点编号
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// 命令Id
        /// </summary>
        public int CommandId{ get; set; }

        /// <summary>
        /// 命令执行状态
        /// </summary>
        public string ExecuteStatus { get; set; }
    }
}
