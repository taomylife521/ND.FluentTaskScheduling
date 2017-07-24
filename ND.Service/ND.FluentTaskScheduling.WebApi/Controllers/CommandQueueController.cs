using ND.FluentTaskScheduling.Domain.interfacelayer;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace ND.FluentTaskScheduling.WebApi.Controllers
{
     [RoutePrefix("api/commandqueue")]
    public class CommandQueueController : BaseController
    {
        ICommandQueueRepository cmdqueuerespository;
        ICommandLibDetailRepository cmdRep;

        ICommandLogRepository cmdLogRep;
        ITaskRepository taskRep;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public CommandQueueController( ITaskRepository taskrepository, ICommandLogRepository cmdLogrepository,ICommandQueueRepository rep,ICommandLibDetailRepository cmdrespository,  INodeRepository nodeRep, IUserRepository userRep)
            : base(nodeRep, userRep)
          {
              cmdqueuerespository = rep;
              cmdRep = cmdrespository;
              taskRep = taskrepository;
              cmdLogRep = cmdLogrepository;
          }
        #region 读取命令队列列表
        /// <summary>
        /// 读取命令队列列表
        /// </summary>
        /// <param name="req">读取命令队列列表请求</param>
        /// <returns></returns>
        [ResponseType(typeof(ResponseBase<LoadCommandQueueListResponse>))]
        [HttpPost, Route("loadcommandqueuelist")]
        public ResponseBase<LoadCommandQueueListResponse> LoadCommandQueueList(LoadCommandQueueListRequest req)
        {
            try
            {
               

               int nodeid = req.NodeId;
                 var node = noderepository.FindSingle(x => x.id == nodeid);
                    if (node == null)
                    {
                        return ResponseToClient<LoadCommandQueueListResponse>(ResponesStatus.Failed, "当前节点" + req.NodeId + "不存在库中!");
                    }
                   // node.lastmaxid = req.LastMaxId;
                   // node.lastrefreshcommandqueuetime = DateTime.Now;
                    try
                    {
                        noderepository.UpdateNodeById(node.id, new Dictionary<string, string>() { { "lastmaxid", req.LastMaxId.ToString() }, { "lastrefreshcommandqueuetime", DateTime.Now.ToString() } });//, { "refreshcommandqueuestatus", ((int)RefreshCommandQueueStatus.NoRefresh).ToString() }
                       // noderepository.Update(node);
                    }
                    catch(Exception ex)
                    {
                        log.Error("更新节点("+node.id.ToString()+")上次获取的最大命令队列编号("+req.LastMaxId.ToString()+")异常:"+JsonConvert.SerializeObject(ex));
                    }
                int queueStatus = (int)req.CommandStatus;
                List<tb_commandqueue> cmdqueuelist = cmdqueuerespository.Find(x => x.nodeid == (nodeid<=0 ?x.nodeid:nodeid) && ((x.id > req.LastMaxId && x.commandstate == queueStatus) || x.commandstate ==(int)ExecuteStatus.NoExecute)).ToList().Take(node.maxrefreshcommandqueuecount).ToList();
                if (cmdqueuelist.Count <= 0)
                {
                    return ResponseToClient<LoadCommandQueueListResponse>(ResponesStatus.Failed, "当前没有分配给该节点的命令");
                }

                return ResponseToClient<LoadCommandQueueListResponse>(ResponesStatus.Success, "", new LoadCommandQueueListResponse() { CommandQueueList = cmdqueuelist });
            }
            catch (Exception ex)
            {
                return ResponseToClient<LoadCommandQueueListResponse>(ResponesStatus.Exception, "刷新命令队列异常:" + JsonConvert.SerializeObject(ex));
            }
        } 
        #endregion

        #region 读取Web命令队列列表
        /// <summary>
        /// 读取命令队列列表
        /// </summary>
        /// <param name="req">读取命令队列列表请求</param>
        /// <returns></returns>
        [ResponseType(typeof(ResponseBase<PageInfoResponse<List<WebCommandQueueListDto>>>))]
        [HttpPost, Route("loadwebcommandqueuelist")]
        public ResponseBase<PageInfoResponse<List<WebCommandQueueListDto>>> LoadWebCommandQueueList(LoadWebCommandQueueListRequest req)
        {
            try
            {



                DateTime starttime = Convert.ToDateTime(req.CommandQueueCreateTimeRange.Split('/')[0]);
                DateTime endtime = Convert.ToDateTime(req.CommandQueueCreateTimeRange.Split('/')[1]).AddDays(1);
                 int pageIndex = (req.iDisplayStart / req.iDisplayLength) + 1;
                 int totalCount = 0;
                 List<tb_commandqueue> cmdqueuelist = cmdqueuerespository.Find(out totalCount, pageIndex, req.iDisplayLength, x => x.createtime.ToString(), x =>
                    x.nodeid == (req.NodeId <= 0 ? x.nodeid : req.NodeId) 
                    && x.id > req.LastMaxId
                    && x.commandstate == (req.CommandStatus < 0 ? x.commandstate : req.CommandStatus)
                    && x.createtime>= starttime && x.createtime<endtime
                    && x.commanddetailid ==(req.CommandId<=0?x.commanddetailid:req.CommandId)
                    && x.taskid ==(req.TaskId <= 0 ?x.taskid:req.TaskId)
                    && x.id ==(req.CommandQueueId<=0?x.id:req.CommandQueueId)
                    ).ToList();
                if (cmdqueuelist.Count <= 0)
                {
                    return ResponseToClient<PageInfoResponse<List<WebCommandQueueListDto>>>(ResponesStatus.Failed, "命令列表为空");
                }
                
                List<WebCommandQueueListDto> dto = new List<WebCommandQueueListDto>();
                cmdqueuelist.ForEach(x =>
                {
                    dto.Add(new WebCommandQueueListDto()
                    {
                        CommandQueueDetail=x,
                        NodeDetail=noderepository.FindSingle(m=>m.id == x.nodeid),
                        TaskDetail = taskRep.FindSingle(n=>n.id == x.taskid),
                        CommandLog = ((ExecuteStatus)x.commandstate == ExecuteStatus.ExecuteFailed || (ExecuteStatus)x.commandstate == ExecuteStatus.ExecuteSucess) ? cmdLogRep.FindSingle(k=>k.commandqueueid == x.id):null,
                    });
                });
               // LoadWebCommandQueueListResponse adata = new LoadWebCommandQueueListResponse() { CommandQueueList = dto.OrderBy(x => x.CommandQueueDetail.id).ToList() };
                return ResponseToClient<PageInfoResponse<List<WebCommandQueueListDto>>>(ResponesStatus.Success, "", new PageInfoResponse<List<WebCommandQueueListDto>>() { aaData = dto, iTotalDisplayRecords = totalCount, iTotalRecords = totalCount, sEcho = req.sEcho });
            }
            catch (Exception ex)
            {
                return ResponseToClient<PageInfoResponse<List<WebCommandQueueListDto>>>(ResponesStatus.Exception, "刷新命令队列异常:" + JsonConvert.SerializeObject(ex));
            }
        }
        #endregion

        #region 添加命令队列项
        /// <summary>
        /// 添加命令队列项
        /// </summary>
        /// <param name="req">添加命令队列项请求</param>
        /// <returns></returns>
        [ResponseType(typeof(ResponseBase<AddCommandQueueItemResponse>))]
        [HttpPost, Route("addcommandqueueitem")]
        public ResponseBase<AddCommandQueueItemResponse> AddCommandQueueItem(AddCommandQueueItemRequest req)
        {
            try
            {
                int nid=int.Parse(req.NodeId);
                var node = noderepository.FindSingle(x => x.id == nid);
                if (node == null)
                {
                    return ResponseToClient<AddCommandQueueItemResponse>(ResponesStatus.Failed, "当前节点" + req.NodeId + "不存在库中!");
                }
                if(node.nodestatus == (int)NodeStatus.NoRun)
                {
                    return ResponseToClient<AddCommandQueueItemResponse>(ResponesStatus.Failed, "当前节点未启动,暂不能执行此操作!");
                }
                string cmdName = req.CommandName.ToString();
                var commandDetail = cmdRep.FindSingle(x => x.commandmainclassname == cmdName && x.isdel == 0);
                if(commandDetail == null)
                {
                    return ResponseToClient<AddCommandQueueItemResponse>(ResponesStatus.Failed, "未知的命令操作！");
                }
               int r= cmdqueuerespository.Add(new tb_commandqueue()
                {
                     commanddetailid=commandDetail.id,
                     commandmainclassname=commandDetail.commandmainclassname,
                     commandparams=string.IsNullOrEmpty(req.CommandParam) ? "":req.CommandParam,
                     commandstate = (int)ExecuteStatus.NoExecute,
                     createby=int.Parse(req.AdminId),
                     createtime=DateTime.Now,
                     failedcount=0,
                     nodeid=int.Parse(req.NodeId),
                     isdel=0,
                     taskid=int.Parse(req.TaskId),
                     taskversionid=int.Parse(req.TaskVersionId)

                });
                if(r<=0)
                {
                    return ResponseToClient<AddCommandQueueItemResponse>(ResponesStatus.Failed, "执行失败");
                }
                if (!string.IsNullOrEmpty(req.TaskId))//如果task不为空,说明是针对任务的命令
                {
                    int tid = int.Parse(req.TaskId);
                    var task = taskRep.FindSingle(x => x.id == tid);
                    if(req.CommandName == CommandName.StartTaskCommand)
                    {
                        task.taskschedulestatus = (int)TaskScheduleStatus.WaitSchedule;
                        taskRep.Update(task);
                    }
                }

                return ResponseToClient<AddCommandQueueItemResponse>(ResponesStatus.Success, "");
            }
            catch (Exception ex)
            {
                return ResponseToClient<AddCommandQueueItemResponse>(ResponesStatus.Exception, "添加命令队列项异常:" + JsonConvert.SerializeObject(ex));
            }
        }

        
        #endregion


        #region MyRegion
        /// <summary>
        /// 更新节点对应状态
        /// </summary>
        /// <param name="req">载入配置信息请求类</param>
        /// <remarks>配置信息</remarks>
        /// <returns></returns>
        // [SignAuthorize]
        [ResponseType(typeof(ResponseBase<EmptyResponse>))]
        [HttpPost, Route("updatecommandqueuestatus")]
        public ResponseBase<EmptyResponse> UpdateCommandQueueStatus(UpdateCommandQueueStatusRequest req)
        {

            var node = noderepository.FindSingle(x => x.id == req.NodeId);
            if (node == null)
            {
                return ResponseToClient<EmptyResponse>(ResponesStatus.Failed, "当前节点" + req.NodeId + "不存在库中!");
            }

            tb_commandqueue cmdqueue = cmdqueuerespository.FindSingle(x => x.id == req.CommandQueueId && x.nodeid == req.NodeId);
            if (cmdqueue == null)
            {
                return ResponseToClient<EmptyResponse>(ResponesStatus.Failed, "当前任务队列不存在该id:" + req.CommandQueueId + ",NodeId=" + req.NodeId);
            }
            cmdqueue.commandstate = (int)req.CommandStatus;
            cmdqueuerespository.Update(cmdqueue);


            return ResponseToClient<EmptyResponse>(ResponesStatus.Success, "");
        } 
        #endregion
    }
}
