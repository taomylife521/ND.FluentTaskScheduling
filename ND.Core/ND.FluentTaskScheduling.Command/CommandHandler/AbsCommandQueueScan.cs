using ND.FluentTaskScheduling.Command.asyncrequest;
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
// 文件名称(File Name)：AbsCommandQueueScan.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-12 14:37:39         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-12 14:37:39          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandHandler
{
   /// <summary>
   /// 抽象的命令队列扫描器
   /// </summary>
   public abstract class AbsCommandQueueScan:ICommandQueueScan,IDisposable
    {
       public static event EventHandler<ScanCommandEventArgs> OnInitEvent;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static StringBuilder strLog = new StringBuilder();

       

        static Task task;

        static bool isHaveStartThread = false;//是否已开启任务扫描

         CancellationTokenSource importCts = new CancellationTokenSource();//是否取消刷新命令队列线程

     
        #region 事件封装
        public static void onScanMsg(string msg, Exception ex = null)
        {
           
            var logMsg = string.IsNullOrEmpty(msg) ? "" : DateTime.Now + ":" + msg;
            if (string.IsNullOrEmpty(msg) && ex == null)
            {
                logMsg = "";
            }
            else
            {
                if (ex != null)
                {
                    logMsg = logMsg + ",异常信息:" + JsonConvert.SerializeObject(ex);
                }
            }
            log.Info(logMsg);
            strLog.AppendLine(logMsg+"<br/>");
            if (OnInitEvent != null)
            {
                OnInitEvent(null, new ScanCommandEventArgs() { msg = msg, ex = ex });
            }
        }
        #endregion


        #region 异步开启扫描命令队列
        public virtual void StartScanCommandQueueAsync()
        {
            try
            {
                if (!isHaveStartThread)
                {
                    task = Task.Factory.StartNew(() => {
                        while (true)
                        {
                            try
                            {
                                onScanMsg("----------------------------------" + DateTime.Now + ":准备接受命令并运行消息循环开始...-----------------------------------");
                                if (!importCts.Token.IsCancellationRequested)
                                {
                                    

                                    System.Threading.Thread.Sleep(1000);
                                    try
                                    {
                                        StartScanCommandQueue();
                                    }
                                    catch (Exception ex)
                                    {
                                        onScanMsg("扫描命令队列器发生不可恢复严重错误", ex);
                                    }

                                    System.Threading.Thread.Sleep(3000);
                                }else
                                   //当取消线程时，停止扫描 
                                {
                                    onScanMsg("收到扫描停止命令,扫描线程已终止");
                                    break;
                                }
                            }
                            catch(Exception ex)
                            {
                                onScanMsg("扫描命令异常，异常信息:"+JsonConvert.SerializeObject(ex));
                            }
                            finally
                            {
                                onScanMsg("----------------------------------" + DateTime.Now + ":消息循环结束...-----------------------------------");
                                Console.WriteLine(strLog.ToString());
                                LogProxy.AddNodeLog(strLog.ToString(), LogType.RefreshCommandQueueLog);//上报扫描命令队列日志
                                strLog.Clear();
                            }
                           
                        }
                    }, importCts.Token);
                    isHaveStartThread = true;
                }
                var r = NodeProxy.PostToServer<EmptyResponse, UpdateNodeRefreshCommandQueueStatusRequest>(ProxyUrl.UpdateNodeRefreshCmdQueueStatus_Url, new UpdateNodeRefreshCommandQueueStatusRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Web, RefreshStatus = RefreshCommandQueueStatus.Refreshing });
                if (r.Status != ResponesStatus.Success)
                {
                    onScanMsg("更新节点刷新命令队列状态(刷新中)失败,服务器返回:" + JsonConvert.SerializeObject(r));
                }
            }
            catch (Exception ex)
            {
                isHaveStartThread = false;
                onScanMsg("开启线程刷新命令队列失败,异常信息:" + JsonConvert.SerializeObject(ex));
            }
            //return isHaveStartThread;
        } 
        #endregion
        
        public abstract bool StartScanCommandQueue();


        #region 停止扫描命令队列
        public virtual bool StopScanCommandQueue()
        {
            try
            {
                var r = NodeProxy.PostToServer<EmptyResponse, UpdateNodeRefreshCommandQueueStatusRequest>(ProxyUrl.UpdateNodeRefreshCmdQueueStatus_Url, new UpdateNodeRefreshCommandQueueStatusRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Web, RefreshStatus = RefreshCommandQueueStatus.NoRefresh });
                if (r.Status != ResponesStatus.Success)
                {
                    onScanMsg("更新节点刷新命令队列状态(停止刷新)失败,服务器返回:" + JsonConvert.SerializeObject(r));
                }
                isHaveStartThread = false;
                importCts.Cancel();
                return true;
            }
            catch (Exception ex)
            {
                onScanMsg("停止线程刷新命令队列失败,异常信息:" + JsonConvert.SerializeObject(ex));
                return false;
            }
        } 
        #endregion


        public void Dispose()
        {
            this.Dispose();
        }

        #region 是否已停止扫描线程
        /// <summary>
        /// 是否已停止扫描线程 
        /// </summary>
        /// <returns>true:已停止扫描 false：未停止</returns>
        public bool IsHaveStopScanCommandQueue()
        {
            return isHaveStartThread ? false : true;
        } 
        #endregion
    }
}
