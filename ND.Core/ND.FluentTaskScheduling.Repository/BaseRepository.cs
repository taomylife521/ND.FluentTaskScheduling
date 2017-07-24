using ND.FluentTaskScheduling.Domain.interfacelayer;
using ND.FluentTaskScheduling.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using System.Linq.Expressions;

//**********************************************************************
//
// 文件名称(File Name)：BaseRepository.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-07 11:48:58         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-07 11:48:58          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Repository
{
   public class BaseRepository<T>:IRepository<T> where T:class
    {
       
       protected TaskSchedulingDBContext Context = new TaskSchedulingDBContext();

       public T FindSingle(System.Linq.Expressions.Expression<Func<T, bool>> exp = null)
       {
           return Context.Set<T>().AsNoTracking().FirstOrDefault(exp);
       }

       public IQueryable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> exp = null)
       {
           return Filter(exp);
       }

       public IQueryable<T> Find(out int totalCount, int pageindex = 1, int pagesize = 10, System.Linq.Expressions.Expression<Func<T, string>> orderby = null, System.Linq.Expressions.Expression<Func<T, bool>> exp = null)
       {
           totalCount = 0;
           if (pageindex < 1) pageindex = 1;

            totalCount = Filter(exp).Count();
           if(orderby ==null)
           {
               return Filter(exp).Skip(pagesize * (pageindex - 1)).Take(pagesize);
           }
           return Filter(exp).OrderByDescending(orderby).Skip(pagesize * (pageindex - 1)).Take(pagesize);
       }

       public int GetCount(System.Linq.Expressions.Expression<Func<T, bool>> exp = null)
       {
           return Filter(exp).Count();
       }

       public void Add(T entity)
       {
           Context.Set<T>().Add(entity);
           Save();
       }

       //public T Add(T entity)
       //{
       //    Context.Set<T>().Add(entity);
       //    Save();
       //    return entity;
       //}

       public void Update(T entity)
       {
           
           var entry = this.Context.Entry(entity);
           //todo:如果状态没有任何更改，会报错
           entry.State = EntityState.Modified;
           

           Save();
       }

       public void Delete(T entity)
       {
           Context.Set<T>().Remove(entity);
           Save();
       }

       public void Update(System.Linq.Expressions.Expression<Func<T, bool>> exp, T entity)
       {
           //TODO: 暂时有问题，EntityFramework.Extension的Update必须有new操作
           Context.Set<T>().Where(exp).Update(u => entity);
       }

       public void Delete(System.Linq.Expressions.Expression<Func<T, bool>> exp)
       {
           Context.Set<T>().Where(exp).Delete();
       }

       public void Save()
       {
           try
           {
               Context.SaveChanges();
           }
           catch(Exception ex)
           {
               string msg = ex.Message;
           }
       }

       private IQueryable<T> Filter(Expression<Func<T, bool>> exp)
       {
           var dbSet = Context.Set<T>().AsNoTracking().AsQueryable();
           if (exp != null)
               dbSet = dbSet.Where(exp).AsNoTracking();
           return dbSet;
       }
    }
}
