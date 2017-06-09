using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//**********************************************************************
//
// 文件名称(File Name)：AuthUtil.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-15 13:48:46         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-15 13:48:46          
//             修改理由：         
//**********************************************************************

namespace ND.FluentTaskScheduling.Web.App_Start
{
    public class AuthUtil
    {

        private static string GetToken()
        {
            string token = HttpContext.Current.Request.QueryString["Token"];
            if (!String.IsNullOrEmpty(token)) return token;

            var cookie = HttpContext.Current.Request.Cookies["Token"];
            return cookie == null ? String.Empty : cookie.Value;
        }

      
    }
}