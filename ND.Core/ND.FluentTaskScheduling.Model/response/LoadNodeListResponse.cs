using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadNodeListResponse.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-21 14:37:14         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-21 14:37:14          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.response
{
    public class LoadNodeListResponse
    {


        public List<NodeDetailDto> NodeList { get; set; }
    }

    public class NodeDetailDto
    {
        /// <summary>
        /// 总任务个数
        /// </summary>
        public int TotalTaskCount { get; set; }

        /// <summary>
        ///// 运行中的任务个数
        ///// </summary>
        //public int RunningTaskCount { get; set; }

        /// <summary>
        /// 总命令数量
        /// </summary>
        public int TotalCommandCount { get; set; }

        /// <summary>
        /// 待执行的命令数量
        /// </summary>
        public int WaitCommandCount { get; set; }

        public tb_node NodeDetail { get; set; }
    }
}
