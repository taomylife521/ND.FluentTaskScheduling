using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//**********************************************************************
//
// 文件名称(File Name)：LoadNodeConfigResponse.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-07 13:53:36         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-07 13:53:36          
//             修改理由：         
//**********************************************************************

namespace ND.FluentTaskScheduling.Model.response
{
    /// <summary>
    /// 获取节点配置响应
    /// </summary>
    public class LoadNodeConfigResponse
    {
      

        /// <summary>
        /// 节点信息
        /// </summary>
        [JsonProperty("node")]
         public tb_node Node { get; set; }

        ///// <summary>
        ///// 当前节点id
        ///// </summary>
        // [JsonProperty("nodeid")]
        // public string NodeId { get; set; }

        ///// <summary>
        ///// 报警类型
        ///// </summary>
        // [JsonProperty("alarmtype")]
        // public AlarmType Alarmtype { get; set; }

         /// <summary>
         /// 报警人邮件或手机号
         /// </summary>
         [JsonProperty("alarmperson")]
         public string AlarmPerson { get; set; }
    }
}