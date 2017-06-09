using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadTaskIdListResponse.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-26 10:41:45         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-26 10:41:45          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.response
{
    public class LoadTaskIdListResponse
    {
        public LoadTaskIdListResponse()
        {
            TaskIdList = new List<int>();
        }
        public List<int> TaskIdList { get; set; }
    }
}
