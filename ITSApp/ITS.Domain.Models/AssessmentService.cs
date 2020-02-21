/*
  *Latest Version 1.1
  
 
 Page Name:  AssessmentService.cs                      
 Version:  1.0                                              
 Purpose: to create AssessmentService model class                                                      
 
  
 * Modified By : Manjit Singh
 * Date        : 11-Dec-2012
 * Description : added property for ReferrerProjectTreatmentID
 * Version     : 1.1

*/

namespace ITS.Domain.Models
{
    public class AssessmentService
    {
        public int AssessmentServiceID { get; set; }
        public string AssessmentServiceName { get; set; }
        public int AssessmentTypeID { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }
    }
}
