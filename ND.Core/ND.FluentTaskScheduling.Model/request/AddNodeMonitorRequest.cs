using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AddNodeMonitorRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-27 13:52:38         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-27 13:52:38          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    public class AddNodeMonitorRequest:RequestBase
    {
        public int NodeId { get; set; }
        public string Name { get; set; }

        public string Discription { get; set; }

        public string ClassName { get; set; }

        public string ClassNameSpace { get; set; }

        public int MonitorStatus { get; set; }

        public int Internal { get; set; }
    }
}
