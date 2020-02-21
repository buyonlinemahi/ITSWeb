using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.UserModel;
using ITS.Domain.Models;
namespace ITS.Domain.ViewModels
{
    public class UserDetailViewModel
    {
        public User User { get; set; }
        public IEnumerable<ITS.Domain.Models.UserModel.ReferrersName> Referrers { get; set; }
        public IEnumerable<ITS.Domain.Models.UserModel.SuppliersName> Suppliers { get; set; }
        public IEnumerable<ReferrerLocation> ReferrerLocations { get; set; }
        public IEnumerable<ReferrerProject> ReferrerProjects { get; set; }
        public IEnumerable<ReferrerProject> UserProjects { get; set; }
    }
}
