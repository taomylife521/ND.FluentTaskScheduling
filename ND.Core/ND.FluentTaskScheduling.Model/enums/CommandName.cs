using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandName.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-18 17:08:28         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-18 17:08:28          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.enums
{
    public enum CommandName
    {
        [Description("重启节点命令")]
        RestartNodeCommand=0,

        [Description("停止节点命令")]
        StopNodeCommand = 1,

        [Description("开启刷新命令队列命令")]
        StartRefreshCommandQueueNodeCommand = 2,

        [Description("停止节点刷新命令队列命令")]
        StopRefreshCommandQueueNodeCommand = 3,

        [Description("开启任务命令")]
        StartTaskCommand=4,

        [Description("停止任务命令")]
        StopTaskCommand=5,

        [Description("暂停任务调度命令")]
        PauseSchduleTaskCommand = 6,

        [Description("恢复任务调度命令")]
        RecoverScheduleTaskCommand = 7
        


    }
}
