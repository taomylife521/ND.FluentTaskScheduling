using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：RequestBase.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-07 15:29:50         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-07 15:29:50          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model
{
    public enum Source
    {
        Web=0,
        Node=1
    }
    public class RequestBase
    {
        /// <summary>
        /// 来源
        /// </summary>
         [JsonProperty("source")]
        public Source Source { get; set; }
    }

    public class PageRequestBase:RequestBase
    {
        private int _iDisplayLength = 9999999;
        private int _sEcho = 1;
        private int _iDisplayStart = 0;
        /// <summary>
        /// 每页显示条数
        /// </summary>
        [JsonProperty("idisplaylength")]
        public int iDisplayLength { get { return _iDisplayLength; } set { _iDisplayLength = value; } }

        /// <summary>
        /// 记录操作的次数
        /// </summary>
        [JsonProperty("secho")]
        public int sEcho { get { return _sEcho; } set { _sEcho = value; } }

        /// <summary>
        /// 起始条数
        /// </summary>
        [JsonProperty("idisplaystart")]
        public int iDisplayStart { get { return _iDisplayStart; } set { _iDisplayStart = value; } }
    }
}
