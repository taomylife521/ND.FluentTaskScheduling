using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AddTaskRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-14 15:54:34         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-14 15:54:34          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    public class AddTaskRequest:RequestBase
    {
        /// <summary>
        /// 后台创建人
        /// </summary>
        public string AdminId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 任务参数
        /// </summary>
        public string TaskParams { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        public string TaskDescription { get; set; }

        /// <summary>
        /// 组别id
        /// </summary>
        public int GroupId { get; set; }

        public int NodeId { get; set; }

        /// <summary>
        /// 任务命名空间
        /// </summary>
        public string TaskNameSpace { get; set; }

        /// <summary>
        /// 任务主类名
        /// </summary>
        public string TaskMainClassName { get; set; }


        /// <summary>
        /// 任务Corn表达式
        /// </summary>
        public string TaskCorn { get; set; }
        
        /// <summary>
        /// 任务文件名称
        /// </summary>
        public string TaskDllFileName { get; set; }

        /// <summary>
        /// 任务dll数据
        /// </summary>
        public byte[] TaskDll { get; set; }

        /// <summary>
        /// 任务备注
        /// </summary>
        public string TaskRemark { get; set; }

        /// <summary>
        /// 是否启用报警0-不启用 1-启用
        /// </summary>
        public int IsEnabledAlarm { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        public int AlarmType { get; set; }

        /// <summary>
        /// 报警人
        /// </summary>
        public string AlarmUserId { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int TaskType { get; set; }
    }
}
