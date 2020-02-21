using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITSPublicApp.Domain.Models;

namespace ITSPublicApp.Domain.ViewModels
{
    public class AuthorisationViewModel
    {
        public NotificationBubble notificationBubble { get; set; }
        public IEnumerable<SupplierCasePatient> supplierCasePatient { get; set; }
    }
}
