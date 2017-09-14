using System;
using System.Net.Mail;
using System.Net.Mime;

namespace YZ.Common.Mail
{
    /// <summary>
    /// 发送邮件
    /// </summary>
    public class SendMail
    {
        private MailMessage mailMessage;
        private SmtpClient smtpClient;
        private string password;//发件人 密码  
        /**/
        /// <summary>    
        /// 处审核后类的实例    
        /// </summary>    
        /// <param name="To">收件人 地址</param>    
        /// <param name="From">发件人 地址</param>    
        /// <param name="Body">邮件 正文</param>    
        /// <param name="Title">邮件的 主题</param>    
        /// <param name="Password">发件人 密码</param>    
        public SendMail(string To, string From, string Body, string Title, string Password)
        {
            mailMessage = new MailMessage();
            mailMessage.To.Add(To);
            mailMessage.From = new System.Net.Mail.MailAddress(From);
            mailMessage.Subject = Title;
            mailMessage.Body = Body;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.Priority = System.Net.Mail.MailPriority.Normal;
            this.password = Password;
            Console.WriteLine("send mail sucssesful");
        }
        /**/
        /// <summary>    
        /// 添加 附件    
        /// </summary>    
        public void Attachments(string Path)
        {
            string[] path = Path.Split(',');
            Attachment data;
            ContentDisposition disposition;
            for (int i = 0; i < path.Length; i++)
            {
                data = new Attachment(path[i], MediaTypeNames.Application.Octet);//实例化 附件    
                disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(path[i]);//获取附件的 创建日期   
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(path[i]);//获取附件的 修改日期    
                disposition.ReadDate = System.IO.File.GetLastAccessTime(path[i]);//获取附件的 读取日期    
                mailMessage.Attachments.Add(data);//添加到 附件 中    
            }
        }
        /**/
        /// <summary>    
        /// 异步 发送邮件  
        /// </summary>    
        /// <param name="CompletedMethod"></param>    
        public void SendAsync(SendCompletedEventHandler CompletedMethod)
        {
            if (mailMessage != null)
            {
                smtpClient = new SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(mailMessage.From.Address, password);//设置 发件人身份 的票据    
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Host = "smtp." + mailMessage.From.Host;
                smtpClient.SendCompleted += new SendCompletedEventHandler(CompletedMethod);//注册 异步 发送邮件 完成时的事件    
                smtpClient.SendAsync(mailMessage, mailMessage.Body);
            }
        }
        /**/
        /// <summary>    
        /// 发送邮件   
        /// </summary>    
        public void Send()
        {
            if (mailMessage != null)
            {
                smtpClient = new SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(mailMessage.From.Address, password);//设置 发件人身份 的票据    
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Host = "smtp." + mailMessage.From.Host;
                smtpClient.Send(mailMessage);
            }
        }
    }

    /// <summary>
    /// 使用HTML模板发送电子邮件
    /// </summary>
    public class SendMailUseHtmlModel
    {

        public void SendEmail(string email_from, string email_to, string email_cc, string userName, string name, string myName)
        {

            try
            {
                // 建立一个邮件实体   
                MailAddress from = new MailAddress(email_from);


                MailAddress to = new MailAddress(email_to);
                MailMessage message = new MailMessage(from, to);

                string strbody = ReplaceText(userName, name, myName);

                if (email_cc.ToString() != string.Empty)
                {
                    foreach (string ccs in email_cc.Split(';'))
                    {
                        MailAddress cc = new MailAddress(ccs);
                        message.CC.Add(cc);
                    }
                }

                message.IsBodyHtml = true;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Priority = MailPriority.High;
                message.Body = strbody;  //邮件BODY内容  
                message.Subject = "Subject";

                SmtpClient smtp = new SmtpClient();
                //smtp.Host = Configuration.MailHost;
                //smtp.Port = Configuration.MailHostPort;
                smtp.Credentials = new System.Net.NetworkCredential(email_from, "emailpassword");

                smtp.Send(message); //发送邮件  

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 替换模板中的字段值   
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="name"></param>
        /// <param name="myName"></param> 
        /// <returns></returns>
        public string ReplaceText(String userName, string name, string myName)
        {
            string path = string.Empty;
            //模版路径
            //path = HttpContext.Current.Server.MapPath("EmailTemplate\\emailTemplate.html");

            if (path == string.Empty)
            {
                return string.Empty;
            }
            System.IO.StreamReader sr = new System.IO.StreamReader(path);
            string str = string.Empty;
            str = sr.ReadToEnd();
            str = str.Replace("$USER_NAME$", userName);
            str = str.Replace("$NAME$", name);
            str = str.Replace("$MY_NAME$", myName);

            return str;
        }

        //模版

        // <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">  
        //<html xmlns="http://www.w3.org/1999/xhtml" >  
        //<head>  
        //    <title>HTML Report</title>  
        //</head>  
        //<body>  
        //<p >$USER_NAME$:</p>  


        //<p>My name is $NAME$</p>  
        //<p >This is a Test Email,<br />  
        //  $MY_NAME$</p>  
        //</body>  
        //</html>
    }
}
