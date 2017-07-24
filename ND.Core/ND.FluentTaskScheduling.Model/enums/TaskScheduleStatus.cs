using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskScheduleStatus.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-14 13:53:39         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-14 13:53:39          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.enums
{
    /// <summary>
    /// 任务调度状态
    /// </summary>
    public enum TaskScheduleStatus
    {
        /// <summary>
        /// 未调度
        /// </summary>
        [Description("未调度")]
        NoSchedule=0,//未调度

        /// <summary>
        /// 待调度
        /// </summary>
        [Description("待调度")]
        WaitSchedule = 1,

        /// <summary>
        /// 调度中
        /// </summary>
        [Description("调度中")]
        Scheduling=2,

        /// <summary>
        /// 暂停调度
        /// </summary>
        [Description("暂停调度")]
        PauseSchedule = 3,

        /// <summary>
        /// 停止调度
        /// </summary>
        [Description("停止调度")]
        StopSchedule = 4,

        /// <summary>
        /// 恢复调度
        /// </summary>
        [Description("恢复调度")]
        RecoverSchedule = 5,
    }
}
