using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Core;
using ND.FluentTaskScheduling.Core.CommandHandler;
using ND.FluentTaskScheduling.Core.CommandSet;

using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
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
// 文件名称(File Name)：StopRefreshCommandQueueCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-11 13:39:17         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-11 13:39:17          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandSet
{
    public class StopRefreshCommandQueueNodeCommand : AbstractCommand
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public override string CommandDisplayName
        {
            get
            {
                return "停止节点刷新命令队列命令";
            }

        }
        public override string CommandDescription
        {
            get
            {
                return "该命令用于向节点发起停止刷新命令队列命令";
            }

        }
        /// <summary>
        /// 执行
        /// </summary>
        public override RunCommandResult Execute()
        {
            
           
            try
            {
               
                RefreshCommandQueueStatus refreshStatus = RefreshCommandQueueStatus.Refreshing;

                if (CommandQueueScanManger.IsStopCommandQueue())
                {
                    refreshStatus = RefreshCommandQueueStatus.NoRefresh;
                }
                else
                {
                    bool isStop = CommandQueueScanManger.StopScanCommandQueue();
                    if (isStop)
                    {
                        refreshStatus = RefreshCommandQueueStatus.NoRefresh;
                    }
                    else
                    {
                        refreshStatus = RefreshCommandQueueStatus.Refreshing;
                    }
                }
                var r = NodeProxy.PostToServer<EmptyResponse, UpdateNodeRefreshCommandQueueStatusRequest>(ProxyUrl.UpdateNodeRefreshCmdQueueStatus_Url, new UpdateNodeRefreshCommandQueueStatusRequest() { NodeId = int.Parse(this.AppConfig["nodeid"]), Source = (Source)int.Parse(this.AppConfig["source"]), RefreshStatus = refreshStatus });
                if (r.Status != ResponesStatus.Success)
                {
                   ShowCommandLog("停止节点刷新命令队列失败,服务器返回:"+JsonConvert.SerializeObject(r));
                }
                //添加日志
                ShowCommandLog("停止节点刷新命令队列成功");
                return new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteSucess };

            }
            catch (Exception ex)
            {
                ShowCommandLog("停止节点刷新命令队列失败,异常信息:" + JsonConvert.SerializeObject(ex));
                return new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteException, Ex = ex, Message = ex.Message };
            }
            
        }

        /// <summary>
        /// 初始化命令所需参数
        /// </summary>
        public override void InitAppConfig()
        {
            if (!this.AppConfig.ContainsKey("nodeid"))
            {
                this.AppConfig.Add("nodeid", GlobalNodeConfig.NodeID.ToString());
            }

            if (!this.AppConfig.ContainsKey("source"))
            {
                this.AppConfig.Add("source", ((int)Source.Node).ToString());
            }
        }
    }
}
