using ND.FluentTaskScheduling.Domain.interfacelayer;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.TaskInterface;
//**********************************************************************
//
// 文件名称(File Name)：NodeController.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 15:51:20         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 15:51:20          
//             修改理由：         
//**********************************************************************

namespace ND.FluentTaskScheduling.WebApi.Controllers
{
      [RoutePrefix("api/node")]
    public class NodeController:BaseController
    {
          //public INodeRepository repository;
          //public IUserRepository userrepository;
          //public NodeController(INodeRepository rep,IUserRepository userRep)
          //{
          //    //repository = rep;
          //    //userrepository = userRep;
          //}
          public ICommandQueueRepository commandQueueRep;
          public ITaskLogRepository tasklogRep;
          public ITaskRepository taskRep;
          public ITaskVersionRepository taskversionRep;
          public INodeMonitorRepository nodemonitorRep;
          public NodeController(INodeMonitorRepository monitorrepository,ITaskRepository taskrepository, ITaskVersionRepository taskversionrepository, ICommandQueueRepository commandqueuerepository, ITaskLogRepository tasklogrepository, INodeRepository nodeRep, IUserRepository userRep)
              : base(nodeRep, userRep)
          {
              commandQueueRep = commandqueuerepository;
              tasklogRep = tasklogrepository;
              taskRep = taskrepository;
              taskversionRep = taskversionrepository;
              nodemonitorRep = monitorrepository;
          }
        #region 载入节点配置信息
          /// <summary>
          /// 载入节点配置信息
          /// </summary>
          /// <param name="req">载入配置信息请求类</param>
          /// <remarks>配置信息</remarks>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<LoadNodeConfigResponse>))]
          [HttpPost, Route("loadnodeconfig")]
          public ResponseBase<LoadNodeConfigResponse> LoadNodeConfig(LoadNodeConfigRequest req)
          {
              int nodeId = int.Parse(req.NodeId);
              var node = noderepository.FindSingle(x => x.id == nodeId);
              if (node == null)
              {
                  return ResponseToClient<LoadNodeConfigResponse>(ResponesStatus.Failed, "当前节点" + req.NodeId + "不存在库中!");
              }
              List<int> useridlist = node.alarmuserid.ConvertToList();
              List<string> emaillist = new List<string>();
              userrepository.Find(x => useridlist.Contains(x.id)).ToList().ForEach(x =>
              {
                  if (node.alarmtype == (int)AlarmType.Email)
                  {
                      emaillist.Add(x.useremail);
                  }
                  else if (node.alarmtype == (int)AlarmType.SMS)
                  {
                      emaillist.Add(x.usermobile);
                  }
              });
              LoadNodeConfigResponse data = new LoadNodeConfigResponse() { Node = node, AlarmPerson = string.Join(",",emaillist) };
              return ResponseToClient<LoadNodeConfigResponse>(ResponesStatus.Success, "", data);
          } 
          #endregion

        #region 载入节点列表
          /// <summary>
          /// 载入节点列表
          /// </summary>
          /// <param name="req">载入节点列表请求类</param>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<PageInfoResponse<List<NodeDetailDto>>>))]
          [HttpPost, Route("loadnodelist")]
          public ResponseBase<PageInfoResponse<List<NodeDetailDto>>> LoadNodeList(LoadNodeListRequest req)
          {
              try
              {
                  DateTime starttime = Convert.ToDateTime(req.NodeCreateTimeRange.Split('/')[0]);
                  DateTime endtime = Convert.ToDateTime(req.NodeCreateTimeRange.Split('/')[1]).AddDays(1);
                  int pageIndex = (req.iDisplayStart / req.iDisplayLength) + 1;
                  int totalCount = 0;
                  var node = noderepository.Find(out totalCount, pageIndex, req.iDisplayLength, x => x.id.ToString(), x =>
                      x.id == (req.NodeId <= 0 ? x.id : req.NodeId)
                     && x.refreshcommandqueuestatus == (req.ListenCommandQueueStatus < 0 ? x.refreshcommandqueuestatus : req.ListenCommandQueueStatus)
                     && x.nodestatus == (req.NodeRunStatus < 0 ? x.nodestatus : req.NodeRunStatus)
                     && x.createtime >= starttime && x.createtime < endtime
                     && x.createby==(req.CreateBy<=0?x.createby:req.CreateBy)
                     && x.alarmtype == (req.AlarmType<0?x.alarmtype:req.AlarmType)
                      ).ToList();
                  if (node == null || node.Count <= 0)
                  {
                      return ResponseToClient<PageInfoResponse<List<NodeDetailDto>>>(ResponesStatus.Failed, "当前未查到任何节点");
                  }
                  List<NodeDetailDto> nodelist = new List<NodeDetailDto>();
                  //LoadNodeListResponse data = new LoadNodeListResponse() { NodeList=nodelist };
                  node.ForEach(x =>
                  {
                      NodeDetailDto detail = new NodeDetailDto();
                      detail.TotalCommandCount = commandQueueRep.Find(m => m.nodeid == x.id).ToList().Count;//命令总数量
                      int waitExecute = (int)ExecuteStatus.NoExecute;
                      // int execute=(int)ExecuteStatus.NoExecute;
                      detail.WaitCommandCount = commandQueueRep.Find(m => m.nodeid == x.id && m.commandstate == waitExecute).ToList().Count;//命令总数量
                      detail.TotalTaskCount = taskversionRep.Find(m => m.nodeid == x.id).Select(m => m.taskid).Distinct().Count();
                      nodelist.Add(detail);
                      detail.NodeDetail = x;
                      //detail.RunningTaskCount = tasklogRep.Find(m => m.nodeid == x.id && m.taskstatus ==).Select(m => m.taskid).Distinct().Count();
                  });
                  //data.NodeList = nodelist;
                  return ResponseToClient<PageInfoResponse<List<NodeDetailDto>>>(ResponesStatus.Success, "", new PageInfoResponse<List<NodeDetailDto>>() { aaData = nodelist, iTotalDisplayRecords = totalCount, iTotalRecords = totalCount, sEcho = req.sEcho });
              }
              catch(Exception ex)
              {
                  return ResponseToClient<PageInfoResponse<List<NodeDetailDto>>>(ResponesStatus.Exception, ex.Message );
              }
          }
          #endregion

        #region 载入节点详情
          /// <summary>
          /// 载入节点详情
          /// </summary>
          /// <param name="req">载入节点详情请求类</param>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<LoadNodeDetailResponse>))]
          [HttpPost, Route("loadnodedetail")]
          public ResponseBase<LoadNodeDetailResponse> LoadNodeDetail(LoadNodeDetailRequest req)
          {
              try
              {
                
                  var node = noderepository.FindSingle(x =>x.id == req.NodeId);
                  if (node == null)
                  {
                      return ResponseToClient<LoadNodeDetailResponse>(ResponesStatus.Failed, "当前未查到任何节点");
                  }
                  return ResponseToClient<LoadNodeDetailResponse>(ResponesStatus.Success, "", new LoadNodeDetailResponse() { NodeDetail=node });
              }
              catch (Exception ex)
              {
                  return ResponseToClient<LoadNodeDetailResponse>(ResponesStatus.Exception, ex.Message);
              }
          }
          #endregion

        #region 添加节点
          /// <summary>
          /// 添加节点
          /// </summary>
          /// <param name="req">添加节点请求类</param>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<AddNodeResponse>))]
          [HttpPost, Route("addnode")]
          public ResponseBase<AddNodeResponse> AddNode(AddNodeRequest req)
          {
              var node=noderepository.FindSingle(x => x.nodename == req.nodename && x.isdel==0);
              if(node != null)
              {
                  return ResponseToClient<AddNodeResponse>(ResponesStatus.Failed, "该节点名称已存在");
              }
              DateTime dt = DateTime.Now;
              noderepository.Add(new tb_node()
              {
                  alarmtype=req.alarmtype,
                  alarmuserid=req.alarmuserid,
                  nodediscription=req.nodediscription,
                  createby = req.adminid,
                  createtime=dt,
                  ifcheckstate=req.ifcheckstate,
                  isdel=0,
                  isenablealarm=req.isenablealarm,
                  maxrefreshcommandqueuecount=req.maxrefreshcommandqueuecount,
                  nodelastmodifytime=dt,
                  nodeip=req.nodeip,
                  nodename=req.nodename,
                  nodestatus =(int)NodeStatus.NoRun,
                  refreshcommandqueuestatus = (int)RefreshCommandQueueStatus.NoRefresh
              });
              return ResponseToClient<AddNodeResponse>(ResponesStatus.Success, "");
          }
        #endregion

        #region 更新节点
          /// <summary>
          /// 更新节点
          /// </summary>
          /// <param name="req">更新节点请求类</param>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<EmptyResponse>))]
          [HttpPost, Route("updatenodeinfo")]
          public ResponseBase<EmptyResponse> UpdateNodeInfo(UpdateNodeInfoRequest req)
          {
              
               var node = noderepository.FindSingle(x => x.id == req.nodeid && x.isdel == 0);
               if (node == null)
               {
                   return ResponseToClient<EmptyResponse>(ResponesStatus.Failed, "该节点已不存在");
               }
              DateTime dt = DateTime.Now;
              node.ifcheckstate = req.ifcheckstate;
              node.isenablealarm = req.isenablealarm;
              node.maxrefreshcommandqueuecount = req.maxrefreshcommandqueuecount;
              node.nodediscription = string.IsNullOrEmpty(req.nodediscription)?"":req.nodediscription;
              node.nodeip = req.nodeip;
              node.nodelastmodifytime = dt;
              node.nodename = req.nodename;
              node.alarmtype = req.alarmtype;
              node.alarmuserid = req.alarmuserid;
              noderepository.Update(node);
              return ResponseToClient<EmptyResponse>(ResponesStatus.Success, "");
          }
          #endregion


        #region 更新节点刷新命令状态
        /// <summary>
        /// 更新节点刷新命令状态
        /// </summary>
        /// <param name="req">更新节点刷新命令请求</param>
        /// <remarks></remarks>
        /// <returns></returns>
        // [SignAuthorize]
        [ResponseType(typeof(ResponseBase<EmptyResponse>))]
        [HttpPost, Route("updatenoderefreshcmdqueuestatus")]
          public ResponseBase<EmptyResponse> UpdateNodeRefreshCommandQueueStatus(UpdateNodeRefreshCommandQueueStatusRequest req)
        {
            var node = noderepository.FindSingle(x => x.id == req.NodeId);
            if (node == null)
            {
                return ResponseToClient<EmptyResponse>(ResponesStatus.Failed, "当前节点" + req.NodeId + "不存在库中!");
            }
            var nodeModel = noderepository.FindSingle(x => x.id == req.NodeId);
            nodeModel.refreshcommandqueuestatus = (int)req.RefreshStatus;
            Dictionary<string, string> dic=new Dictionary<string, string>() {  { "refreshcommandqueuestatus", ((int)req.RefreshStatus).ToString() } };
            if(req.RefreshStatus == RefreshCommandQueueStatus.Refreshing)
            {
                if (!string.IsNullOrEmpty(req.MonitorClassName))//如果是监控过来的则更新监控插件
                {
                    UpdateNodeMonitor(new UpdateNodeMonitorRequest() { MonitorClassName = req.MonitorClassName, MonitorStatus = (int)MonitorStatus.Monitoring, NodeId = req.NodeId, Source = req.Source });
                }
               dic.Add("lastrefreshcommandqueuetime",DateTime.Now.AddSeconds(10).ToString("yyyy-MM-dd HH:mm:ss"));
            }
            noderepository.UpdateNodeById(nodeModel.id,dic);//, { "refreshcommandqueuestatus", ((int)RefreshCommandQueueStatus.NoRefresh).ToString() }
            return ResponseToClient<EmptyResponse>(ResponesStatus.Success, "");
        } 
        #endregion


        #region 更新节点运行状态
        /// <summary>
        /// 更新节点运行状态
        /// </summary>
        /// <param name="req">更新节点刷新命令请求</param>
        /// <remarks></remarks>
        /// <returns></returns>
        // [SignAuthorize]
        [ResponseType(typeof(ResponseBase<EmptyResponse>))]
        [HttpPost, Route("updatenodestatus")]
        public ResponseBase<EmptyResponse> UpdateNodeStatus(UpdateNodeStatusRequest req)
        {
            var node = noderepository.FindSingle(x => x.id == req.NodeId);
            if (node == null)
            {
                return ResponseToClient<EmptyResponse>(ResponesStatus.Failed, "当前节点" + req.NodeId + "不存在库中!");
            }
            var nodeModel = noderepository.FindSingle(x => x.id == req.NodeId);
            nodeModel.nodestatus = (int)req.NodeStatus;
            nodeModel.nodelastupdatetime =req.NodeStatus==NodeStatus.NoRun? nodeModel.nodelastupdatetime.AddYears(-1):nodeModel.nodelastupdatetime;
            noderepository.Update(nodeModel);
            if(req.NodeStatus == NodeStatus.NoRun)//停止
            {
                //把当前节点下所有的任务都改为未调度
                List<int> taskidList = taskversionRep.Find(x => x.nodeid == req.NodeId).Select(x => x.taskid).ToList();
                
                List<tb_task> tasklist=taskRep.Find(x => taskidList.Contains(x.id)).ToList();

                taskRep.UpdateById(taskidList, new Dictionary<string, string>() { { "taskschedulestatus", ((int)TaskScheduleStatus.StopSchedule).ToString() }, { "ispauseschedule", "0" } });
                
                
            }
            return ResponseToClient<EmptyResponse>(ResponesStatus.Success, "");
        } 
        #endregion

        #region 更新节点心跳
         /// <summary>
        /// 更新节点运行状态
        /// </summary>
        /// <param name="req">更新节点刷新命令请求</param>
        /// <remarks></remarks>
        /// <returns></returns>
        // [SignAuthorize]
        [ResponseType(typeof(ResponseBase<EmptyResponse>))]
        [HttpPost, Route("updatenodeheatbeat")]
        public ResponseBase<EmptyResponse> UpdateNodeHeatbeat(UpdateNodeHeatbeatRequest req)
        {
            var node = noderepository.FindSingle(x => x.id == req.NodeId);
            if (node == null)
            {
                return ResponseToClient<EmptyResponse>(ResponesStatus.Failed, "当前节点" + req.NodeId + "不存在库中!");
            }
            var nodeModel = noderepository.FindSingle(x => x.id == req.NodeId);
            if(!string.IsNullOrEmpty(req.MonitorClassName))//如果是监控过来的则更新监控插件
            {
                UpdateNodeMonitor(new UpdateNodeMonitorRequest() { MonitorClassName=req.MonitorClassName,MonitorStatus=(int)MonitorStatus.Monitoring,NodeId=req.NodeId,Source=req.Source});
            }
            //noderepository.Update(nodeModel);
            noderepository.UpdateNodeById(nodeModel.id, new Dictionary<string, string>() { { "nodestatus", ((int)NodeStatus.Running).ToString() }, { "nodelastupdatetime", DateTime.Now.AddSeconds(10).ToString() } });
            return ResponseToClient<EmptyResponse>(ResponesStatus.Success, "");
        }
        #endregion

        #region 添加节点监控
        /// <summary>
        /// 添加节点监控
        /// </summary>
        /// <param name="req">添加节点监控请求</param>
        /// <remarks></remarks>
        /// <returns></returns>
        // [SignAuthorize]
        [ResponseType(typeof(ResponseBase<EmptyResponse>))]
        [HttpPost, Route("addnodemonitor")]
        public ResponseBase<EmptyResponse> AddNodeMonitor(AddNodeMonitorRequest req)
        {
            var node = noderepository.FindSingle(x => x.id == req.NodeId);
            if (node == null)
            {
                return ResponseToClient<EmptyResponse>(ResponesStatus.Failed, "当前节点" + req.NodeId + "不存在库中!");
            }
            nodemonitorRep.Delete(x => x.nodeid == req.NodeId && x.classname == req.ClassName && x.classnamespace == req.ClassNameSpace);
            nodemonitorRep.Add(new tb_nodemonitor() { 
                name=req.Name,
                discription=req.Discription,
                classname=req.ClassName,
                classnamespace=req.ClassNameSpace,
                createby=0,
                createtime=DateTime.Now,
                isdel=0,
                lastmonitortime=DateTime.Now.AddSeconds(10),
                monitorstatus=(int)MonitorStatus.Monitoring,
                nodeid=req.NodeId,
                interval=req.Internal

                
            });
            return ResponseToClient<EmptyResponse>(ResponesStatus.Success, "");
        }
        #endregion

        #region 更新节点监控
        /// <summary>
        /// 更新节点监控
        /// </summary>
        /// <param name="req">更新节点监控请求</param>
        /// <remarks></remarks>
        /// <returns></returns>
        // [SignAuthorize]
        [ResponseType(typeof(ResponseBase<EmptyResponse>))]
        [HttpPost, Route("updatenodemonitor")]
        public ResponseBase<EmptyResponse> UpdateNodeMonitor(UpdateNodeMonitorRequest req)
        {
            var nodemonitor = nodemonitorRep.FindSingle(x => x.nodeid == req.NodeId && x.classname==req.MonitorClassName);
            if (nodemonitor == null)
            {
                return ResponseToClient<EmptyResponse>(ResponesStatus.Failed, "当前节点" + req.NodeId + "不存在库中!");
            }
            Dictionary<string, string> dic=new Dictionary<string, string>() { { "monitorstatus", (req.MonitorStatus).ToString() } };
            if(req.MonitorStatus == (int)MonitorStatus.Monitoring)
            {
                dic.Add("lastmonitortime", DateTime.Now.AddSeconds(10).ToString("yyyy-MM-dd HH:mm:ss"));
            }
            nodemonitorRep.UpdateById(new List<int>() { nodemonitor.id }, dic);
            return ResponseToClient<EmptyResponse>(ResponesStatus.Success, "");
        }
        #endregion

        #region 载入节点监控组件列表
        /// <summary>
        /// 载入节点列表
        /// </summary>
        /// <param name="req">载入节点列表请求类</param>
        /// <returns></returns>
        // [SignAuthorize]
        [ResponseType(typeof(ResponseBase<PageInfoResponse<List<tb_nodemonitor>>>))]
        [HttpPost, Route("loadnodemonitorlist")]
        public ResponseBase<PageInfoResponse<List<tb_nodemonitor>>> LoadNodeMonitorList(LoadNodeMonitorListRequest req)
        {
            try
            {
               
                int pageIndex = (req.iDisplayStart / req.iDisplayLength) + 1;
                int totalCount = 0;
                var nodemonitorlist = nodemonitorRep.Find(out totalCount, pageIndex, req.iDisplayLength, x => x.id.ToString(), x =>
                    x.nodeid == (req.NodeId <= 0 ? x.nodeid : req.NodeId)
                    && x.monitorstatus==(req.MonitorStatus<0?x.monitorstatus:req.MonitorStatus)
                    && x.isdel==0
                    ).OrderByDescending(x=>x.createtime).ToList();
                if (nodemonitorlist == null || nodemonitorlist.Count <= 0)
                {
                    return ResponseToClient<PageInfoResponse<List<tb_nodemonitor>>>(ResponesStatus.Failed, "当前未查到任何节点");
                }
               
                //data.NodeList = nodelist;
                return ResponseToClient<PageInfoResponse<List<tb_nodemonitor>>>(ResponesStatus.Success, "", new PageInfoResponse<List<tb_nodemonitor>>() { aaData = nodemonitorlist, iTotalDisplayRecords = totalCount, iTotalRecords = totalCount, sEcho = req.sEcho });
            }
            catch (Exception ex)
            {
                return ResponseToClient<PageInfoResponse<List<tb_nodemonitor>>>(ResponesStatus.Exception, ex.Message);
            }
        }
        #endregion
    }
}