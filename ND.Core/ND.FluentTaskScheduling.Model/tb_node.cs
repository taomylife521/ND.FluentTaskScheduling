/**  版本信息模板在安装目录下，可自行修改。
* tb_node.cs
*
* 功 能： N/A
* 类 名： tb_node
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
	/// tb_node:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	
	public partial class tb_node
	{
		public tb_node()
		{}
		#region Model
		private int _id;
		private string _nodename;
        private DateTime _nodelastmodifytime= Convert.ToDateTime("2099-12-30");
		private string _nodeip;
		private DateTime _nodelastupdatetime= Convert.ToDateTime("2099-12-30");
        private DateTime _lastrefreshcommandqueuetime = Convert.ToDateTime("2099-12-30");
		private bool _ifcheckstate= false;
		private string _nodecommanddllversion="";
        private int _alarmtype = 0;
        private string _alarmuserid = "";
        private int _isenablealarm = 0;

        private int _refreshcommandqueuestatus = 0;
        private int _createby = 0;
        private int _nodestatus = 0;
        private int _isdel = 0;
        private int _maxrefreshcommandqueuecount = 0;
        private DateTime _createtime = DateTime.Now;

        private string _nodediscription = "";
        private string _machinename = "";
        private string _performancecollectjson = "";

        private int _lastmaxid = 0;

        private int _ifmonitor = 1;

        /// <summary>
        /// 是否监控
        /// </summary>
        public int ifmonitor
        {
            set { _ifmonitor = value; }
            get { return _ifmonitor; }
        }

        /// <summary>
        /// 节点性能收集json
        /// </summary>
        public string performancecollectjson
        {
            set { _performancecollectjson = value; }
            get { return _performancecollectjson; }
        }

        /// <summary>
        /// 节点机器名称
        /// </summary>
        public string machinename
        {
            set { _machinename = value; }
            get { return _machinename; }
        }

        /// <summary>
        /// 保存上次取得最大id值
        /// </summary>
        public int lastmaxid
        {
            set { _lastmaxid = value; }
            get { return _lastmaxid; }
        }

        /// <summary>
        /// 节点描述
        /// </summary>
        public string nodediscription
        {
            set { _nodediscription = value; }
            get { return _nodediscription; }
        }

        /// <summary>
        /// 最大刷取命令个数
        /// </summary>
        public int maxrefreshcommandqueuecount
        {
            set { _maxrefreshcommandqueuecount = value; }
            get { return _maxrefreshcommandqueuecount; }
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
        /// 上次刷新队列时间
        /// </summary>
        public DateTime lastrefreshcommandqueuetime
        {
            set { _lastrefreshcommandqueuetime = value; }
            get { return _lastrefreshcommandqueuetime; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public int createby
        {
            set { _createby = value; }
            get { return _createby; }
        }

        /// <summary>
        /// 节点状态
        /// </summary>
        public int nodestatus
        {
            set { _nodestatus = value; }
            get { return _nodestatus; }
        }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int isdel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
        /// <summary>
        /// 刷新命令队列状态
        /// </summary>
        public int refreshcommandqueuestatus
        {
            set { _refreshcommandqueuestatus = value; }
            get { return _refreshcommandqueuestatus; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int isenablealarm
        {
            set { _isenablealarm = value; }
            get { return _isenablealarm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string alarmuserid
        {
            set { _alarmuserid = value; }
            get { return _alarmuserid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int alarmtype
        {
            set { _alarmtype = value; }
            get { return _alarmtype; }
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
		/// 
		/// </summary>
		public string nodename
		{
			set{ _nodename=value;}
			get{return _nodename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime nodelastmodifytime
		{
            set { _nodelastmodifytime = value; }
            get { return _nodelastmodifytime; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string nodeip
		{
			set{ _nodeip=value;}
			get{return _nodeip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime nodelastupdatetime
		{
			set{ _nodelastupdatetime=value;}
			get{return _nodelastupdatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool ifcheckstate
		{
			set{ _ifcheckstate=value;}
			get{return _ifcheckstate;}
		}
		/// <summary>
		/// 节点对应命令dll版本
		/// </summary>
		public string nodecommanddllversion
		{
			set{ _nodecommanddllversion=value;}
			get{return _nodecommanddllversion;}
		}
		#endregion Model

	}
}

