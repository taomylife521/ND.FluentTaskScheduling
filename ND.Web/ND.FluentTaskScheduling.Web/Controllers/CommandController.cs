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
    /// 命令日志
    /// </summary>
    public class CommandController : BaseController
    {
        // GET: Command
        public ActionResult Index(int commandid=-1)
        {
            ViewBag.commandid = commandid;
           // ResponseBase<PageInfoResponse<List<tb_commandlibdetail>>> tasklist = PostToServer<PageInfoResponse<List<tb_commandlibdetail>>, LoadWebCommandListRequest>(ClientProxy.LoadWebCommandList_Url, new LoadWebCommandListRequest() { CommandId = commandid, iDisplayLength = 20, iDisplayStart = 0, sEcho = 1, Source = Source.Web });
            return View();
            
        }

        [HttpPost]
        public JsonResult Index(LoadWebCommandListRequest req)
        {

            ViewBag.commandid = req.CommandId.ToString();

            ResponseBase<PageInfoResponse<List<tb_commandlibdetail>>> list = PostToServer<PageInfoResponse<List<tb_commandlibdetail>>, LoadWebCommandListRequest>(ClientProxy.LoadWebCommandList_Url, new LoadWebCommandListRequest() { CommandId = req.CommandId, iDisplayLength = req.iDisplayLength, iDisplayStart = req.iDisplayStart, sEcho = req.sEcho, Source = Source.Web });
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
    }
}