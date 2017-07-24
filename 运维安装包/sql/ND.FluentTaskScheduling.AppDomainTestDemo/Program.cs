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
using System.Timers;

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

            #region 测试AppDomain域内存的释放和加载
            //try
            //{
            //    int second = 6000;
            //    for (int i = 1; i < 3; i++)
            //    {
            //        Console.WriteLine("等待" + second + "秒开始第" + i + "次执行");
            //        Thread.Sleep(second);
            //        string dllPath = Path.Combine(@"D:\ND.Application\ND.FluentTaskScheduling\ND.TaskDemo\ND.FluentTaskScheduling.AppDomainTask\bin\Release", "ND.FluentTaskScheduling.AppDomainTask.dll");
            //        AppDomainSetup setup = new AppDomainSetup();
            //        if (File.Exists(dllPath + ".config"))
            //        {
            //            setup.ConfigurationFile = dllPath + ".config";
            //        }
            //        setup.ShadowCopyFiles = "false";
            //        setup.ApplicationBase = Path.GetDirectoryName(dllPath);
            //        AppDomain domain = AppDomain.CreateDomain(Path.GetFileName(dllPath).Replace(".dll", "").Replace(".exe", ""), null, setup);
            //        AppDomain.MonitoringIsEnabled = true;
            //        AbstractTask obj2 = (AbstractTask)domain.CreateInstanceFromAndUnwrap(dllPath, "ND.FluentTaskScheduling.AppDomainTask.MarshalByTask");
            //        RunTaskResult res = obj2.RunTask();
            //        Console.WriteLine("第" + i + "次执行完成,等待" + second + "开始卸载");
            //        Thread.Sleep(second);
            //        //obj2.Dispose();
            //        // AppDomain.Unload(domain);
            //        // GC.Collect();
            //        //GC.WaitForPendingFinalizers();
            //        //GC.Collect();
            //        //EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            //        Console.WriteLine("第" + i + "次卸载完成");

            //    }

            //}
            //catch (Exception ex)
            //{
            //    // string msg = ex.Message;
            //    //domain = null;
            //    // return null;
            //    Console.WriteLine("异常：" + ex.ToString());
            //} 
            #endregion

            #region 排序算法练习
            //List<int> list = new List<int>() { 3, 2, 5, 1, 8, 7, 9 };
            //#region 常见排序算法
            //#region 冒泡排序
            ////for (int i = 0; i < list.Count; i++)
            ////{
            ////    for (int j = 0; j < list.Count - 1; j++)
            ////    {
            ////        if (list[j] > list[j + 1])
            ////        {
            ////            int temp = list[j];
            ////            list[j] = list[j + 1];
            ////            list[j + 1] = temp;
            ////        }

            ////    }
            ////}
            //#endregion

            //#region 快速排序

            //quickSort(0, list.Count - 1, ref list);
            //#endregion
            //#endregion


            //for (var i = 0; i < list.Count; i++)
            //{
            //    Console.WriteLine(list[i]);
            //}
            //int index = BinarySearch(7, 0, list.Count - 1, ref list);
            //Console.WriteLine("index=" + index + ",value=" + list[index]); 
            #endregion

            #region 模拟线程超时并自动销毁
            #region 使用CancelTokenSource
           //CancellationTokenSource source = new CancellationTokenSource();
           // source.CancelAfter(TimeSpan.FromMilliseconds(2000));
           // Task task=Task.Factory.StartNew(() =>
           // {
           //     while (true)
           //     {
           //         try
           //         {
           //             source.Token.ThrowIfCancellationRequested();
           //             Console.WriteLine("子方法执行中...");


           //             Thread.Sleep(1000);
           //             Console.WriteLine("子方法执行完毕...");
           //         }
           //         catch (OperationCanceledException ex)
           //         {
           //             Console.WriteLine("已捕获取消异常:" + ex.Message);
           //             break;
           //         }
           //         catch (Exception ex)
           //         {
           //             Console.WriteLine("异常:" + ex.Message);
           //             break;
           //         }

           //     }
           //     Console.WriteLine("线程已终止");

           // }, source.Token);
           
            #endregion

            #region 使用CancelTokenSource基于Thread 的实现方式
            //CancellationTokenSource source = new CancellationTokenSource();
            //Thread thread = new Thread(new ThreadStart(() =>
            //{

            //    while (true)
            //    {
            //        Console.WriteLine("现在时间:"+DateTime.Now);
            //    }
            //}));
            //thread.IsBackground = true;
            //thread.Start();

            //source.Token.Register(() =>
            //{
            //    Console.WriteLine("超时时间已到,开始终止线程");
            //    thread.Abort();
            //    Console.WriteLine("超时时间已到,终止线程完成");
            //});
            //source.CancelAfter(2000);
            #endregion

            #region 使用Join实现方式
            //Thread thread =new Thread(new ThreadStart(() =>
            //{
            //    while (true)
            //    {
            //        Console.WriteLine("现在时间:"+DateTime.Now);
            //    }
            //}));
            //thread.Start();
            //thread.Join(2000);
            //Console.WriteLine("join超时时间已到,开始终止线程");
            //thread.Abort();
            //Console.WriteLine("join超时时间已到,终止线程完成");
            #endregion

            #region 基于System.Timers.Timer的实现方式

            //DateTime dtNow = DateTime.Now;
            //Thread thread = new Thread(new ThreadStart(() =>
            //{
            //    while (true)
            //    {
            //        Console.WriteLine("现在时间:" + DateTime.Now);
            //    }

            //}));
            //thread.IsBackground = true;
            //thread.Start();
            //System.Timers.Timer timer = new System.Timers.Timer();
            //timer.Interval = 1000;
            //timer.Elapsed += (sender,e) =>
            //{
            //    double db=DateTime.Now.Subtract(dtNow).TotalSeconds;
            //    if (db > 2)//大于2秒说明已经超时
            //    {
            //        Console.WriteLine("任务已执行超过了:" + db + "秒");
            //        Console.WriteLine("超时时间已到，开始终止线程");
            //        thread.Abort();
            //        Console.WriteLine("超时时间已到，终止线程完成");
            //        timer.Stop();
            //    }
            //};
            //timer.Start();
            
            
            #endregion

            #region 基于信号量和CancellationTokenSource的实现方式
            //AutoResetEvent autoReset = new AutoResetEvent(false);
            //CancellationTokenSource source = new CancellationTokenSource();
          
            //Task.Factory.StartNew(() =>
            //{
            //    try
            //    {
            //        while (true)
            //        {
            //            source.Token.ThrowIfCancellationRequested();
            //            Console.WriteLine("子线程:Work starting.");

            //            // Simulate time spent working.
            //            Thread.Sleep(5000);//new Random().Next(500, 2000)
            //            autoReset.Set();
            //            // Signal that work is finished.
            //            Console.WriteLine("子线程:Work ending.");

            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        autoReset.Set();
            //        Console.WriteLine("子线程收到异常:"+e.Message+",线程退出!");
            //       // throw;
            //    }
               

                
            //}, source.Token);
            //Console.WriteLine("主线程只等待1秒");
            //autoReset.WaitOne(1000);//等待超过1秒，则需要取消该线程
            //Console.WriteLine("主线程等待超时,开始继续执行,同时调用Source.Cancel()");
            //source.Cancel();
          
            #endregion
            #endregion

            #region 模拟数组反转
            //List<int> list = new List<int>(){1,2,3,4,5,6,7};
            //int i = 0;
            //int j = list.Count - 1;
            //while (i < j)
            //{
            //    int temp = list[i];
            //    list[i] = list[j];
            //    list[j] = temp;
            //    i++;
            //    j--;

            //}
            //foreach (var i1 in list)
            //{
            //    Console.WriteLine(i1);
            //}
            //int k=0;
            //for(int i=0,j=0;i<10,j<6;i++,j++)
            //{
            //    k = i + j;
            //}
            //string s = "aaabccccef";
            //char[] c = s.ToCharArray();
            //var cc = c.GroupBy(e => e).OrderByDescending(e => e.Count()).ToList();
            //for (int i = 0; i < cc.Count; i++)
            //{
            //    Console.WriteLine("{0}\t{1}", cc[i].Key.ToString(), cc[i].Count().ToString());
            //}
            //var ccc = c.GroupBy(e => e).OrderByDescending(e => e.Count()).First().ToList();

            //Console.WriteLine("{0}出现{1}次！", ccc[0].ToString(), ccc.Count().ToString());
            #endregion

           Console.WriteLine("ok");
           Console.ReadKey();
        }

       

        #region 快速排序

        public static void quickSort(int left,int right,ref List<int> list)
        {
            int i, j, temp,t;
            if (left > right)
            {
                return;
            }
             temp = list[left];//基准数
            i = left;
            j = right;
            
            while (i != j)
            {
                while (list[j] >= temp && i < j)
                {
                    j--;
                }
                while (list[i] <= temp && i < j)
                {
                    i++;
                }
                //交换两个数在数组中的位置 
                if (i < j)
                {
                    t = list[i];
                    list[i] = list[j];
                    list[j] = t;
                } 

            }
            //最终将基准数归位 
            list[left] = list[i];
            list[i] = temp;
            quickSort(left, i - 1,ref list);//继续处理左边的，这里是一个递归的过程 
            quickSort(i + 1, right,ref list);//继续处理右边的 ，这里是一个递归的过程 

        }
        #endregion

        #region 二分查找

        public static int BinarySearch(int value,int left,int right,ref List<int> list)
        {
            if (left > right)
            {
                return -1;
            }
            int middle = (left + right) / 2;
            if (list[middle] == value)
            {
                return middle;
            }
            else if (list[middle] > value)
            {
                return BinarySearch(value, left, right - 1, ref list);
            }
            else if (list[middle] < value)
            {
               return BinarySearch(value, middle+1, right, ref list);
            }
            return -1;

        }
        #endregion
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
