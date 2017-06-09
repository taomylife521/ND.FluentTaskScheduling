using ND.FluentTaskScheduling.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
//**********************************************************************
//
// 文件名称(File Name)：GlobalExceptionFilter.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-10 11:31:40         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-10 11:31:40          
//             修改理由：         
//**********************************************************************

namespace ND.FluentTaskScheduling.WebApi.Filters
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute 
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //1.异常日志记录（正式项目里面一般是用log4net记录异常日志）
           string message=DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "——" +
                actionExecutedContext.Exception.GetType().ToString() + "：" + actionExecutedContext.Exception.Message + "——堆栈信息：" +
                actionExecutedContext.Exception.StackTrace;

           

            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new ResponseBase<EmptyResponse>() { Status = ResponesStatus.Exception, Msg = message }))
            };
            base.OnException(actionExecutedContext);
        }
        //public  void OnException2(ExceptionContext filterContext)
        //{
        //    string message = string.Format("消息类型：{0}<br>消息内容：{1}<br>引发异常的方法：{2}<br>引发异常源：{3}"
        //       , filterContext.Exception.GetType().Name
        //       , filterContext.Exception.Message
        //        , filterContext.Exception.TargetSite
        //        , filterContext.Exception.Source + filterContext.Exception.StackTrace
        //        );
        //    ResponseBase<EmptyResponse> rep = new ResponseBase<EmptyResponse>()
        //    {
        //        Status= ResponesStatus.Exception,
        //        Msg=message
        //    };
        //    filterContext.RequestContext.HttpContext.Response.Write(rep);
         
        //}
    }
}