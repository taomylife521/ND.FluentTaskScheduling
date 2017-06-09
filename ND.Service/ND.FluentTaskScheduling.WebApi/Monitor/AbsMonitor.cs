
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
namespace ND.FluentTaskScheduling.WebApi
{
    public abstract class AbsMonitor
    {
        protected Task _task;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        CancellationTokenSource importCts = new CancellationTokenSource();//是否取消监控
        /// <summary>
        /// 监控间隔时间 （毫秒）
        /// </summary>
        public virtual int Interval { get; set; }

        public AbsMonitor()
        {
            _task=Task.Factory.StartNew(TryRun,importCts.Token);
            //_thread = new System.Threading.Thread(TryRun);
            //_thread.IsBackground = true;
           // _thread.Start();
        }

        private void TryRun()
        {
            while (true)
            {
                try
                {
                    Run();
                    System.Threading.Thread.Sleep(Interval);
                }
                catch (Exception exp)
                {
                   log.Error(this.GetType().Name + "监控严重错误,异常信息:"+JsonConvert.SerializeObject(exp));
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
        protected virtual void CancelRun()
        {
            try
            {
                importCts.Cancel();
            }
            catch(Exception ex)
            {
                log.Error(this.GetType().Name + "取消监控异常,异常信息:" + JsonConvert.SerializeObject(ex));
            }
        }
    }
}
