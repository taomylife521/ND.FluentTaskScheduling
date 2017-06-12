using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Command.Monitor;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NodeHeartBeatMonitor.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-24 17:01:52         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-24 17:01:52          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.Monitor
{
    /// <summary>
    /// 上报心跳监控
    /// </summary>
    public class NodeHeartBeatMonitor:AbsMonitor
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override string Name
        {
            get
            {
                return "节点心跳监控";
            }
            
        }

        public override string Discription
        {
            get
            {
                return "每隔5秒上报一次节点心跳";
            }

        }
        
        /// <summary>
        /// 监控间隔时间(ms为单位)
        /// </summary>
        public override int Interval
        {
            get
            {
                return 5000;
            }
        }
        protected override void Run()
        {
            try
            {
                

                var r = NodeProxy.PostToServer<EmptyResponse, UpdateNodeHeatbeatRequest>(ProxyUrl.UpdateNodeHeatbeat_Url, new UpdateNodeHeatbeatRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Node,MonitorClassName=this.GetType().Name });
                if (r.Status != ResponesStatus.Success)
                {
                    LogProxy.AddNodeErrorLog("上报心跳失败,请求地址:" + ProxyUrl.UpdateNodeHeatbeat_Url + ",返回内容:" + JsonConvert.SerializeObject(r));
                }
            }
            catch(Exception ex)
            {
                string msg="上传心跳异常,信息:" + ex.Message + ",异常:" + JsonConvert.SerializeObject(ex);
                LogProxy.AddNodeErrorLog(msg);
            }
        }
    }
}
