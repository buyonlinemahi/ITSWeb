using ITS.Infrastructure.ApplicationServices.Contracts;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;


namespace ITS.Infrastructure.ApplicationServices
{
    public class EmailService : IEmail
    {
        public bool SendGeneralEmail(string to, string replyTo, string subject, string message)
        {
            return SendMail(to, null, replyTo, subject, message, null);
        }
        public bool SendGeneralEmail(string to, string cc, string replyTo, string subject, string message)
        {
            return SendMail(to, cc, replyTo, subject, message, null);
        }

        public bool SendGeneralEmail(string to, string cc, string replyTo, string subject, string message, Attachment attachment)
        {
            return SendMail(to, cc, replyTo, subject, message, attachment);
        }

        private bool SendMail(string to, string cc, string replyTo, string subject, string message, Attachment attachment)
        {
            using (MailMessage mailMessage = new MailMessage() { IsBodyHtml = true, HeadersEncoding = UTF8Encoding.UTF8 })
            {
                mailMessage.To.Add(to);

                if (attachment != null)
                    mailMessage.Attachments.Add(attachment);

                if (!String.IsNullOrEmpty(cc))
                    mailMessage.CC.Add(cc);

                if (!String.IsNullOrEmpty(replyTo))
                    mailMessage.ReplyToList.Add(replyTo);

                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;
                mailMessage.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Support@Innovatehmg.co.uk");
                mailMessage.ReplyTo = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["ReplyTo"].ToString(), "Support@Innovatehmg.co.uk");
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = System.Configuration.ConfigurationManager.AppSettings["EmailHost"].ToString(); 
                smtp.Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["EmailPort"].ToString()); 
                smtp.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["EmailUserId"].ToString(), System.Configuration.ConfigurationManager.AppSettings["EmailPwd"].ToString());
                smtp.EnableSsl = true;
                smtp.Send(mailMessage);
            }
            return true;
        }

        public bool SendMultipleEmail(string to, string replyTo, string subject, string message)
        {
            using (MailMessage mailMessage = new MailMessage() { IsBodyHtml = true, HeadersEncoding = UTF8Encoding.UTF8 })
            {
                mailMessage.To.Add(to);
                string[] Multi = replyTo.Split(',');
                foreach (string MultiEmailIDs in Multi)
                {
                    if (MultiEmailIDs != "")
                        mailMessage.To.Add(MultiEmailIDs);
                }
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.IsBodyHtml = true;
                mailMessage.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["SupportInnovateHMGEmail"].ToString(), "Support@Innovatehmg.co.uk");
                mailMessage.ReplyTo = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["ReplyTo"].ToString(), "Support@Innovatehmg.co.uk");
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = System.Configuration.ConfigurationManager.AppSettings["EmailHost"].ToString();
                smtp.Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["EmailPort"].ToString());
                smtp.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["EmailUserId"].ToString(), System.Configuration.ConfigurationManager.AppSettings["EmailPwd"].ToString());
                smtp.EnableSsl = true;
                smtp.Send(mailMessage);
            }
            return true;
        }
    }
}