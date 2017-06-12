using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：MonitorPoolManagerInstance.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-06-12 11:42:08         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-06-12 11:42:08          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.Monitor
{
    /// <summary>
    /// 本地节点监控池管理器
    /// </summary>
    public class MonitorPoolManager
    {
        /// <summary>
        /// 命令运行池
        /// </summary>
        private static Dictionary<string, MonitorRunTimeInfo> _monitorRuntimePool = new Dictionary<string, MonitorRunTimeInfo>();
        /// <summary>
        /// 命令池管理者,全局仅一个实例
        /// </summary>
        private static readonly MonitorPoolManager monitorPoolManagerInstance;

        /// <summary>
        /// 命令池管理操作锁标记
        /// </summary>
        private static readonly object locktag = new object();

        /// <summary>
        /// 静态初始化
        /// </summary>
        static MonitorPoolManager()
        {
            monitorPoolManagerInstance = new MonitorPoolManager();
           
        }

        /// <summary>
        /// 获取任务池的全局唯一实例
        /// </summary>
        /// <returns></returns>
        public static MonitorPoolManager CreateInstance()
        {
            return monitorPoolManagerInstance;
        }
        public void Dispose()
        {
            lock (locktag)
            {
                _monitorRuntimePool.Clear();
                _monitorRuntimePool = null;
            }
        }

        /// <summary>
        /// 将命令移入命令池
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="taskruntimeinfo"></param>
        /// <returns></returns>
        public bool Add(string id, MonitorRunTimeInfo commandruntimeinfo)
        {
            lock (locktag)
            {
                if (!_monitorRuntimePool.ContainsKey(id))
                {
                    _monitorRuntimePool.Add(id, commandruntimeinfo);
                    return true;
                }
                return false;
            }
        }


        /// <summary>
        /// 将任务移出任务池
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public bool Remove(string commanddetailid)
        {
            lock (locktag)
            {
                if (_monitorRuntimePool.ContainsKey(commanddetailid))
                {
                    _monitorRuntimePool.Remove(commanddetailid);
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
        public MonitorRunTimeInfo Get(string commanddetailid)
        {
           
            lock (locktag)
            {
                if (!_monitorRuntimePool.ContainsKey(commanddetailid))
                {
                    return null;
                }
                if (_monitorRuntimePool.ContainsKey(commanddetailid))
                {
                    return _monitorRuntimePool[commanddetailid];
                }
                return null;
            }
        }
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        public List<MonitorRunTimeInfo> GetList()
        {
            return _monitorRuntimePool.Values.ToList();
        }
    }
}
