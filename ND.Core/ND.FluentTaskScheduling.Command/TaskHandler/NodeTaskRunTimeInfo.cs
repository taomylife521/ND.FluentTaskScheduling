using ND.FluentTaskScheduling.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.FluentTaskScheduling.TaskInterface;

//**********************************************************************
//
// 文件名称(File Name)：NodeTaskRunTimeInfo.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 14:03:41         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 14:03:41          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.TaskHandler
{
    public class NodeTaskRunTimeInfo
    {
        /// <summary>
        /// 任务所在的应用程序域
        /// </summary>
        public AppDomain Domain;

        /// <summary>
        /// 任务信息
        /// </summary>
        public tb_task TaskModel;

        /// <summary>
        /// 任务当前版本信息
        /// </summary>
        public tb_taskversion TaskVersionModel;

        /// <summary>
        /// 应用程序域中任务dll实例引用
        /// </summary>
        public AbstractTask DllTask;

        /// <summary>
        /// 任务锁机制,用于执行状态的锁定，保证任务单次运行
        /// </summary>
        public TaskLock TaskLock;
    }
}
