using ND.FluentTaskScheduling.Domain.interfacelayer;
using ND.FluentTaskScheduling.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：NodeMonitorRepository.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-27 13:45:26         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-27 13:45:26          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Repository
{
    public class NodeMonitorRepository : BaseRepository<tb_nodemonitor>, INodeMonitorRepository
    {
        public void UpdateById(List<int> ids, Dictionary<string, string> fileds)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dbo.tb_nodemonitor set ");

            foreach (var item in fileds)
            {
                strSql.Append(item.Key + " = '" + item.Value + "',");

            }
            string sql = strSql.ToString().Substring(0, strSql.Length - 1);
            sql = sql + " where id in (" + string.Join(",", ids.ToArray()) + ")";

            Context.Database.ExecuteSqlCommand(sql);
        }
    }
}
