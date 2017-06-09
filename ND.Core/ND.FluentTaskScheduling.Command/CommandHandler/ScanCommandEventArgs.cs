using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskEventArgs.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 14:01:01         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 14:01:01          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandHandler
{
    public class ScanCommandEventArgs:EventArgs
    {
        public string msg { get; set; }

        public Exception ex { get; set; }
    }
}
