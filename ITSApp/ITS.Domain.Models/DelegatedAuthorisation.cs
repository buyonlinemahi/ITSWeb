
/*
Page Name:  DelegatedAuthorisation.cs                      
Version:  1.0                                              
Purpose: create DelegatedAuthorisation model class                                                      
Revision History:                                        
                                                           
1.0 – 11/10/2012 Satya 

*/
/// <summary>
/// 
/// </summary>
namespace ITS.Domain.Models
{
    public class DelegatedAuthorisation
    {
        public int DelegatedAuthorisationID { get; set; }
        public string DeletegatedAuthorisationName { get; set; }
        //public int DelegatedAuthorisationTypeID { get; set; }
        public int TreatmentCategoryID { get; set; }
        public double Amount { get; set; }
        public int ReferrerProjectTreatmentAuthorisationID { get; set; }
    }
}
