using ND.FluentTaskScheduling.Model;
//**********************************************************************
//
// 文件名称(File Name)：        
// 功能描述(Description)：     
// 作者(Author)：               
// 日期(Create Date)： 2017-04-07 10:55:21         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期: 2017-04-07 10:55:21           
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:  2017-04-07 10:55:21         
//             修改理由：         
//
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.FluentTaskScheduling.Domain.interfacelayer
{
    public interface INodeRepository:IRepository<tb_node>
    {
       void UpdateNodeById(int id,Dictionary<string,string> fileds);
    }
}
