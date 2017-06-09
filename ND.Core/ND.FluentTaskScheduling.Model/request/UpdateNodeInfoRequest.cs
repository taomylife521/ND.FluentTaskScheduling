using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：UpdateNodeInfoRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-09 17:54:45         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-09 17:54:45          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    /// <summary>
    /// 更新节点信息请求
    /// </summary>
    public class UpdateNodeInfoRequest:RequestBase
    {
        /// <summary>
        /// 后台登录人id
        /// </summary>
        public int adminid { get; set; }

        /// <summary>
        /// 节点编号
        /// </summary>
        public int nodeid { get; set; }

        /// <summary>
        /// 节点描述
        /// </summary>
        public string nodediscription { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string nodename { get; set; }

        /// <summary>
        /// 节点ip
        /// </summary>
        public string nodeip { get; set; }


        /// <summary>
        /// 是否检查心跳
        /// </summary>
        public bool ifcheckstate { get; set; }

        /// <summary>
        /// 报警类型
        /// </summary>
        public int alarmtype { get; set; }


        /// <summary>
        /// 报警人
        /// </summary>
        public string alarmuserid { get; set; }

        /// <summary>
        /// 是否启用报警
        /// </summary>
        public int isenablealarm { get; set; }

        /// <summary>
        /// 单次获取最大数量
        /// </summary>
        public int maxrefreshcommandqueuecount { get; set; }
    }
}
