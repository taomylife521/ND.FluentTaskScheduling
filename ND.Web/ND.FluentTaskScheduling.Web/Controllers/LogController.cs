using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using ND.FluentTaskScheduling.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace ND.FluentTaskScheduling.Web.Controllers
{
    public class LogController : BaseController
    {
        public LogController()
        {
            ViewBag.NodeList = new List<NodeDetailDto>();
            ResponseBase<PageInfoResponse<List<NodeDetailDto>>> nodelist = PostToServer<PageInfoResponse<List<NodeDetailDto>>, LoadNodeListRequest>(ClientProxy.LoadNodeList_Url, new LoadNodeListRequest() { Source = Source.Web });
            if (nodelist.Status == ResponesStatus.Success)
            {
                ViewBag.NodeList = nodelist.Data.aaData;
            }

           
        }

        // GET: Log
        public ActionResult TaskExcuteLog(int taskid = 0)
        {
            string daterange = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.TaskId = taskid.ToString();
            ViewBag.TaskLogCreateTimeRange = daterange;
            ViewBag.NodeId = "-1";
            ViewBag.TaskExecuteStatus = "-1";
            //ViewBag.TaskList = new List<TaskListDto>();
            ResponseBase<PageInfoResponse<List<TaskListDto>>> tasklist2 = PostToServer<PageInfoResponse<List<TaskListDto>>, LoadTaskListRequest>(ClientProxy.LoadTaskList_Url, new LoadTaskListRequest() { Source = Source.Web });
            if (tasklist2.Status == ResponesStatus.Success)
            {
                ViewBag.TaskList = tasklist2.Data.aaData;
            }
           
            return View();
        }

        [HttpPost]
        public JsonResult TaskExcuteLog(LoadTaskExecuteLogRequest req)
        {
            ViewBag.TaskId = req.TaskId.ToString();
            ViewBag.TaskLogCreateTimeRange = req.TaskLogCreateTimeRange.ToString();
            ViewBag.NodeId = req.NodeId.ToString();
            ViewBag.TaskExecuteStatus = req.TaskExecuteStatus.ToString();
            ResponseBase<PageInfoResponse<List<TaskLogListDto>>> tasklist = PostToServer<PageInfoResponse<List<TaskLogListDto>>, LoadTaskExecuteLogRequest>(ClientProxy.LoadTaskExecuteLogList_Url, req);
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


        public ActionResult CommandQueueExcuteLog(int commandqueueid = -1)
        {
            ViewBag.CommandList = new List<tb_commandlibdetail>();
            ResponseBase<PageInfoResponse<List<tb_commandlibdetail>>> commandlist = PostToServer<PageInfoResponse<List<tb_commandlibdetail>>, LoadWebCommandListRequest>(ClientProxy.LoadWebCommandList_Url, new LoadWebCommandListRequest() { iDisplayLength = 9999, iDisplayStart = 0, sEcho = 1, CommandId = -1, Source = Source.Web });
            if (commandlist.Status == ResponesStatus.Success)
            {
                ViewBag.CommandList = commandlist.Data.aaData;
            }
            

            string daterange = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.LogCreateTimeRange = daterange;
            ViewBag.NodeId = "-1";
            ViewBag.CommandQueueId = commandqueueid.ToString();
            ViewBag.ExecuteStatus = "-1";
            ViewBag.CommandId = "-1";
            ViewBag.CommandQueueId = commandqueueid;
           

            return View();

           
        }
        [HttpPost]
        public JsonResult CommandQueueExcuteLog([Form]LoadCommandQueueExecuteLogRequest req)
        {
            ViewBag.LogCreateTimeRange = req.LogCreateTimeRange.ToString();
            ViewBag.NodeId = req.NodeId.ToString();
            ViewBag.CommandQueueId = req.CommandQueueId.ToString();
            ViewBag.ExecuteStatus = req.ExecuteStatus.ToString();
            ViewBag.CommandId = req.CommandId.ToString();
            req.LogCreateTimeRange = req.CommandQueueId <= 0 ? req.LogCreateTimeRange : "1900-01-01/2099-12-30";
            ResponseBase<PageInfoResponse<List<CommandLogListDto>>> tasklist = PostToServer<PageInfoResponse<List<CommandLogListDto>>, LoadCommandQueueExecuteLogRequest>(ClientProxy.LoadCommandQueueExecuteLog_Url, req);// new LoadCommandQueueExecuteLogRequest() { CommandQueueId = commandqueueid, Source = Source.Web }
            if (tasklist.Status != ResponesStatus.Success)
            {
                return Json(new {
                    sEcho=0,
                    iTotalRecords=0,
                    iTotalDisplayRecords=0,
                    aaData=new List<string>()
                });
            }
            return Json(tasklist.Data);
        }


     
     


        public ActionResult NodeLog(int nodeid=0)
        {
            string daterange = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.NodeId = nodeid.ToString();
            ViewBag.NodeLogCreateTimeRange = daterange;
            ViewBag.NodeLogType = "-1";
            return View();
        }

        [HttpPost]
        public JsonResult NodeLog(LoadNodeLogRequest req)
        {
            ViewBag.NodeId = req.NodeId.ToString();
            ViewBag.NodeLogCreateTimeRange = req.NodeLogCreateTimeRange.ToString();
            ViewBag.NodeLogType = req.NodeLogType.ToString();
            ResponseBase<PageInfoResponse<List<tb_log>>> tasklist = PostToServer<PageInfoResponse<List<tb_log>>, LoadNodeLogRequest>(ClientProxy.LoadNodeLogList_Url, req);
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
    }
}