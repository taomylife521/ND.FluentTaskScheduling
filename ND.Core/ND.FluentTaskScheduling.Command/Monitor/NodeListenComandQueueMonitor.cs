using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Command.Monitor;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.Model.request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NodeListenComandQueueMonitor.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-26 15:17:04         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-26 15:17:04          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.Monitor
{
    /// <summary>
    /// 节点监听命令队列监控
    /// </summary>
    public class NodeListenComandQueueMonitor : AbsMonitor
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override string Name
        {
            get
            {
                return "节点命令队列监控";
            }

        }

        public override string Discription
        {
            get
            {
                return "每隔5秒获取下命令队列中未执行的命令";
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
                var req = new UpdateNodeRefreshCommandQueueStatusRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Web, RefreshStatus = RefreshCommandQueueStatus.Refreshing,MonitorClassName=this.GetType().Name };
                var r = NodeProxy.PostToServer<EmptyResponse, UpdateNodeRefreshCommandQueueStatusRequest>(ProxyUrl.UpdateNodeRefreshCmdQueueStatus_Url, req);
                if (r.Status != ResponesStatus.Success)
                {
                    string title = "节点(编号):" + GlobalNodeConfig.NodeInfo.nodename + "(" + GlobalNodeConfig.NodeInfo.id.ToString() + ")上报监听命令状态失败,请及时处理!";
                    StringBuilder strContent = new StringBuilder();
                    strContent.AppendLine("请求地址:" + ProxyUrl.UpdateNodeRefreshCmdQueueStatus_Url + "<br/>");
                    strContent.AppendLine("请求参数:" + JsonConvert.SerializeObject(req) + "<br/>");
                    strContent.AppendLine("响应结果:" + JsonConvert.SerializeObject(r) + "<br/>");
                    AlarmHelper.AlarmAsync(GlobalNodeConfig.NodeInfo.isenablealarm, (AlarmType)GlobalNodeConfig.NodeInfo.alarmtype, GlobalNodeConfig.Alarm, title, strContent.ToString());
                    LogProxy.AddNodeErrorLog(strContent.ToString());
                }
            }
            catch(Exception ex)
            {
                LogProxy.AddNodeErrorLog("节点("+GlobalNodeConfig.NodeInfo.nodename+"("+GlobalNodeConfig.NodeInfo.id.ToString()+")"+")监听命令队列监控异常:"+ex.Message+",异常:"+JsonConvert.SerializeObject(ex));
                AlarmHelper.AlarmAsync(GlobalNodeConfig.NodeInfo.isenablealarm, (AlarmType)GlobalNodeConfig.NodeInfo.alarmtype, GlobalNodeConfig.Alarm, "节点(" + GlobalNodeConfig.NodeInfo.nodename + "(" + GlobalNodeConfig.NodeInfo.id.ToString() + ")" + ")监听命令队列监控异常", JsonConvert.SerializeObject(ex));
            }
            
        }
    }
}
