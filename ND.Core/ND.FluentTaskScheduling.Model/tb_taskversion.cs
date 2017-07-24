/**  版本信息模板在安装目录下，可自行修改。
* tb_version.cs
*
* 功 能： N/A
* 类 名： tb_version
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
	
	public partial class tb_taskversion
	{
		public tb_taskversion()
		{}
		#region Model
		private int _id;
		private int _taskid;
		private int _version;
		private DateTime _versioncreatetime;
		private byte[] _zipfile;
		private string _zipfilename;
        private string _taskparams = "";

        /// <summary>
        /// 任务参数
        /// </summary>
        public string taskparams 
        {
            set { _taskparams = value; }
            get { return _taskparams; }
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
		public int taskid
		{
			set{ _taskid=value;}
			get{return _taskid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int version
		{
			set{ _version=value;}
			get{return _version;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime versioncreatetime
		{
			set{ _versioncreatetime=value;}
			get{return _versioncreatetime;}
		}
		/// <summary>
		/// 压缩文件二进制文件
		/// </summary>
		public byte[] zipfile
		{
			set{ _zipfile=value;}
			get{return _zipfile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string zipfilename
		{
			set{ _zipfilename=value;}
			get{return _zipfilename;}
		}
		#endregion Model

	}
}

