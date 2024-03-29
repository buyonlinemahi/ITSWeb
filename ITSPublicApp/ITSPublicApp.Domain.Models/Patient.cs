﻿using System;

namespace ITSPublicApp.Domain.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostCode { get; set; }
        public DateTime InjuryDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public int GenderID { get; set; }
        public string Email { get; set; }
        public bool HasLegalRep { get; set; }
        public int? SolicitorID { get; set; }
        public int HasInjuredPartyRepresentative { get; set; }
        public int? InjuredID { get; set; }
        public string SpecialInstructionNotes { get; set; }
        public string PatientFullName{ get { return this.FirstName + ' ' + this.LastName; } }
        public int? PrimaryConditionID { get; set; }
       
    }
}
