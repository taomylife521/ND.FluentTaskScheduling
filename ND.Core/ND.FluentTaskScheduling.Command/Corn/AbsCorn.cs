using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AbsCorn.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-06 15:27:16         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-06 15:27:16          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Command.Corn
{
    public class AbsCorn
    {
         public string Corn;
         public AbsCorn(string corn)
        {
            Corn = corn;
        }

        public virtual void Parse()
        { }

        protected virtual T ParseCmd<T>(string name, string cmd)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(cmd))
                { 
                    if(typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition()== typeof(Nullable<>))
                        return (T)Convert.ChangeType(cmd, Nullable.GetUnderlyingType(typeof(T)));
                    else
                        return (T)Convert.ChangeType(cmd, typeof(T));
                }
                return default(T);
            }
            catch (Exception exp)
            {
                throw new Exception("Corn表达式解析失败:" + name + " corn:" + Corn);
            }
        }
    }
}
