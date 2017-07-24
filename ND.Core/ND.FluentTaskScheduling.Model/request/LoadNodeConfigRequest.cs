using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//**********************************************************************
//
// 文件名称(File Name)：LoadNodeConfigRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-07 13:52:12         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-07 13:52:12          
//             修改理由：         
//**********************************************************************

namespace ND.FluentTaskScheduling.Model.request
{
    /// <summary>
    /// 获取节点配置请求类
    /// </summary>
    public class LoadNodeConfigRequest : RequestBase
    {
        /// <summary>
        /// 节点id
        /// </summary>
         [JsonProperty("nodeid")]
        public string NodeId { get; set; }
    }
}