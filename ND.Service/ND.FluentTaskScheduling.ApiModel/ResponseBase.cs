using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：ResponseBase.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-03-30 14:30:58         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-03-30 14:30:58          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.ApiModel
{
    public enum OperatorStatus
    {
        Success = 0,
        Failed = 1,
        Exception = 2,
    }
    public class ResponseBase<T>
    {
        public OperatorStatus OperStatus { get; set; }
        public string OperatorMsg { get; set; }

        public Exception Ex { get; set; }

        public T Data { get; set; }
        public DateTime CreateTime { get { return DateTime.Now; } }
    }
    public class ResponseBase : ResponseBase<object>
    {

    }
}
