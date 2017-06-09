using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AlarmType.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-07 16:21:53         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-07 16:21:53          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Helper
{
   public enum AlarmType
    {
       /// <summary>
       /// 邮件报警
       /// </summary>
       [Description("邮件报警")]
       Email=0,

       /// <summary>
       /// 短信报警
       /// </summary>
       [Description("短信报警")]
       SMS=1
    }
}
