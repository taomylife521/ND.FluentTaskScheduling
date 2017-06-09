/**  版本信息模板在安装目录下，可自行修改。
* tb_taskgroup.cs
*
* 功 能： N/A
* 类 名： tb_taskgroup
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
	
	public partial class tb_taskgroup
	{
		public tb_taskgroup()
		{}
		#region Model
		private int _id;
		private string _groupname="";
		private int _createby=0;
		private DateTime _createtime;
		private int _isdel=0;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 任务范畴名称
		/// </summary>
		public string groupname
		{
			set{ _groupname=value;}
			get{return _groupname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int createby
		{
			set{ _createby=value;}
			get{return _createby;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime createtime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int isdel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		#endregion Model

	}
}

