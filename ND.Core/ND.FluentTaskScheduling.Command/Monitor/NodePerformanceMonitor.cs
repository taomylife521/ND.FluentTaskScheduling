using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Command.Monitor;
using ND.FluentTaskScheduling.Core.PerformanceCollect;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NodePerformanceMonitor.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-15 17:46:54         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-15 17:46:54          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.Monitor
{
    public class NodePerformanceMonitor : AbsMonitor
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static List<PerformanceCounterCollectTask> pcounts = new List<PerformanceCounterCollectTask>();
        public NodePerformanceMonitor()
        {
            List<NodePerformanceCollectConfig> configlist = JsonConvert.DeserializeObject<List<NodePerformanceCollectConfig>>(GlobalNodeConfig.NodeInfo.performancecollectjson);
            foreach (var item in configlist)
            {
                pcounts.Add(new PerformanceCounterCollectTask(item));
            }
           
        }

        public override string Name
        {
            get
            {
                return "节点计算机性能监控";
            }

        }

        public override string Discription
        {
            get
            {
                return "每隔6秒上报下本节点所在计算机的内存,cpu,io,iis等指标性能";
            }

        }

        /// <summary>
        /// 监控间隔时间(ms为单位)10s上报一次性能
        /// </summary>
        public override int Interval
        {
            get
            {
                return 6000;
            }
        }
        protected override void Run()
        {
            try
            {
                if (GlobalNodeConfig.NodeInfo.ifmonitor == 0)
                    return;
                if(pcounts.Count <=0)
                {
                    return;
                }
                tb_nodeperformance model = new tb_nodeperformance();
                string fileinstallpath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') ;
                double dirsizeM = -1;
                if (System.IO.Directory.Exists(fileinstallpath))
                {
                    long dirsize = IOHelper.DirSize(new DirectoryInfo(fileinstallpath));
                    dirsizeM = (double)dirsize / 1024 / 1024;
                    model.installdirsize = (double)dirsizeM;
                    
                }
                
                foreach (var p in pcounts)
                {
                    var c = p.Collect();
                    if (p.Name.Contains("cpu"))
                    {
                        model.cpu = (double)c;
                    }
                    else if (p.Name.Contains("内存"))
                    {
                        model.memory = (double)c;
                    }
                    else if (p.Name.Contains("网络发送"))
                    {
                        model.networkupload = (double)c;
                    }
                    else if (p.Name.Contains("网络下载"))
                    {
                        model.networkdownload = (double)c;
                    }
                    else if (p.Name.Contains("物理磁盘读"))
                    {
                        model.ioread = (double)c;
                    }
                    else if (p.Name.Contains("物理磁盘写"))
                    {
                        model.iowrite = (double)c;
                    }
                    else if (p.Name.Contains("IIS请求"))
                    {
                        model.iisrequest = (double)c;
                    }
                }
                model.nodeid = GlobalNodeConfig.NodeID;
                model.lastupdatetime = DateTime.Now;
                AddNodePerformanceRequest req = new AddNodePerformanceRequest() { NodePerformance = model, Source = Source.Node,MonitorClassName=this.GetType().Name };
                var r = NodeProxy.PostToServer<EmptyResponse, AddNodePerformanceRequest>(ProxyUrl.AddNodePerformance_Url, req);
                if (r.Status != ResponesStatus.Success)
                {
                    LogProxy.AddNodeErrorLog("节点性能上报出错,请求地址:" + ProxyUrl.AddNodePerformance_Url + ",请求内容:" + JsonConvert.SerializeObject(req) + ",返回结果:" + JsonConvert.SerializeObject(r));
                }
            }
            catch(Exception ex)
            {
                LogProxy.AddNodeErrorLog("节点性能监控时出错:nodeid=" + GlobalNodeConfig.NodeID+ ",异常信息:" + JsonConvert.SerializeObject(ex));
            }

            
        }
    }
}
