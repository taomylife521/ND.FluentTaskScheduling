using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//**********************************************************************
//
// 文件名称(File Name)：GlobalConfig.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-02 14:31:06         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-02 14:31:06          
//             修改理由：         
//**********************************************************************

namespace ND.FluentTaskScheduling.WebApi
{
    public class GlobalConfig
    {
        static GlobalConfig()
        {
            MonitorList = new List<AbsMonitor>();
        }
        public static List<AbsMonitor> MonitorList { get; set; }
    }
}