
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AbstractTask.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 10:56:25         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 10:56:25          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.TaskInterface
{
    public abstract class AbstractTask : MarshalByRefObject, IDisposable
    {
        
        public static event EventHandler<string> OnProcessing;

        public static object obj = new object();
        /// <summary>
        /// 任务的配置信息，类似项目app.config文件配置
        /// 测试时需要手工代码填写配置,线上环境需要在任务发布的时候配置
        /// </summary>
        public TaskAppConfigInfo AppConfig = new TaskAppConfigInfo();

         public tb_task TaskDetail = new tb_task();

         public tb_taskversion TaskVersionDetail = new tb_taskversion();

         private StringBuilder strLog = new StringBuilder();

         private string AlarmEmailList = "";
         private string AlarmMobileList = "";

         #region MyRegion
         ///// <summary>
         ///// 线上环境运行入口
         ///// </summary>
         //public RunTaskResult TryRunTask()
         //{
         //    RunTaskResult result = new RunTaskResult() { RunStatus = RunStatus.Failed };
         //    try
         //    {
         //        ShowProcessIngLog("----------------------" + DateTime.Now + ":开始执行任务-----------------------------------------");
         //        DateTime startTime = DateTime.Now;
         //        //开始执行任务，上报开始执行日志，并更新任务版本状态为执行中
         //        Stopwatch sw = new Stopwatch();
         //        sw.Start();
         //        try
         //        {
         //            result = RunTask();
         //        }
         //        catch (Exception ex)
         //        {
         //            result = new RunTaskResult() { RunStatus = RunStatus.Exception, Message = ex.Message, Ex = ex };
         //            ShowProcessIngLog("执行任务异常,异常信息:" + JsonConvert.SerializeObject(ex));
         //        }
         //        sw.Stop();
         //        ShowProcessIngLog("----------------------" + DateTime.Now + ":执行任务完成-----------------------------------------");
         //        DateTime endTime = DateTime.Now;
         //        TimeSpan ts = sw.Elapsed;
         //        long times = sw.ElapsedMilliseconds / 1000;//秒 
         //        //上报任务执行日志和执行结果，并更新最后一次任务状态

         //    }
         //    catch (Exception exp)
         //    {
         //        ShowProcessIngLog("执行任务异常,异常信息:" + JsonConvert.SerializeObject(exp));
         //        ShowProcessIngLog("----------------------" + DateTime.Now + ":执行任务完成-----------------------------------------");

         //    }
         //    finally
         //    {
         //        strLog.Clear();
         //    }
         //    return result;
         //} 
         #endregion

        public AbstractTask()
        {
            InitTaskAppConfig();
        }

        public void ShowProcessIngLog(string content)
        {
            strLog.AppendLine(content + "<br/>");
            if (OnProcessing != null)
            {
                OnProcessing(this, content);
            }
        }
        public void ClearLog()
        {
            strLog.Clear();
        }
        public string GetLog()
        {
           return strLog.ToString();
        }
        /// <summary>
        /// 运行任务
        /// </summary>
        /// <returns></returns>
        public abstract RunTaskResult RunTask();


        /// <summary>
        /// 初始化任务配置
        /// </summary>
        public abstract  void InitTaskAppConfig();

        /// <summary>
        /// 更新是否暂停调度
        /// </summary>
        public virtual void UpdateTaskPauseStatus(int isPauseSchdule)
        {
            this.TaskDetail.ispauseschedule = isPauseSchdule;
        }
      
        public virtual void SetAlarmList(string emaillist="",string mobilelist="")
        {
            AlarmEmailList = emaillist;
            AlarmMobileList = mobilelist;
        }
       

        public string GetAlarmEmailList()
        {
           return AlarmEmailList ;
        }
        public string GetAlarmMobileList()
        {
            return AlarmMobileList;
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }
        public void TryDispose()
        {
            try
            {
                Dispose();
            }
            catch (Exception ex)
            {
                ShowProcessIngLog("尝试释放任务资源异常,异常信息:" + JsonConvert.SerializeObject(ex));
            }
        }

        /*忽略默认的对象租用行为，以便“在主机应用程序域运行时始终”将对象保存在内存中.   
         这种机制将对象锁定到内存中，防止对象被回收，但只能在主机应用程序运行   
         期间做到这样。*/
        [SecurityPermissionAttribute(SecurityAction.Demand,
                               Flags = SecurityPermissionFlag.Infrastructure)]
        public override object InitializeLifetimeService()
        {
            //return null;
            ILease lease = (ILease)base.InitializeLifetimeService();
            // Normally, the initial lease time would be much longer.
            // It is shortened here for demonstration purposes.
            if (lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.Zero;//这里改成0，则是无限期
                //lease.SponsorshipTimeout = TimeSpan.FromSeconds(10);
                //lease.RenewOnCallTime = TimeSpan.FromSeconds(2);
            }
            return lease;
        }
    }
}
