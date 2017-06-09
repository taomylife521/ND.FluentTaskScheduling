using ND.FluentTaskScheduling.Command.asyncrequest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：PerformanceCounterCollectTask.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-16 14:02:33         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-16 14:02:33          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.PerformanceCollect
{
    public class PerformanceCounterCollectTask:AbsCollect,ICollect<float>
    {
        private PerformanceCounter counter = null;
        public PerformanceCounterCollectTask(NodePerformanceCollectConfig config)
        {
            
           this.Name =config.CollectName;
            counter = new PerformanceCounter();
            
            counter.CategoryName = config.CategoryName;
            counter.CounterName = config.CounterName;
            counter.InstanceName = config.InstanceName;
            counter.MachineName = ".";
            try
            {
                Collect();//开启采集
                System.Threading.Thread.Sleep(100);
            }
            catch { }
        }
        public float Collect()
        {
            try
            {
                return counter.NextValue();
            }
            catch (Exception exp)
            {
                LogProxy.AddNodeErrorLog(string.Format("服务器[id:{3}] {0}-{1}-{2} 性能计数器出错:", counter.CategoryName, counter.CounterName, counter.InstanceName, GlobalNodeConfig.NodeID));
                return -1;
            }
        }
    }
}
