using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.FluentTaskScheduling.Domain.interfacelayer;
using ND.FluentTaskScheduling.Model;
//**********************************************************************
//
// 文件名称(File Name)：NodeRepository.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-07 13:34:00         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-07 13:34:00          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Repository
{
    public class NodeRepository:BaseRepository<tb_node>,INodeRepository
    {
        public void UpdateNodeById(int id,Dictionary<string,string> fileds)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update dbo.tb_node set ");
           
            foreach (var item in fileds)
            {
                strSql.Append(item.Key + " = '" + item.Value + "',");
                
            }
          string sql=  strSql.ToString().Substring(0, strSql.Length - 1);
          sql = sql + " where id=" + id;
           
            Context.Database.ExecuteSqlCommand(sql);
        }
    }
}
