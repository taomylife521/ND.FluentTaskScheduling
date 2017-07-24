using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.FluentTaskScheduling.Domain.interfacelayer;
using ND.FluentTaskScheduling.Model;
//**********************************************************************
//
// 文件名称(File Name)：TempDataRepository.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-07 13:37:36         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-07 13:37:36          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Repository
{
    public class TempDataRepository:BaseRepository<tb_tempdata>,ITempDataRepository
    {
    }
}
