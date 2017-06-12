//**********************************************************************
//
// 文件名称(File Name)：        
// 功能描述(Description)：     
// 作者(Author)：               
// 日期(Create Date)： 2017-06-12 11:24:12         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期: 2017-06-12 11:24:12           
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:  2017-06-12 11:24:12         
//             修改理由：         
//
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.FluentTaskScheduling.Core.SchedulerComponent
{
    /// <summary>
    /// 调度组件接口
    /// </summary>
    public interface ISchedulerComponent
    {
        /// <summary>
        /// 添加调度任务
        /// </summary>
         bool AddSchedulerJob();

        /// <summary>
        /// 添加调度任务并获取下次运行时间
        /// </summary>
        bool AddSchedulerJob(ref string nextRunTime);

        /// <summary>
        /// 移除调度任务
        /// </summary>
        bool RemoveSchedulerJob();

        /// <summary>
        /// 暂停调度任务
        /// </summary>
        bool PauseSchedulerJob();

        /// <summary>
        /// 恢复调度任务
        /// </summary>
        bool ResumeSchedulerJob();
    }
}
