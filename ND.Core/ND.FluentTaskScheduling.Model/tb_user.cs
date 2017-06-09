/**  版本信息模板在安装目录下，可自行修改。
* tb_user.cs
*
* 功 能： N/A
* 类 名： tb_user
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
	public partial class tb_user
	{
		public tb_user()
		{}
		#region Model
		private int _id;
        private string _userpassword;
		private string _username;
		private int _userrole;
		private DateTime _userupdatetime;
        private string _usermobile;
		private string _useremail;
        private string _realname;
        private int _createby = 0;
        private int _isdel = 0; 
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
        /// 创建人
        /// </summary>
        public int createby
        {
            set { _createby = value; }
            get { return _createby; }
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
        /// 员工姓名
        /// </summary>
        public string realname
        {
            set { _realname = value; }
            get { return _realname; }
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
		/// 员工密码
		/// </summary>
        public string userpassword
		{
            set { _userpassword = value; }
            get { return _userpassword; }
		}
		/// <summary>
		/// 登录账户
		/// </summary>
		public string username
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 员工角色，查看代码枚举：开发人员，管理员
		/// </summary>
		public int userrole
		{
			set{ _userrole=value;}
			get{return _userrole;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime userupdatetime
		{
            set { _userupdatetime = value; }
            get { return _userupdatetime; }
		}
		/// <summary>
		/// 员工手机号码
		/// </summary>
		public string usermobile
		{
            set { _usermobile = value; }
            get { return _usermobile; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string useremail
		{
			set{ _useremail=value;}
			get{return _useremail;}
		}
		#endregion Model

	}
}

