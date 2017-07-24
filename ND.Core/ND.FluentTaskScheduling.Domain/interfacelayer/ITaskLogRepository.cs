using ND.FluentTaskScheduling.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：ICommandLogRepository.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-12 15:52:23         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-12 15:52:23          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Domain.interfacelayer
{
    public interface ITaskLogRepository : IRepository<tb_tasklog>
    {
        int AddTaskLog(tb_tasklog task);
    }
}
