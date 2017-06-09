using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.TaskInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskDisposer.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-10 14:52:50         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-10 14:52:50          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.TaskHandler
{
    public static class TaskDisposer
    {
        //[DllImport("psapi.dll")]
       // public static extern int EmptyWorkingSet(IntPtr hwProc);

        /// <summary>
        /// 任务的资源释放
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="taskruntimeinfo"></param>
        /// <returns></returns>
        public static bool DisposeTask(int taskid, NodeTaskRunTimeInfo taskruntimeinfo, bool isforceDispose,Action<string> ShowCommandLog)
        {
            try
            {
                if (taskruntimeinfo != null && taskruntimeinfo.DllTask != null)
                    try { taskruntimeinfo.DllTask = null; }
                    catch (Exception ex)
                    {
                        if (ShowCommandLog != null)
                        {
                            ShowCommandLog("强制资源释放之任务资源释放异常,异常信息:" + JsonConvert.SerializeObject(ex));
                            LogProxy.AddTaskErrorLog("强制资源释放之任务资源释放异常,异常信息:" + JsonConvert.SerializeObject(ex), taskid);
                        }
                    }
                if (taskruntimeinfo != null && taskruntimeinfo.Domain != null)
                    try { 
                        new AppDomainLoaderHelper<AbstractTask>().UnLoad(taskruntimeinfo.Domain); 
                        taskruntimeinfo.Domain = null;
                        GC.Collect();
                    }
                    catch (Exception e)
                    {
                        if (ShowCommandLog != null)
                        {
                            ShowCommandLog("强制资源释放之应用程序域释放异常,异常信息:" + JsonConvert.SerializeObject(e));
                            LogProxy.AddTaskErrorLog("强制资源释放之应用程序域释放异常,异常信息:" + JsonConvert.SerializeObject(e), taskid);
                        }
                    }
                if (TaskPoolManager.CreateInstance().Get(taskid.ToString()) != null)
                    try { TaskPoolManager.CreateInstance().Remove(taskid.ToString()); }
                    catch (Exception e)
                    {
                        if (ShowCommandLog != null)
                        {
                            ShowCommandLog("强制资源释放之任务池释放异常,异常信息:" + JsonConvert.SerializeObject(e));
                            LogProxy.AddTaskErrorLog("强制资源释放之任务池释放异常,异常信息:" + JsonConvert.SerializeObject(e), taskid);
                        }
                    }
                if (ShowCommandLog != null) ShowCommandLog("节点已对任务进行资源释放完成,任务id:" + taskid);
                return true;
            }
            catch(Exception ex)
            {
                LogProxy.AddTaskErrorLog("释放任务资源异常,异常信息:" + JsonConvert.SerializeObject(ex), taskid);
                return false;
            }
        }
    }
}
