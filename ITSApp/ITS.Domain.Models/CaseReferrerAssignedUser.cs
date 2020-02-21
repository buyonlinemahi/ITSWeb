
namespace ITS.Domain.Models
{
    public class CaseReferrerAssignedUser
    {
        public int CaseReferrerAssignedUserID { get; set; }
        public int UserID { get; set; }
        public string EncUserID { get; set; }
        public int CaseID { get; set; }
        public string UserName { get; set; }
    }
}
