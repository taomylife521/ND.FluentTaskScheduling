using Newtonsoft.Json;
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
namespace ND.FluentTaskScheduling.Model
{
    public enum ResponesStatus
    {
        Failed = 0,
        Success = 1,
        Exception = 2,
    }
    public class ResponseBase<T>
    {
        public  ResponseBase()
        {
            //Data=default(T);
        }
         [JsonProperty("status")]
        public ResponesStatus Status { get; set; }

         [JsonProperty("msg")]
        public string Msg { get; set; }

            [JsonProperty("ex")]
        public Exception Ex { get; set; }

          [JsonProperty("data")]
        public T Data { get; set; }

          [JsonProperty("createtime")]
        public DateTime CreateTime { get { return DateTime.Now; } }
    }
    public class ResponseBase : ResponseBase<object>
    {

    }
}
