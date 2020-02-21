using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models;

namespace ITS.Domain.ViewModels
{
    public class SupplierPractitionerViewModel
    {
        public Practitioner Practitioners { get; set; }
        public PractitionerRegistration Registrations { get; set; }

    }
}
