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
namespace ND.FluentTaskScheduling.Model
{
	/// <summary>
	/// 任务表
	/// </summary>

	public partial class tb_task
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
		private int _nodeid=0;
		private DateTime _taskcreatetime= Convert.ToDateTime("2099-12-30");
		private DateTime _taskupdatetime= Convert.ToDateTime("2099-12-30");
		private DateTime _tasklaststarttime= Convert.ToDateTime("2099-12-30");
		private DateTime _tasklastendtime= Convert.ToDateTime("2099-12-30");
		private int _taskerrorcount=0;
		private int _taskruncount=0;
        private int _lasttaskstatus = 0;
		private int _createby=0;
		
		private string _taskremark="";
		private DateTime _tasklasterrortime= Convert.ToDateTime("2099-12-30");
		
		private string _taskcron="";
		private int _isdel=0;
        private DateTime _createtime = DateTime.Now;

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
		/// 节点id
		/// </summary>
		public int nodeid
		{
			set{ _nodeid=value;}
			get{return _nodeid;}
		}
		/// <summary>
		/// 任务创建时间
		/// </summary>
		public DateTime taskcreatetime
		{
			set{ _taskcreatetime=value;}
			get{return _taskcreatetime;}
		}
		/// <summary>
		/// 任务更新时间
		/// </summary>
		public DateTime taskupdatetime
		{
			set{ _taskupdatetime=value;}
			get{return _taskupdatetime;}
		}
		/// <summary>
		/// 任务上次开始时间
		/// </summary>
		public DateTime tasklaststarttime
		{
			set{ _tasklaststarttime=value;}
			get{return _tasklaststarttime;}
		}
		/// <summary>
		/// 任务上次结束时间
		/// </summary>
		public DateTime tasklastendtime
		{
			set{ _tasklastendtime=value;}
			get{return _tasklastendtime;}
		}
		/// <summary>
		/// 任务出错次数
		/// </summary>
		public int taskerrorcount
		{
			set{ _taskerrorcount=value;}
			get{return _taskerrorcount;}
		}
		/// <summary>
		/// 任务运行次数
		/// </summary>
		public int taskruncount
		{
			set{ _taskruncount=value;}
			get{return _taskruncount;}
		}
		/// <summary>
		/// 0 -未执行 1-执行中 2-执行完成
		/// </summary>
        public int lasttaskstatus
		{
            set { _lasttaskstatus = value; }
            get { return _lasttaskstatus; }
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
		/// 任务上次出错时间
		/// </summary>
		public DateTime tasklasterrortime
		{
			set{ _tasklasterrortime=value;}
			get{return _tasklasterrortime;}
		}
		
		/// <summary>
		/// 任务所需taskcron表达式
		/// </summary>
		public string taskcron
		{
			set{ _taskcron=value;}
			get{return _taskcron;}
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

