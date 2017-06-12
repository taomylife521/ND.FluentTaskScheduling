using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandParams.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-11 14:59:54         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-11 14:59:54          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandHandler
{
    public class CommandParams
    {
        /// <summary>
        /// 节点参数
        /// </summary>
        public string NodeParams { get; set; }

        /// <summary>
        /// 任务参数
        /// </summary>
        public string TaskParams { get; set; }
    }
}
