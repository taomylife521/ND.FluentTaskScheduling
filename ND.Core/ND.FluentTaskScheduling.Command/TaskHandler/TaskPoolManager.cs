using ND.FluentTaskScheduling.Command.Corn;
using ND.FluentTaskScheduling.Model.enums;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskPoolManager.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 15:03:26         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 15:03:26          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.TaskHandler
{
    public class TaskPoolManager
    {
        /// <summary>
        /// 任务运行池
        /// </summary>
        private static Dictionary<string, NodeTaskRunTimeInfo> TaskRuntimePool = new Dictionary<string, NodeTaskRunTimeInfo>();
        /// <summary>
        /// 任务池管理者,全局仅一个实例
        /// </summary>
        private static TaskPoolManager _taskpoolmanager;
        /// <summary>
        /// 任务池管理操作锁标记
        /// </summary>
        private static object _locktag = new object();
        /// <summary>
        /// 任务池执行计划
        /// </summary>
        private static IScheduler _sched;

        /// <summary>
        /// 静态初始化
        /// </summary>
        static TaskPoolManager()
        {
            _taskpoolmanager = new TaskPoolManager();
            ISchedulerFactory sf = new StdSchedulerFactory();
            _sched = sf.GetScheduler();
            _sched.Start();
        }
        /// <summary>
        /// 资源释放
        /// </summary>
        public virtual void Dispose()
        {
            if (_sched != null && !_sched.IsShutdown)
                _sched.Shutdown();
        }
        /// <summary>
        /// 获取任务池的全局唯一实例
        /// </summary>
        /// <returns></returns>
        public static TaskPoolManager CreateInstance()
        {
            return _taskpoolmanager;
        }
        /// <summary>
        /// 将任务移入任务池
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="taskruntimeinfo"></param>
        /// <returns></returns>
        public bool Add(string taskid, NodeTaskRunTimeInfo taskruntimeinfo, ref string nextrunTime)
        {
            lock (_locktag)
            {
                if (!TaskRuntimePool.ContainsKey(taskid))
                {
                    JobDetail jobDetail = new JobDetail(taskruntimeinfo.TaskModel.id.ToString(), taskruntimeinfo.TaskModel.groupid.ToString(), typeof(TaskJob));// 任务名，任务组，任务执行类  
                    var trigger = CornFactory.CreateTigger(taskruntimeinfo);
                    _sched.ScheduleJob(jobDetail, trigger);
                    nextrunTime = Convert.ToDateTime(trigger.GetNextFireTimeUtc()).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                    nextrunTime = nextrunTime.IndexOf("0001-01") > -1 ? "2099-12-30" : nextrunTime;

                    TaskRuntimePool.Add(taskid, taskruntimeinfo);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 任务池中的任务进行更新
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="taskruntimeinfo"></param>
        /// <returns></returns>
        public bool UpdateTaskSchduleStatus(string taskid, TaskScheduleStatus status)
        {
            lock (_locktag)
            {
                if (TaskRuntimePool.ContainsKey(taskid))
                {
                    TaskRuntimePool[taskid].TaskModel.ispauseschedule =
                        status == TaskScheduleStatus.PauseSchedule ? 1 : 0;
                    TaskRuntimePool[taskid].TaskModel.taskschedulestatus = (int) status;
                }
            }
            //bool flag =Remove(taskid);
            //if (!flag)
            //{
            //    return false;
            //}
            //Add(taskid, taskruntimeinfo);
            return true;
            
        }

        /// <summary>
        /// 将任务移出任务池
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public bool Remove(string taskid)
        {
            lock (_locktag)
            {
                if (TaskRuntimePool.ContainsKey(taskid))
                {
                    var taskruntimeinfo = TaskRuntimePool[taskid];
                    _sched.PauseTrigger(taskruntimeinfo.TaskModel.id.ToString(), taskruntimeinfo.TaskModel.groupid.ToString());// 停止触发器   
                    _sched.UnscheduleJob(taskruntimeinfo.TaskModel.id.ToString(), taskruntimeinfo.TaskModel.groupid.ToString());// 移除触发器  
                    _sched.DeleteJob(taskruntimeinfo.TaskModel.id.ToString(), taskruntimeinfo.TaskModel.groupid.ToString());// 删除任务

                    TaskRuntimePool.Remove(taskid);
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 获取任务池中任务运行时信息
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public NodeTaskRunTimeInfo Get(string taskid)
        {
            
            lock (_locktag)
            {
                if (!TaskRuntimePool.ContainsKey(taskid))
                {
                    return null;
                }
                if (TaskRuntimePool.ContainsKey(taskid))
                {
                    return TaskRuntimePool[taskid];
                }
                return null;
            }
        }
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        public List<NodeTaskRunTimeInfo> GetList()
        {
            return TaskRuntimePool.Values.ToList();
        }
    }
}
