using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：tb_nodeperformance.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-16 14:23:10         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-16 14:23:10          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model
{
    public class tb_nodeperformance
    {
        public tb_nodeperformance()
        { }
        #region Model
        private int _id;
        private int _nodeid = 0;
        private double _cpu = 0;
        private double _memory = 0;
        private double _installdirsize = 0;
        private DateTime _lastupdatetime = DateTime.Now;
        private double _networkupload = 0;
        private double _networkdownload = 0;
        private double _ioread = 0;
        private double _iowrite = 0;
        private double _iisrequest = 0;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 节点编号
        /// </summary>
        public int nodeid
        {
            set { _nodeid = value; }
            get { return _nodeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double cpu
        {
            set { _cpu = value; }
            get { return _cpu; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double memory
        {
            set { _memory = value; }
            get { return _memory; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double installdirsize
        {
            set { _installdirsize = value; }
            get { return _installdirsize; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime lastupdatetime
        {
            set { _lastupdatetime = value; }
            get { return _lastupdatetime; }
        }
        /// <summary>
        /// 网络发送
        /// </summary>
        public double networkupload
        {
            set { _networkupload = value; }
            get { return _networkupload; }
        }
        /// <summary>
        /// 网络下载
        /// </summary>
        public double networkdownload
        {
            set { _networkdownload = value; }
            get { return _networkdownload; }
        }
        /// <summary>
        /// io读
        /// </summary>
        public double ioread
        {
            set { _ioread = value; }
            get { return _ioread; }
        }
        /// <summary>
        /// io 写
        /// </summary>
        public double iowrite
        {
            set { _iowrite = value; }
            get { return _iowrite; }
        }
        /// <summary>
        /// iis请求
        /// </summary>
        public double iisrequest
        {
            set { _iisrequest = value; }
            get { return _iisrequest; }
        }
        #endregion Model
    }
}
