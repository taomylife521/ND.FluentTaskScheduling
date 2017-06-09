using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：SimpleCorn.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 15:28:35         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 15:28:35          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Command.Corn
{
    /// <summary>
    /// 格式[simple,0,1,2012-01-01 17:25,2012-01-01 17:25]
    /// </summary>
    public class SimpleCorn:AbsCorn
    {
        public SimpleCorn(string corn)
            : base(corn)
        {

        }
        public SimpleCornInfo ConInfo { get; set; }
        public override void Parse()
        {
            ConInfo = new SimpleCornInfo();
            string[] cmds = Corn.Replace("[", "").Replace("]", "").Split(',');
            if (cmds.Length == 5)
            {
                ConInfo.RepeatInterval = ParseCmd<int?>("RepeatInterval", cmds[1]);
                ConInfo.RepeatCount = ParseCmd<int?>("RepeatCount", cmds[2]);
                ConInfo.StartTime = ParseCmd<DateTime?>("StartTime", cmds[3]);
                ConInfo.EndTime = ParseCmd<DateTime?>("EndTime", cmds[4]);
            }
            else
            {
                throw new Exception("Corn表达式解析失败,corn:" + Corn);
            }
        }

       
    }

    public class SimpleCornInfo
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? RepeatInterval { get; set; }
        public int? RepeatCount { get; set; }
    }
}
