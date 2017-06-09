using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：RoleName.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-12 17:29:33         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-12 17:29:33          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.enums
{
    public enum RoleName
    {
        [Description("管理员")]
        Manger=0,

        [Description("开发人员")]
        Developer=1
    }
}
