using ND.FluentTaskScheduling.Core;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：CommandQueueProxy.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-10 16:08:58         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-10 16:08:58          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Command.asyncrequest
{
    public class NodeProxy
    {
        public static ResponseBase<TResponse> PostToServer<TResponse,TRequest>(string url,TRequest request) where TResponse:class where TRequest: RequestBase
        {
            ResponseBase<TResponse> r = HttpClientHelper.PostResponse<ResponseBase<TResponse>>(url, JsonConvert.SerializeObject(request));
            return r;
        }
    }
}
