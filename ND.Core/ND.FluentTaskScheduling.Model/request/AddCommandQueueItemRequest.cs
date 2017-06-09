using ND.FluentTaskScheduling.Model.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AddCommandQueueItemRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-18 16:59:50         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-18 16:59:50          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    /// <summary>
    /// 添加命令队列项请求
    /// </summary>
   public class AddCommandQueueItemRequest:RequestBase
    {
       /// <summary>
       /// 命令名称
       /// </summary>
       public CommandName CommandName { get; set; }

        ///<summary>
       /// 命令名称
       /// </summary>
       public string CommandParam { get; set; }

       /// <summary>
       /// 任务id
       /// </summary>
       public string TaskId { get; set; }

       /// <summary>
       /// 任务版本id
       /// </summary>
       public string TaskVersionId { get; set; }

       /// <summary>
       /// 节点id
       /// </summary>
       public string NodeId { get; set; }

       /// <summary>
       /// 后台操作人id
       /// </summary>
       public string AdminId { get; set; }
    }
}
