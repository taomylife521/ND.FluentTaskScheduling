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
    [RoutePrefix("api/log")]
    public class LogController : BaseController
    {
          ILogRepository logrespository;
          IErrorRepository errorrespository;
          IRefreshCommandQueueLogRepository refreshcommandQueueLogRepository;
          ICommandLogRepository cmdLogRespository;
          ICommandQueueRepository cmdqueuerespository;
          ITaskLogRepository taskLogRep;
          ICommandLibDetailRepository commandRep;
          public LogController(  ICommandLibDetailRepository commandrepository,ILogRepository rep,ITaskLogRepository taskLogRepository, IErrorRepository errRep, IRefreshCommandQueueLogRepository refreshlogRep,INodeRepository nodeRep, IUserRepository userRep,ICommandLogRepository cmdLogRep,ICommandQueueRepository cmdqueueRep)
            : base(nodeRep, userRep)
          {
              logrespository = rep;
              errorrespository = errRep;
              refreshcommandQueueLogRepository = refreshlogRep;
              cmdLogRespository = cmdLogRep;
              cmdqueuerespository = cmdqueueRep;
              taskLogRep = taskLogRepository;
              commandRep = commandrepository;
          }

        #region 添加日志
          /// <summary>
          /// 添加日志信息
          /// </summary>
          /// <param name="req">添加日志请求类</param>
          /// <remarks></remarks>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<EmptyResponse>))]
          [HttpPost, Route("addlog")]
          public ResponseBase<EmptyResponse> AddLog(AddLogRequest req)
          {
              try
              {
                  //var node = noderepository.FindSingle(x => x.id == req.NodeId);
                  //if (node == null)
                  //{
                  //    return ResponseToClient<EmptyResponse>(ResponesStatus.Failed, "当前节点" + req.NodeId + "不存在库中!");
                  //}
                  switch (req.LogType)
                  {
                      case LogType.CommonError:
                      case LogType.SystemError:
                          errorrespository.Add(new tb_error()
                          {
                              errorcreatetime = DateTime.Now,
                              errortype = (int)req.LogType,
                              msg = req.Msg,
                              nodeid = req.NodeId,
                              taskid = req.TaskId
                          });
                          break;
                      //case LogType.CommonLog:
                      //case LogType.SystemLog:
                      //case LogType.NodeInitLog:  
                      //    break;
                      case LogType.RefreshCommandQueueLog://刷新命令队列日志
                          refreshcommandQueueLogRepository.Add(new tb_refreshcommadqueuelog()
                          {
                              logcreatetime = DateTime.Now,
                              logtype = (int)req.LogType,
                              msg = req.Msg,
                              nodeid = req.NodeId,
                              taskid = req.TaskId
                          });
                          break;
                      default:
                          logrespository.Add(new tb_log()
                          {
                              logcreatetime = DateTime.Now,
                              logtype = (int)req.LogType,
                              msg = req.Msg,
                              nodeid = req.NodeId,
                              taskid = req.TaskId
                          });
                          break;
                  }

                  return ResponseToClient<EmptyResponse>(ResponesStatus.Success, "");
              }
              catch (Exception ex)
              {
                  return ResponseToClient<EmptyResponse>(ResponesStatus.Exception, JsonConvert.SerializeObject(ex));
              }
          } 
          #endregion

        #region 添加命令执行日志并更新命令队列状态
         /// <summary>
          /// 添加日志信息
          /// </summary>
          /// <param name="req">添加命令执行日志请求类</param>
          /// <remarks></remarks>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<EmptyResponse>))]
          [HttpPost, Route("addcommandexecutelog")]
          public ResponseBase<EmptyResponse> AddCommandExecuteLog(AddCommandExecuteLogRequest req)
          {
              try
              {
                  //添加执行命令日志
                  cmdLogRespository.Add(new tb_commandlog()
                  {
                      commanddetailid=req.CommandDetailId,
                      commandendtime=Convert.ToDateTime(req.CommandEndTime),
                      commandparams=req.CommandParams,
                      commandqueueid=req.CommandQueueId,
                      commandstarttime=Convert.ToDateTime(req.CommandStartTime),
                      commandstate=req.ExecuteStatus,
                      logcreatetime=DateTime.Now,
                      msg=req.CommandExecuteLog,
                      nodeid= req.NodeId,
                      totalruntime=int.Parse(req.TotalTime),
                      commandresult=req.CommandResult,
                  });
                  var cmdqueuemodel = cmdqueuerespository.FindSingle(x => x.id == req.CommandQueueId && x.nodeid==req.NodeId);
                  if(cmdqueuemodel == null)
                  {
                      return ResponseToClient<EmptyResponse>(ResponesStatus.Failed, "更新命令队列状态失败");
                  }
                  cmdqueuemodel.commandstate = req.ExecuteStatus;
                  //更新命令队列执行状态
                  if (req.ExecuteStatus != (int)ExecuteStatus.ExecuteSucess)
                  {
                      //更新命令队列失败次数
                      cmdqueuemodel.failedcount+=1;
                  }
                  cmdqueuerespository.Update(cmdqueuemodel);//更新命令实体失败次数
                  return ResponseToClient<EmptyResponse>(ResponesStatus.Success, "");
              }
              catch (Exception ex)
              {
                  return ResponseToClient<EmptyResponse>(ResponesStatus.Exception, JsonConvert.SerializeObject(ex));
              }
          } 
        #endregion


        #region 查看命令队列执行日志
          /// <summary>
          /// 添加日志信息
          /// </summary>
          /// <param name="req">查看命令执行日志请求类</param>
          /// <remarks></remarks>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<PageInfoResponse<List<CommandLogListDto>>>))]
          [HttpPost, Route("loadcommandqueueexecutelog")]
          public ResponseBase<PageInfoResponse<List<CommandLogListDto>>> LoadCommandQueueExecuteLog(LoadCommandQueueExecuteLogRequest req)
          {
              try
              {
                  int executestatus = string.IsNullOrEmpty(req.ExecuteStatus) ? -1 : int.Parse(req.ExecuteStatus);
                  DateTime starttime = Convert.ToDateTime(req.LogCreateTimeRange.Split('/')[0]);
                  DateTime endtime =Convert.ToDateTime(req.LogCreateTimeRange.Split('/')[1]).AddDays(1);
                  int pageIndex = (req.iDisplayStart / req.iDisplayLength) + 1;
                  int totalCount = 0;
                  List<tb_commandlog> logList = cmdLogRespository.Find(out totalCount, pageIndex, req.iDisplayLength, x => x.id.ToString(), x => 
                      x.commandqueueid == (req.CommandQueueId<=0?x.commandqueueid:req.CommandQueueId)
                      && x.nodeid ==(req.NodeId<=0?x.nodeid:req.NodeId)
                      && x.commanddetailid == (req.CommandId<=0?x.commanddetailid:req.CommandId)
                      && x.logcreatetime >= starttime && x.logcreatetime < endtime
                      && x.commandstate == (executestatus<0?x.commandstate:executestatus)
                      ).OrderByDescending(x=>x.logcreatetime).ToList();
                  if(logList.Count <= 0)
                  {
                      return ResponseToClient<PageInfoResponse<List<CommandLogListDto>>>(ResponesStatus.Failed, "暂无对应的执行日志列表");
                  }
                  List<CommandLogListDto> commandloglist = new List<CommandLogListDto>();
                  logList.ForEach(x =>
                  {
                      commandloglist.Add(new CommandLogListDto()
                      {
                          CommandDetail = commandRep.FindSingle(m=>m.id==x.commanddetailid),
                          CommandLogDetail=x,
                          NodeDetail = noderepository.FindSingle(m=>m.id==x.nodeid)
                      });
                  });
                  return ResponseToClient<PageInfoResponse<List<CommandLogListDto>>>(ResponesStatus.Success, "", new PageInfoResponse<List<CommandLogListDto>>{ aaData= commandloglist,iTotalDisplayRecords=totalCount, iTotalRecords=totalCount,sEcho=req.sEcho});
              }
              catch (Exception ex)
              {
                  return ResponseToClient<PageInfoResponse<List<CommandLogListDto>>>(ResponesStatus.Exception, JsonConvert.SerializeObject(ex));
              }
          } 
        #endregion


        #region 查看任务调度执行日志
          /// <summary>
          /// 查看任务调度执行日志信息
          /// </summary>
          /// <param name="req">查看任务调度执行日志信息请求类</param>
          /// <remarks></remarks>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<PageInfoResponse<List<TaskLogListDto>>>))]
          [HttpPost, Route("loadtaskexecuteloglist")]
          public ResponseBase<PageInfoResponse<List<TaskLogListDto>>> LoadTaskExecuteLog(LoadTaskExecuteLogRequest req)
          {
              try
              {
                  DateTime starttime = Convert.ToDateTime(req.TaskLogCreateTimeRange.Split('/')[0]);
                  DateTime endtime = Convert.ToDateTime(req.TaskLogCreateTimeRange.Split('/')[1]).AddDays(1);
                  int pageIndex = (req.iDisplayStart / req.iDisplayLength) + 1;
                  int totalCount = 0;
                  var tasklogList = taskLogRep.Find(out totalCount, pageIndex, req.iDisplayLength,  x=> x.logcreatetime.ToString(), x =>
                      x.taskid == (req.TaskId <= 0 ? x.taskid : req.TaskId)
                      && x.nodeid == (req.NodeId <= 0 ? x.nodeid : req.NodeId)
                      && x.taskstatus == (req.TaskExecuteStatus < 0 ? x.taskstatus : req.TaskExecuteStatus)
                      &&x.logcreatetime >= starttime && x.logcreatetime < endtime
                      ).OrderByDescending(x => x.logcreatetime).ToList();//.OrderByDescending(x=>x.id)

                  if (tasklogList.Count <= 0)
                  {
                      return ResponseToClient<PageInfoResponse<List<TaskLogListDto>>>(ResponesStatus.Failed, "暂无对应的任务执行日志列表");
                  }
                  List<TaskLogListDto> dtolist = new List<TaskLogListDto>();
                  tasklogList.ForEach(x =>
                  {
                      dtolist.Add(new TaskLogListDto()
                      {
                          TaskLogDetail=x,
                          NodeDetail=noderepository.FindSingle(m=>m.id == x.nodeid)
                      });
                  });
                  return ResponseToClient<PageInfoResponse<List<TaskLogListDto>>>(ResponesStatus.Success, "", new PageInfoResponse<List<TaskLogListDto>>() { aaData = dtolist,iTotalDisplayRecords=totalCount,iTotalRecords=totalCount,sEcho=req.sEcho });
              }
              catch (Exception ex)
              {
                  return ResponseToClient<PageInfoResponse<List<TaskLogListDto>>>(ResponesStatus.Exception, JsonConvert.SerializeObject(ex));
              }
          }
          #endregion

        #region 查看节点日志
          /// <summary>
          /// 查看节点日志
          /// </summary>
          /// <param name="req">查看节点日志请求类</param>
          /// <remarks></remarks>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<PageInfoResponse<List<tb_log>>>))]
          [HttpPost, Route("loadnodelog")]
          public ResponseBase<PageInfoResponse<List<tb_log>>> LoadNodeLog(LoadNodeLogRequest req)
          {
              try
              {
                  DateTime starttime = Convert.ToDateTime(req.NodeLogCreateTimeRange.Split('/')[0]);
                  DateTime endtime = Convert.ToDateTime(req.NodeLogCreateTimeRange.Split('/')[1]).AddDays(1);
                  int pageIndex = (req.iDisplayStart / req.iDisplayLength) + 1;
                  int totalCount = 0;
                  List<tb_log> logList = new List<tb_log>();
                  switch((LogType)req.NodeLogType)
                  { 
                      case LogType.CommonError:
                      case LogType.SystemError:
                          {
                             List<tb_error> errorList= errorrespository.Find(out totalCount, pageIndex, req.iDisplayLength, x => x.id.ToString(),
                                    x => x.nodeid == (req.NodeId <= 0 ? x.nodeid : req.NodeId)
                                    && x.errorcreatetime >= starttime && x.errorcreatetime < endtime
                                    && x.errortype == (req.NodeLogType < 0 ? x.errortype : req.NodeLogType)).OrderByDescending(x => x.errorcreatetime).ToList();
                             errorList.ForEach(x =>
                             {
                                 logList.Add(new tb_log()
                                 {
                                     logcreatetime=x.errorcreatetime,
                                     logtype=x.errortype,
                                     msg=x.msg,
                                     nodeid=x.nodeid,
                                     taskid=x.taskid
                                 });
                             });
                          }
                          break;
                      case LogType.RefreshCommandQueueLog:
                          {
                              List<tb_refreshcommadqueuelog> refreshlogList = refreshcommandQueueLogRepository.Find(out totalCount, pageIndex, req.iDisplayLength, x => x.id.ToString(),
                                   x => x.nodeid == (req.NodeId <= 0 ? x.nodeid : req.NodeId)
                                    && x.logcreatetime >= starttime && x.logcreatetime < endtime
                                    && x.logtype == (req.NodeLogType < 0 ? x.logtype : req.NodeLogType)
                                  ).OrderByDescending(x => x.logcreatetime).ToList();
                              refreshlogList.ForEach(x =>
                              {
                                  logList.Add(new tb_log()
                                  {
                                      logcreatetime = x.logcreatetime,
                                      logtype = x.logtype,
                                      msg = x.msg,
                                      nodeid = x.nodeid,
                                      taskid = x.taskid
                                  });
                              });
                          }
                          break;
                      default:
                          {
                              logList = logrespository.Find(out totalCount, pageIndex, req.iDisplayLength, x => x.id.ToString(),
                                x => x.nodeid == (req.NodeId <= 0 ? x.nodeid : req.NodeId)
                                    && x.logcreatetime >= starttime && x.logcreatetime < endtime
                                    && x.logtype == (req.NodeLogType < 0 ? x.logtype : req.NodeLogType)
                                ).OrderByDescending(x => x.logcreatetime).ToList();
                          }
                          break;
                 
                  }
                  if (logList.Count <= 0)
                  {
                      return ResponseToClient<PageInfoResponse<List<tb_log>>>(ResponesStatus.Failed, "暂无对应的节点日志列表");
                  }
                  return ResponseToClient<PageInfoResponse<List<tb_log>>>(ResponesStatus.Success, "", new PageInfoResponse<List<tb_log>>() { aaData = logList,iTotalDisplayRecords=totalCount,iTotalRecords=totalCount,sEcho=req.sEcho });
              }
              catch (Exception ex)
              {
                  return ResponseToClient<PageInfoResponse<List<tb_log>>>(ResponesStatus.Exception, JsonConvert.SerializeObject(ex));
              }
          }
          #endregion

    }
}
