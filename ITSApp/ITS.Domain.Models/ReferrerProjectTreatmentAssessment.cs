/*
Page Name:  ReferrerProjectTreatmentAssessment.cs                      
Version:  1.0                                              
Purpose: to create ReferrerProjectTreatmentAssessment model class                                                      
Revision History:                                        
                                                           
1.0 – 27-Nov-2012 Manjit Singh 

*/

using System.Collections.Generic;

namespace ITS.Domain.Models
{
    public class ReferrerProjectTreatmentAssessment
    {
        public int ReferrerProjectTreatmentAssessmentID { get; set; }
        public int AssessmentServiceID { get; set; }
        public int AssessmentTypeID { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
     }
}
