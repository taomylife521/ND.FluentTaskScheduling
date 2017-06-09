using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadNodePerformancelistResponse.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-16 15:30:13         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-16 15:30:13          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.response
{
    public class LoadNodePerformancelistResponse
    {
        public LoadNodePerformancelistResponse()
        {
            NodePerfomance = new Dictionary<string, List<tb_nodeperformance>>();
        }
        /// <summary>
        /// 每个节点对应的性能统计
        /// </summary>
        public Dictionary<string, List<tb_nodeperformance>> NodePerfomance { get; set; }
    }
}
