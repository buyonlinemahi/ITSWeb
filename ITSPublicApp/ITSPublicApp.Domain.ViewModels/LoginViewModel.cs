using ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Domain.ViewModels
{
    public class LoginViewModel
    {
        public ITSUser User { get; set; }
        public int FailedAttemptCount { get; set; }
        public string InvalidMsg { get; set; }
        public string ErrorAlert { get; set; }
    }
}