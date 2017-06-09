using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using ND.FluentTaskScheduling.TaskInterface;
using ND.FluentTaskScheduling.Web.App_Start;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ND.FluentTaskScheduling.Web.Controllers
{
    public class TaskController : BaseController
    {
        public TaskController()
        {
            ViewBag.NodeList = new List<NodeDetailDto>();
            ResponseBase<PageInfoResponse<List<NodeDetailDto>>> nodelist = PostToServer<PageInfoResponse<List<NodeDetailDto>>, LoadNodeListRequest>(ClientProxy.LoadNodeList_Url, new LoadNodeListRequest() { Source = Source.Web });
            if (nodelist.Status == ResponesStatus.Success)
            {
                ViewBag.NodeList = nodelist.Data.aaData;
            }
        }
        // GET: Task
        public ActionResult Index(int taskid=-1,int nodeid=-1)
        {
            ViewBag.TaskList = new List<TaskListDto>();
            ResponseBase<PageInfoResponse<List<TaskListDto>>> tasklist2 = PostToServer<PageInfoResponse<List<TaskListDto>>, LoadTaskListRequest>(ClientProxy.LoadTaskList_Url, new LoadTaskListRequest() { Source = Source.Web });
            if (tasklist2.Status == ResponesStatus.Success)
            {
                ViewBag.TaskList = tasklist2.Data.aaData;
            }
            string daterange = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToString("yyyy-MM-dd");
            ViewBag.NodeId = nodeid.ToString();
            ViewBag.TaskCreateTimeRange = daterange;
            ViewBag.TaskName = "";
            ViewBag.TaskExecuteStatus = "-1";
            ViewBag.TaskSchduleStatus = "-1";
            ViewBag.TaskId = taskid;
            ViewBag.TaskType = "-1";
            return View();
            //LoadTaskListRequest request = new LoadTaskListRequest()
            //{
            //    NodeId=int.Parse(ViewBag.NodeId),
            //    Source= Source.Web,
            //    TaskCreateTimeRange = ViewBag.TaskCreateTimeRange,
            //    TaskName=ViewBag.TaskName,
            //    TaskExecuteStatus = int.Parse(ViewBag.TaskExecuteStatus),
            //    TaskSchduleStatus = int.Parse(ViewBag.TaskSchduleStatus)
            //};
            //ResponseBase<LoadTaskListResponse> tasklist = PostToServer<LoadTaskListResponse, LoadTaskListRequest>(ClientProxy.LoadTaskList_Url, request);
            //if(tasklist.Status != ResponesStatus.Success)
            //{
            //    return View(new List<TaskListDto>());
            //}
            //return View(tasklist.Data.TaskList);
        }

        [HttpPost]
        public JsonResult Index(LoadTaskListRequest request)
        {
           
            ViewBag.NodeId = request.NodeId.ToString();
            ViewBag.TaskCreateTimeRange = request.TaskCreateTimeRange.ToString();
            ViewBag.TaskName = request.TaskName == null?"":request.TaskName.ToString();
            ViewBag.TaskExecuteStatus = request.TaskExecuteStatus.ToString();
            ViewBag.TaskSchduleStatus = request.TaskSchduleStatus.ToString();
            ViewBag.TaskId = request.TaskId.ToString();
            ViewBag.TaskType = request.TaskType.ToString();
            request.TaskCreateTimeRange = request.TaskId <= 0 ? request.TaskCreateTimeRange : "1900-01-01/2099-12-30";

            ResponseBase<PageInfoResponse<List<TaskListDto>>> tasklist = PostToServer<PageInfoResponse<List<TaskListDto>>, LoadTaskListRequest>(ClientProxy.LoadTaskList_Url, request);
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
        public ActionResult Add(HttpPostedFileBase TaskDllStream, AddTaskRequest model)
        {
           
            model.Source = Source.Web;
            if (TaskDllStream != null)
            {
               
                Stream dll = TaskDllStream.InputStream;
                byte[] dllbyte = new byte[dll.Length];
                dll.Read(dllbyte, 0, Convert.ToInt32(dll.Length));
                model.TaskDll = dllbyte;
            }
            model.AdminId =  UserInfo.id.ToString();
            model.AlarmUserId = Request.Form["AlarmUserId"] == null ? "" : Request.Form["AlarmUserId"].ToString();
            model.IsEnabledAlarm = Request.Form["IsEnabledAlarm"] == null ? 0 : (Request.Form["IsEnabledAlarm"].ToString().ToLower() == "on" ? 1 : 0);
            ResponseBase<AddTaskResponse> tasklist = PostToServer<AddTaskResponse, AddTaskRequest>(ClientProxy.AddTask_Url, model);
            if (tasklist.Status != ResponesStatus.Success)
            {
                return View(new List<tb_task>());
            }
            return RedirectToAction("index");
        }

        public ActionResult Edit(int taskid=0,int taskversionid=0)
        {
            ViewBag.taskid = taskid;
            ViewBag.taskversionid = taskversionid;
            var taskversionreq = new LoadTaskVersionRequest() { TaskId = taskid, TaskVersionId = taskversionid, Source = Source.Web };
            var r = PostToServer<LoadTaskVersionResponse, LoadTaskVersionRequest>(ClientProxy.TaskVersionDetail_Url, taskversionreq);
                if (r.Status != ResponesStatus.Success)
                {
                    return View(new LoadTaskVersionResponse());
                }
            return View(r.Data);
        }

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase TaskDllStream, UpdateTaskInfoRequest model)
        {
            model.Source = Source.Web;
            model.AlarmUserId = Request.Form["AlarmUserId"]==null?"":Request.Form["AlarmUserId"].ToString();
            model.IsEnabledAlarm = Request.Form["IsEnabledAlarm"] == null ? 0 : (Request.Form["IsEnabledAlarm"].ToString().ToLower() == "on" ? 1 : 0);
           
            if (TaskDllStream != null)
            {
                string filename = TaskDllStream.FileName;
                Stream dll = TaskDllStream.InputStream;
                byte[] dllbyte = new byte[dll.Length];
                dll.Read(dllbyte, 0, Convert.ToInt32(dll.Length));
                model.TaskDll = dllbyte;
                
            }
           // model.TaskId =int.Parse(ViewBag.taskid);
           // model.TaskVersionId = int.Parse(ViewBag.taskversionid);
            ResponseBase<UpdateTaskInfoResponse> tasklist = PostToServer<UpdateTaskInfoResponse, UpdateTaskInfoRequest>(ClientProxy.UpdateTask_Url, model);
            if (tasklist.Status != ResponesStatus.Success)
            {
                //ModelState.AddModelError("AddTaskFailed", tasklist.Msg);
                return View(new List<tb_task>());
              
            }
            return RedirectToAction("/index");
        }

        #region 更新任务状态
        [HttpPost]
        public JsonResult ChangeTaskStatus(string nodeid, string taskid, string taskversionid, string commandname)
        {
           
            try
            {
                //添加启动命令 
                AddCommandQueueItemRequest model = new AddCommandQueueItemRequest()
                {
                    AdminId = "0",
                    CommandName = (CommandName)(int.Parse(commandname)),
                    CommandParam = "",
                    NodeId = nodeid,
                    TaskId = taskid,
                    TaskVersionId = taskversionid,
                };
                ResponseBase<AddCommandQueueItemResponse> rep = PostToServer<AddCommandQueueItemResponse, AddCommandQueueItemRequest>(ClientProxy.AddCommandQueue_Url, model);

                
               // var ret = JsonConvert.SerializeObject(rep, setting);
               // Response.Write(ret);
              //  return Json(rep, JsonRequestBehavior.AllowGet);
               // return Json(rep);
                return Json(rep);
            }
            catch(Exception ex)
            {
                var rep2=new ResponseBase<AddCommandQueueItemResponse>() { Status = ResponesStatus.Exception, Msg = ex.Message };
                return Json(rep2);
               // var ret2 = JsonConvert.SerializeObject(rep2, setting);
               // var ret2 = JsonConvert.SerializeObject(rep2, setting);
               // return Json(rep2,JsonRequestBehavior.AllowGet);
                //Response.Write(ret2);
                //return Json(rep2, JsonRequestBehavior.AllowGet);
            }
        } 
        #endregion
    }
}