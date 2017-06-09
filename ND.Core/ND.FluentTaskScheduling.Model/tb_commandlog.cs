using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：tb_commandlog.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-12 15:54:37         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-12 15:54:37          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model
{
    public partial class tb_commandlog
    {
        public tb_commandlog()
        { }
        #region Model
        private int _id;
        private string _msg;
        private DateTime _logcreatetime;
        private int _nodeid;
        private int _commanddetailid = 0;
        private string _commandparams = "";
        private int _commandstate = 0;
        private DateTime _commandstarttime = Convert.ToDateTime("2099-12-30");
        private DateTime _commandendtime = Convert.ToDateTime("2099-12-30");
        private decimal _totalruntime = 0M;
        private int _commandqueueid = 0;
        private string _commandresult = "";

        /// <summary>
        /// 
        /// </summary>
        public string commandresult
        {
            set { _commandresult = value; }
            get { return _commandresult; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int commandqueueid
        {
            set { _commandqueueid = value; }
            get { return _commandqueueid; }
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
        /// 
        /// </summary>
        public string msg
        {
            set { _msg = value; }
            get { return _msg; }
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
        /// 命令详情id
        /// </summary>
        public int commanddetailid
        {
            set { _commanddetailid = value; }
            get { return _commanddetailid; }
        }
        /// <summary>
        /// 当前执行命令所需参数
        /// </summary>
        public string commandparams
        {
            set { _commandparams = value; }
            get { return _commandparams; }
        }
        /// <summary>
        /// 命令执行状态，参考代码枚举
        /// </summary>
        public int commandstate
        {
            set { _commandstate = value; }
            get { return _commandstate; }
        }
        /// <summary>
        /// 命令开始时间
        /// </summary>
        public DateTime commandstarttime
        {
            set { _commandstarttime = value; }
            get { return _commandstarttime; }
        }
        /// <summary>
        /// 命令结束时间
        /// </summary>
        public DateTime commandendtime
        {
            set { _commandendtime = value; }
            get { return _commandendtime; }
        }
        /// <summary>
        /// 分钟为单位，总运行时间
        /// </summary>
        public decimal totalruntime
        {
            set { _totalruntime = value; }
            get { return _totalruntime; }
        }
        #endregion Model
    }
}
