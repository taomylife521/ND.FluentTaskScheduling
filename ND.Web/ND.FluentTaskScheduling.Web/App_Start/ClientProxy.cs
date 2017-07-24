using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//**********************************************************************
//
// 文件名称(File Name)：ClientProxy.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-17 15:27:18         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-17 15:27:18          
//             修改理由：         
//**********************************************************************

namespace ND.FluentTaskScheduling.Web.App_Start
{
    public class ClientProxy
    {
        public static readonly string Base_Url =System.Configuration.ConfigurationManager.AppSettings["TaskApiUrl"];// "http://localhost:21794/api/";

        public static readonly string Statis_Url = Base_Url.TrimEnd('/') + "/statis/" + "platformstatis/";

        public static readonly string LoadTaskList_Url = Base_Url.TrimEnd('/') + "/task/" + "tasklist/";
        public static readonly string AddTask_Url = Base_Url.TrimEnd('/') + "/task/" + "addtask/";
        public static readonly string UpdateTask_Url = Base_Url.TrimEnd('/') + "/task/" + "updatetask/";
        public static readonly string TaskVersionDetail_Url = Base_Url.TrimEnd('/') + "/task/" + "taskversiondetail/";

        public static readonly string LoadNodeList_Url = Base_Url.TrimEnd('/') + "/node/" + "loadnodelist/";
        public static readonly string LoadNodeDetail_Url = Base_Url.TrimEnd('/') + "/node/" + "loadnodedetail/";
        public static readonly string AddNode_Url = Base_Url.TrimEnd('/') + "/node/" + "addnode/";
        public static readonly string UpdateNodeInfo_Url = Base_Url.TrimEnd('/') + "/node/" + "updatenodeinfo/";
        public static readonly string LoadNodeMonitorList_Url = Base_Url.TrimEnd('/') + "/node/" + "loadnodemonitorlist/";


        public static readonly string LoadUserList_Url = Base_Url.TrimEnd('/') + "/admin/" + "loaduserlist/";
        public static readonly string AddUser_Url = Base_Url.TrimEnd('/') + "/admin/" + "adduser/";
        public static readonly string UpdateUser_Url = Base_Url.TrimEnd('/') + "/admin/" + "updateuser/";
        public static readonly string LoadUserDetail_Url = Base_Url.TrimEnd('/') + "/admin/" + "loaduserdetail/";
        public static readonly string CheckUNameAndPwd_Url = Base_Url.TrimEnd('/') + "/admin/" + "checkunameandpwd/";
        
        
        
       
        

        public static readonly string AddCommandQueue_Url = Base_Url.TrimEnd('/') + "/commandqueue/" + "addcommandqueueitem/";

        public static readonly string LoadWebCommandQueueList_Url = Base_Url.TrimEnd('/') + "/commandqueue/" + "loadwebcommandqueuelist/";

        public static readonly string LoadCommandQueueExecuteLog_Url = Base_Url.TrimEnd('/') + "/log/" + "loadcommandqueueexecutelog/";//获取命令执行日志详情
        public static readonly string LoadTaskExecuteLogList_Url = Base_Url.TrimEnd('/') + "/log/" + "loadtaskexecuteloglist/";
        public static readonly string LoadNodeLogList_Url = Base_Url.TrimEnd('/') + "/log/" + "loadnodelog/";


        /// <summary>
        /// 载入命令列表url
        /// </summary>
        public static readonly string LoadWebCommandList_Url = Base_Url.TrimEnd('/') + "/command/" + "loadwebcommandlist/";



        /// <summary>
        /// 载入任务性能列表url
        /// </summary>
        public static readonly string LoadPerformanceList_Url = Base_Url.TrimEnd('/') + "/performance/" + "loadperformancelist/";

        /// <summary>
        /// 载入节点性能列表
        /// </summary>
        public static readonly string LoadNodePerformanceList_Url = Base_Url.TrimEnd('/') + "/performance/" + "loadnodeperformancelist/";
        

        public static ResponseBase<TResponse> PostToServer<TResponse, TRequest>(string url, TRequest request)
            where TResponse : class
            where TRequest : RequestBase
        {
            ResponseBase<TResponse> r = HttpClientHelper.PostResponse<ResponseBase<TResponse>>(url, JsonConvert.SerializeObject(request));
            return r;
        }
    }
}