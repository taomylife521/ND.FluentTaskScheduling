using System;
namespace ND.FluentTaskScheduling.CrawlKeGui.DAL
{
	/// <summary>
	/// 航空公司规则表
	/// </summary>
	[Serializable]
	public partial class FlightAirRule
	{
		public FlightAirRule()
		{}
		#region Model
		private string _id="";
		private string _airline="";
		private string _seatclass="";
		private string _returnn="";
        private string _change = "";
		private string _endorsement="";
		private string _returnndis="";
		private bool _isdel= false;
		private int _createby=0;
		private DateTime _createtime= DateTime.Now;
		/// <summary>
		/// 
		/// </summary>
		public string id
		{
			set{ _id=value;}
			get{return _id;}
		}

        /// <summary>
        /// 改期规定
        /// </summary>
        public string change
        {
            set { _change = value; }
            get { return _change; }
        }
		/// <summary>
		/// 航空公司代码
		/// </summary>
		public string airline
		{
			set{ _airline=value;}
			get{return _airline;}
		}
		/// <summary>
		/// 舱位码
		/// </summary>
		public string seatclass
		{
			set{ _seatclass=value;}
			get{return _seatclass;}
		}
		/// <summary>
		/// 退票规定
		/// </summary>
		public string returnn
		{
			set{ _returnn=value;}
			get{return _returnn;}
		}
		/// <summary>
		/// 签转规定
		/// </summary>
		public string endorsement
		{
			set{ _endorsement=value;}
			get{return _endorsement;}
		}
		/// <summary>
		/// 退票说明 例如:10-2-50 起飞2小时前收10， 起飞俩小时候收50
		/// </summary>
		public string returnndis
		{
			set{ _returnndis=value;}
			get{return _returnndis;}
		}
		/// <summary>
		/// 是否删除  0：未删除； 1：删除
		/// </summary>
		public bool isdel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		/// <summary>
		/// 创建人Id
		/// </summary>
		public int createby
		{
			set{ _createby=value;}
			get{return _createby;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime createtime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

