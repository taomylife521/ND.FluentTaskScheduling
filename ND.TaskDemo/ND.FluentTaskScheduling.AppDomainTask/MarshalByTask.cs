using ND.FluentTaskScheduling.TaskInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.FluentTaskScheduling.AppDomainTask
{
    public class MarshalByTask : AbstractTask
    {
        //  Call this method via a proxy.
       

        public override RunTaskResult RunTask()
        {
            Task.Factory.StartNew(() =>
            {
                List<string> rlist = new List<string>();
                for (int r = 0; r < 100000; r++)
                {
                    string guid = Guid.NewGuid().ToString();
                    rlist.Add(guid);
                    Console.WriteLine(DateTime.Now + "(" + this.AppConfig["a"] + "):" + guid);//"(" + this.AppConfig["a"] + ")
                }
                //rlist = null;

            });
           
            return new RunTaskResult(){RunStatus = (int)RunStatus.Normal};
        }

        public override void InitTaskAppConfig()
        {
            if (!this.AppConfig.ContainsKey("a"))
            {
                this.AppConfig.Add("a","ccc");
            }
        }
    }
}
