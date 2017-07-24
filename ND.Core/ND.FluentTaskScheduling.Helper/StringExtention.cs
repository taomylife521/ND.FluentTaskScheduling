using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：StringExtention.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-07 16:36:20         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-07 16:36:20          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Helper
{
    public static class StringExtention
    {
        /// <summary>
        /// 转换成泛型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <param name="splitchar"></param>
        /// <returns></returns>
        public static List<int> ConvertToList(this string str, char splitchar = ',')
        {
            List<int> lst = new List<int>();
            str.Split(splitchar).ToList().ForEach(x =>
            {
                lst.Add(Convert.ToInt32(x));
            });

            return lst;
        }
    }
}
