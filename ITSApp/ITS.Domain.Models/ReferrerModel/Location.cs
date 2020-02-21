
namespace ITS.Domain.Models.ReferrerModel
{
    public class Location
    {
        public int ReferrerLocationID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostCode { get; set; }
        public bool IsMainOffice { get; set; }
        public int ReferrerID { get; set; }
       public bool IsActive { get; set; }
    }
}
