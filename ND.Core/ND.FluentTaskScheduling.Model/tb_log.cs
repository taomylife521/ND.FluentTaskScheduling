/**  版本信息模板在安装目录下，可自行修改。
* tb_log.cs
*
* 功 能： N/A
* 类 名： tb_log
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
	/// tb_log:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>

	public partial class tb_log
	{
		public tb_log()
		{}
		#region Model
		private int _id;
		private string _msg;
		private int _logtype;
		private DateTime _logcreatetime;
		private int _taskid;
		private int _nodeid;
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
		public string msg
		{
			set{ _msg=value;}
			get{return _msg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int logtype
		{
			set{ _logtype=value;}
			get{return _logtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime logcreatetime
		{
			set{ _logcreatetime=value;}
			get{return _logcreatetime;}
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
		public int nodeid
		{
			set{ _nodeid=value;}
			get{return _nodeid;}
		}
		#endregion Model

	}
}

