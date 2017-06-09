using ND.FluentTaskScheduling.TaskInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadCommandQueueListResponse.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-10 15:06:35         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-10 15:06:35          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.response
{
   public class LoadCommandQueueListResponse
    {
        [JsonProperty("commandqueuelist")]
       public List<tb_commandqueue> CommandQueueList { get; set; }
    }

   public class LoadWebCommandQueueListResponse
   {
       [JsonProperty("commandqueuelist")]
       public List<WebCommandQueueListDto> CommandQueueList { get; set; }
   }

    public class WebCommandQueueListDto
    {
        public tb_commandqueue CommandQueueDetail { get; set; }

        public tb_task TaskDetail { get; set; }

        public tb_node NodeDetail { get; set; }

        public tb_commandlog CommandLog { get; set; }
    }
   public class CommandLibDetailDto
   {
       public tb_commandlibdetail CommandDetail { get; set; }
       public tb_commandqueue CommandQueueItem { get; set; }
   }
}
