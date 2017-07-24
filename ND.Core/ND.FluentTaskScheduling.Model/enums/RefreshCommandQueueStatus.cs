using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：RefreshCommandQueueStatus.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-11 13:43:14         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-11 13:43:14          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.enums
{
    /// <summary>
    /// 节点刷新命令队列状态
    /// </summary>
    public enum RefreshCommandQueueStatus
    {
        /// <summary>
        /// 未监听
        /// </summary>
         [Description("未监听")]
        NoRefresh=0,

        /// <summary>
        /// 监听中
        /// </summary>
         [Description("监听中")]
         Refreshing = 1,

         ///// <summary>
         ///// 刷新中
         ///// </summary>
         //[Description("停止刷新")]
         //Refreshing = 1,

        ///// <summary>
        ///// 执行成功
        ///// </summary>
        // [Description("执行成功")]
        //ExecutSucess = 2,

        ///// <summary>
        ///// 执行错误
        //  [Description("执行错误")]
        //ExecutError = 3,

      
    }
}
