using ITS.Domain.Models;

namespace ITS.Domain.ViewModels
{
    public class LoginViewModel
    {
        public ITSUser User { get; set; }
        public int FailedAttemptCount { get; set; }
        public string InvalidMsg { get; set; }
        public string ReturnUrl { get; set; }
    }
}
