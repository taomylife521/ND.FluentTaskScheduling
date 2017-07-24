using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：MonitorStatus.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-27 13:49:08         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-27 13:49:08          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.enums
{
    public enum MonitorStatus
    {
        /// <summary>
        /// 未监控
        /// </summary>
        [Description("未监控")]
        NoMonitor = 0,

        /// <summary>
        /// 监控中
        /// </summary>
        [Description("监控中")]
        Monitoring = 1
    }
}
