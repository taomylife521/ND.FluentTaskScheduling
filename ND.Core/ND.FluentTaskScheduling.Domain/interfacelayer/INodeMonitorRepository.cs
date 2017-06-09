using ND.FluentTaskScheduling.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：INodeMonitorRepository.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-27 13:42:50         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-27 13:42:50          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Domain.interfacelayer
{
    public interface INodeMonitorRepository:IRepository<tb_nodemonitor>
    {
        /// <summary>
        /// 根据id 更新
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="fileds"></param>
        void UpdateById(List<int> ids, Dictionary<string, string> fileds);
    }
}
