using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskType.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-10 13:40:20         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-10 13:40:20          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.enums
{
    /// <summary>
    /// 任务类型
    /// </summary>
    public enum TaskType
    {
        /// <summary>
        /// 调度任务
        /// </summary>
         [Description("调度任务")]
        SchedulingTask=0,

        /// <summary>
        /// 单次任务
        /// </summary>
        [Description("单次任务")]
        OnceTask=1
    }
}
