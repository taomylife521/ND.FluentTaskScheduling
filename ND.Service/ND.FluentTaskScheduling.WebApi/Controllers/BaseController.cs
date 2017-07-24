using ND.FluentTaskScheduling.Domain.interfacelayer;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.WebApi.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//**********************************************************************
//
// 文件名称(File Name)：BaseController.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-03-30 14:21:51         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-03-30 14:21:51          
//             修改理由：         
//**********************************************************************

namespace ND.FluentTaskScheduling.WebApi.Controllers
{
    [WebApiExceptionFilter]
    [WebApiValidateActionFilter]
    public class BaseController:ApiController
    {
        public INodeRepository noderepository;
        public IUserRepository userrepository;

        public BaseController(INodeRepository nodeRep, IUserRepository userRep)
        {
            noderepository = nodeRep;
            userrepository = userRep;
        }
        public virtual ResponseBase<T> ResponseToClient<T>(ResponesStatus status, string message, T data = null) where T : class
        {

            ResponseBase<T> response = new ResponseBase<T> { Status = status, Msg = message, Data = data };
           // IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            //timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            //return JsonConvert.SerializeObject(response, Formatting.None, timeFormat);
            return response;
        }

        //public virtual ResponseBase<object> ResponseToClient(ResponesStatus status, string message, object data = null) 
        //{
        //    return ResponseToClient<object>(status, message, data);
        //}
    }
}