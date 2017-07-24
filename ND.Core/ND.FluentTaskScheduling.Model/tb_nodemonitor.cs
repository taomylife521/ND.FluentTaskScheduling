using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：tb_nodemonitor.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-27 13:44:56         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-27 13:44:56          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model
{
    public class tb_nodemonitor
    {
        public tb_nodemonitor()
		{}
		#region Model
		private int _id;
        private int _nodeid=0;
		private string _name="";
		private string _discription="";
		private string _classname="";
        private string _classnamespace = "";
		private int _monitorstatus=0;
		private DateTime _lastmonitortime= Convert.ToDateTime("2099-12-30");
		private int _isdel=0;
		private int _createby=0;
		private DateTime _createtime= DateTime.Now;
        private int _interval = 0;

        /// <summary>
        /// 间隔时间ms单位
        /// </summary>
        public int interval
        {
            set { _interval = value; }
            get { return _interval; }
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
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 监控名称
		/// </summary>
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 监控描述
		/// </summary>
		public string discription
		{
			set{ _discription=value;}
			get{return _discription;}
		}
		/// <summary>
		/// 监控类名
		/// </summary>
		public string classname
		{
			set{ _classname=value;}
			get{return _classname;}
		}
		/// <summary>
		/// 命名空间
		/// </summary>
		public string classnamespace
		{
            set { _classnamespace = value; }
            get { return _classnamespace; }
		}
		/// <summary>
		/// 0-未监控 1-监控中
		/// </summary>
		public int monitorstatus
		{
			set{ _monitorstatus=value;}
			get{return _monitorstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime lastmonitortime
		{
			set{ _lastmonitortime=value;}
			get{return _lastmonitortime;}
		}
		/// <summary>
		/// 是否删除
		/// </summary>
		public int isdel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		/// <summary>
		/// 创建人
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
		#endregion Model
    }
}
