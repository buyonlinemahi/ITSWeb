using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models
{
   public class PasswordHistory
    {
        public int PasswordHistoryID { get; set; }
        public int UserID { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
