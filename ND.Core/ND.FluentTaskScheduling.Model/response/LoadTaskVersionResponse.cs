using ND.FluentTaskScheduling.TaskInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadTaskVersionResponse.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-13 14:30:10         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-13 14:30:10          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.response
{
   public class LoadTaskVersionResponse
    {
       public tb_taskversion TaskVersionDetail { get; set; }

       public tb_task TaskDetail { get; set; }

       public string AlarmEmailList { get; set; }

       public string AlarmMobileList { get; set; }
    }
}
