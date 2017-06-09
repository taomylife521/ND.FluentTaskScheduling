using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：ICommandQueueScan.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-12 14:21:25         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-12 14:21:25          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandHandler
{
    /// <summary>
    /// 命令队列扫描器
    /// </summary>
    public interface ICommandQueueScan
    {
         
          
          /// <summary>
          /// 异步开启命令队列扫描
          /// </summary>
          /// <returns></returns>
          void StartScanCommandQueueAsync();
         

         /// <summary>
         /// 开启扫描命令队列
         /// </summary>
         /// <returns></returns>
          bool StartScanCommandQueue();

        /// <summary>
        /// 停止扫描命令队列
        /// </summary>
        /// <returns></returns>
          bool StopScanCommandQueue();
         
         /// <summary>
         /// 是否已经停止扫描该命令队列
         /// </summary>
         /// <returns></returns>
          bool IsHaveStopScanCommandQueue();
    }
}
