using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：UpdateUserRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-12 17:00:20         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-12 17:00:20          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    public class UpdateUserRequest:RequestBase
    {
        private string _usermobile = "";
        private string _useremail = "";
        private string _userrealname = "";
        private string _userpassword = "";
        private int _adminid = 0;

        /// <summary>
        /// 登录人id
        /// </summary>
        public int AdminId { get { return _adminid; } set { _adminid = value; } }

        /// <summary>
        /// 修改用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户手机
        /// </summary>
        public string UserMobile { get { return _usermobile; } set { _usermobile = value; } }

        /// <summary>
        /// 用户邮件
        /// </summary>
        public string UserEmail { get { return _useremail; } set { _useremail = value; } }

        /// <summary>
        /// 用户真名
        /// </summary>
        public string UserRealName { get { return _userrealname; } set { _userrealname = value; } }


        /// <summary>
        /// 用户密码
        /// </summary>
        public string UserPassword { get { return _userpassword; } set { _userpassword = value; } }
    }
}
