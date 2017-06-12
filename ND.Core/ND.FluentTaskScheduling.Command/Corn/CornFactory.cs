using ND.FluentTaskScheduling.Core.TaskHandler;

using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CornFactory.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 15:25:34         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 15:25:34          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Command.Corn
{
    public class CornFactory
    {
        public static Trigger CreateTigger(NodeTaskRunTimeInfo taskruntimeinfo)
        {
            if (taskruntimeinfo.TaskVersionModel.taskcron.Contains("["))
            {
                var customcorn = CustomCornFactory.GetCustomCorn(taskruntimeinfo.TaskVersionModel.taskcron);
                customcorn.Parse();
                if (customcorn is SimpleCorn || customcorn is RunOnceCorn)
                {
                    var simplecorn = customcorn as SimpleCorn;
                    // 定义调度触发规则，比如每1秒运行一次，共运行8次
                    SimpleTrigger simpleTrigger = new SimpleTrigger(taskruntimeinfo.TaskModel.id.ToString(), taskruntimeinfo.TaskModel.groupid.ToString());
                    if (simplecorn.ConInfo.StartTime != null)
                        simpleTrigger.StartTimeUtc = simplecorn.ConInfo.StartTime.Value.ToUniversalTime();
                    //else
                    //    simpleTrigger.StartTimeUtc = DateTime.Now.ToUniversalTime();
                    if (simplecorn.ConInfo.EndTime != null)
                        simpleTrigger.EndTimeUtc = simplecorn.ConInfo.EndTime.Value.ToUniversalTime();
                    if (simplecorn.ConInfo.RepeatInterval != null)
                        simpleTrigger.RepeatInterval = TimeSpan.FromSeconds(simplecorn.ConInfo.RepeatInterval.Value);
                    else
                        simpleTrigger.RepeatInterval = TimeSpan.FromSeconds(1);
                    if (simplecorn.ConInfo.RepeatCount != null)
                        simpleTrigger.RepeatCount = simplecorn.ConInfo.RepeatCount.Value - 1;//因为任务默认执行一次，所以减一次
                    else
                        simpleTrigger.RepeatCount = int.MaxValue;//不填，则默认最大执行次数
                    return simpleTrigger;
                }
                return null;
            }
            else
            {
                CronTrigger trigger = new CronTrigger(taskruntimeinfo.TaskModel.id.ToString(), taskruntimeinfo.TaskModel.groupid.ToString());// 触发器名,触发器组  
                trigger.CronExpressionString = taskruntimeinfo.TaskVersionModel.taskcron;// 触发器时间设定  
                return trigger;
            }
        }
    }
}
