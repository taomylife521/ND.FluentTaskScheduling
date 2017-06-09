using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadTaskListRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-17 15:16:31         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-17 15:16:31          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    public class LoadTaskListRequest:PageRequestBase
    {
        private int _taskid = -1;
      //  private string _taskCreateTimeRange = DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToString("yyyy-MM-dd");
        private string _taskCreateTimeRange =  "1900-01-01/2099-12-30";
        private int _nodeid = -1;
        private string _taskname = "";
        private int _taskSchduleStatus = -1;
        private int _taskExecuteStatus = -1;
        private int _alarmtype = -1;
        private int _createby = -1;
        private int _tasktype = -1;
        /// <summary>
        /// 任务名称
        /// </summary>
        public int TaskId { get { return _taskid; } set { _taskid = value; } }

        /// <summary>
        /// 报警类型
        /// </summary>
        public int AlarmType { get { return _alarmtype; } set { _alarmtype = value; } }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateBy { get { return _createby; } set { _createby = value; } }

         /// <summary>
         /// 任务创建时间范围
         /// </summary>
        public string TaskCreateTimeRange { get { return _taskCreateTimeRange; } set { _taskCreateTimeRange = value; } }

         /// <summary>
         /// 节点id
         /// </summary>
        public int NodeId { get { return _nodeid; } set { _nodeid = value; } }

        public string TaskName { get { return _taskname; } set { _taskname = value; } }


        public int TaskSchduleStatus { get { return _taskSchduleStatus; } set { _taskSchduleStatus = value; } }

        /// <summary>
        /// 任务执行状态
        /// </summary>
        public int TaskExecuteStatus { get { return _taskExecuteStatus; } set { _taskExecuteStatus = value; } }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int TaskType { get { return _tasktype; } set { _tasktype = value; } }


    }
}
