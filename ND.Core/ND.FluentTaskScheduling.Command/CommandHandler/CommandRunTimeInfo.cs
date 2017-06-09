using ND.FluentTaskScheduling.Core.CommandSet;
using ND.FluentTaskScheduling.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandRunTimeInfo.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-05 17:12:47         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-05 17:12:47          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandHandler
{
    /// <summary>
    /// 命令运行时信息
    /// </summary>
   public class CommandRunTimeInfo
    {
       

       /// <summary>
       /// 命令基类引用
       /// </summary>
       public AbstractCommand commandBase { get; set; }
    }
}
