using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：tb_tasklog.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-13 14:09:35         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-13 14:09:35          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model
{
    public partial class tb_tasklog
    {
        public tb_tasklog()
        { }
        #region Model
        private int _id;
        private string _logmsg = "";
        private DateTime _logcreatetime;
        private int _nodeid;
        private int _taskid = 0;
        private int _taskversionid = 0;
        private string _taskparams = "";
        private int _taskstatus = 0;
        private DateTime _taskstarttime = Convert.ToDateTime("2099-12-30");
        private DateTime _taskendtime = Convert.ToDateTime("2099-12-30");
        private decimal _totalruntime;
        private string _taskrunresult = "";
        private DateTime _nextruntime = Convert.ToDateTime("2099-12-30");

        /// <summary>
        /// 下次执行时间
        /// </summary>
        public DateTime nextruntime
        {
            set { _nextruntime = value; }
            get { return _nextruntime; }
        }

        /// <summary>
        /// 任务运行结果
        /// </summary>
        public string taskrunresult
        {
            set { _taskrunresult = value; }
            get { return _taskrunresult; }
        }

        /// <summary>
        /// 任务版本id
        /// </summary>
        public int taskversionid
        {
            set { _taskversionid = value; }
            get { return _taskversionid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 当前任务执行的日志消息
        /// </summary>
        public string logmsg
        {
            set { _logmsg = value; }
            get { return _logmsg; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime logcreatetime
        {
            set { _logcreatetime = value; }
            get { return _logcreatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int nodeid
        {
            set { _nodeid = value; }
            get { return _nodeid; }
        }
        /// <summary>
        /// 任务id 关联任务表tb_task
        /// </summary>
        public int taskid
        {
            set { _taskid = value; }
            get { return _taskid; }
        }
        /// <summary>
        /// 任务参数
        /// </summary>
        public string taskparams
        {
            set { _taskparams = value; }
            get { return _taskparams; }
        }
        /// <summary>
        /// 任务状态 0-未执行 1-执行中 2-执行成功 3-执行失败
        /// </summary>
        public int taskstatus
        {
            set { _taskstatus = value; }
            get { return _taskstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime taskstarttime
        {
            set { _taskstarttime = value; }
            get { return _taskstarttime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime taskendtime
        {
            set { _taskendtime = value; }
            get { return _taskendtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal totalruntime
        {
            set { _totalruntime = value; }
            get { return _totalruntime; }
        }
        #endregion Model
    }
}
