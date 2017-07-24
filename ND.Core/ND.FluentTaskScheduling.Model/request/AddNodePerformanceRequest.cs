using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AddNodePerformanceRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-16 14:28:37         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-16 14:28:37          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    public class AddNodePerformanceRequest : MonitorRequestBase
    {
        public AddNodePerformanceRequest()
        {
            NodePerformance = new tb_nodeperformance();
        }
        public tb_nodeperformance NodePerformance { get; set; }

       
    }
}
