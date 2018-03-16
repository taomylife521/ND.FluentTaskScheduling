using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：AlarmHelper.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2017-04-07 16:48:59         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2017-04-07 16:48:59          
//             修改理由：         
//**********************************************************************
namespace ND.FluentTaskScheduling.TimingSendEmailTask
{

    #region Email
    public class Email
    {
        /// <summary>
        /// 发送者
        /// </summary>
        public string mailFrom { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string[] mailToArray { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        public string[] mailCcArray { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string mailSubject { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string mailBody { get; set; }

        /// <summary>
        /// 发件人密码
        /// </summary>
        public string mailPwd { get; set; }

        /// <summary>
        /// SMTP邮件服务器
        /// </summary>
        public string host { get; set; }

        /// <summary>
        /// 正文是否是html格式
        /// </summary>
        public bool isbodyHtml { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string[] attachmentsPath { get; set; }

        public bool Send()
        {
            //使用指定的邮件地址初始化MailAddress实例
            MailAddress maddr = new MailAddress(mailFrom);
            //初始化MailMessage实例
            MailMessage myMail = new MailMessage();


            //向收件人地址集合添加邮件地址
            if (mailToArray != null)
            {
                for (int i = 0; i < mailToArray.Length; i++)
                {
                    myMail.To.Add(mailToArray[i].ToString());
                }
            }

            //向抄送收件人地址集合添加邮件地址
            if (mailCcArray != null)
            {
                for (int i = 0; i < mailCcArray.Length; i++)
                {
                    myMail.CC.Add(mailCcArray[i].ToString());
                }
            }
            //发件人地址
            myMail.From = maddr;

            //电子邮件的标题
            myMail.Subject = mailSubject;

            //电子邮件的主题内容使用的编码
            myMail.SubjectEncoding = Encoding.UTF8;

            //电子邮件正文
            myMail.Body = mailBody;

            //电子邮件正文的编码
            myMail.BodyEncoding = Encoding.Default;

            myMail.Priority = MailPriority.High;

            myMail.IsBodyHtml = isbodyHtml;

            //在有附件的情况下添加附件
            try
            {
                if (attachmentsPath != null && attachmentsPath.Length > 0)
                {
                    Attachment attachFile = null;
                    foreach (string path in attachmentsPath)
                    {
                        attachFile = new Attachment(path);
                        myMail.Attachments.Add(attachFile);
                    }
                }
            }
            catch (Exception err)
            {
                throw new Exception("在添加附件时有错误:" + err);
            }

            SmtpClient smtp = new SmtpClient();
            //指定发件人的邮件地址和密码以验证发件人身份
            smtp.Credentials = new System.Net.NetworkCredential(mailFrom, mailPwd);


            //设置SMTP邮件服务器
            smtp.Host = host;

            try
            {
                //将邮件发送到SMTP邮件服务器
                smtp.Send(myMail);
                return true;

            }
            catch (System.Net.Mail.SmtpException ex)
            {
                return false;
            }
            finally
            {
                smtp.Dispose();
                maddr = null;
                myMail.Dispose();
                myMail = null;


            }

        }
    } 
    #endregion
   public class AlarmHelper2
    {
       /// <summary>
       /// 预警
       /// </summary>
       /// <param name="isEnableAlarm">是否开启预警</param>
       /// <param name="alarmType">预警类型</param>
       /// <param name="alarmperson">预警人，多个以逗号分隔</param>
       /// <param name="title">预警标题</param>
       /// <param name="content">预警内容</param>
       public static bool Alarm(int isEnableAlarm, string alarmperson,string title,string content,string mailfrom="",string mailpwd="",string host="")
       {
               if(isEnableAlarm!=1)//不启用报警
               {
                   return true;
               }
         
            try
            {
                Email email = new Email();
                email.mailFrom = mailfrom;
                email.mailPwd = mailpwd;
                email.mailSubject = title;
                email.mailBody = content;
                email.isbodyHtml = true;    //是否是HTML
                email.host = host;//如果是QQ邮箱则：smtp:qq.com,依次类推
                email.mailToArray = alarmperson.Split(',').ToList().ToArray();// new string[] { "******@qq.com","12345678@qq.com"};//接收者邮件集合
                // email.mailCcArray = new string[] { "******@qq.com" };//抄送者邮件集合
                bool r = email.Send();
                email = null;
                return r;
            }
            catch(Exception ex)
            {
                string ss = ex.Message;
                return false;
            }
                 
       }

       /// <summary>
       /// 异步预警
       /// </summary>
       /// <param name="isEnableAlarm">是否开启预警</param>
       /// <param name="alarmType">预警类型</param>
       /// <param name="alarmperson">预警人，多个以逗号分隔</param>
       /// <param name="title">预警标题</param>
       /// <param name="content">预警内容</param>
       public static void AlarmAsync(int isEnableAlarm, string alarmperson, string title, string content, string mailfrom = "", string mailpwd = "", string host = "")
       {
           Task.Factory.StartNew(() =>
           {
               try
               {
                   Alarm(isEnableAlarm, alarmperson, title, content);
               }
               catch(Exception ex)
               {

               }
           });
       }
    }
}
