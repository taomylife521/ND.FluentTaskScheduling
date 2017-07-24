/**  版本信息模板在安装目录下，可自行修改。
* tb_task.cs
*
* 功 能： N/A
* 类 名： tb_task
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017-04-05 10:52:24   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace ND.FluentTaskScheduling.TaskInterface
{
	/// <summary>
	/// 任务表
	/// </summary>
	public partial class tb_task:MarshalByRefObject
	{
		public tb_task()
		{}
		#region Model
		private int _id;
		private string _taskname="";
		private string _taskdescription="";
		private string _tasknamespace="";
		private string _taskclassname="";
		private int _groupid=0;
		
		private int _isdel=0;
        private int _createby = 0;
        private DateTime _createtime = DateTime.Now;
        private string _taskremark = "";
        private int _taskschedulestatus = 0;
        private int _ispauseschedule = 0;
        private int _alarmtype = 0;
        private string _alarmuserid = "";
        private int _isenablealarm = 0;
        private int _tasktype = 0;
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
        /// 任务类型
        /// </summary>
        public int tasktype
        {
            set { _tasktype = value; }
            get { return _tasktype; }
        }

        /// <summary>
        /// 是否启用报警
        /// </summary>
        public int isenablealarm
        {
            set { _isenablealarm = value; }
            get { return _isenablealarm; }
        }
        /// <summary>
        /// 报警人
        /// </summary>
        public string alarmuserid
        {
            set { _alarmuserid = value; }
            get { return _alarmuserid; }
        }

        /// <summary>
        /// 报警类型
        /// </summary>
        public int alarmtype
        {
            set { _alarmtype = value; }
            get { return _alarmtype; }
        }

        /// <summary>
        /// 是否暂停调度
        /// </summary>
        public int ispauseschedule
        {
            set { _ispauseschedule = value; }
            get { return _ispauseschedule; }
        }

        /// <summary>
        /// 任务调度状态
        /// </summary>
        public int taskschedulestatus
        {
            set { _taskschedulestatus = value; }
            get { return _taskschedulestatus; }
        }
      


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createtime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }

		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 任务名称
		/// </summary>
		public string taskname
		{
			set{ _taskname=value;}
			get{return _taskname;}
		}
		/// <summary>
		/// 任务描述
		/// </summary>
		public string taskdescription
		{
			set{ _taskdescription=value;}
			get{return _taskdescription;}
		}
		/// <summary>
		/// 任务命名空间
		/// </summary>
		public string tasknamespace
		{
			set{ _tasknamespace=value;}
			get{return _tasknamespace;}
		}
		/// <summary>
		/// 任务类名
		/// </summary>
		public string taskclassname
		{
			set{ _taskclassname=value;}
			get{return _taskclassname;}
		}
		/// <summary>
		/// 范畴id
		/// </summary>
		public int groupid
		{
            set { _groupid = value; }
            get { return _groupid; }
		}
		
		/// <summary>
		/// 创建人
		/// </summary>
		public int createby
		{
			set{ _createby=value;}
			get{return _createby;}
		}
		
		/// <summary>
		/// 任务备注
		/// </summary>
		public string taskremark
		{
			set{ _taskremark=value;}
			get{return _taskremark;}
		}
		
		/// <summary>
		/// 是否删除
		/// </summary>
		public int isdel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		#endregion Model

	}
}

