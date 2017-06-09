using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//**********************************************************************
//
// 文件名称(File Name)：RegexValidator.CS        
// 功能描述(Description)：     
// 作者(Author)：Administrator               
// 日期(Create Date)： 2016/1/28 10:19:01         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/1/28 10:19:01          
//             修改理由：         
//**********************************************************************

namespace ND.FluentTaskScheduling.Model.request.validator
{
    public class RegexValidator
    {
        /// <summary>
        /// 身份证
        /// </summary>
        public static readonly string IDCard = @"^(^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$)|(^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[Xx])$)$";

        /// <summary>
        /// 港澳通行证
        /// </summary>
        public static readonly string PassCard = @"^[HMhm]{1}([0-9]{10}|[0-9]{8})$";

        /// <summary>
        /// 护照
        /// </summary>
        public static readonly string Passport = @"^[a-zA-Z0-9]{5,17}$";

        /// <summary>
        /// 台胞证
        /// </summary>
        public static readonly string Taiwan =@"(^[0-9]{8}$)|(^[0-9]{10}$)";

        /// <summary>
        /// 手机号
        /// </summary>
        public static readonly string Mobile = @"^1\d{10}$";
        
        /// <summary>
        /// 航空公司二字码
        /// </summary>
        public static readonly string AirlineCode = @"^[1-9a-zA-Z]{2}$";

        /// <summary>
        /// 舱位码
        /// </summary>
        public static readonly string SeatCode = @"^[A-Z]{1}([0-9]{1})?(\,[A-Z]{1}([0-9]{1})?)*$";
        

        /// <summary>
        /// 机场三字码或城市三字码
        /// </summary>
        public static readonly string CityCode = @"^[a-zA-Z]{3}$";

        /// <summary>
        /// 只能为数字或字符串
        /// </summary>
        public static readonly string NumberOrLetter = @"^[A-Za-z0-9]+$";

        /// <summary>
        /// 验证日期格式 yyyy-MM-dd
        /// </summary>
        public static readonly string Date = @"^\d{4}-\d{2}-\d{2}$";

        /// <summary>
        /// 只匹配0和1
        /// </summary>
        public static readonly string OnlyZeroOrOne = @"^[0-1]{1}$";

        /// <summary>
        /// 只能输入数字和小数点
        /// </summary>

        public static readonly string OnlyNumberAndPoint=@"^\d+(\.\d+)?$";
    }
}