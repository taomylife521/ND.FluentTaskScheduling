using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NodeStatus.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-13 13:16:05         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-13 13:16:05          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.enums
{
    public enum NodeStatus
    {
        /// <summary>
        /// 没有运行
        /// </summary>
        [Description("未运行")]
        NoRun=0,

        /// <summary>
        /// 运行中
        /// </summary>
        [Description("运行中")]
        Running=1
    }
}
