using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.FluentTaskScheduling.Command.Monitor;
using ND.FluentTaskScheduling.Model;

//**********************************************************************
//
// 文件名称(File Name)：MonitorRunTimeInfo.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-06-12 11:43:18         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-06-12 11:43:18          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.Monitor
{
    /// <summary>
    /// 监控运行时信息
    /// </summary>
   public class MonitorRunTimeInfo
    {
        public MonitorRunTimeInfo()
        {
            MonitorInfo = new tb_nodemonitor();
        }
       /// <summary>
       /// 监控信息
       /// </summary>
       public tb_nodemonitor MonitorInfo { get; set; }

       /// <summary>
       /// 监控实例
       /// </summary>
       public AbsMonitor MonitorBaseInstance { get; set; }
    }
}
