using ND.FluentTaskScheduling.Core.CommandSet;
using ND.FluentTaskScheduling.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandFactory.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-05 17:34:31         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-05 17:34:31          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandHandler
{
   public class CommandFactory
    {
       public static CommandRunTimeInfo Create(tb_commandlibdetail command)
       {
           CommandRunTimeInfo runTimeInfo = new CommandRunTimeInfo();
           string namespacestr = typeof(AbstractCommand).Namespace;
           var obj = Assembly.GetAssembly(typeof(AbstractCommand)).CreateInstance(namespacestr + "." + command.commandmainclassname.ToString(), true);
           if (obj != null && obj is AbstractCommand)
           {
               var commandInfo = (obj as AbstractCommand);
               commandInfo.CommandDetail = command;
               commandInfo.CommandDescription = command.commanddescription;
               commandInfo.CommandDisplayName = command.commandname;
               commandInfo.CommandVersion = command.commandlibid.ToString();
               runTimeInfo.commandBase = commandInfo;
           }
           return runTimeInfo;
       }
    }
}
