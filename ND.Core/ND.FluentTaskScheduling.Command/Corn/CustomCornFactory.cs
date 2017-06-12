using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CustomCornFactory.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 15:26:49         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 15:26:49          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Command.Corn
{
    public class CustomCornFactory
    {
        public static AbsCorn GetCustomCorn(string corn)
        {
            string[] cmds = corn.Replace("[", "").Replace("]", "").Split(',');
            string cmdname = cmds[0].ToLower();
            if (cmdname == "runonce")
            {
                return new RunOnceCorn(corn);
            }
            else if (cmdname == "simple")
            {
                return new SimpleCorn(corn);
            }
            throw new Exception("不可解析的自定义corn表达式");
        }
    }
}
