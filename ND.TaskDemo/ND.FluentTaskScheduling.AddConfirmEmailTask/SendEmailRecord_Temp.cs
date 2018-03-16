/**  版本信息模板在安装目录下，可自行修改。
* SendEmailRecord_Temp.cs
*
* 功 能： N/A
* 类 名： SendEmailRecord_Temp
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2017-10-28 16:29:14   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace ND.FluentTaskScheduling.AddConfirmEmailTask
{
	/// <summary>
	/// 已发送邮件记录表
	/// </summary>
	[Serializable]
	public partial class SendEmailRecord_Temp
	{
		public SendEmailRecord_Temp()
		{}
		#region Model
		private int _id;
		private long? _orderid;
		private long? _hotelid;
		private DateTime? _sendtime=Convert.ToDateTime("1900-01-01");
        private DateTime? _replytime = Convert.ToDateTime("1900-01-01");
		private string _content;
		private int? _supplierid;
		private string _suppliername;
		private string _sendperson;
		private string _recipients;
		private bool _isreply= false;
		private bool _replyresult= false;
		private int? _type=1;
		private bool _isdel= false;
		private bool _issend= false;
		private string _emailtitle="";
		private int? _sendcount=0;
		private int? _emailtype=1;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 订单号
		/// </summary>
		public long? OrderId
		{
			set{ _orderid=value;}
			get{return _orderid;}
		}
		/// <summary>
		/// 酒店ID
		/// </summary>
		public long? HotelId
		{
			set{ _hotelid=value;}
			get{return _hotelid;}
		}
		/// <summary>
		/// 发件时间
		/// </summary>
		public DateTime? SendTime
		{
			set{ _sendtime=value;}
			get{return _sendtime;}
		}
		/// <summary>
		/// 回复时间
		/// </summary>
		public DateTime? ReplyTime
		{
			set{ _replytime=value;}
			get{return _replytime;}
		}
		/// <summary>
		/// 邮件内容
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 供应商编号
		/// </summary>
		public int? SupplierId
		{
			set{ _supplierid=value;}
			get{return _supplierid;}
		}
		/// <summary>
		/// 供应商名称
		/// </summary>
		public string SupplierName
		{
			set{ _suppliername=value;}
			get{return _suppliername;}
		}
		/// <summary>
		/// 发件人
		/// </summary>
		public string SendPerson
		{
			set{ _sendperson=value;}
			get{return _sendperson;}
		}
		/// <summary>
		/// 收件人
		/// </summary>
		public string Recipients
		{
			set{ _recipients=value;}
			get{return _recipients;}
		}
		/// <summary>
		/// 是否回复 0未回复 1已回复  默认0
		/// </summary>
		public bool IsReply
		{
			set{ _isreply=value;}
			get{return _isreply;}
		}
		/// <summary>
		/// 回复结果  0没房 1有房 默认0
		/// </summary>
		public bool ReplyResult
		{
			set{ _replyresult=value;}
			get{return _replyresult;}
		}
		/// <summary>
		/// 类型  1酒店  2旅游
		/// </summary>
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 是否删除 0正常 1删除
		/// </summary>
		public bool IsDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		/// <summary>
		/// 0-未发送 1-已发送
		/// </summary>
		public bool IsSend
		{
			set{ _issend=value;}
			get{return _issend;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmailTitle
		{
			set{ _emailtitle=value;}
			get{return _emailtitle;}
		}
		/// <summary>
		/// 发送次数
		/// </summary>
		public int? SendCount
		{
			set{ _sendcount=value;}
			get{return _sendcount;}
		}
		/// <summary>
		/// 0-供应商询房邮件 1-向供应商发送入住确认邮件
		/// </summary>
		public int? EmailType
		{
			set{ _emailtype=value;}
			get{return _emailtype;}
		}
		#endregion Model

	}
}

