using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.FluentTaskScheduling.Domain.interfacelayer;
using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.TaskInterface;
using ND.FluentTaskScheduling.Model.request;
using ND.FluentTaskScheduling.Model.response;
using System.Linq.Expressions;
//**********************************************************************
//
// 文件名称(File Name)：TaskRepository.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-07 13:36:01         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-07 13:36:01          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Repository
{
    public class TaskRepository:BaseRepository<tb_task>,ITaskRepository
    {
        public  int AddTask(tb_task task)
        {
            try
            {
                Context.Set<tb_task>().Add(task);
                Save();
                return task.id;
            }
            catch(Exception ex)
            {
                return -1;
            }
        }


        public void UpdateById(List<int> ids, Dictionary<string, string> fileds)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dbo.tb_task set ");

            foreach (var item in fileds)
            {
                strSql.Append(item.Key + " = '" + item.Value + "',");

            }
            string sql = strSql.ToString().Substring(0, strSql.Length - 1);
            sql = sql + " where id in (" + string.Join(",",ids.ToArray())+")";

            Context.Database.ExecuteSqlCommand(sql);
        }

        public List<TaskListDto> LoadTaskPageList(out int totalCount, LoadTaskListRequest req, System.Linq.Expressions.Expression<Func<tb_task,string>> orderby = null)
        {
            DateTime starttime = Convert.ToDateTime(req.TaskCreateTimeRange.Split('/')[0]);
            DateTime endtime = Convert.ToDateTime(req.TaskCreateTimeRange.Split('/')[1]).AddDays(1);
            int pageIndex = (req.iDisplayStart / req.iDisplayLength) + 1;
             totalCount = 0;
           var table2 = (
                       from c in Context.tb_task
                        join p in Context.tb_taskversion
                        on c.id equals p.taskid
                        where c.createtime>=starttime && c.createtime<endtime
                        && c.taskschedulestatus == (req.TaskSchduleStatus < 0 ? c.taskschedulestatus : req.TaskSchduleStatus)
                       // && c.taskname.Contains(string.IsNullOrEmpty(req.TaskName) ? c.taskname:req.TaskName)
                        && p.nodeid == (req.NodeId <= 0 ? p.nodeid : req.NodeId)
                        && p.taskrunstatus == (req.TaskExecuteStatus < 0 ? p.taskrunstatus : req.TaskExecuteStatus)
                        && c.id ==(req.TaskId <=0 ?c.id:req.TaskId)
                        && c.createby ==(req.CreateBy<=0 ?c.createby:req.CreateBy)
                        && c.alarmtype ==(req.AlarmType<0?c.alarmtype:req.AlarmType)
                        && c.tasktype ==(req.TaskType<0?c.tasktype:req.TaskType)
                        select new TaskListDto()
                        {
                            Task = c,
                            TaskVersion = p
                        }
                        );
            if(orderby ==null)
            {
                table2 = table2.OrderByDescending(x => x.Task.id);
            }
            else
            {
                table2 = table2.OrderByDescending(x => orderby);
            }

            List<TaskListDto> table = table2.ToList();
            totalCount = table.Count();
            List<TaskListDto> tasklist= table.Skip(req.iDisplayLength * (pageIndex - 1)).Take(req.iDisplayLength).ToList();
            tasklist.ForEach(x =>
            {
                x.TaskVersion.zipfile = null;
                x.Node = Context.tb_node.FirstOrDefault(m => m.id == x.TaskVersion.nodeid);
            });
            return tasklist;
        }

      
    }
}
