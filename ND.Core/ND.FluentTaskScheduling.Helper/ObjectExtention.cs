using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：ObjectExtention.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-13 17:33:51         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-13 17:33:51          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Helper
{
    public static class ObjectExtention
    {
        public static T CloneObj<T>(this T t) where T:new()
        {
            T to = new T();
            var fieldFroms = t.GetType().GetProperties();
            var fieldTos = to.GetType().GetProperties();
            int lenTo = fieldTos.Length;

            for (int i = 0, l = fieldFroms.Length; i < l; i++)
            {
                for (int j = 0; j < lenTo; j++)
                {
                    if (fieldTos[j].Name != fieldFroms[i].Name) continue;
                    fieldTos[j].SetValue(to, fieldFroms[i].GetValue(t, null), null);
                    break;
                }
            }
            return to;
        }

    }
}
