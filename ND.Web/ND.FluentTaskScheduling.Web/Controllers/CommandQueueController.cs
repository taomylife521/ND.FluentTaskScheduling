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
    /// 命令队列
    /// </summary>
    public class CommandQueueController : BaseController
    {
        public CommandQueueController()
        {
            ViewBag.CommandList = new List<tb_commandlibdetail>();
            ResponseBase<PageInfoResponse<List<tb_commandlibdetail>>> commandlist = PostToServer<PageInfoResponse<List<tb_commandlibdetail>>, LoadWebCommandListRequest>(ClientProxy.LoadWebCommandList_Url, new LoadWebCommandListRequest() { iDisplayLength = 9999, iDisplayStart = 0, sEcho = 1, CommandId = -1, Source = Source.Web });
            if (commandlist.Status == ResponesStatus.Success)
            {
                ViewBag.CommandList = commandlist.Data.aaData;
            }

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
        // GET: CommandQueue
        public ActionResult Index(int nodeid = -1, int executestatus = -1, int commandqueueid = -1)
        {
            string daterange = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.CommandQueueCreateTimeRange = daterange;
            ViewBag.CommandId = "-1";
            ViewBag.ExecuteStatus = executestatus.ToString();
            ViewBag.NodeId = nodeid.ToString();
            ViewBag.TaskId = "-1";
            ViewBag.CommandQueueId = commandqueueid.ToString();

          
            //ResponseBase<LoadWebCommandQueueListResponse> tasklist = PostToServer<LoadWebCommandQueueListResponse, LoadWebCommandQueueListRequest>(ClientProxy.LoadWebCommandQueueList_Url, new LoadWebCommandQueueListRequest() { NodeId = -1, CommandId = -1, CommandQueueCreateTimeRange = daterange, CommandStatus = -1, LastMaxId = -1, Source = Source.Web, TaskId = -1 });
            //if (tasklist.Status != ResponesStatus.Success)
            //{
            //    return View(new List<WebCommandQueueListDto>());
            //}
            //return View(tasklist.Data.CommandQueueList);
            return View(new List<WebCommandQueueListDto>());
            
        }
         [HttpPost]
        public JsonResult Index(LoadWebCommandQueueListRequest req)
        {
           
            ViewBag.CommandQueueCreateTimeRange = req.CommandQueueCreateTimeRange;
            ViewBag.CommandId = req.CommandId.ToString();
            ViewBag.ExecuteStatus = req.CommandStatus.ToString();
            ViewBag.NodeId = req.NodeId.ToString();
            ViewBag.TaskId = req.TaskId.ToString();
            ViewBag.CommandQueueId = req.CommandQueueId.ToString();
            ResponseBase<PageInfoResponse<List<WebCommandQueueListDto>>> tasklist = PostToServer<PageInfoResponse<List<WebCommandQueueListDto>>, LoadWebCommandQueueListRequest>(ClientProxy.LoadWebCommandQueueList_Url, req);
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