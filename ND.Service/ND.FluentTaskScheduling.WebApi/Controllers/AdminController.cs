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
      [RoutePrefix("api/admin")]
    public class AdminController:BaseController
    {
         


          public AdminController( INodeRepository nodeRep, IUserRepository userRep)
              : base(nodeRep, userRep)
          {
             
          }
      

        #region 载入用户列表
          /// <summary>
          /// 载入用户列表
          /// </summary>
          /// <param name="req">载入用户列表请求类</param>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<PageInfoResponse<List<tb_user>>>))]
          [HttpPost, Route("loaduserlist")]
          public ResponseBase<PageInfoResponse<List<tb_user>>> LoadUserList(LoadUserListRequest req)
          {
              try
              {
                  DateTime starttime = Convert.ToDateTime(req.UserCreateTimeRange.Split('/')[0]);
                  DateTime endtime = Convert.ToDateTime(req.UserCreateTimeRange.Split('/')[1]).AddDays(1);
                  int pageIndex = (req.iDisplayStart / req.iDisplayLength) + 1;
                  int totalCount = 0;
                  List<tb_user> user = userrepository.Find(out totalCount, pageIndex, req.iDisplayLength, k=>k.id.ToString(), x =>
                      x.useremail == (string.IsNullOrEmpty(req.UserEmail) ? x.useremail : req.UserEmail)
                     && x.usermobile == (string.IsNullOrEmpty(req.UserMobile) ? x.usermobile : req.UserMobile)
                     && x.realname == (string.IsNullOrEmpty(req.UserRealName) ? x.realname : req.UserRealName)
                     && x.createtime>=starttime && x.createtime<endtime
                      ).ToList();
                  if (user == null || user.Count <= 0)
                  {
                      return ResponseToClient<PageInfoResponse<List<tb_user>>>(ResponesStatus.Failed, "当前未查到任何用户");
                  }
                  return ResponseToClient<PageInfoResponse<List<tb_user>>>(ResponesStatus.Success, "", new PageInfoResponse<List<tb_user>>() { aaData = user, iTotalDisplayRecords = totalCount, iTotalRecords = totalCount, sEcho = req.sEcho });
              }
              catch(Exception ex)
              {
                  return ResponseToClient<PageInfoResponse<List<tb_user>>>(ResponesStatus.Exception, ex.Message);
              }
          }
          #endregion

        #region 添加用户
          /// <summary>
          /// 添加用户
          /// </summary>
          /// <param name="req">添加用户请求类</param>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<AddUserResponse>))]
          [HttpPost, Route("adduser")]
          public ResponseBase<AddUserResponse> AddUser(AddUserRequest req)
          {
              DateTime dt = DateTime.Now;
              userrepository.Add(new tb_user()
              {
                  createby=req.AdminId,
                  createtime=dt,
                  isdel=0,
                  realname=req.UserRealName,
                  useremail=req.UserEmail,
                  usermobile=req.UserMobile,
                  username=req.UserMobile,
                  userpassword=req.UserPassword,
                  userrole=0,
                  userupdatetime=dt
              });
              return ResponseToClient<AddUserResponse>(ResponesStatus.Success, "");
          }
        #endregion


        #region 更新用户
          /// <summary>
          /// 更新用户
          /// </summary>
          /// <param name="req">更新用户请求类</param>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<EmptyResponse>))]
          [HttpPost, Route("updateuser")]
          public ResponseBase<EmptyResponse> UpdateUser(UpdateUserRequest req)
          {
              DateTime dt = DateTime.Now;
              tb_user user=userrepository.FindSingle(x => x.id == req.UserId);
              if(user== null)
              {
                  return ResponseToClient<EmptyResponse>(ResponesStatus.Failed, "当前用户不存在");
              }
              if (!string.IsNullOrEmpty(req.UserEmail)) { user.useremail = req.UserEmail; }
              if (!string.IsNullOrEmpty(req.UserMobile)) { user.usermobile = req.UserMobile; user.username = req.UserMobile; }
              if (!string.IsNullOrEmpty(req.UserPassword)) { user.userpassword = req.UserPassword; }
              if (!string.IsNullOrEmpty(req.UserRealName)) { user.realname = req.UserRealName; }
             
              userrepository.Update(user);
              return ResponseToClient<EmptyResponse>(ResponesStatus.Success, "");
          }
          #endregion

        #region 获取用户详情
          /// <summary>
          /// 获取用户详情
          /// </summary>
          /// <param name="req">获取用户详情请求类</param>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<LoadUserDetailResponse>))]
          [HttpPost, Route("loaduserdetail")]
          public ResponseBase<LoadUserDetailResponse> LoadUserDetail(LoadUserDetailRequest req)
          {
              DateTime dt = DateTime.Now;
              tb_user user = userrepository.FindSingle(x => x.id == req.UserId);
              if (user == null)
              {
                  return ResponseToClient<LoadUserDetailResponse>(ResponesStatus.Failed, "当前用户不存在");
              }
              return ResponseToClient<LoadUserDetailResponse>(ResponesStatus.Success, "", new LoadUserDetailResponse() { UserDetail=user});
          }
          #endregion

        #region 校验用户名和密码是否正确
         /// <summary>
          /// 获取用户详情
          /// </summary>
          /// <param name="req">获取用户详情请求类</param>
          /// <returns></returns>
          // [SignAuthorize]
          [ResponseType(typeof(ResponseBase<CheckUNameAndPwdResponse>))]
          [HttpPost, Route("checkunameandpwd")]
          public ResponseBase<CheckUNameAndPwdResponse> CheckUNameAndPwd(CheckUNameAndPwdRequest req)
          {
              tb_user user = userrepository.FindSingle(x => x.username == req.UserName && x.userpassword == req.Password);
              if (user == null)
              {
                  return ResponseToClient<CheckUNameAndPwdResponse>(ResponesStatus.Failed, "用户名或密码不正确");
              }
              return ResponseToClient<CheckUNameAndPwdResponse>(ResponesStatus.Success, "", new CheckUNameAndPwdResponse() { UserInfo = user });
          }
        #endregion


       
    }
}