using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：MonitorBase.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-27 15:34:20         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-27 15:34:20          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
   public class MonitorRequestBase:RequestBase
    {
        private string monitorClassName = "";

        /// <summary>
        /// 节点监控类名
        /// </summary>
        public string MonitorClassName { get { return monitorClassName; } set { monitorClassName = value; } }
    }
}
