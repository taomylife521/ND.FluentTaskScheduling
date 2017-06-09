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
namespace ND.FluentTaskScheduling.TaskInterface
{
    //public class RunTaskResult : RunTaskResult<object>
    //{
    //    public RunTaskResult(RunStatus runStatus = RunStatus.Normal, string message = "", Exception ex = null, object data = null)
    //        : base(runStatus, message, ex, data)
    //    {

    //    }

    //}
   [Serializable]
    public class RunTaskResult: MarshalByRefObject
    {
        /// <summary>
        /// 运行状态
        /// </summary>
       // public RunStatus RunStatus { get; set; }

        /// <summary>
        /// 运行状态
        /// </summary>
        public int RunStatus { get; set; }

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
        public object Data { get; set; }
        public RunTaskResult()
        {
            //this.RunStatus = TaskInterface.RunStatus.Failed;
            this.OnceAgain = false;
            this.Message = "";
            this.Ex = null;
            this.Data = null;
            
        }
        public RunTaskResult(RunStatus runStatus)
        {
            RunStatus = 0;
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

        public RunTaskResult(RunStatus runStatus, string message, Exception ex,object obj)
            : this(runStatus, message,ex)
        {
            obj = obj;
        }

      
    }
}
