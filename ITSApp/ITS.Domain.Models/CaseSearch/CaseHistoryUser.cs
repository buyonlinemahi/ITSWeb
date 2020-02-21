using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITS.Domain.Models.CaseSearch
{
    public class CaseHistoryUser
    {
        public int CaseHistoryID { get; set; }
        
        public int CaseID { get; set; }
        
        public DateTime EventDate { get; set; }
        
        public int UserID { get; set; }
        
        public string EventDescription { get; set; }
        
        public int EventTypeID { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string UserName { get; set; }  
    }
}
