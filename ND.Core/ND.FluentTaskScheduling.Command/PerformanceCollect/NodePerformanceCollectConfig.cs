using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NodePerformanceCollectConfig.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-16 13:50:44         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-16 13:50:44          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Core.PerformanceCollect
{
    [Serializable]
   public class NodePerformanceCollectConfig
    {
        public string CollectName { get; set; }
        public string CategoryName { get; set; }
        public string CounterName { get; set; }
        public string InstanceName { get; set; }
        public CollectType CollectType { get; set; }
        public ContrastWarningValue MoreThanWarningValue { get; set; }
        public ContrastWarningValue LessThanWarningValue { get; set; }
        public ContrastWarningValue EqualWarningValue { get; set; }
    }

   public enum CollectType
   {
       [Description("系统")]
       System,
       [Description("自定义")]
       Custom
   }

   public class ContrastWarningValue
   {
       public double Value { get; set; }
       public bool IsWarning { get; set; }
   }
}
