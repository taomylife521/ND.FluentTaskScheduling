using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using ND.FluentTaskScheduling.Web.App_Start;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//**********************************************************************
//
// 文件名称(File Name)：BaseController.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-17 15:11:54         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-17 15:11:54          
//             修改理由：         
//**********************************************************************

namespace ND.FluentTaskScheduling.Web.Controllers
{
    public class JsonNetResult :JsonResult
    {
        public JsonSerializerSettings Settings { get; private set; }
        public JsonNetResult()
        {
            Settings = new JsonSerializerSettings
            {
                //这句是解决问题的关键,也就是json.net官方给出的解决配置选项.                 
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet && string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON GET is not allowed");
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(this.ContentType) ? "application/json" : this.ContentType;
            if (this.ContentEncoding != null)
                response.ContentEncoding = this.ContentEncoding;
            if (this.Data == null)
                return;
            var scriptSerializer = JsonSerializer.Create(this.Settings);
            using (var sw = new StringWriter())
            {
                scriptSerializer.Serialize(sw, this.Data);
                response.Write(sw.ToString());
            }
        }
    }

    public class BaseController : Controller
    {
        public  tb_user UserInfo = null;
        public BaseController(bool isRequestUser=true)
        {
                if (isRequestUser)
                {
                    ResponseBase<PageInfoResponse<List<tb_user>>> list = PostToServer<PageInfoResponse<List<tb_user>>, LoadUserListRequest>(ClientProxy.LoadUserList_Url, new LoadUserListRequest());
                    if (list.Status != ResponesStatus.Success)
                    {
                        ViewBag.UserList = new List<tb_user>();
                    }
                    else
                    {
                        ViewBag.UserList = list.Data.aaData;
                    }
                }
            
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["LoginUserInfo"] == null)
            {
               filterContext.Result= LoginResult("");
               return;
            }
            else
            {
                UserInfo = filterContext.HttpContext.Session["LoginUserInfo"] as tb_user;
            }
            
            //base.OnActionExecuting(filterContext);
        }
       
        public virtual ActionResult LoginResult(string username)
        {
            return new RedirectResult("/Login/Index");
        }
        
        protected override void HandleUnknownAction(string actionName)
        {
            try
            {

                this.View(actionName).ExecuteResult(this.ControllerContext);

            }
            catch (InvalidOperationException ieox)
            {

                ViewData["error"] = "Unknown Action: \"" + Server.HtmlEncode(actionName) + "\"";

                ViewData["exMessage"] = ieox.Message;

                this.View("Error").ExecuteResult(this.ControllerContext);

            }
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
        public  ResponseBase<TResponse> PostToServer<TResponse, TRequest>(string url, TRequest request)
            where TResponse : class
            where TRequest : RequestBase
        {
            ResponseBase<TResponse> r = HttpClientHelper.PostResponse<ResponseBase<TResponse>>(url, JsonConvert.SerializeObject(request));
            return r;
        }
    }
}