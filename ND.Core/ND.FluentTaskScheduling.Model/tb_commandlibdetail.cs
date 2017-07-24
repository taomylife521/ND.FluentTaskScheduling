using Newtonsoft.Json;
/**  版本信息模板在安装目录下，可自行修改。
* tb_commandlibdetail.cs
*
* 功 能： N/A
* 类 名： tb_commandlibdetail
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
	/// tb_commandlibdetail:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    //[Serializable]
	public  class tb_commandlibdetail
	{
		public tb_commandlibdetail()
		{}
		#region Model
       
		private int _id;
      
		private int _commandlibid=0;
       
		private string _commandname="";
      
		private string _commanddescription="";
     
		private string _commandnamespace="";
      
		private string _commandmainclassname="";

        private string _commandparams = "";
     
		private int _createby=0;
     
		private DateTime _createtime= Convert.ToDateTime("2099-12-30");
     
		private int _isdel=0;
        private int _maxexeceptionretrycount = 0;

        /// <summary>
        /// 最大异常重试次数
        /// </summary>

        public int maxexeceptionretrycount
        {
            set { _maxexeceptionretrycount = value; }
            get { return _maxexeceptionretrycount; }
        }

        /// <summary>
        /// 命令参数
        /// </summary>

        public string commandparams 
        {
            set { _commandparams = value; }
            get { return _commandparams; }
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
		/// 命令库id
		/// </summary>
       
		public int commandlibid
		{
			set{ _commandlibid=value;}
			get{return _commandlibid;}
		}
		/// <summary>
		/// 命令名称
		/// </summary>
       
		public string commandname
		{
			set{ _commandname=value;}
			get{return _commandname;}
		}
		/// <summary>
		/// 命令描述
		/// </summary>
       
		public string commanddescription
		{
			set{ _commanddescription=value;}
			get{return _commanddescription;}
		}
		/// <summary>
		/// 命令命名空间
		/// </summary>
       
		public string commandnamespace
		{
			set{ _commandnamespace=value;}
			get{return _commandnamespace;}
		}
		/// <summary>
		/// 命令class类名
		/// </summary>
        
		public string commandmainclassname
		{
			set{ _commandmainclassname=value;}
			get{return _commandmainclassname;}
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


        #region Model2
        //private int _id;
        //private int _commandlibid = 0;
        //private string _commandname = "";
        //private string _commanddescription = "";
        //private string _commandnamespace = "";
        //private string _commandmainclassname = "";
        //private int _createby = 0;
        //private DateTime _createtime = Convert.ToDateTime("2099-12-30");
        //private int _isdel = 0;
        ///// <summary>
        ///// 
        ///// </summary>
        //[JsonProperty("id")]
        //public int id
        //{
        //    set { _id = value; }
        //    get { return _id; }
        //}
        ///// <summary>
        ///// 命令库id
        ///// </summary>
        //[JsonProperty("commandlibid")]
        //public int commandlibid
        //{
        //    set { _commandlibid = value; }
        //    get { return _commandlibid; }
        //}
        ///// <summary>
        ///// 命令名称
        ///// </summary>
        //[JsonProperty("commandname")]
        //public string commandname
        //{
        //    set { _commandname = value; }
        //    get { return _commandname; }
        //}
        ///// <summary>
        ///// 命令描述
        ///// </summary>
        //[JsonProperty("commanddescription")]
        //public string commanddescription
        //{
        //    set { _commanddescription = value; }
        //    get { return _commanddescription; }
        //}
        ///// <summary>
        ///// 命令命名空间
        ///// </summary>
        //[JsonProperty("commandnamespace")]
        //public string commandnamespace
        //{
        //    set { _commandnamespace = value; }
        //    get { return _commandnamespace; }
        //}
        ///// <summary>
        ///// 命令class类名
        ///// </summary>
        //[JsonProperty("commandmainclassname")]
        //public string commandmainclassname
        //{
        //    set { _commandmainclassname = value; }
        //    get { return _commandmainclassname; }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //[JsonProperty("createby")]
        //public int createby
        //{
        //    set { _createby = value; }
        //    get { return _createby; }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //[JsonProperty("createtime")]
        //public DateTime createtime
        //{
        //    set { _createtime = value; }
        //    get { return _createtime; }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //[JsonProperty("isdel")]
        //public int isdel
        //{
        //    set { _isdel = value; }
        //    get { return _isdel; }
        //}
        #endregion Model

	}
}

