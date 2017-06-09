//**********************************************************************
//
// 文件名称(File Name)：        
// 功能描述(Description)：     
// 作者(Author)：               
// 日期(Create Date)： 2017-04-07 10:46:52         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期: 2017-04-07 10:46:52           
//             修改理由：         
//
//       R2:
//             修改作者:          
//             修改日期:  2017-04-07 10:46:52         
//             修改理由：         
//
//**********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ND.FluentTaskScheduling.Domain.interfacelayer
{
   public interface IRepository<T> where T:class
    {
       T FindSingle(Expression<Func<T, bool>> exp = null);

       IQueryable<T> Find(Expression<Func<T, bool>> exp = null);

       IQueryable<T> Find(out int totalCount, int pageindex = 1, int pagesize = 10, System.Linq.Expressions.Expression<Func<T, string>> orderby=null,
            Expression<Func<T, bool>> exp = null);

       int GetCount(Expression<Func<T, bool>> exp = null);

       void Add(T entity);

       /// <summary>
       /// 更新一个实体的所有属性
       /// </summary>
       void Update(T entity);

       void Delete(T entity);

       /// <summary>
       /// 批量更新
       /// </summary>
       void Update(Expression<Func<T, bool>> exp, T entity);

       /// <summary>
       /// 批量删除
       /// </summary>
       void Delete(Expression<Func<T, bool>> exp);

       void Save();
    }
}
