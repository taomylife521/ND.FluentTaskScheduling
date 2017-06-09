using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadCommandQueueExecuteLogResponse.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-19 14:33:30         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-19 14:33:30          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.response
{
    public class LoadCommandQueueExecuteLogResponse
    {
        /// <summary>
        /// 命令队列日志列表
        /// </summary>
        public List<CommandLogListDto> CommandLogList { get; set; }
    }

    public class CommandLogListDto
    {
        /// <summary>
        /// 命令日志详情
        /// </summary>
        public tb_commandlog CommandLogDetail { get; set; }

        /// <summary>
        /// 命令详情
        /// </summary>
        public tb_commandlibdetail CommandDetail { get; set; }


        /// <summary>
        /// 节点详情
        /// </summary>
        public tb_node NodeDetail { get; set; }
    }
}
