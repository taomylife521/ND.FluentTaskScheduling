using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadNodeDetail.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-09 17:31:00         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-09 17:31:00          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
   public class LoadNodeDetailRequest:RequestBase
    {
       /// <summary>
       /// 节点id
       /// </summary>
       public int NodeId { get; set; }
    }
}
