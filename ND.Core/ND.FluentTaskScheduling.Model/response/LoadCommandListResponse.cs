using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadCommandListResponse.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-10 10:26:06         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-10 10:26:06          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.response
{
   public class LoadCommandListResponse
    {
        /// <summary>
        /// 节点id
        /// </summary>
        [JsonProperty("nodeid")]
        public string NodeId { get; set; }

        /// <summary>
        /// 命令库详情列表
        /// </summary>
        [JsonProperty("commandlibdetaillist")]
        public List<tb_commandlibdetail> CommandLibDetailList { get; set; }

        
    }

  
}
