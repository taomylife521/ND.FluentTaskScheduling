/**  版本信息模板在安装目录下，可自行修改。
* tb_commandqueue.cs
*
* 功 能： N/A
* 类 名： tb_commandqueue
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
	/// tb_commandqueue:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	
	public partial class tb_commandqueue
	{
        public tb_commandqueue()
        { }
        #region Model
        private int _id;
        private string _commandmainclassname;
        private int _commanddetailid = 0;
        private int _commandstate = 0;
        private int _taskid;
        private int _taskversionid;
       
        private int _nodeid;
        private string _commandparams = "";
        private int _createby = 0;
        private DateTime _createtime = DateTime.Now;
        private int _isdel = 0;
        private int _failedcount = 0;


      

        /// <summary>
        /// 任务版本id
        /// </summary>
        public int taskversionid
        {
            set { _taskversionid = value; }
            get { return _taskversionid; }
        }

        /// <summary>
        /// 失败次数
        /// </summary>
        public int failedcount
        {
            set { _failedcount = value; }
            get { return _failedcount; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 命令名，参考代码枚举
        /// </summary>
        public string commandmainclassname
        {
            set { _commandmainclassname = value; }
            get { return _commandmainclassname; }
        }
        /// <summary>
        /// 命令详情id
        /// </summary>
        public int commanddetailid
        {
            set { _commanddetailid = value; }
            get { return _commanddetailid; }
        }
        /// <summary>
        /// 命令执行状态，参考代码枚举
        /// </summary>
        public int commandstate
        {
            set { _commandstate = value; }
            get { return _commandstate; }
        }
        /// <summary>
        /// 任务id
        /// </summary>
        public int taskid
        {
            set { _taskid = value; }
            get { return _taskid; }
        }
        /// <summary>
        /// 节点id
        /// </summary>
        public int nodeid
        {
            set { _nodeid = value; }
            get { return _nodeid; }
        }
        /// <summary>
        /// 当前执行命令所需参数
        /// </summary>
        public string commandparams
        {
            set { _commandparams = value; }
            get { return _commandparams; }
        }
        /// <summary>
        /// 创建人id
        /// </summary>
        public int createby
        {
            set { _createby = value; }
            get { return _createby; }
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
        /// 0-不删除 1-删除
        /// </summary>
        public int isdel
        {
            set { _isdel = value; }
            get { return _isdel; }
        }
        #endregion Model

	}
}

