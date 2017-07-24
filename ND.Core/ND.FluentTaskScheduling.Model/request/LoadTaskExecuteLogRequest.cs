using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadTaskExecuteLogRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-21 10:12:53         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-21 10:12:53          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    public class LoadTaskExecuteLogRequest : PageRequestBase
    {
       /// <summary>
       /// 任务编号
       /// </summary>
       public int TaskId { get; set; }

       /// <summary>
       /// 任务日志创建起始时间
       /// </summary>
       public string TaskLogCreateTimeRange { get; set; }

       /// <summary>
       /// 节点id
       /// </summary>
       public int NodeId { get; set; }


       /// <summary>
       /// 任务执行状态
       /// </summary>
       public int TaskExecuteStatus { get; set; }

     

      
    }
}
