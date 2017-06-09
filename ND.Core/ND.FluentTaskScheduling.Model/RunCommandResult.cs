using ND.FluentTaskScheduling.Model.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：RunCommandResult.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-12 15:42:20         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-12 15:42:20          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model
{
    //public enum CommandRunStatus
    //{
    //    [Description("正常")]
    //    Normal = 0,

    //    [Description("失败")]
    //    Failed = 1,

    //    [Description("异常")]
    //    Exception = 2

    //}
    [Serializable]
    public class RunCommandResult 
    {
        /// <summary>
        /// 运行状态
        /// </summary>
        public ExecuteStatus ExecuteStatus { get; set; }

        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// 运行输出消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 运行异常
        /// </summary>
        public Exception Ex { get; set; }

        /// <summary>
        /// 运行异常
        /// </summary>
        public object Data { get; set; }


    }
   // [Serializable]
   //public class RunCommandResult<T>
   // {
   //     /// <summary>
   //     /// 运行状态
   //     /// </summary>
   //     public CommandRunStatus RunStatus { get; set; }

   //     /// <summary>
   //     /// 重试次数
   //     /// </summary>
   //     public int RetryCount { get; set; }

   //     /// <summary>
   //     /// 运行输出消息
   //     /// </summary>
   //     public string Message { get; set; }

   //     /// <summary>
   //     /// 运行异常
   //     /// </summary>
   //     public Exception Ex { get; set; }

   //     /// <summary>
   //     /// 运行异常
   //     /// </summary>
   //     public T Data { get; set; }
   // }
}
