using System.Net.Mail;

namespace ITS.Infrastructure.ApplicationServices.Contracts
{
    public interface IEmail
    {
        bool SendGeneralEmail(string to, string replyTo, string subject, string message);
        bool SendGeneralEmail(string to, string cc, string replyTo, string subject, string message);
        bool SendGeneralEmail(string to, string cc, string replyTo, string subject, string message, Attachment attachment);
        bool SendMultipleEmail(string to, string replyTo, string subject, string message);
    }
}
