using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITS.Domain.Models.ReferrerModel;

namespace ITS.Domain.ViewModels
{
    public class ReferrerIndexViewModel
    {
        public IEnumerable<Referrer> Referrers { get; set; }
    }
}
