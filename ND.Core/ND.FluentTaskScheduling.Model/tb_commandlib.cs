/**  版本信息模板在安装目录下，可自行修改。
* tb_commandlib.cs
*
* 功 能： N/A
* 类 名： tb_commandlib
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017-04-05 10:52:23   N/A    初版
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
	/// tb_commandlib:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	
	public partial class tb_commandlib
	{
		public tb_commandlib()
		{}
		#region Model
		private int _id;
		private string _commandfilename="";
		private string _commandversion="";
		private string _commandsrcfilepath="";
		private string _commandsavefilepath="";
		private int _createby;
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
		/// 命令文件名称
		/// </summary>
		public string commandfilename
		{
			set{ _commandfilename=value;}
			get{return _commandfilename;}
		}
		/// <summary>
		/// 命令版本信息
		/// </summary>
		public string commandversion
		{
			set{ _commandversion=value;}
			get{return _commandversion;}
		}
		/// <summary>
		/// 命令dll本地path
		/// </summary>
		public string commandsrcfilepath
		{
			set{ _commandsrcfilepath=value;}
			get{return _commandsrcfilepath;}
		}
		/// <summary>
		/// 命令保存文件路径,根据此路径找寻dll
		/// </summary>
		public string commandsavefilepath
		{
			set{ _commandsavefilepath=value;}
			get{return _commandsavefilepath;}
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

