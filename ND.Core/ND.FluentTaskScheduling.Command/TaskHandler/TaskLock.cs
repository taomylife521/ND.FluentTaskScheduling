using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskLock.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 14:05:27         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 14:05:27          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.TaskHandler
{
   public class TaskLock
    {
        //处理锁机制
        private bool _ifrunning = false;
        private object _firstlockTag = new object();//用于锁_ifrunning变量
        private object _twolock = new object();
        //内部加锁
        private bool TryToLockSingleInstance()
        {
            if (_ifrunning == true)
                return false;
            lock (_firstlockTag)
            {
                if (_ifrunning == true)
                    return false;
                else
                {
                    _ifrunning = true;
                    return true;
                }
            }
        }
        //内部释放锁
        private void EndToLockSingleInstance()
        {
            _ifrunning = false;
        }

        public void Invoke(Action action)
        {
            //上次未结束，不再触发,仅运行一次实例
            if (this.TryToLockSingleInstance())
            {
                try
                {
                    lock (_twolock)
                    {
                        action();
                    }
                }
                catch (Exception exp)
                {
                    throw exp;
                }
                finally
                {
                    EndToLockSingleInstance();
                }
            }
        }
    }
}
