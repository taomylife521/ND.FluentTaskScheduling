using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadNodeListRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-21 14:36:27         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-21 14:36:27          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    public class LoadNodeListRequest:PageRequestBase
    {
        private int _nodeid = -1;
        private int _noderunstatus = -1;
        private int _listencommandqueuestatus=-1;
        private int _createby = -1;
        private string _nodecreatetimerange="1900-01-01/2099-12-30";

        private int _alarmtype = -1;

        /// <summary>
        /// 节点编号
        /// </summary>
        public int NodeId { get{return _nodeid;} set{_nodeid=value;} }

        /// <summary>
        /// 节点运行状态
        /// </summary>
        public int NodeRunStatus { get{return _noderunstatus;} set{_noderunstatus=value;} }

        /// <summary>
        /// 监听命令队列状态
        /// </summary>
        public int ListenCommandQueueStatus { get{return _listencommandqueuestatus;} set{_listencommandqueuestatus=value;} }

        /// <summary>
        /// 节点创建时间范围
        /// </summary>
        public string NodeCreateTimeRange{ get{return _nodecreatetimerange;} set{_nodecreatetimerange=value;} }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy { get { return _createby; } set { _createby = value; } }


        /// <summary>
        /// 报警类型
        /// </summary>
        public int AlarmType { get { return _alarmtype; } set { _alarmtype = value; } }
    }
}
