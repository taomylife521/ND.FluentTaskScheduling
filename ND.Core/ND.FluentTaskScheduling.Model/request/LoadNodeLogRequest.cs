using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadNodeLogRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-21 15:31:51         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-21 15:31:51          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    /// <summary>
    /// 获取节点日志请求
    /// </summary>
    public class LoadNodeLogRequest:PageRequestBase
    {
        /// <summary>
        /// 节点id
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// 节点日志创建时间范围
        /// </summary>
        public string NodeLogCreateTimeRange { get; set; }

        /// <summary>
        /// 节点日志类型
        /// </summary>
        public int NodeLogType { get; set; }
    }
}
