using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.TaskInterface;
using System.Diagnostics;
using System.Threading;
using ND.FluentTaskScheduling.CrawlKeGui;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Reflection;
using System.Runtime.InteropServices;
using System.IO;

namespace ND.FluentTaskScheduling.AppDomainTestDemo
{
    #region test
    public class AppService1 : MarshalByRefObject
    {
        public void PrintString(string contents) { Console.WriteLine(contents); }

        public override object InitializeLifetimeService()
        {
            ILease lease = (ILease)base.InitializeLifetimeService();
            if (lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.Zero;
                // lease.RenewOnCallTime = TimeSpan.FromSeconds(20);
            } return lease;
        }
    }

    public class AppService2 : MarshalByRefObject
    {
        public void PrintString(string contents) { Console.WriteLine(contents); }

        public override object InitializeLifetimeService()
        {
            ILease lease = (ILease)base.InitializeLifetimeService();
            if (lease.CurrentState == LeaseState.Initial)
            {
                lease.InitialLeaseTime = TimeSpan.FromSeconds(10);
                lease.RenewOnCallTime = TimeSpan.FromSeconds(40);
            } return lease;
        }
    } 
   
    public class MyTracking:ITrackingHandler 
    {  
        public MyTracking()  {   
            //   // TODO: 在此处添加构造函数逻辑   // 
        }

         public void MarshaledObject(object obj,ObjRef or) 
         {   
             Console.WriteLine();

             Console.WriteLine("对象" + obj.ToString() + " is marshaled at " + DateTime.Now.ToShortTimeString() + ",IsFromThisAppDomain=" + or.IsFromThisAppDomain());
         }

         public void UnmarshaledObject(object obj, ObjRef or) 
         {
             Console.WriteLine(); Console.WriteLine("对象" + obj.ToString() + " is unmarshaled at " + DateTime.Now.ToShortTimeString() + ",IsFromThisAppDomain=" + or.IsFromThisAppDomain()); 
         }

          public void DisconnectedObject(object obj) 
          { 
              Console.WriteLine(obj.ToString() + " is disconnected at " + DateTime.Now.ToShortTimeString()); 
          } 
    }
    #endregion

   

    class Program
    {
        [DllImport("psapi.dll")]
        public static extern int EmptyWorkingSet(IntPtr hwProc);
        static void Main(string[] args)
        {
            //AbstractTask.OnProcessing += AbstractTask_OnProcessing;
            //CrawlKeGuiTask task = new CrawlKeGuiTask();
            //task.InitTaskAppConfig();
            //task.RunTask();
            //List<string> lst = new List<string>() { "1", "3", "5", "7" };
            //List<string> lst2 = new List<string>();
            //lst.ForEach(x=>{
            //    System.Console.WriteLine("lst:"+x);
            //    lst2.Add(x);

            //});

            //lst2.ForEach(x =>
            //{
            //    Console.WriteLine("lst2:"+x);
            //});

            //Console.WriteLine(GetPerfCount("Process", "% Processor Time", "_Total"));
            //Console.WriteLine(GetPerfCount(".NET CLR Memory", "# Bytes in all Heaps", "_Global_"));
            //Console.WriteLine(GetPerfCount("SQLServer:General Statistics", "User Connections"));

            #region MyRegion
            //TcpChannel channel = new TcpChannel(8081);
            //ChannelServices.RegisterChannel(channel);
            //LifetimeServices.LeaseTime = TimeSpan.Zero;
            //TrackingServices.RegisterTrackingHandler(new MyTracking());
            //AppService1 service1 = new AppService1();
            //ObjRef objRef1 = RemotingServices.Marshal((MarshalByRefObject)service1, "AppService1");
            //AppService2 service2 = new AppService2();
            //ObjRef objRef2 = RemotingServices.Marshal((MarshalByRefObject)service2, "AppService2");
            //Console.WriteLine("Remoting服务启动，按退出...");
            //Console.ReadLine(); 
            #endregion

            // Get and display the friendly name of the default AppDomain.
            #region test
            //string callingDomainName = Thread.GetDomain().FriendlyName;
            //Console.WriteLine(callingDomainName);

            //// Get and display the full name of the EXE assembly.
            //string exeAssembly = Assembly.GetEntryAssembly().FullName;
            //Console.WriteLine(exeAssembly);

            //// Construct and initialize settings for a second AppDomain.
            //AppDomainSetup ads = new AppDomainSetup();
            //ads.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;

            //ads.DisallowBindingRedirects = false;
            //ads.DisallowCodeDownload = true;
            //ads.ConfigurationFile =
            //    AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            //Task.Factory.StartNew(() =>
            //{
            //    for (int i = 0; i < 2; i++)
            //    {
            //        Thread.Sleep(6000);
            //        // Create the second AppDomain.
            //        AppDomain ad2 = AppDomain.CreateDomain("AD #2", null, ads);

            //        // Create an instance of MarshalbyRefType in the second AppDomain. 
            //        // A proxy to the object is returned.
            //        MarshalByRefType mbrt =
            //            (MarshalByRefType)ad2.CreateInstanceAndUnwrap(
            //                exeAssembly,
            //                typeof(MarshalByRefType).FullName
            //            );

            //        // Call a method on the object via the proxy, passing the 
            //        // default AppDomain's friendly name in as a parameter.
            //        mbrt.SomeMethod("ad2");
            //        Console.WriteLine("调用完成，等待6秒开始卸载");
            //        Thread.Sleep(6000);
            //        // Unload the second AppDomain. This deletes its object and 
            //        // invalidates the proxy object.
            //        AppDomain.Unload(ad2);
            //        GC.Collect();
            //        GC.WaitForPendingFinalizers();
            //        GC.Collect();
            //        EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            //        Console.WriteLine("卸载完成，等待6秒开始执行下个AppDomain");
            //        Thread.Sleep(6000);
            //        AppDomain ad3 = AppDomain.CreateDomain("AD #2", null, ads);

            //        // Create an instance of MarshalbyRefType in the second AppDomain. 
            //        // A proxy to the object is returned.
            //        MarshalByRefType mbrt3 =
            //            (MarshalByRefType)ad3.CreateInstanceAndUnwrap(
            //                exeAssembly,
            //                typeof(MarshalByRefType).FullName
            //            );

            //        // Call a method on the object via the proxy, passing the 
            //        // default AppDomain's friendly name in as a parameter.
            //        mbrt3.SomeMethod("ad3");
            //        Console.WriteLine("ad3调用完成,等待6秒开始卸载");
            //        Thread.Sleep(6000);
            //        // Unload the second AppDomain. This deletes its object and 
            //        // invalidates the proxy object.
            //        AppDomain.Unload(ad3);
            //        GC.Collect();
            //        GC.WaitForPendingFinalizers();
            //        GC.Collect();
            //        EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            //        Console.WriteLine("ad3卸载完成,等待6秒开始退出");
            //        Thread.Sleep(6000);
            //        try
            //        {
            //            // Call the method again. Note that this time it fails 
            //            // because the second AppDomain was unloaded.
            //            //mbrt.SomeMethod(callingDomainName);
            //            Console.WriteLine("Sucessful call.");
            //        }
            //        catch (AppDomainUnloadedException)
            //        {
            //            Console.WriteLine("Failed call; this is expected.");
            //        }
            //    }


            //}); 
            #endregion

            try
            {
                int second = 6000;
                for (int i = 1; i < 3; i++)
                {
                    Console.WriteLine("等待" + second + "秒开始第" + i + "次执行");
                    Thread.Sleep(second);
                    string dllPath = Path.Combine(@"D:\ND.Application\ND.FluentTaskScheduling\ND.TaskDemo\ND.FluentTaskScheduling.AppDomainTask\bin\Release", "ND.FluentTaskScheduling.AppDomainTask.dll");
                    AppDomainSetup setup = new AppDomainSetup();
                    if (File.Exists(dllPath + ".config"))
                    {
                        setup.ConfigurationFile = dllPath + ".config";
                    }
                    setup.ShadowCopyFiles = "false";
                    setup.ApplicationBase = Path.GetDirectoryName(dllPath);
                    AppDomain domain = AppDomain.CreateDomain(Path.GetFileName(dllPath).Replace(".dll", "").Replace(".exe", ""), null, setup);
                    AppDomain.MonitoringIsEnabled = true;
                    AbstractTask obj2 = (AbstractTask)domain.CreateInstanceFromAndUnwrap(dllPath, "ND.FluentTaskScheduling.AppDomainTask.MarshalByTask");
                    RunTaskResult res = obj2.RunTask();
                    Console.WriteLine("第" + i + "次执行完成,等待" + second + "开始卸载");
                    Thread.Sleep(second);
                    //obj2.Dispose();
                   // AppDomain.Unload(domain);
                   // GC.Collect();
                    //GC.WaitForPendingFinalizers();
                    //GC.Collect();
                    //EmptyWorkingSet(Process.GetCurrentProcess().Handle);
                    Console.WriteLine("第" + i + "次卸载完成");

                }
                //Console.WriteLine("ok");
                //return obj;
            }
            catch (Exception ex)
            {
               // string msg = ex.Message;
                //domain = null;
               // return null;
                Console.WriteLine("异常："+ex.ToString());
            }
           
            

            
           Console.WriteLine("ok");
           Console.ReadKey();
        }

        static void AbstractTask_OnProcessing(object sender, string e)
        {
            Console.WriteLine(DateTime.Now+":"+e);

        }

        /// <summary>
        /// 获取计数器样本并为其返回计算所得值--有实例的计数器(对于大多数的计数器)
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="counterName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static float GetPerfCount(string categoryName, string counterName, string instance)
        {
            PerformanceCounter counter = new PerformanceCounter
            {
                CategoryName = categoryName,
                CounterName = counterName,
                InstanceName = instance,
                MachineName = ".",
                ReadOnly = true
            };
            counter.NextValue();
            Thread.Sleep(200);
            try
            {
                if (counter != null)
                {
                    return counter.NextValue();
                }
            }
            catch (Exception)
            {
                return -2f;
            }
            return -1f;
        }

        /// <summary>
        /// 获取计数器样本并为其返回计算所得值--无实例的计数器
        /// 比如categoryName=SQLServer:General Statistics，counterName=User Connections
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="counterName"></param>
        /// <returns></returns>
        public static float GetPerfCount(string categoryName, string counterName)
        {
            PerformanceCounter counter = new PerformanceCounter
            {
                CategoryName = categoryName,
                CounterName = counterName
            };
            counter.NextValue();
            Thread.Sleep(200);
            try
            {
                if (counter != null)
                {
                    return counter.NextValue();
                }
            }
            catch (Exception)
            {
                return -2f;
            }
            return -1f;
        }
    }
}
