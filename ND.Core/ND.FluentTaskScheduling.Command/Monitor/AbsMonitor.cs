using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Core;
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
// 文件名称(File Name)：BaseMonitor.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 16:06:48         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 16:06:48          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Command.Monitor
{
    public abstract class AbsMonitor
    {
        protected Task _task;

        CancellationTokenSource importCts = new CancellationTokenSource();//是否取消监控
        /// <summary>
        /// 监控间隔时间 （毫秒）
        /// </summary>
        public virtual int Interval { get; set; }

        /// <summary>
        /// 监控名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 监控描述
        /// </summary>
        public virtual string Discription{get;set;}

        public AbsMonitor()
        {
            _task=Task.Factory.StartNew(TryRun,importCts.Token);
            //_thread = new System.Threading.Thread(TryRun);
            //_thread.IsBackground = true;
           // _thread.Start();
        }

        private void TryRun()
        {
            var r = NodeProxy.PostToServer<EmptyResponse, AddNodeMonitorRequest>(ProxyUrl.AddNodeMonitorRequest_Url, new AddNodeMonitorRequest() { NodeId = GlobalNodeConfig.NodeID,Name=Name,Discription=Discription,Internal=this.Interval, ClassName=this.GetType().Name,ClassNameSpace=this.GetType().FullName,MonitorStatus=(int)MonitorStatus.Monitoring, Source = Source.Node });
            if (r.Status != ResponesStatus.Success)
            {
                //记录日志，并抛出异常
                LogProxy.AddNodeErrorLog("请求" + ProxyUrl.AddNodeMonitorRequest_Url + "上报节点信息失败,服务端返回信息:" + JsonConvert.SerializeObject(r));
            }
            while (true)
            {
                try
                {
                   
                    if(importCts.Token.IsCancellationRequested)//如果要是已取消则break
                    {
                        //LogProxy.AddNodeLog(this.GetType().Name + "监控组件收到取消请求,已取消监控!");
                        var r2 = NodeProxy.PostToServer<EmptyResponse, UpdateNodeMonitorRequest>(ProxyUrl.UpdateNodeMonitorRequest_Url, new UpdateNodeMonitorRequest() { NodeId = GlobalNodeConfig.NodeID, MonitorClassName = this.GetType().Name, MonitorStatus = (int)MonitorStatus.NoMonitor, Source = Source.Node });
                        if (r2.Status != ResponesStatus.Success)
                        {
                            //记录日志，并抛出异常
                            LogProxy.AddNodeErrorLog("请求" + ProxyUrl.UpdateNodeMonitorRequest_Url + "上报节点监控(停止监控)信息失败,服务端返回信息:" + JsonConvert.SerializeObject(r));
                        }
                        break;
                    }
                    Run();
                    System.Threading.Thread.Sleep(Interval);
                }
                catch (Exception exp)
                {
                    LogProxy.AddNodeErrorLog(this.GetType().Name + "监控严重错误,异常信息:"+JsonConvert.SerializeObject(exp));
                }
                
            }

        }

        /// <summary>
        /// 监控执行方法约定
        /// </summary>
        protected abstract void Run();
        

        /// <summary>
        /// 取消执行
        /// </summary>
        public virtual void CancelRun()
        {
            try
            {
                importCts.Cancel();
                //LogProxy.AddNodeLog(this.GetType().Name + "监控组件取消监控!");
                //var r = NodeProxy.PostToServer<EmptyResponse, UpdateNodeMonitorRequest>(ProxyUrl.UpdateNodeMonitorRequest_Url, new UpdateNodeMonitorRequest() { NodeId = GlobalNodeConfig.NodeID, MonitorClassName = Name, MonitorStatus = (int)MonitorStatus.NoMonitor, Source = Source.Node });
                //if (r.Status != ResponesStatus.Success)
                //{
                //    //记录日志，并抛出异常
                //    LogProxy.AddNodeErrorLog("请求" + ProxyUrl.UpdateNodeMonitorRequest_Url + "上报节点监控(停止监控)信息失败,服务端返回信息:" + JsonConvert.SerializeObject(r));
                //}
            }
            catch(Exception ex)
            {
                LogProxy.AddNodeErrorLog(this.GetType().Name + "取消监控异常,异常信息:" + JsonConvert.SerializeObject(ex));
            }
        }
    }
}
