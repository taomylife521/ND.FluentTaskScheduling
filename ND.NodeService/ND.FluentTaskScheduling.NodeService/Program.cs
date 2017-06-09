using ND.FluentTaskScheduling.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ND.FluentTaskScheduling.NodeService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                HostFactory.Run(x =>
               {
                   x.Service<TaskManagerNodeService>(s =>
                   {
                       s.ConstructUsing(name => new TaskManagerNodeService());
                       s.WhenStarted(tc => tc.Start());
                       s.WhenStopped(tc => tc.Stop());



                   });
                   x.RunAsLocalSystem();
                   string discription = ConfigurationManager.AppSettings["WindowsServiceDiscription"] == null ? "分布式任务调度程序windows服务描述" : ConfigurationManager.AppSettings["WindowsServiceDiscription"].ToString();
                   string displayname = ConfigurationManager.AppSettings["WindowsServiceDisplayName"] == null ? "TaskManagerNodeServiceDisplayName" : ConfigurationManager.AppSettings["WindowsServiceDisplayName"].ToString();
                   string serviceName = ConfigurationManager.AppSettings["WindowsServiceName"] == null ? "TaskManagerNodeServiceWindowsServiceName" : ConfigurationManager.AppSettings["WindowsServiceName"].ToString();
                   x.SetDescription(discription);
                   x.SetDisplayName(displayname);
                   x.SetServiceName(serviceName);
               });
            }
            catch(Exception ex)
            {
                Console.WriteLine("服务启动失败:"+JsonConvert.SerializeObject(ex));
            }
            Console.ReadKey();
        }
        
    }
}
