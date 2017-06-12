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
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：StartRefreshCommandQueueCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-11 14:21:06         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-11 14:21:06          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandSet
{
    /// <summary>
    /// 开启节点刷新命令队列
    /// </summary>
    public class StartRefreshCommandQueueNodeCommand : AbstractCommand
    {
        
        public override string CommandDisplayName
        {
            get
            {
                return "开启节点刷新命令队列命令";
            }

        }
        public override string CommandDescription
        {
            get
            {
                return "该命令用于向节点发起开启刷新命令队列线程命令";
            }

        }
        /// <summary>
        /// 执行
        /// </summary>
        public override RunCommandResult Execute()
        {


            try
            {
                RefreshCommandQueueStatus refreshStatus = RefreshCommandQueueStatus.NoRefresh;

                if (CommandQueueScanManger.IsStopCommandQueue())//true 停止
                {
                    refreshStatus = RefreshCommandQueueStatus.Refreshing;
                }
                else
                {
                  CommandQueueScanManger.StartScanCommandQueueAsync();
                  refreshStatus = RefreshCommandQueueStatus.Refreshing;
                   
                }
                var r = NodeProxy.PostToServer<EmptyResponse, UpdateNodeRefreshCommandQueueStatusRequest>(ProxyUrl.UpdateNodeRefreshCmdQueueStatus_Url, new UpdateNodeRefreshCommandQueueStatusRequest() { NodeId = int.Parse(this.AppConfig["nodeid"]), Source = (Source)int.Parse(this.AppConfig["source"]), RefreshStatus = refreshStatus });
                if (r.Status != ResponesStatus.Success)
                {
                  
                    ShowCommandLog("开启节点刷新命令队列失败,服务器返回:" + JsonConvert.SerializeObject(r));
                    
                }
                
                //添加日志
                ShowCommandLog("开启节点刷新命令队列成功");
                return new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteSucess };

            }
            catch (Exception ex)
            {
               
                ShowCommandLog("开启节点刷新命令队列异常,异常信息:" + JsonConvert.SerializeObject(ex));
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
