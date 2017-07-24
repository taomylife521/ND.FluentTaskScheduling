using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：PageInfoResponse.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-04 13:21:02         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-04 13:21:02          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.response
{
    public class PageInfoResponse<T>
    {
        public int sEcho { get; set; }

        /// <summary>
        /// 过滤前总条数
        /// </summary>
        public int iTotalRecords { get; set; }

        /// <summary>
        /// 过滤后总条数
        /// </summary>
        public int iTotalDisplayRecords { get; set; }

        public T aaData { get; set; }
    }
}
