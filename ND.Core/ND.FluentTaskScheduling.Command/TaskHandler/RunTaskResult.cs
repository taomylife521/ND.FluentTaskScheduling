using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：RunTaskResult.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 10:57:36         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 10:57:36          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.TaskHandler
{
    public class RunTaskResult : RunTaskResult<object>
    {
        public RunTaskResult(RunStatus runStatus = RunStatus.Normal, string message = "", Exception ex = null, object data = null)
            : base(runStatus, message, ex, data)
        {

        }

    }
    public class RunTaskResult<T> : EventArgs
    {
        /// <summary>
        /// 运行状态
        /// </summary>
        public RunStatus RunStatus { get; set; }

        /// <summary>
        /// 是否从新再运行一次
        /// </summary>
        public bool OnceAgain { get; set; }

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
        public T Data { get; set; }
        public RunTaskResult()
        {

        }
        public RunTaskResult(RunStatus runStatus)
        {
            RunStatus = runStatus;
        }

        public RunTaskResult(RunStatus runStatus, string message)
            : this(runStatus)
        {
            Message = message;
        }

        public RunTaskResult(RunStatus runStatus, string message, Exception ex)
            : this(runStatus, message)
        {
            Ex = ex;
        }

        public RunTaskResult(RunStatus runStatus, string message, Exception ex, T data)
            : this(runStatus, message, ex)
        {
            Data = data;
        }
    }
}
