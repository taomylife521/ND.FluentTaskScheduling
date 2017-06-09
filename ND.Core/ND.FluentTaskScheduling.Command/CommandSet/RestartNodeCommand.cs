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
// 文件名称(File Name)：RestartNodeCommand.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-10 15:30:20         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-10 15:30:20          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.CommandSet
{
    public class RestartNodeCommand : AbstractCommand
    {
       
        public override string CommandDisplayName
        {
            get
            {
                return "重新启动节点";
            }

        }
        public override string CommandDescription
        {
            get
            {
                return "该命令用于各个节点的重新启动";
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
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);
                //添加日志
                ShowCommandLog("重新启动windows服务成功");
                return new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteSucess };

            }
            catch (Exception ex)
            {
                ShowCommandLog("重新启动windows服务失败,异常信息:" + JsonConvert.SerializeObject(ex));
                return new RunCommandResult() { ExecuteStatus = ExecuteStatus.ExecuteException, Ex = ex, Message = ex.Message };
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
