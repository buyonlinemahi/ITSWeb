﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
    public class CaseContact
    {
        public int CaseContactID { get; set; }
        public int CaseID { get; set; }
        public int UserID { get; set; }
    }
}
