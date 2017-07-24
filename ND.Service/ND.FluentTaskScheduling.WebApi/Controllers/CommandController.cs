using ND.FluentTaskScheduling.Domain.interfacelayer;
using ND.FluentTaskScheduling.Model;
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
     [RoutePrefix("api/command")]
    public class CommandController : BaseController
    {
           public ICommandLibDetailRepository cmdlibdetailrepository;
          public ICommandLibRepository cmdrepository;
         
          public CommandController(ICommandLibDetailRepository rep, ICommandLibRepository cmdRep,INodeRepository nodeRep,IUserRepository userRep):base(nodeRep,userRep)
          {
              cmdlibdetailrepository = rep;
              cmdrepository = cmdRep;
              //noderepository = nodeRep;
             // userrepository = userRep;
          }


          #region 载入命令列表
          /// <summary>
          /// 载入命令列表
          /// </summary>
          /// <param name="req">载入命令列表请求类</param>
          /// <remarks>命令列表</remarks>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<LoadCommandListResponse>))]
          [HttpPost, Route("loadcommandlist")]
          public ResponseBase<LoadCommandListResponse> LoadCommandList(LoadCommandListRequest req)
          {
              try
              {
                  tb_node node=null;
                  if (!string.IsNullOrEmpty(req.NodeId))
                  {
                      int nodeId = int.Parse(req.NodeId);
                      if (nodeId > 0)
                      {
                           node = noderepository.FindSingle(x => x.id == nodeId);
                          if (node == null)
                          {
                              return ResponseToClient<LoadCommandListResponse>(ResponesStatus.Failed, "当前节点" + req.NodeId + "不存在库中!");
                          }
                      }
                  }
                  List<tb_commandlibdetail> lib = cmdlibdetailrepository.Find(x => x.isdel == 0).OrderByDescending(x => x.createtime).Take(node.maxrefreshcommandqueuecount).ToList();
                  if (lib.Count <= 0)
                  {
                      return ResponseToClient<LoadCommandListResponse>(ResponesStatus.Failed, "当前暂无命令列表");
                  }
                  LoadCommandListResponse data = new LoadCommandListResponse()
                  {
                      NodeId = req.NodeId,
                      CommandLibDetailList = lib

                  };
                  return ResponseToClient<LoadCommandListResponse>(ResponesStatus.Success, "", data);
              }
              catch (Exception ex)
              {
                  return ResponseToClient<LoadCommandListResponse>(ResponesStatus.Exception, JsonConvert.SerializeObject(ex));
              }
          } 
          #endregion


          #region 载入Web命令列表
          /// <summary>
          /// 载入命令列表
          /// </summary>
          /// <param name="req">载入命令列表请求类</param>
          /// <remarks>命令列表</remarks>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<PageInfoResponse<List<tb_commandlibdetail>>>))]
          [HttpPost, Route("loadwebcommandlist")]
          public ResponseBase<PageInfoResponse<List<tb_commandlibdetail>>> LoadWebCommandList(LoadWebCommandListRequest req)
          {
              try
              {
                  int pageIndex = (req.iDisplayStart / req.iDisplayLength) + 1;
                  int totalCount = 0;
                  List<tb_commandlibdetail> lib = cmdlibdetailrepository.Find(out totalCount, pageIndex, req.iDisplayLength, x=>x.id.ToString(),
                      x => x.isdel == 0
                          && x.id ==(req.CommandId <= 0 ?x.id:req.CommandId)
                      ).ToList();
                  if (lib.Count <= 0)
                  {
                      return ResponseToClient<PageInfoResponse<List<tb_commandlibdetail>>>(ResponesStatus.Failed, "当前暂无命令列表");
                  }
                  return ResponseToClient<PageInfoResponse<List<tb_commandlibdetail>>>(ResponesStatus.Success, "", new PageInfoResponse<List<tb_commandlibdetail>>() { aaData=lib,iTotalDisplayRecords=totalCount,iTotalRecords=totalCount,sEcho=req.sEcho});
              }
              catch (Exception ex)
              {
                  return ResponseToClient<PageInfoResponse<List<tb_commandlibdetail>>>(ResponesStatus.Exception, JsonConvert.SerializeObject(ex));
              }
          }
          #endregion

       
    }
}
