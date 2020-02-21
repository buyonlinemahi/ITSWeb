using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Infrastructure.ApplicationServices.Contracts;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Globalization;
using System.Web;
using System.DirectoryServices;

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
                mailMessage.From = new System.Net.Mail.MailAddress(System.Configuration.ConfigurationManager.AppSettings["EmailUserId"].ToString(), "Support@Innovatehmg.co.uk");
                mailMessage.ReplyTo = new MailAddress("support@innovatehmg.co.uk", "Support@Innovatehmg.co.uk");
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = System.Configuration.ConfigurationManager.AppSettings["EmailHost"].ToString();
                smtp.Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["EmailPort"].ToString());
                smtp.Credentials = new NetworkCredential(System.Configuration.ConfigurationManager.AppSettings["EmailUserId"].ToString(), System.Configuration.ConfigurationManager.AppSettings["EmailPwd"].ToString());
                smtp.EnableSsl = true;
                smtp.Send(mailMessage);
            }
            return true;
        }

        public void CreateErrorLog(string errorMessage, string Stacktrace)
        {
            try
            {
                DirectoryInfo dr = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath("~/"));
                if (!dr.CreateSubdirectory("ErrorLog").Exists)
                    dr.CreateSubdirectory("ErrorLog");
                string path = "~/ErrorLog/" + DateTime.Today.ToString("MM-dd-yyyy") + ".txt";
                if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                    File.Create(System.Web.HttpContext.Current.Server.MapPath(path)).Close();
                using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    w.WriteLine("\r\nLog Entry : ");
                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() + Environment.NewLine;
                    err += "Error Message:" + errorMessage + Environment.NewLine;
                    err += "Error Stacktrace:" + Stacktrace + Environment.NewLine;
                    err += "User IP:" + System.Web.HttpContext.Current.Request.UserHostAddress.ToString();  
                    w.WriteLine(err);
                    w.WriteLine("_________________________________________________________");
                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.StackTrace);
            }
        }
        public string getVirtualPath()
        {
            DirectoryEntry w3svc = new DirectoryEntry("IIS://localhost/w3svc");
            HttpRequest httprequest = System.Web.HttpContext.Current.Request;
            DirectoryEntry entry = new DirectoryEntry("IIS://localhost/W3SVC/" + httprequest.ServerVariables["INSTANCE_ID"].ToString().Trim() + "/Root/Storage");
            return entry.Properties["Path"].Value.ToString().Trim();
        }
    }
}
