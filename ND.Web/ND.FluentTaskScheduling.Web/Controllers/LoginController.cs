using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using ND.FluentTaskScheduling.Web.App_Start;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ND.FluentTaskScheduling.Web.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()
        {
           // HttpContext.Session["LoginUserInfo"] = null;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            HttpContext.Session["LoginUserInfo"] = null;
            return RedirectToAction("/Index");
           
        }

        [HttpPost]
        public JsonResult Index(string username,string password)
        {
            ResponseBase<CheckUNameAndPwdResponse> result = PostToServer<CheckUNameAndPwdResponse, CheckUNameAndPwdRequest>(ClientProxy.CheckUNameAndPwd_Url, new CheckUNameAndPwdRequest() { Source = Source.Web, UserName = username, Password = password });
            if(result.Status == ResponesStatus.Success)//校验成功，则登录
            {
                //写入session
                HttpContext.Session["LoginUserInfo"] = result.Data.UserInfo;

            }
            return Json(result);
        }

        public ResponseBase<TResponse> PostToServer<TResponse, TRequest>(string url, TRequest request)
            where TResponse : class
            where TRequest : RequestBase
        {
            ResponseBase<TResponse> r = HttpClientHelper.PostResponse<ResponseBase<TResponse>>(url, JsonConvert.SerializeObject(request));
            return r;
        }
    }
}