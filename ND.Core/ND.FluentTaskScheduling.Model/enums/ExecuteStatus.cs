using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskRunStatus.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-14 13:17:06         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-14 13:17:06          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.enums
{
    /// <summary>
    /// 任务运行状态
    /// </summary>
    public enum ExecuteStatus
    {
        

        [Description("未执行")]
        NoExecute= 0,

        [Description("执行中")]
        Executing=1,//-执行中 

        [Description("执行成功")]
        ExecuteSucess=2,//-执行成功

        [Description("执行失败")]
        ExecuteFailed=3,//-执行失败

        [Description("执行异常")]
        ExecuteException = 4,//-执行异常

       

       
    }
}
