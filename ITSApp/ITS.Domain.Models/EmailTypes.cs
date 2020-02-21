
/*
Page Name:  EmailSetup.cs                      
Version:  1.0                                              
Purpose: create EmailSetup model class                                                      
Revision History:                                        
                                                           
1.0 – 12/07/2012 Vijay Kumar 

*/


namespace ITS.Domain.Models
{
   public class EmailTypes
    {

       public int EmailTypeID { get; set; }
       public string EmailTypeName { get; set; }
       public int UserTypeID { get; set; }
       public int EmailTypeValueID { get; set; }
       public int ReferrerProjectTreatmentID { get; set; }
    }
}
