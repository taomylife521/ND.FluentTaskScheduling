using ND.FluentTaskScheduling.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskFactory.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 14:57:55         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 14:57:55          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.TaskHandler
{
    public class TaskFactory
    {
        public NodeTaskRunTimeInfo Create(string  taskid)
        {
            NodeTaskRunTimeInfo taskruntimeinfo = TaskPoolManager.CreateInstance().Get(taskid.ToString());
            if (taskruntimeinfo != null)
            {
                throw new Exception("任务已在运行中");
            }
             taskruntimeinfo = new NodeTaskRunTimeInfo();
             taskruntimeinfo.TaskLock = new TaskLock();
            // taskruntimeinfo.TaskModel = task;

             return taskruntimeinfo;

        }
    }
}
