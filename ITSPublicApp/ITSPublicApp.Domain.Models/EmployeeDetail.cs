using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITSPublicApp.Domain.Models
{
    public class EmployeeDetail
    {      
        public int EmployeeDetailID { get; set; }
        public int? UsualJobRoleTypeID { get; set; }    
        public string UsualHours { get; set; }    
        public int? CurrentRoleTypeID { get; set; }    
        public string CurrentHours { get; set; }      
        public DateTime?  DateofFirstAbsence { get; set; }     
        public string OfficeLocation { get; set; }    
        public string TypeofIllness { get; set; }     
        public string PreRelatedAbsence { get; set; }      
        public string MedicationOrTreatment { get; set; }     
        public string EAP { get; set; }  
        public string IllnessEmpAbilityToPerform { get; set; }    
        public string FurtherQueries { get; set; }      
        public int?  CurrentlyAbsentFromWorkID { get; set; }      
        public int?  AgileWorkerID { get; set; }     
        public int?  OfficeBasedID { get; set; }
        public string LineManager { get; set; }
        public string CostCentreDivision { get; set; }
        public string EmployeeNumber { get; set; }
        public string AdditionalQuestion1 { get; set; }
        public string AdditionalQuestion2 { get; set; }
        public string FurtherQuestion1 { get; set; }
        public string FurtherQuestion2 { get; set; }
        public string NationalINSNumber { get; set; }
        public string jobTitle { get; set; }
    }
}


