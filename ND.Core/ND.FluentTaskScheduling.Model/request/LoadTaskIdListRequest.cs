using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadTaskIdListRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-26 10:38:57         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-26 10:38:57          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    public class LoadTaskIdListRequest : RequestBase
    {
       /// <summary>
       /// 任务调度状态
       /// </summary>
       public int TaskScheduleStatus { get; set; }
       public int NodeId { get; set; }

      

    }
}
