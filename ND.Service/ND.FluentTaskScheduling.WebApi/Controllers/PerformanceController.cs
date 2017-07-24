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
      [RoutePrefix("api/performance")]
    public class PerformanceController : BaseController
    {
          public IPerformanceRepository performancerepository;
          public ITaskRepository taskrep;
          public INodeRepository noderep;
          public INodePerformanceRepository nodeperformanceRep;
          public INodeMonitorRepository nodemonitorRep;

          public PerformanceController(INodeMonitorRepository nodemonitorRepostory,INodeRepository noderepostory, INodePerformanceRepository nodeperformanceRepostory,IPerformanceRepository rep,  ITaskRepository taskrepostory,  INodeRepository nodeRep, IUserRepository userRep)
              : base(nodeRep, userRep)
          {
              performancerepository = rep;
          taskrep=taskrepostory;
          nodeperformanceRep = nodeperformanceRepostory;
          noderep = noderepository;
          nodemonitorRep = nodemonitorRepostory;
          }


        #region 添加任务性能
          /// <summary>
          /// 载入命令列表
          /// </summary>
          /// <param name="req">载入命令列表请求类</param>
          /// <remarks>命令列表</remarks>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<EmptyResponse>))]
          [HttpPost, Route("addperformance")]
          public ResponseBase<EmptyResponse> AddPerformance(AddPerformanceRequest req)
          {
              try
              {
                      int nodeId = req.NodeId;
                      if (nodeId > 0)
                      {
                          var node = noderepository.FindSingle(x => x.id == nodeId);
                          if (node == null)
                          {
                              return ResponseToClient<EmptyResponse>(ResponesStatus.Failed, "当前节点" + req.NodeId + "不存在库中!");
                          }
                      }
                     
                      performancerepository.Add(new tb_performance()
                      {
                          cpu = double.Parse(req.Cpu),
                          installdirsize = double.Parse(req.InstallDirsize),
                          lastupdatetime=Convert.ToDateTime(req.Lastupdatetime),
                          memory = double.Parse(req.Memory),
                          nodeid=req.NodeId,
                          taskid=req.TaskId
                      });

                      return ResponseToClient<EmptyResponse>(ResponesStatus.Success, "");
              }
              catch (Exception ex)
              {
                  return ResponseToClient<EmptyResponse>(ResponesStatus.Exception, JsonConvert.SerializeObject(ex));
              }
          }
          #endregion

        #region 获取任务的性能列表
          /// <summary>
          /// 载入命令列表
          /// </summary>
          /// <param name="req">载入命令列表请求类</param>
          /// <remarks>命令列表</remarks>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<PageInfoResponse<LoadPerformancelistResponse>>))]
          [HttpPost, Route("loadperformancelist")]
          public ResponseBase<PageInfoResponse<LoadPerformancelistResponse>> LoadPerformancelist(LoadPerformancelistRequest req)
          {
              try
              {
                  int pageIndex = (req.iDisplayStart / req.iDisplayLength) + 1;
                  int totalCount = 0;
                  LoadPerformancelistResponse performanceDetail = new LoadPerformancelistResponse();

                  List<int> taskidlist = taskrep.LoadTaskPageList(out totalCount, new LoadTaskListRequest() { iDisplayLength = req.iDisplayLength, sEcho = req.sEcho, iDisplayStart = req.iDisplayStart }).Select(x=>x.Task.id).Distinct().ToList();
                  taskidlist.ForEach(m =>
                  {
                      var performancelist = performancerepository.Find(x => m == x.taskid && x.taskid == (req.TaskId <= 0 ? x.taskid : req.TaskId) && x.nodeid == (req.NodeId <= 0 ? x.nodeid : req.NodeId)).OrderByDescending(x => x.lastupdatetime).Take(15).ToList().OrderBy(x => x.lastupdatetime).ToList();
                      performancelist.ForEach(x =>
                      {
                          if (performanceDetail.TaskPerfomance.ContainsKey(x.taskid.ToString()))
                          {
                              performanceDetail.TaskPerfomance[x.taskid.ToString()].Add(x);
                          }
                          else
                          {
                              performanceDetail.TaskPerfomance.Add(x.taskid.ToString(), new List<tb_performance>() { x });
                          }
                      });

                  });
                  
                
                
                 return ResponseToClient<PageInfoResponse<LoadPerformancelistResponse>>(ResponesStatus.Success, "", new PageInfoResponse<LoadPerformancelistResponse>() { aaData = performanceDetail,iTotalDisplayRecords=totalCount,iTotalRecords=totalCount,sEcho= req.sEcho });
              }
              catch (Exception ex)
              {
                  return ResponseToClient<PageInfoResponse<LoadPerformancelistResponse>>(ResponesStatus.Exception, JsonConvert.SerializeObject(ex));
              }
          }
        #endregion

          #region 添加节点所在机子性能
          /// <summary>
          /// 载入命令列表
          /// </summary>
          /// <param name="req">添加节点所在机子性能请求类</param>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<EmptyResponse>))]
          [HttpPost, Route("addnodeperformance")]
          public ResponseBase<EmptyResponse> AddNodePerformance(AddNodePerformanceRequest req)
          {
              try
              {
                  req.NodePerformance.lastupdatetime = DateTime.Now;
                  nodeperformanceRep.Add(req.NodePerformance);
                  if (!string.IsNullOrEmpty(req.MonitorClassName))
                  {
                      var nodemonitor = nodemonitorRep.FindSingle(x => x.nodeid == req.NodePerformance.nodeid && x.classname == req.MonitorClassName);
                      if (nodemonitor != null)
                      {
                          Dictionary<string, string> dic = new Dictionary<string, string>() { { "lastmonitortime", DateTime.Now.AddSeconds(10).ToString("yyyy-MM-dd HH:mm:ss") }, { "monitorstatus", ((int)MonitorStatus.Monitoring).ToString() } };
                          nodemonitorRep.UpdateById(new List<int>() { nodemonitor.id }, dic);
                      }
                     
                  }
                  return ResponseToClient<EmptyResponse>(ResponesStatus.Success, "");
              }
              catch (Exception ex)
              {
                  return ResponseToClient<EmptyResponse>(ResponesStatus.Exception, JsonConvert.SerializeObject(ex));
              }
          }
          #endregion

          #region 获取节点的性能列表
          /// <summary>
          /// 载入命令列表
          /// </summary>
          /// <param name="req">载入命令列表请求类</param>
          /// <remarks>命令列表</remarks>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<PageInfoResponse<LoadNodePerformancelistResponse>>))]
          [HttpPost, Route("loadnodeperformancelist")]
          public ResponseBase<PageInfoResponse<LoadNodePerformancelistResponse>> LoadNodePerformancelist(LoadNodePerformancelistRequest req)
          {
              try
              {
                  int pageIndex = (req.iDisplayStart / req.iDisplayLength) + 1;
                  int totalCount = 0;
                  LoadNodePerformancelistResponse performanceDetail = new LoadNodePerformancelistResponse();
                 List<int> nodeidlist= noderep.Find(out totalCount, pageIndex, req.iDisplayLength, m => m.id.ToString(), x =>x.nodestatus ==(req.NodeRunStatus<=0?x.nodestatus:req.NodeRunStatus) &&x.id ==(req.NodeId <= 0?x.id:req.NodeId)).Select(k=>k.id).ToList();
                 nodeidlist.ForEach(m =>
                 {
                     var performancelist = nodeperformanceRep.Find(x => m==x.nodeid  && x.nodeid == (req.NodeId<=0?x.nodeid:req.NodeId)).OrderByDescending(x => x.lastupdatetime).Take(15).ToList().OrderBy(x => x.lastupdatetime).ToList();
                     performancelist.ForEach(x =>
                     {
                         if (performanceDetail.NodePerfomance.ContainsKey(x.nodeid.ToString()))
                         {
                             performanceDetail.NodePerfomance[x.nodeid.ToString()].Add(x);
                         }
                         else
                         {
                             performanceDetail.NodePerfomance.Add(x.nodeid.ToString(), new List<tb_nodeperformance>() { x });
                         }
                     });
                 });
                
                  
                  return ResponseToClient<PageInfoResponse<LoadNodePerformancelistResponse>>(ResponesStatus.Success, "", new PageInfoResponse<LoadNodePerformancelistResponse>() { aaData = performanceDetail, iTotalDisplayRecords = totalCount, iTotalRecords = totalCount, sEcho = req.sEcho });
              }
              catch (Exception ex)
              {
                  return ResponseToClient<PageInfoResponse<LoadNodePerformancelistResponse>>(ResponesStatus.Exception, JsonConvert.SerializeObject(ex));
              }
          }
          #endregion
    }
}
