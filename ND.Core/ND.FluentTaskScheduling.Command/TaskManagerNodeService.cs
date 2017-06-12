using ND.FluentTaskScheduling.Command;
using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Command.CommandHandler;
using ND.FluentTaskScheduling.Core;
using ND.FluentTaskScheduling.Core.CommandHandler;
using ND.FluentTaskScheduling.Core.CommandSet;
using ND.FluentTaskScheduling.Core.Monitor;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskManagerNodeService.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-05 10:19:36         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-05 10:19:36          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core
{
    public class TaskManagerNodeService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static StringBuilder strLog = new StringBuilder();

        #region 节点启动
        public void Start()
        {
            
            //OnInit("\r\n");
            // OnInit("\r\n");
            OnInit("----------------------" + DateTime.Now + ":初始化节点开始-----------------------------------------");
            try
            {
                OnInit("**开始请求节点配置信息**");
                if (System.Configuration.ConfigurationSettings.AppSettings.AllKeys.Contains("NodeID"))
                {
                    GlobalNodeConfig.NodeID = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["NodeID"]);
                }
                var r = NodeProxy.PostToServer<LoadNodeConfigResponse, LoadNodeConfigRequest>(ProxyUrl.LoadNodeConfig_Url, new LoadNodeConfigRequest() { NodeId = GlobalNodeConfig.NodeID.ToString(), Source = Source.Node });
                if (r.Status != ResponesStatus.Success)
                {
                    //记录日志，并抛出异常
                    LogProxy.AddNodeErrorLog("请求" + ProxyUrl.LoadNodeConfig_Url + "获取节点配置失败,服务端返回信息:" + JsonConvert.SerializeObject(r));
                    throw new Exception("请求" + ProxyUrl.LoadNodeConfig_Url + "获取节点配置失败,服务端返回信息:" + JsonConvert.SerializeObject(r));
                }
                GlobalNodeConfig.NodeInfo = r.Data.Node;
                GlobalNodeConfig.Alarm = r.Data.AlarmPerson.ToString();
                if (GlobalNodeConfig.NodeID <= 0)
                    GlobalNodeConfig.NodeID = r.Data.Node.id;
              
                //初始化配置信息
                OnInit("**请求节点配置信息完成,请求结果:" + JsonConvert.SerializeObject(r) + "**");




                //初始化命令池
                OnInit("**开启初始化节点命令池**");
                ICommandPoolBuilder builder = new CommandPoolBuilder();
                builder.OnInitEvent += builder_OnInitEvent;
                builder.BuildCommandPool();
                OnInit("**初始化节点命令池完成:本地节点命令池数量:" + CommandPoolManager.CreateInstance().GetList().Count + "**");




                OnInit("**开始初始化监控线程**");
                //初始化监控信息
                GlobalNodeConfig.Monitors.Add(new NodeHeartBeatMonitor());//心跳
                GlobalNodeConfig.Monitors.Add(new TaskPerformanceMonitor());//任务性能监控
                GlobalNodeConfig.Monitors.Add(new NodePerformanceMonitor());//节点性能监控
                GlobalNodeConfig.Monitors.Add(new NodeTaskSchedulingMonitor());//节点调度中的任务监控
                GlobalNodeConfig.Monitors.Add(new NodeListenComandQueueMonitor());//节点监听命令队列监控
                GlobalNodeConfig.Monitors.Add(new TimingGcCollectMonitor());//开启定时收集GC
                OnInit("**监控线程已开启**");


                OnInit("**开启循环监听命令队列线程**");
                IOHelper.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + GlobalNodeConfig.TaskSharedDllsDir + @"\");
                CommandQueueScanManger.StartScanCommandQueueAsync();//循环监听命令队列线程
                OnInit("**循环监听命令队列线程已开启**");

                //刷新节点运行状态
                var r2 = NodeProxy.PostToServer<EmptyResponse, UpdateNodeStatusRequest>(ProxyUrl.UpdateNodeStatus_Url, new UpdateNodeStatusRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Node, NodeStatus = Model.enums.NodeStatus.Running });
                if (r2.Status != ResponesStatus.Success)
                {
                    string msg = "更新节点运行状态失败,请求地址:" + ProxyUrl.UpdateNodeStatus_Url + ",服务器返回参数:" + JsonConvert.SerializeObject(r2);
                    OnInit(msg);
                    AlarmHelper.AlarmAsync(GlobalNodeConfig.NodeInfo.isenablealarm, (AlarmType)GlobalNodeConfig.NodeInfo.alarmtype, GlobalNodeConfig.Alarm, "更新节点(" + GlobalNodeConfig.NodeID.ToString() + ")的运行状态失败", msg + ",节点启动日志:" + strLog.ToString());
                }
                else
                {
                    OnInit("更新节点运行状态成功");
                }
                OnInit("节点启动成功");

            }
            catch (Exception ex)
            {
                string exmsg = "节点启动异常:" + JsonConvert.SerializeObject(ex);
                OnInit(exmsg);
                AlarmHelper.AlarmAsync(GlobalNodeConfig.NodeInfo.isenablealarm, (AlarmType)GlobalNodeConfig.NodeInfo.alarmtype, GlobalNodeConfig.Alarm, "节点(" + GlobalNodeConfig.NodeID.ToString() + ")启动异常,请注意查看", exmsg + ",节点启动日志:" + strLog.ToString());
            }
            finally
            {
                OnInit("----------------------" + DateTime.Now + ":初始化节点结束-----------------------------------------");
                LogProxy.AddNodeLog(strLog.ToString(), LogType.NodeInitLog);
                //log.Info(strLog.ToString());
                // EventLogHelper.Info(strLog.ToString());
                strLog.Clear();

            }
        } 
        #endregion

        private void OnInit(string msg, Exception ex=null)
        {
           
          
            var logMsg =string.IsNullOrEmpty(msg)?"":DateTime.Now+":"+ msg;
            if(string.IsNullOrEmpty(msg) && ex==null)
            {
                logMsg = "";
            }
            else
            {
                if(ex!=null)
                {
                    logMsg = logMsg + ",异常信息:" + JsonConvert.SerializeObject(ex);
                }
            }
            strLog.AppendLine(logMsg+"<br/>");

            log.Info(logMsg);
           
            Console.WriteLine(logMsg);
        }


      

       
        /// <summary>
        /// 命令池初始化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void builder_OnInitEvent(object sender,CommandEventArgs e)
        {
            OnInit(e.msg, e.ex);
        }

        #region 节点停止
        public void Stop()
        {
            try
            {
                OnInit("----------------------" + DateTime.Now + ":停止节点开始-----------------------------------------");
                //1.停止所有监控扫描
                GlobalNodeConfig.Monitors.ForEach(x =>
                {
                    x.CancelRun();
                });
                OnInit(DateTime.Now+":监控停止完成");
                //2.停止刷新命令队列
                 CommandQueueScanManger.StopScanCommandQueue();
                 OnInit(DateTime.Now + ":停止命令队列监听完成");
                //刷新节点运行状态
                var r2 = NodeProxy.PostToServer<EmptyResponse, UpdateNodeStatusRequest>(ProxyUrl.UpdateNodeStatus_Url, new UpdateNodeStatusRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Node, NodeStatus = Model.enums.NodeStatus.NoRun });
                
                if (r2.Status != ResponesStatus.Success)
                {
                    string msg = "更新节点运行状态失败,请求地址:" + ProxyUrl.UpdateNodeStatus_Url + ",服务器返回参数:" + JsonConvert.SerializeObject(r2);
                    OnInit(msg);
                    AlarmHelper.Alarm(GlobalNodeConfig.NodeInfo.isenablealarm, (AlarmType)GlobalNodeConfig.NodeInfo.alarmtype, GlobalNodeConfig.Alarm, "节点(" + GlobalNodeConfig.NodeID.ToString() + ")停止,更新运行状态失败", msg);
                }
                AlarmHelper.Alarm(GlobalNodeConfig.NodeInfo.isenablealarm, (AlarmType)GlobalNodeConfig.NodeInfo.alarmtype, GlobalNodeConfig.Alarm, "节点(" + GlobalNodeConfig.NodeID.ToString() + ")停止,请及时处理", DateTime.Now + ":当前节点(" + GlobalNodeConfig.NodeID.ToString() + ")已停止运行,如正常停止请忽略此预警");
                OnInit("节点服务停止");
            }
            catch (Exception ex)
            {
                string exemsg = "节点停止异常:" + JsonConvert.SerializeObject(ex);
                OnInit(exemsg);
                AlarmHelper.Alarm(GlobalNodeConfig.NodeInfo.isenablealarm, (AlarmType)GlobalNodeConfig.NodeInfo.alarmtype, GlobalNodeConfig.Alarm, "节点(" + GlobalNodeConfig.NodeID.ToString() + ")停止异常,请及时处理!", exemsg + ",节点停止日志:" + strLog.ToString());
            }
            finally
            {
                OnInit("----------------------" + DateTime.Now + ":停止节点结束-----------------------------------------");
                LogProxy.AddNodeLog(strLog.ToString(), LogType.NodeStopLog);
                strLog.Clear();
            }
        } 
        #endregion
    }
}
