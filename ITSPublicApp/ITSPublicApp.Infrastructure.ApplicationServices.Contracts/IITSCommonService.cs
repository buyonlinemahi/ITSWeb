using System.Net.Mail;

namespace ITSPublicApp.Infrastructure.ApplicationServices.Contracts
{
    public interface IITSCommonService
    {
        bool SendGeneralEmail(string to, string replyTo, string subject, string message);
        bool SendGeneralEmail(string to, string cc, string replyTo, string subject, string message);
        bool SendGeneralEmail(string to, string cc, string replyTo, string subject, string message, Attachment attachment);
        void CreateErrorLog(string errorMessage, string Stacktrace);
    }
}
