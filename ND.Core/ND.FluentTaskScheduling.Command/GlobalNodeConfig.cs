using ND.FluentTaskScheduling.Command.Monitor;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：GlobalNodeConfig.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 16:05:57         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 16:05:57          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core
{
    public class GlobalNodeConfig
    {
        static GlobalNodeConfig()
        {
            NodeInfo = new tb_node();
        }
        /// <summary>
        /// 节点详情
        /// </summary>
        public static tb_node NodeInfo { get; set; }

        /// <summary>
        /// 当前节点标识
        /// </summary>
        public static int NodeID { get; set; }

        ///// <summary>
        ///// 报警方式
        ///// </summary>
        //public static AlarmType AlarmType { get; set; }

        /// <summary>
        /// 当前出问题要报警的人
        /// </summary>
        public static string Alarm { get; set; }

        //  /// <summary>
        ///// 是否启用报警
        ///// </summary>
        //public static int IsEnableAlarm { get; set; }

        ///// <summary>
        ///// 上次取得命令队列最大编号
        ///// </summary>
        //public static int LastMaxId { get; set; }

        /// <summary>
        /// 任务调度平台web url地址
        /// </summary>
        public static string TaskManagerWebUrl { get { return System.Configuration.ConfigurationSettings.AppSettings["TaskApiUrl"]; } }
        /// <summary>
        /// 任务dll根目录
        /// </summary>
        public static string TaskDllDir = "任务dll根目录";
        /// <summary>
        /// 任务dll本地版本缓存
        /// </summary>
        public static string TaskDllCompressFileCacheDir = "任务dll版本缓存";
        /// <summary>
        /// 任务平台共享程序集
        /// </summary>
        public static string TaskSharedDllsDir = "任务dll共享程序集";
        /// <summary>
        /// 任务平台节点使用的监控插件
        /// </summary>
        public static List<AbsMonitor> Monitors = new List<AbsMonitor>();
    }
}
