using ND.FluentTaskScheduling.Command.asyncrequest;
using ND.FluentTaskScheduling.Core.CommandHandler;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using ND.FluentTaskScheduling.Model.request;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AbstractCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-05 10:33:10         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-05 10:33:10          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandSet
{
    /// <summary>
    /// 抽象的命令定义，统一约定：针对节点的命令以NodeCommand结尾 针对任务的命令以TaskCommand结尾
    /// </summary>
   public abstract class AbstractCommand
    {
       private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       private string _commandVersion = "1.0";
       public AbstractCommand()
       {
           InitAppConfig();
       }
       /// <summary>
       /// 命令显示名称
       /// </summary>
       public virtual string CommandDisplayName{get;set;}

       /// <summary>
       /// 命令显示名称
       /// </summary>
       public virtual string CommandDescription{get;set;}

       /// <summary>
       /// 命令详情
       /// </summary>
       public tb_commandlibdetail CommandDetail { get; set; }

       /// <summary>
       /// 当前命令队列
       /// </summary>
       public tb_commandqueue CommandQueue { get; set; }

         /// <summary>
        /// 命令的配置信息，类似项目app.config文件配置
        /// 测试时需要手工代码填写配置,线上环境需要在任务发布的时候配置
        /// </summary>
        public CommandAppConfigInfo AppConfig = new CommandAppConfigInfo();

        static StringBuilder strLog = new StringBuilder();



        public string CommandVersion { get { return _commandVersion; } set { _commandVersion = value; } }//从左到右,第一位:代表大版本迭代 ，第二位：代表大版本下的大更新 第三位:代表bug修复次数

       /// <summary>
       /// 命令执行方法约定
       /// </summary>
       public void TryExecute()
       {
           try
           {
               ShowCommandLog("----------------------" + DateTime.Now + ":开始执行命令-----------------------------------------");
               DateTime startTime = DateTime.Now;
               Stopwatch sw = new Stopwatch();
               RunCommandResult r;
               sw.Start();
               try
               {
                   r = Execute();
                   int retryCount=0;
                   while (r.ExecuteStatus == ExecuteStatus.ExecuteException && retryCount < this.CommandDetail.maxexeceptionretrycount)
                   {
                       int ct=retryCount + 1;
                       ShowCommandLog("**********第" + ct + "次重试 Start**********");
                       r = Execute();
                       retryCount += 1;
                       ShowCommandLog("**********第" + ct + "次重试 End**********");
                   }
                   r.RetryCount += retryCount;

               }
               catch (Exception ex)
               {

                   r = new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteException, Message = ex.Message, Ex = ex };
                   ShowCommandLog("执行命令异常,异常信息:" + JsonConvert.SerializeObject(ex));
               }
               sw.Stop();
               ShowCommandLog("----------------------" + DateTime.Now + ":执行命令完成-----------------------------------------");
               DateTime endTime = DateTime.Now;
               TimeSpan ts = sw.Elapsed;
               long times = sw.ElapsedMilliseconds / 1000;//秒 
               AddCommandExecuteLogRequest addLogReq = new AddCommandExecuteLogRequest()
               {
                   NodeId = GlobalNodeConfig.NodeID,
                   Source = Source.Node,
                   CommandEndTime = endTime.ToString("yyyy-MM-dd HH:mm:ss"),
                   CommandExecuteLog = strLog.ToString(),
                   CommandParams = JsonConvert.SerializeObject(this.AppConfig),
                   CommandQueueId = CommandQueue.id,
                   CommandResult = JsonConvert.SerializeObject(r),
                   ExecuteStatus=(int)r.ExecuteStatus,
                   CommandStartTime = startTime.ToString("yyyy-MM-dd HH:mm:ss"),
                   TotalTime = times.ToString(),
                   CommandDetailId=CommandDetail.id,   
               };
               //上报命令执行日志和执行结果
               var r2 = NodeProxy.PostToServer<EmptyResponse, AddCommandExecuteLogRequest>(ProxyUrl.AddCommandExecuteLog_Url, addLogReq);
               if (r2.Status != ResponesStatus.Success)
               {
                   ShowCommandLog("上报命令(" + CommandDetail.commandmainclassname + ")的执行日志失败,请求地址:" + ProxyUrl.AddCommandExecuteLog_Url + ",请求参数:" + JsonConvert.SerializeObject(addLogReq) + ",服务器返回参数:" + JsonConvert.SerializeObject(r2));
               }
               if(r.ExecuteStatus == ExecuteStatus.ExecuteException)//命令执行异常,报警
               {
                   string title = "当前命令队列(" + CommandQueue.id + ")执行失败,请及时处理";
                   StringBuilder strContent = new StringBuilder();
                   strContent.AppendLine("节点编号:" + GlobalNodeConfig.NodeID);
                   strContent.AppendLine("命令队列编号/命令编号:" + CommandQueue.id + "/" + CommandDetail.id.ToString());
                   strContent.AppendLine("命令执行参数:" + JsonConvert.SerializeObject(this.AppConfig));
                   strContent.AppendLine("命令执行起/止时间:" + startTime.ToString("yyyy-MM-dd HH:mm:ss") + "/" + endTime.ToString("yyyy-MM-dd HH:mm:ss"));
                   strContent.AppendLine("命令执行耗时(s):" + times.ToString());
                   strContent.AppendLine("命令执行结果,状态:" + r.ExecuteStatus.description() + ",执行结果:" + JsonConvert.SerializeObject(r));
                   AlarmHelper.AlarmAsync(GlobalNodeConfig.NodeInfo.isenablealarm, (AlarmType)GlobalNodeConfig.NodeInfo.alarmtype, GlobalNodeConfig.Alarm, title, strContent.ToString());
               }
              
              
           }
           catch(Exception ex)
           {

               ShowCommandLog("执行命令异常,异常信息:" + JsonConvert.SerializeObject(ex));
               ShowCommandLog("----------------------" + DateTime.Now + ":执行命令完成-----------------------------------------");
               log.Error(strLog.ToString());
              
           }
           finally
           {
               strLog.Clear();//正常执行完情况strLog日志
           }
       }

        /// <summary>
        /// 命令执行方法约定
        /// </summary>
       public virtual RunCommandResult Execute() 
        {
            return new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteException, Message = "没有找到要执行的方法" };
        }

     

        public void ShowCommandLog(string msg)
        {
            strLog.AppendLine(msg + "<br/>");
            log.Info(msg);
        }
         /// <summary>
        /// 初始化命令配置参数
        /// </summary>
        public abstract void InitAppConfig();
        
    }
}
