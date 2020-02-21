
/*
Page Name:  PricingTypes.cs                   
Version:  1.0                                              
Purpose: to create PricingTypes model class                                                                                             
                                                           
1.0 – 08-dec-2012 Robin Singh 

*/


namespace ITS.Domain.Models
{
    public class PricingTypes
    {

        public int PricingTypeID { get; set; }
        public string PricingTypeName { get; set; }
        public decimal? Price { get; set; }
        public int ReferrerProjectTreatmentID { get; set; }

    }
}
