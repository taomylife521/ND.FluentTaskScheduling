using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using ND.FluentTaskScheduling.TaskInterface;
//**********************************************************************
//
// 文件名称(File Name)：        
// 功能描述(Description)：     
// 作者(Author)：               
// 日期(Create Date)： 2017-04-07 10:56:43         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期: 2017-04-07 10:56:43           
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:  2017-04-07 10:56:43         
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
    public interface ITaskRepository:IRepository<tb_task>
    {
        /// <summary>
        /// 添加任务并返回自增id
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        int AddTask(tb_task task);

        /// <summary>
        /// 根据id 更新
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="fileds"></param>
        void UpdateById(List<int> ids, Dictionary<string, string> fileds);

        /// <summary>
        /// 载入任务分页列表
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="req"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        List<TaskListDto> LoadTaskPageList(out int totalCount, LoadTaskListRequest req, System.Linq.Expressions.Expression<Func<tb_task,string>> orderby = null);
    }
}
