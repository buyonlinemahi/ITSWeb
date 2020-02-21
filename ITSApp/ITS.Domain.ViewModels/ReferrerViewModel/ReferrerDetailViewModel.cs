using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.ReferrerModel;
using DLModel = ITS.Domain.Models;
namespace ITS.Domain.ViewModels
{
    public class ReferrerDetailViewModel
    {
        public Referrer Referrer { get; set; }
        public Location ReferrerMainLocation { get; set; }
        public IList<Location> ReferrerLocations { get; set; }
        public IEnumerable<TreatmentCategory> ReferrerTreamentCategories { get; set; }
        public IList<ReferrerProjectTreatmentCategoryViewModel> ReferrerProjects { get; set; }
        public IEnumerable<DLModel.ReferrerGroup> ReferrerGroupDetail { get; set; }
        public IEnumerable<DLModel.UserModel.User> UserDetail { get; set; }
        public IList<ReferrerGroupViewModel> ReferrerGroups { get; set; }
    }
}
