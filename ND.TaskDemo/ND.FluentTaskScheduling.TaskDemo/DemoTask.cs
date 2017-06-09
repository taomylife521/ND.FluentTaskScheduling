using ND.FluentTaskScheduling.TaskInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.FluentTaskScheduling.TaskDemo
{
    public class DemoTask:AbstractTask
    {
        public override RunTaskResult RunTask()
        {
           
            ShowProcessIngLog(DateTime.Now + ":我执行了,参数:" + JsonConvert.SerializeObject(this.AppConfig));
            return new RunTaskResult() { RunStatus = (int)RunStatus.Normal};
        }

        public override void InitTaskAppConfig()
        {
           if(!this.AppConfig.ContainsKey("aa"))
           {
               this.AppConfig.Add("aa", new Random().Next(1,99).ToString());
           }
        }
    }
}
