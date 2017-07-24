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
    public class NodeController : BaseController
    {
        // GET: Node
        public ActionResult Index(int nodeid=-1)
        {
            ViewBag.NodeList = new List<NodeDetailDto>();
            ResponseBase<PageInfoResponse<List<NodeDetailDto>>> nodelist = PostToServer<PageInfoResponse<List<NodeDetailDto>>, LoadNodeListRequest>(ClientProxy.LoadNodeList_Url, new LoadNodeListRequest() { Source = Source.Web });
            if (nodelist.Status == ResponesStatus.Success)
            {
                ViewBag.NodeList = nodelist.Data.aaData;
            }
            string daterange = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.NodeId = nodeid.ToString();
            ViewBag.NodeRunStatus = "-1";
            ViewBag.ListenCommandQueueStatus = "-1";
            ViewBag.NodeCreateTimeRange = daterange;
            return View();
            
        }
        [HttpPost]
        public JsonResult Index(LoadNodeListRequest req)
        {
            ResponseBase<PageInfoResponse<List<NodeDetailDto>>> tasklist = PostToServer<PageInfoResponse<List<NodeDetailDto>>, LoadNodeListRequest>(ClientProxy.LoadNodeList_Url, req);
            if (tasklist.Status != ResponesStatus.Success)
            {
                return Json(new
                {
                    sEcho = 0,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<string>()
                });
            }
            return Json(tasklist.Data);

        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddNodeRequest req)
        {
            req.adminid =  UserInfo.id;
            req.alarmuserid=Request.Form["alarmuserid"].ToString();
            req.ifcheckstate = Request.Form["ifcheckstate"].ToString().ToLower() == "on" ? true : false;
            req.isenablealarm = Request.Form["isenablealarm"].ToString().ToLower() == "on" ? 1 : 0;
            //req.alarmtype =  Request.Form["isenablealarm"].ToString().ToLower() == "on"
            ResponseBase<AddNodeResponse> result = PostToServer<AddNodeResponse, AddNodeRequest>(ClientProxy.AddNode_Url, req);
            if(result.Status != ResponesStatus.Success)
            {
                return View();
            }
            return RedirectToAction("/index");
        }


        public ActionResult Edit(int nodeid)
        {
           // ViewBag.nodeid = nodeid;
            var nodereq = new LoadNodeDetailRequest() { NodeId=nodeid, Source = Source.Web };
            var r = PostToServer<LoadNodeDetailResponse, LoadNodeDetailRequest>(ClientProxy.LoadNodeDetail_Url, nodereq);
            if (r.Status != ResponesStatus.Success)
            {
                return View(new LoadNodeDetailResponse());
            }
            return View(r.Data);
        }

        [HttpPost]
        public ActionResult Edit(UpdateNodeInfoRequest model)
        {
            model.Source = Source.Web;
            model.ifcheckstate = Request.Form["ifcheckstate"] == null ? false : (Request.Form["ifcheckstate"].ToString().ToLower() == "on" ? true : false);
            model.alarmuserid = Request.Form["alarmuserid"] == null ? "" : Request.Form["alarmuserid"].ToString();
            model.isenablealarm = Request.Form["isenablealarm"] == null ? 0 : (Request.Form["isenablealarm"].ToString().ToLower() == "on" ? 1 : 0);
            model.adminid = UserInfo.id;

           
            // model.TaskId =int.Parse(ViewBag.taskid);
            // model.TaskVersionId = int.Parse(ViewBag.taskversionid);
            ResponseBase<EmptyResponse> result = PostToServer<EmptyResponse, UpdateNodeInfoRequest>(ClientProxy.UpdateNodeInfo_Url, model);
            if (result.Status != ResponesStatus.Success)
            {
                //ModelState.AddModelError("AddTaskFailed", tasklist.Msg);
                return View(new LoadNodeDetailResponse());

            }
            return RedirectToAction("/index");
        }
    }
}