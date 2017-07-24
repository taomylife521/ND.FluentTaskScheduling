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
    public class NodeMonitorController : BaseController
    {
        public NodeMonitorController()
        {
            ViewBag.NodeList = new List<NodeDetailDto>();
            ResponseBase<PageInfoResponse<List<NodeDetailDto>>> nodelist = PostToServer<PageInfoResponse<List<NodeDetailDto>>, LoadNodeListRequest>(ClientProxy.LoadNodeList_Url, new LoadNodeListRequest() { Source = Source.Web });
            if (nodelist.Status == ResponesStatus.Success)
            {
                ViewBag.NodeList = nodelist.Data.aaData;
            }
        }
        // GET: NodeMonitor
        public ActionResult Index(int nodeid = -1)
        {
           
            ViewBag.NodeId = nodeid.ToString();
            ViewBag.MonitorStatus = "-1";
            return View();
        }
        [HttpPost]
        public JsonResult Index(LoadNodeMonitorListRequest req)
        {
            ViewBag.NodeId = req.NodeId.ToString();
            ViewBag.MonitorStatus = req.MonitorStatus.ToString();
            ResponseBase<PageInfoResponse<List<tb_nodemonitor>>> nodelist = PostToServer<PageInfoResponse<List<tb_nodemonitor>>, LoadNodeMonitorListRequest>(ClientProxy.LoadNodeMonitorList_Url, req);
            if (nodelist.Status != ResponesStatus.Success)
            {
                return Json(new
                {
                    sEcho = 0,
                    iTotalRecords = 0,
                    iTotalDisplayRecords = 0,
                    aaData = new List<string>()
                });
            }

            return Json(nodelist.Data);
        }
    }
}