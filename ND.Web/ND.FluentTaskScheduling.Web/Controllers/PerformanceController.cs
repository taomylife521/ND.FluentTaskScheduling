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
    public class PerformanceController : BaseController
    {
        public PerformanceController()
        {
            ViewBag.NodeList = new List<NodeDetailDto>();
            ResponseBase<PageInfoResponse<List<NodeDetailDto>>> nodelist = PostToServer<PageInfoResponse<List<NodeDetailDto>>, LoadNodeListRequest>(ClientProxy.LoadNodeList_Url, new LoadNodeListRequest() { Source = Source.Web });
            if (nodelist.Status == ResponesStatus.Success)
            {
                ViewBag.NodeList = nodelist.Data.aaData;
            }
            ViewBag.TaskList = new List<TaskListDto>();
            ResponseBase<PageInfoResponse<List<TaskListDto>>> tasklist2 = PostToServer<PageInfoResponse<List<TaskListDto>>, LoadTaskListRequest>(ClientProxy.LoadTaskList_Url, new LoadTaskListRequest() { Source = Source.Web });
            if (tasklist2.Status == ResponesStatus.Success)
            {
                ViewBag.TaskList = tasklist2.Data.aaData;
            }
        }
        // GET: Performance
        public ActionResult Index(int nodeid=-1,int taskid=-1)
        {
            ResponseBase<PageInfoResponse<LoadPerformancelistResponse>> result = PostToServer<PageInfoResponse<LoadPerformancelistResponse>, LoadPerformancelistRequest>(ClientProxy.CheckUNameAndPwd_Url, new LoadPerformancelistRequest() { NodeId = nodeid,Source= Model.Source.Web,sEcho=1,iDisplayLength=10,iDisplayStart=1, TaskId = taskid });
            if(result.Status != ResponesStatus.Success)
            {
                return View(new LoadPerformancelistResponse());
            }
            return View(result.Data.aaData);
        }

        public ActionResult TaskIndex(int nodeid = -1, int taskid = -1)
        {
            ViewBag.NodeId = nodeid.ToString();
            ViewBag.TaskId = taskid.ToString();
            ResponseBase<PageInfoResponse<LoadPerformancelistResponse>> result = PostToServer<PageInfoResponse<LoadPerformancelistResponse>, LoadPerformancelistRequest>(ClientProxy.LoadPerformanceList_Url, new LoadPerformancelistRequest() { NodeId = nodeid, Source = Model.Source.Web, sEcho = 1, iDisplayLength = 10, iDisplayStart = 1,  TaskId = taskid });
            if (result.Status != ResponesStatus.Success)
            {
                return View(new LoadPerformancelistResponse());
            }
            return View(result.Data.aaData);
        }
        [HttpPost]
        public ActionResult TaskIndex(LoadPerformancelistRequest req)
        {
            ViewBag.NodeId = req.NodeId.ToString();
            ViewBag.TaskId = req.TaskId.ToString();
            ResponseBase<PageInfoResponse<LoadPerformancelistResponse>> result = PostToServer<PageInfoResponse<LoadPerformancelistResponse>, LoadPerformancelistRequest>(ClientProxy.LoadPerformanceList_Url, new LoadPerformancelistRequest() { NodeId = req.NodeId, Source = Model.Source.Web, sEcho = 1, iDisplayLength = 10, iDisplayStart = 1, TaskId = req.TaskId });
            if (result.Status != ResponesStatus.Success)
            {
                return View(new LoadPerformancelistResponse());
            }
            return View(result.Data.aaData);
        }

        public ActionResult NodeIndex(int nodeid = -1,int noderunstatus=-1)
        {
            ViewBag.NodeId = nodeid.ToString();
            ViewBag.NodeRunStatus = noderunstatus.ToString();
            ResponseBase<PageInfoResponse<LoadNodePerformancelistResponse>> result = PostToServer<PageInfoResponse<LoadNodePerformancelistResponse>, LoadNodePerformancelistRequest>(ClientProxy.LoadNodePerformanceList_Url, new LoadNodePerformancelistRequest() { NodeId = nodeid, Source = Model.Source.Web, sEcho = 1, iDisplayLength = 10, iDisplayStart = 1,NodeRunStatus=noderunstatus });
            if (result.Status != ResponesStatus.Success)
            {
                return View(new LoadNodePerformancelistResponse());
            }
            return View(result.Data.aaData);
        }

         [HttpPost]
        public ActionResult NodeIndex(LoadNodePerformancelistRequest req)
        {
            ViewBag.NodeId = req.NodeId.ToString();
            ViewBag.NodeRunStatus = req.NodeRunStatus.ToString();
            ResponseBase<PageInfoResponse<LoadNodePerformancelistResponse>> result = PostToServer<PageInfoResponse<LoadNodePerformancelistResponse>, LoadNodePerformancelistRequest>(ClientProxy.LoadNodePerformanceList_Url, new LoadNodePerformancelistRequest() { NodeId = req.NodeId, Source = Model.Source.Web, sEcho = 1, iDisplayLength = 10, iDisplayStart = 1, NodeRunStatus = req.NodeRunStatus });
            if (result.Status != ResponesStatus.Success)
            {
                return View(new LoadNodePerformancelistResponse());
            }
            return View(result.Data.aaData);
        }
    }
}