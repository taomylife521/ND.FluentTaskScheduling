
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
//**********************************************************************
//
// 文件名称(File Name)：WebApiActionFilterAttribute.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-24 13:17:19         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-24 13:17:19          
//             修改理由：         
//**********************************************************************

namespace ND.FluentTaskScheduling.WebApi.Filters
{
    public class WebApiValidateActionFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                //actionContext.Response = actionContext.Request.CreateResponse(
                //    HttpStatusCode.BadRequest, new ApiResourceValidationErrorWrapper(actionContext.ModelState));
            }
           // base.OnActionExecuting(actionContext);
        }
       
   
    }
    
   
}