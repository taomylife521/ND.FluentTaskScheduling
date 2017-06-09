using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：LoadUserListRequest.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-05-09 13:51:44         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-05-09 13:51:44          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.Model.request
{
    /// <summary>
    /// 获取用户列表请求
    /// </summary>
    public class LoadUserListRequest : PageRequestBase
    {
        private string _usercreatetimerange = "1900-01-01/2099-12-30";
        private string _usermobile = "";
        private string _useremail = "";
        private string _userrealname = "";
        private int _adminid = -1;

        /// <summary>
        ///后台用户id
        /// </summary>
        public int AdminId { get { return _adminid; } set { _adminid = value; } }
        /// <summary>
        /// 用户创建时间范围
        /// </summary>
        public string UserCreateTimeRange { get { return _usercreatetimerange; } set { _usercreatetimerange = value; } }

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
    }
}
