using ND.FluentTaskScheduling.Model.enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：UpdateNodeRefreshCommandQueueStatusRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-11 13:50:12         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-11 13:50:12          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    public class UpdateNodeRefreshCommandQueueStatusRequest : MonitorRequestBase
    {
        /// <summary>
        /// 节点id
        /// </summary>
        [JsonProperty("nodeid")]
        public int NodeId { get; set; }

       /// <summary>
       /// 刷新命令队列状态
       /// </summary>
        [JsonProperty("refreshstatus")]
        public RefreshCommandQueueStatus RefreshStatus { get; set; }

      
    }
}
