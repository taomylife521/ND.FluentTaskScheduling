/**  版本信息模板在安装目录下，可自行修改。
* tb_tempdata.cs
*
* 功 能： N/A
* 类 名： tb_tempdata
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017-04-05 10:52:25   N/A    初版
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

	public partial class tb_tempdata
	{
		public tb_tempdata()
		{}
		#region Model
		private int _id;
		private int _taskid;
		private string _tempdatajson;
		private DateTime _tempdatalastupdatetime;
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
		public int taskid
		{
			set{ _taskid=value;}
			get{return _taskid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string tempdatajson
		{
			set{ _tempdatajson=value;}
			get{return _tempdatajson;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime tempdatalastupdatetime
		{
			set{ _tempdatalastupdatetime=value;}
			get{return _tempdatalastupdatetime;}
		}
		#endregion Model

	}
}

