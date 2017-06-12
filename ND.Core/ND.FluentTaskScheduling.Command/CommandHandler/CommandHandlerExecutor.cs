using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Core;
using ND.FluentTaskScheduling.Core.CommandSet;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskPoolBuilder.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 14:08:02         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 14:08:02          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandHandler
{
    /// <summary>
    /// 命令处理执行器
    /// </summary>
    public class CommandHandlerExecutor
    {
        public static event EventHandler<ScanCommandEventArgs> OnInitEvent;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 上一次日志扫描的最大id
        /// </summary>
        public static int lastMaxID = -1;
        private static Task task;
        private static StringBuilder strLog = new StringBuilder();
        public static bool isHaveStartThread=false;
        private static CancellationTokenSource importCts = new CancellationTokenSource();//是否取消刷新命令队列线程

        #region 事件封装
        private static void onInitTaskPool(string msg, Exception ex = null)
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
            strLog.AppendLine(logMsg);
            if (OnInitEvent != null)
            {
                OnInitEvent(null, new ScanCommandEventArgs() { msg = msg, ex = ex });
            }
        } 
        #endregion

        #region 静态构造函数
        static CommandHandlerExecutor()
        {

            try
            {
                if (!isHaveStartThread)
                {
                    task = Task.Factory.StartNew(Scan, importCts.Token);
                    isHaveStartThread = true;
                }
            }
            catch (Exception ex)
            {
                isHaveStartThread = false;
                onInitTaskPool("开启线程刷新命令队列失败,异常信息:" + JsonConvert.SerializeObject(ex));
            }
        } 
        #endregion

        #region 开启扫描命令队列线程
        /// <summary>
        /// 运行处理循环
        /// </summary>
        public static bool StartScanCommandAsync()
        {
            return isHaveStartThread;
        } 
        #endregion

        #region 停止扫描命令队列线程
        /// <summary>
        /// 停止运行处理循环
        /// </summary>
        public static bool StopScanCommandAsync()
        {
            try
            {
                isHaveStartThread = false;
                importCts.Cancel();
                return true;
            }
            catch (Exception ex)
            {
                onInitTaskPool("停止线程刷新命令队列失败,异常信息:" + JsonConvert.SerializeObject(ex));
                return false;
            }
        } 
        #endregion

        #region 私有Scan方法
        static void Scan()
        {

            ScanCommandAndExecuteAsync();//扫描任务并添加到任务池
        } 
        #endregion

        #region 扫描命令队列并执行命令
        /// <summary>
        /// 扫描命令队列并添加到任务池
        /// </summary>
        private static void ScanCommandAndExecute()
        {
            //1.扫描分配给该节点的任务命令（读库或者直接读webapi）
            //2.遍历命令集合，并从命令池中找寻命令并执行，执行成功，更新命令队列，执行失败也更新命令状态
            var r = NodeProxy.PostToServer<LoadCommandQueueListResponse, LoadCommandQueueListRequest>(ProxyUrl.LoadCommandQueueList_Url, new LoadCommandQueueListRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Node, LastMaxId = lastMaxID });
            if (r.Status != ResponesStatus.Success)
            {
                onInitTaskPool("读取命令失败,服务器返回:" + JsonConvert.SerializeObject(r));
                return;
            }

            if (r.Data.CommandQueueList.Count > 0)
            {
                onInitTaskPool("当前节点扫描到" + r.Data.CommandQueueList.Count + "条命令,并执行中");
            }
            foreach (var item in r.Data.CommandQueueList)
            {
                try
                {
                    CommandPoolManager.CreateInstance().Get(item.commanddetailid.ToString()).commandBase.Execute();
                    //更新命令队列节点状态
                    var r2 = NodeProxy.PostToServer<EmptyResponse, UpdateCommandQueueStatusRequest>(ProxyUrl.UpdateCommandQueueStatus_Url, new UpdateCommandQueueStatusRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Node, CommandQueueId = item.id, CommandStatus = CommandQueueStatus.ExecuteSuccess });
                    if (r2.Status != ResponesStatus.Success)
                    {
                        onInitTaskPool("更新命令状态(成功)失败,服务器返回:" + JsonConvert.SerializeObject(r));
                        //记录节点错误日志
                        return;
                    }
                    //更新命令状态为成功
                    onInitTaskPool(string.Format("当前节点执行命令成功! id:{0},命令名:{1},命令参数:{2}", item.id, item.commandmainclassname, item.commandparams));
                }
                catch (Exception ex)
                {
                    //更新命令状态为错误
                    var r2 = NodeProxy.PostToServer<EmptyResponse, UpdateCommandQueueStatusRequest>(ProxyUrl.UpdateCommandQueueStatus_Url, new UpdateCommandQueueStatusRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Node, CommandQueueId = item.id, CommandStatus = CommandQueueStatus.ExecuteError });
                    if (r2.Status != ResponesStatus.Success)
                    {
                        onInitTaskPool("执行节点命令异常,更新命令状态为错误,服务器返回:" + JsonConvert.SerializeObject(r));
                        //记录节点错误日志
                        return;
                    }
                    //添加任务执行失败日志
                    onInitTaskPool("执行节点命令异常:任务id:" + item.taskid + ",命令名称:" + item.commandmainclassname + "(" + item.id + ")", ex);
                }
            }



        } 
        #endregion

        #region 异步循环扫描命令队列并执行命令
        /// <summary>
        /// 异步循环扫描命令队列并添加到任务池中
        /// </summary>
        private static void ScanCommandAndExecuteAsync()
        {
            while (true)
            {
                if (!importCts.Token.CanBeCanceled)//true为已取消
                {
                    onInitTaskPool("----------------------------------" + DateTime.Now + ":准备接受命令并运行消息循环开始...-----------------------------------");
                    System.Threading.Thread.Sleep(1000);
                    try
                    {

                        ScanCommandAndExecute();
                    }
                    catch (Exception ex)
                    {
                        onInitTaskPool("系统级不可恢复严重错误", ex);
                    }
                    onInitTaskPool("----------------------------------" + DateTime.Now + ":消息循环结束...-----------------------------------");
                    LogProxy.AddNodeLog(strLog.ToString(), LogType.RefreshCommandQueueLog);
                    strLog.Clear();
                    System.Threading.Thread.Sleep(3000);
                }
                else
                {

                    importCts.Dispose();
                    //task.Dispose();
                    importCts = null;
                    task = null;
                    break;
                }
            }
        } 
        #endregion


       
    }
}
