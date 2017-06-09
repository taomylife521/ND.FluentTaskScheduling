using ND.FluentTaskScheduling.TaskInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadTaskListResponse.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-17 15:17:39         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-17 15:17:39          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.response
{
    /// <summary>
    /// 载入任务列表响应
    /// </summary>
    public class LoadTaskListResponse
    {
        public LoadTaskListResponse()
        {
            TaskList = new List<TaskListDto>();
        }
        public List<TaskListDto> TaskList { get; set; }
    }

    public class TaskListDto
    {
        public tb_node Node { get; set; }
        public tb_task Task { get; set; }

        public tb_taskversion TaskVersion { get; set; }
    }
}
