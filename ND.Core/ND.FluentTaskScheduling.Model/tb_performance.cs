/**  版本信息模板在安装目录下，可自行修改。
* tb_performance.cs
*
* 功 能： N/A
* 类 名： tb_performance
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
	/// tb_performance:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	
	public partial class tb_performance
	{
		public tb_performance()
		{}
		#region Model
		private int _id;
		private int _nodeid=0;
		private int _taskid;
		private double _cpu;
        private double _memory;
        private double _installdirsize;
		private DateTime _lastupdatetime= DateTime.Now;
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
		public int nodeid
		{
			set{ _nodeid=value;}
			get{return _nodeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int taskid
		{
			set{ _taskid=value;}
			get{return _taskid;}
		}
		/// <summary>
		/// 
		/// </summary>
        public double cpu
		{
			set{ _cpu=value;}
			get{return _cpu;}
		}
		/// <summary>
		/// 
		/// </summary>
        public double memory
		{
			set{ _memory=value;}
			get{return _memory;}
		}
		/// <summary>
		/// 
		/// </summary>
        public double installdirsize
		{
			set{ _installdirsize=value;}
			get{return _installdirsize;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime lastupdatetime
		{
			set{ _lastupdatetime=value;}
			get{return _lastupdatetime;}
		}
		#endregion Model

	}
}

