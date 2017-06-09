using ND.FluentTaskScheduling.Model.enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：UpdateNodeStatusRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-13 13:15:31         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-13 13:15:31          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    public class UpdateNodeStatusRequest:RequestBase
    {
        /// <summary>
        /// 节点id
        /// </summary>
        [JsonProperty("nodeid")]
        public int NodeId { get; set; }


        /// <summary>
        /// 节点状态
        /// </summary>
        [JsonProperty("nodestatus")]
        public NodeStatus NodeStatus { get; set; }
    }
}
