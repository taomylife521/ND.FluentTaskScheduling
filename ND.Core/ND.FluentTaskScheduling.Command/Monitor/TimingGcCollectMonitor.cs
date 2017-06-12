using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Core.CommandHandler;
using ND.FluentTaskScheduling.Command.Monitor;
using Newtonsoft.Json;

//**********************************************************************
//
// 文件名称(File Name)：TimingGcCollectMonitor.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-06-09 15:22:26         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-06-09 15:22:26          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.Monitor
{
    /// <summary>
    /// 定时触发GC收集
    /// </summary>
    public class TimingGcCollectMonitor : AbsMonitor
    {
        public override int Interval { get { return 1000*60*60*5; } }
        public override string Name {
            get { return "定时进行GC收集"; }
        }

        public override string Discription {
            get { return "每次间隔" + Interval / 1000 + "秒进行GC收集"; }
        }

        protected override void Run()
        {
            try
            {
                StringBuilder strDis = new StringBuilder();
                Process  myprocess=Process.GetCurrentProcess();
                strDis.AppendLine("开始执行GC收集,当前进程" + myprocess.ToString() + "占用内存大小:" + myprocess.PagedMemorySize64 / 1024);
               // Console.WriteLine("开始执行GC收集,当前进程" + myprocess.ToString() + "占用内存大小:" + myprocess.PagedMemorySize64 / 1024);
                GC.Collect();
                Process myprocess2 = Process.GetCurrentProcess();
                strDis.AppendLine("GC收集完成,当前进程" + myprocess2.ToString() + "占用内存大小:" + myprocess2.PagedMemorySize64 / 1024);
               // Console.WriteLine("GC收集完成,当前进程" + myprocess2.ToString() + "占用内存大小:" + myprocess2.PagedMemorySize64 / 1024);
                LogProxy.AddNodeLog(strDis.ToString());
                strDis = null;
            }
            catch (Exception ex)
            {
                LogProxy.AddNodeErrorLog("节点定时进行GC收集异常:"+JsonConvert.SerializeObject(ex));
            }
        }
    }
}
