using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadPerformancelistResponse.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-15 14:33:46         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-15 14:33:46          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.response
{
    public class LoadPerformancelistResponse
    {
        public LoadPerformancelistResponse()
        {
           
            TaskPerfomance = new Dictionary<string, List<tb_performance>>();
        }
      

       /// <summary>
       /// 每个任务对应的性能统计
       /// </summary>
       public Dictionary<string, List<tb_performance>> TaskPerfomance { get; set; }
    }

   
}
