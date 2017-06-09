using ND.FluentTaskScheduling.Model;
using ND.FluentTaskScheduling.TaskInterface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：TaskSchedulingDBContext.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-07 11:07:32         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-07 11:07:32          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Repository.Models
{
    public partial class TaskSchedulingDBContext : DbContext
    {
        static TaskSchedulingDBContext()
        {
            Database.SetInitializer<TaskSchedulingDBContext>(null);
        }

        public TaskSchedulingDBContext()
            : base("Name=TaskSchedulingDBContext")
        {
        }

        public DbSet<tb_commandlib> tb_commandlib { get; set; }
        public DbSet<tb_commandlibdetail> tb_commandlibdetail { get; set; }
        public DbSet<tb_commandqueue> tb_commandqueue { get; set; }
        public DbSet<tb_error> tb_error { get; set; }
        public DbSet<tb_log> tb_log { get; set; }
        public DbSet<tb_node> tb_node { get; set; }
        public DbSet<tb_performance> tb_performance { get; set; }
        public DbSet<tb_task> tb_task { get; set; }
        public DbSet<tb_taskgroup> tb_taskgroup { get; set; }
        public DbSet<tb_taskversion> tb_taskversion { get; set; }
        public DbSet<tb_tempdata> tb_tempdata { get; set; }
        public DbSet<tb_user> tb_user { get; set; }

        public DbSet<tb_refreshcommadqueuelog> tb_refreshcommadqueuelog { get; set; }

        public DbSet<tb_commandlog> tb_commandlog { get; set; }

        public DbSet<tb_tasklog> tb_tasklog { get; set; }

        public DbSet<tb_nodeperformance> tb_nodeperformance { get; set; }

        public DbSet<tb_nodemonitor> tb_nodemonitor { get; set; }
       
    }
}
