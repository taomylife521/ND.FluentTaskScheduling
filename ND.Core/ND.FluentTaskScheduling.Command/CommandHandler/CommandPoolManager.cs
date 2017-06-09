using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandPoolManager.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-05 11:10:57         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-05 11:10:57          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandHandler
{
   public class CommandPoolManager:IDisposable
    {
        /// <summary>
        /// 命令运行池
        /// </summary>
       private static Dictionary<string, CommandRunTimeInfo> CommandRuntimePool = new Dictionary<string, CommandRunTimeInfo>();
        /// <summary>
        /// 命令池管理者,全局仅一个实例
        /// </summary>
       private static CommandPoolManager _commandpoolmanager;

       /// <summary>
       /// 命令池管理操作锁标记
       /// </summary>
       private static object _locktag = new object();

         /// <summary>
        /// 静态初始化
        /// </summary>
       static CommandPoolManager()
        {
            _commandpoolmanager = new CommandPoolManager();
           
        }

       /// <summary>
       /// 获取任务池的全局唯一实例
       /// </summary>
       /// <returns></returns>
       public static CommandPoolManager CreateInstance()
       {
           return _commandpoolmanager;
       }
        public void Dispose()
        {
            CommandRuntimePool.Clear();
            CommandRuntimePool = null;
        }

        /// <summary>
        /// 将命令移入命令池
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="taskruntimeinfo"></param>
        /// <returns></returns>
        public bool Add(string commanddetailid, CommandRunTimeInfo commandruntimeinfo)
        {
            lock (_locktag)
            {
                if (!CommandRuntimePool.ContainsKey(commanddetailid))
                {
                    CommandRuntimePool.Add(commanddetailid, commandruntimeinfo);
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
            lock (_locktag)
            {
                if (CommandRuntimePool.ContainsKey(commanddetailid))
                {
                    var taskruntimeinfo = CommandRuntimePool[commanddetailid];


                    CommandRuntimePool.Remove(commanddetailid);
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
        public CommandRunTimeInfo Get(string commanddetailid)
        {
            if (!CommandRuntimePool.ContainsKey(commanddetailid))
            {
                return null;
            }
            lock (_locktag)
            {
                if (CommandRuntimePool.ContainsKey(commanddetailid))
                {
                    return CommandRuntimePool[commanddetailid];
                }
                return null;
            }
        }
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <returns></returns>
        public List<CommandRunTimeInfo> GetList()
        {
            return CommandRuntimePool.Values.ToList();
        }


    }
}
