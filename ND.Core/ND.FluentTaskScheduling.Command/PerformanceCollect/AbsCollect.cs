using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AbsCollect.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-16 14:04:29         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-16 14:04:29          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.PerformanceCollect
{
    /// <summary>
    /// 抽象的性能收集器
    /// </summary>
    public class AbsCollect
    {
        public string Name { get; set; }
        public bool IsCollect = false;
    }
}
