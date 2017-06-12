using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandQueueScanManger.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-12 14:31:08         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-12 14:31:08          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandHandler
{
    /// <summary>
    /// 命令队列扫描器管理
    /// </summary>
   public class CommandQueueScanManger
    {
       private static ICommandQueueScan scaner;
       public static event EventHandler<ScanCommandEventArgs> OnInitEvent;
       static CommandQueueScanManger()
       {
           scaner = new DbCommandQueueScan();
           AbsCommandQueueScan.OnInitEvent += AbsCommandQueueScan_OnInitEvent;
       }

      static void AbsCommandQueueScan_OnInitEvent(object sender, ScanCommandEventArgs e)
       {
           if (OnInitEvent != null)
           {
               OnInitEvent(sender, e);
           }
       }


       public static void StartScanCommandQueueAsync()
       {
           scaner.StartScanCommandQueueAsync();
       }



       public static bool StopScanCommandQueue()
       {
           return scaner.StopScanCommandQueue();
       }

       /// <summary>
       /// 是否已经停止扫描线程
       /// </summary>
       /// <returns>true 代表已停止 false代表未停止</returns>
       public static bool IsStopCommandQueue()
       {
           return scaner.IsHaveStopScanCommandQueue();
       }
    }
}
