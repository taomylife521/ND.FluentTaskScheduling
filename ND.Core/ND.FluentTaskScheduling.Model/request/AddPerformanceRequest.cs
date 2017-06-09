using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AddPerformanceRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-02 15:53:23         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-02 15:53:23          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    public class AddPerformanceRequest :RequestBase
    {
        /// <summary>
        /// 节点id
        /// </summary>
        [JsonProperty("nodeid")]
        public int NodeId { get; set; }

        /// <summary>
        /// 任务id
        /// </summary>
        [JsonProperty("taskid")]
        public int TaskId { get; set; }

        /// <summary>
        /// cpu
        /// </summary>
        [JsonProperty("cpu")]
        public string Cpu { get; set; }

        /// <summary>
        ///memory
        /// </summary>
        [JsonProperty("memory")]
        public string Memory { get; set; }

        /// <summary>
        ///installdirsize磁盘大小
        /// </summary>
        [JsonProperty("installdirsize")]
        public string InstallDirsize { get; set; }

       /// <summary>
       /// 更新时间
       /// </summary>
         [JsonProperty("lastupdatetime")]
        public string Lastupdatetime { get; set; }

        

    }
}
