using ND.FluentTaskScheduling.Command;
using ND.FluentTaskScheduling.Core;
using ND.FluentTaskScheduling.Core.CommandSet;
using ND.FluentTaskScheduling.Core.TaskSet;
using ND.FluentTaskScheduling.Helper;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskManagerNodeService.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-05 10:19:36         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-05 10:19:36          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.NodeService
{
    public class TaskManagerNodeService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void Start()
        {
            OnInit("\r\n");
            OnInit("\r\n");
            OnInit("----------------------"+DateTime.Now+":初始化节点开始-----------------------------------------");
            try
            {
                OnInit("**开始请求节点配置信息**");
                if (System.Configuration.ConfigurationSettings.AppSettings.AllKeys.Contains("NodeID"))
                {
                    GlobalNodeConfig.NodeID = Convert.ToInt32(System.Configuration.ConfigurationSettings.AppSettings["NodeID"]);
                }
               // if (GlobalNodeConfig.NodeID <= 0)//string.IsNullOrWhiteSpace(GlobalNodeConfig.TaskDataBaseConnectString) ||
               // {
                    string url = GlobalNodeConfig.TaskManagerWebUrl.TrimEnd('/') + "/node/" + "loadnodeconfig/";
                    ResponseBase<LoadNodeConfigResponse> r = HttpClientHelper.PostResponse<ResponseBase<LoadNodeConfigResponse>>(url, JsonConvert.SerializeObject(new LoadNodeConfigRequest() { NodeId = GlobalNodeConfig.NodeID.ToString(), Source = Source.Node }));
                    if (r.Status != ResponesStatus.Success)
                    {
                        //记录日志，并抛出异常
                        throw new Exception("请求" + url + "获取节点配置失败,服务端返回信息:"+JsonConvert.SerializeObject(r));
                    }
                    //string connectstring = DESHelper.Decrypt(r.Data.ConnctionString, "ND.FluentTaskScheduling.", "1");
                    //if (string.IsNullOrWhiteSpace(GlobalNodeConfig.TaskDataBaseConnectString))
                    //    GlobalNodeConfig.TaskDataBaseConnectString = connectstring;
                    if (GlobalNodeConfig.NodeID <= 0)
                        GlobalNodeConfig.NodeID = r.Data.Node.id;
                    //初始化配置信息
                    OnInit("**请求节点配置信息完成,请求结果:" + JsonConvert.SerializeObject(r)+"**");
               // }



                //初始化命令池
                OnInit("**开启初始化节点命令池**");
                ICommandPoolBuilder builder = new CommandPoolBuilder();
                builder.BuildCommandPool();
                builder.OnInitEvent += builder_OnInitEvent;
                OnInit("**初始化节点命令池完成:本地节点命令池数量:" + CommandPoolManager.CreateInstance().GetList().Count+"**");

                OnInit("**开启异步请求命令队列初始化任务池线程**");
                //初始化任务池
                TaskPoolBuilder.RunBuildAsync();//异步初始化任务池
                OnInit("**异步初始化任务池线程已开启**");

                TaskPoolBuilder.OnInitTaskPoolEvent += TaskPoolBuilder_OnInitTaskPoolEvent;//记录初始化任务池事件
                OnInit("**开始初始化监控线程**");
                //初始化监控信息
                OnInit("**监控线程已开启**");
                OnInit("节点启动成功");
              
            }
            catch (Exception ex)
            {
                OnInit("节点启动失败:"+JsonConvert.SerializeObject(ex));
            }
            finally
            {
                OnInit("----------------------" + DateTime.Now + ":初始化节点结束-----------------------------------------");
            }
        }

        private void OnInit(string msg, Exception ex=null)
        {
            log.Info(msg);

            Console.WriteLine(msg+",异常信息:"+JsonConvert.SerializeObject(ex));
        }


        /// <summary>
        /// 初始化任务池事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TaskPoolBuilder_OnInitTaskPoolEvent(object sender, TaskEventArgs e)
        {
            OnInit(e.msg, e.ex);
        }

       
        /// <summary>
        /// 命令池初始化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void builder_OnInitEvent(object sender,CommandEventArgs e)
        {
            OnInit(e.msg, e.ex);
        }

        public void Stop()
        {

        }
    }
}
