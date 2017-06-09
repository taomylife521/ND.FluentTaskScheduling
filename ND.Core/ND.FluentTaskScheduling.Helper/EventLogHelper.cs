using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：EventLogHelper.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-12 11:05:00         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-12 11:05:00          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Helper
{
    public class EventLogHelper
    {
        private static EventLog errLog = new EventLog();
        static EventLogHelper()
        {
            errLog.Source = "TaskManagerNodeService";
            

           // errLog.MachineName = ComputerHelper.Instance().GetIPAddress()+"("+ComputerHelper.Instance().GetUserName()+")";
        }

        public static void Info(string logMsg)
        {
            errLog.WriteEntry(logMsg, EventLogEntryType.Information);
        }

        public static void Error(string logMsg)
        {
            errLog.WriteEntry(logMsg, EventLogEntryType.Error);
        }

       
    }
}
