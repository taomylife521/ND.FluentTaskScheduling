using ND.FluentTaskScheduling.Core;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

//**********************************************************************
//
// 文件名称(File Name)：LogProxy.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-10 17:03:20         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-10 17:03:20          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Command.asyncrequest
{
   public class LogProxy
    {
       private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       public static void AddNodeLog(string msg,LogType logType= LogType.SystemLog)
       {
           AddLog(msg, 0, logType);
       }

       public static void AddTaskLog(string msg,int taskid)
       {
           AddLog(msg, taskid, LogType.CommonLog);
       }

       public static void AddNodeErrorLog(string msg)
       {
           AddLog(msg, 0, LogType.SystemError);
       }

       public static void AddTaskErrorLog(string msg,int taskid)
       {
           AddLog(msg, taskid, LogType.CommonError);
       }

       private static void AddLog(string msg,int taskid,LogType logType)
       {
           var r = NodeProxy.PostToServer<EmptyResponse, AddLogRequest>(ProxyUrl.AddLog_Url, new AddLogRequest() { NodeId = GlobalNodeConfig.NodeID, Source = Source.Node, Msg = msg, LogType = logType, TaskId = taskid });
           if (r.Status != ResponesStatus.Success)
           {
               log.Error("请求" + ProxyUrl.AddLog_Url + "获取节点配置失败,服务端返回信息:" + JsonConvert.SerializeObject(r));
           }
       }
    }
}
