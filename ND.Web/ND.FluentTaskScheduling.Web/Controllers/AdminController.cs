using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using ND.FluentTaskScheduling.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ND.FluentTaskScheduling.Web.Controllers
{
    /// <summary>
    /// 后台用户控制器
    /// </summary>
    public class AdminController : BaseController
    {
        public AdminController():base(false) { }
        // GET: Command
        public ActionResult Index(int adminid=-1)
        {
            ViewBag.adminid = adminid;
            ViewBag.UserCreateTimeRange = "";
            ViewBag.UserMobile = "";
            ViewBag.UserEmail = "";
            ViewBag.UserRealName = "";
           // ResponseBase<PageInfoResponse<List<tb_commandlibdetail>>> tasklist = PostToServer<PageInfoResponse<List<tb_commandlibdetail>>, LoadWebCommandListRequest>(ClientProxy.LoadWebCommandList_Url, new LoadWebCommandListRequest() { CommandId = commandid, iDisplayLength = 20, iDisplayStart = 0, sEcho = 1, Source = Source.Web });
            return View();
            
        }

        [HttpPost]
        public JsonResult Index(LoadUserListRequest req)
        {

            ViewBag.adminid = req.AdminId==null?"-1":req.AdminId.ToString();
            ViewBag.UserCreateTimeRange = req.UserCreateTimeRange == null ? "1900-01-01/2099-12-30" : req.UserCreateTimeRange.ToString();
            ViewBag.UserMobile = req.UserMobile == null ? "" : req.UserMobile.ToString();
            ViewBag.UserEmail = req.UserEmail == null ? "" : req.UserEmail.ToString();
            ViewBag.UserRealName = req.UserRealName==null?"":req.UserRealName.ToString();
            req.UserCreateTimeRange = req.UserCreateTimeRange == null ? "1900-01-01/2099-12-30" : req.UserCreateTimeRange;
            
            ResponseBase<PageInfoResponse<List<tb_user>>> list = PostToServer<PageInfoResponse<List<tb_user>>, LoadUserListRequest>(ClientProxy.LoadUserList_Url, req);
            if (list.Status != ResponesStatus.Success)
            {
                return Json(new
                {
                    sEcho = 0,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<string>()
                });
            }
            return Json(list.Data);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddUserRequest req)
        {
            req.AdminId = UserInfo.id;
            ResponseBase<AddUserResponse> result = PostToServer<AddUserResponse, AddUserRequest>(ClientProxy.AddUser_Url, req);
            if (result.Status != ResponesStatus.Success)
            {
                return View();
            }
            return RedirectToAction("/index");
        }


        public ActionResult Edit(int userid=-1)
        {
            ViewBag.userid = userid.ToString();
            var req = new LoadUserDetailRequest() { UserId = userid, Source = Source.Web };
            var r = PostToServer<LoadUserDetailResponse, LoadUserDetailRequest>(ClientProxy.LoadUserDetail_Url, req);
            if (r.Status != ResponesStatus.Success)
            {
                return View(new LoadUserDetailResponse());
            }
            return View(r.Data);
        }

        [HttpPost]
        public ActionResult Edit(UpdateUserRequest model)
        {
            model.Source = Source.Web;
            model.AdminId = UserInfo.id;
            var r = PostToServer<EmptyResponse, UpdateUserRequest>(ClientProxy.UpdateUser_Url, model);
            if (r.Status != ResponesStatus.Success)
            {
                return View(new LoadNodeDetailResponse());
            }
            return RedirectToAction("/index");
        }
    }
}