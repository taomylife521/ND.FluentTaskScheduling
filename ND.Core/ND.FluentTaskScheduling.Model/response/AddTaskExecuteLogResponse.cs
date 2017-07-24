using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AddTaskExecuteLogResponse.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-14 13:39:20         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-14 13:39:20          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.response
{
    /// <summary>
    /// 添加任务执行日志响应
    /// </summary>
   public class AddTaskExecuteLogResponse
    {
       /// <summary>
       /// 日志id
       /// </summary>
        [JsonProperty("logid")]
       public string LogId { get; set; }
    }
}
