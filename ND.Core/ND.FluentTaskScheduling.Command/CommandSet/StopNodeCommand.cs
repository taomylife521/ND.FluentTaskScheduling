using ND.FluentTaskScheduling.Core.CommandSet;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：StopNodeCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-10 15:52:16         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-10 15:52:16          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandSet
{
    public class StopNodeCommand : AbstractCommand
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public override string CommandDisplayName
        {
            get
            {
                return "停止节点服务";
            }

        }
        public override string CommandDescription
        {
            get
            {
                return "该命令用于向节点发起停止命令";
            }

        }
        /// <summary>
        /// 执行
        /// </summary>
        public override RunCommandResult Execute()
        {
            //通知节点TaskProvider任务执行
            ServiceController service = new ServiceController(ConfigurationManager.AppSettings["WindowsServiceName"].ToString());
            try
            {
               
               
                if (service.Status == ServiceControllerStatus.Running)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped);
                }
               
                //添加日志
                ShowCommandLog("停止节点windows服务成功");
                return new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteSucess };

            }
            catch (Exception ex)
            {
                ShowCommandLog("停止节点windows服务失败,异常信息:" + JsonConvert.SerializeObject(ex));
                return new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteException, Message = ex.Message, Ex = ex };
            }
           
        }

        /// <summary>
        /// 初始化命令所需参数
        /// </summary>
        public override void InitAppConfig()
        {
            //if (!this.AppConfig.ContainsKey("taskid"))
            //{
            //    this.AppConfig.Add("taskid", "");
            //}
        }
    }
}
