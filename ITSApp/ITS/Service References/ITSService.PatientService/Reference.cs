﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ITSWebApp.ITSService.PatientService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Patient", Namespace="http://schemas.datacontract.org/2004/07/ITSService.DTO")]
    [System.SerializableAttribute()]
    public partial class Patient : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> BirthDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> EmploymentIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int GenderIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HasInjuredPartyRepresentativeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool HasLegalRepField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HomePhoneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> InjuredIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime InjuryDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MobilePhoneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PatientIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> PolicyIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> PolicyOpenDetailIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PostCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> PrimaryConditionIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RegionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> SolicitorIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SpecialInstructionNotesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WorkPhoneField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> BirthDate {
            get {
                return this.BirthDateField;
            }
            set {
                if ((this.BirthDateField.Equals(value) != true)) {
                    this.BirthDateField = value;
                    this.RaisePropertyChanged("BirthDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string City {
            get {
                return this.CityField;
            }
            set {
                if ((object.ReferenceEquals(this.CityField, value) != true)) {
                    this.CityField = value;
                    this.RaisePropertyChanged("City");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> EmploymentID {
            get {
                return this.EmploymentIDField;
            }
            set {
                if ((this.EmploymentIDField.Equals(value) != true)) {
                    this.EmploymentIDField = value;
                    this.RaisePropertyChanged("EmploymentID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int GenderID {
            get {
                return this.GenderIDField;
            }
            set {
                if ((this.GenderIDField.Equals(value) != true)) {
                    this.GenderIDField = value;
                    this.RaisePropertyChanged("GenderID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int HasInjuredPartyRepresentative {
            get {
                return this.HasInjuredPartyRepresentativeField;
            }
            set {
                if ((this.HasInjuredPartyRepresentativeField.Equals(value) != true)) {
                    this.HasInjuredPartyRepresentativeField = value;
                    this.RaisePropertyChanged("HasInjuredPartyRepresentative");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool HasLegalRep {
            get {
                return this.HasLegalRepField;
            }
            set {
                if ((this.HasLegalRepField.Equals(value) != true)) {
                    this.HasLegalRepField = value;
                    this.RaisePropertyChanged("HasLegalRep");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string HomePhone {
            get {
                return this.HomePhoneField;
            }
            set {
                if ((object.ReferenceEquals(this.HomePhoneField, value) != true)) {
                    this.HomePhoneField = value;
                    this.RaisePropertyChanged("HomePhone");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> InjuredID {
            get {
                return this.InjuredIDField;
            }
            set {
                if ((this.InjuredIDField.Equals(value) != true)) {
                    this.InjuredIDField = value;
                    this.RaisePropertyChanged("InjuredID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime InjuryDate {
            get {
                return this.InjuryDateField;
            }
            set {
                if ((this.InjuryDateField.Equals(value) != true)) {
                    this.InjuryDateField = value;
                    this.RaisePropertyChanged("InjuryDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MobilePhone {
            get {
                return this.MobilePhoneField;
            }
            set {
                if ((object.ReferenceEquals(this.MobilePhoneField, value) != true)) {
                    this.MobilePhoneField = value;
                    this.RaisePropertyChanged("MobilePhone");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PatientID {
            get {
                return this.PatientIDField;
            }
            set {
                if ((this.PatientIDField.Equals(value) != true)) {
                    this.PatientIDField = value;
                    this.RaisePropertyChanged("PatientID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> PolicyID {
            get {
                return this.PolicyIDField;
            }
            set {
                if ((this.PolicyIDField.Equals(value) != true)) {
                    this.PolicyIDField = value;
                    this.RaisePropertyChanged("PolicyID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> PolicyOpenDetailID {
            get {
                return this.PolicyOpenDetailIDField;
            }
            set {
                if ((this.PolicyOpenDetailIDField.Equals(value) != true)) {
                    this.PolicyOpenDetailIDField = value;
                    this.RaisePropertyChanged("PolicyOpenDetailID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PostCode {
            get {
                return this.PostCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.PostCodeField, value) != true)) {
                    this.PostCodeField = value;
                    this.RaisePropertyChanged("PostCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> PrimaryConditionID {
            get {
                return this.PrimaryConditionIDField;
            }
            set {
                if ((this.PrimaryConditionIDField.Equals(value) != true)) {
                    this.PrimaryConditionIDField = value;
                    this.RaisePropertyChanged("PrimaryConditionID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Region {
            get {
                return this.RegionField;
            }
            set {
                if ((object.ReferenceEquals(this.RegionField, value) != true)) {
                    this.RegionField = value;
                    this.RaisePropertyChanged("Region");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> SolicitorID {
            get {
                return this.SolicitorIDField;
            }
            set {
                if ((this.SolicitorIDField.Equals(value) != true)) {
                    this.SolicitorIDField = value;
                    this.RaisePropertyChanged("SolicitorID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SpecialInstructionNotes {
            get {
                return this.SpecialInstructionNotesField;
            }
            set {
                if ((object.ReferenceEquals(this.SpecialInstructionNotesField, value) != true)) {
                    this.SpecialInstructionNotesField = value;
                    this.RaisePropertyChanged("SpecialInstructionNotes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WorkPhone {
            get {
                return this.WorkPhoneField;
            }
            set {
                if ((object.ReferenceEquals(this.WorkPhoneField, value) != true)) {
                    this.WorkPhoneField = value;
                    this.RaisePropertyChanged("WorkPhone");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CasePatientTreatment", Namespace="http://schemas.datacontract.org/2004/07/ITSService.DTO")]
    [System.SerializableAttribute()]
    public partial class CasePatientTreatment : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> BirthDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CaseNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CaseReferrerReferenceNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CaseSpecialInstructionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CaseSubmittedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string GenderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string HomePhoneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime InjuryDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string InjuryTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsTriageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MobilePhoneField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PatientContactNotesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool PatientContactedByInternalUserField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PatientIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PostCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RegionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> SupplierIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TreatmentCategoryIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TreatmentCategoryNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TreatmentTypeNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WorkPhoneField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<System.DateTime> BirthDate {
            get {
                return this.BirthDateField;
            }
            set {
                if ((this.BirthDateField.Equals(value) != true)) {
                    this.BirthDateField = value;
                    this.RaisePropertyChanged("BirthDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CaseNumber {
            get {
                return this.CaseNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.CaseNumberField, value) != true)) {
                    this.CaseNumberField = value;
                    this.RaisePropertyChanged("CaseNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CaseReferrerReferenceNumber {
            get {
                return this.CaseReferrerReferenceNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.CaseReferrerReferenceNumberField, value) != true)) {
                    this.CaseReferrerReferenceNumberField = value;
                    this.RaisePropertyChanged("CaseReferrerReferenceNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CaseSpecialInstruction {
            get {
                return this.CaseSpecialInstructionField;
            }
            set {
                if ((object.ReferenceEquals(this.CaseSpecialInstructionField, value) != true)) {
                    this.CaseSpecialInstructionField = value;
                    this.RaisePropertyChanged("CaseSpecialInstruction");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CaseSubmittedDate {
            get {
                return this.CaseSubmittedDateField;
            }
            set {
                if ((this.CaseSubmittedDateField.Equals(value) != true)) {
                    this.CaseSubmittedDateField = value;
                    this.RaisePropertyChanged("CaseSubmittedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string City {
            get {
                return this.CityField;
            }
            set {
                if ((object.ReferenceEquals(this.CityField, value) != true)) {
                    this.CityField = value;
                    this.RaisePropertyChanged("City");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Gender {
            get {
                return this.GenderField;
            }
            set {
                if ((object.ReferenceEquals(this.GenderField, value) != true)) {
                    this.GenderField = value;
                    this.RaisePropertyChanged("Gender");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string HomePhone {
            get {
                return this.HomePhoneField;
            }
            set {
                if ((object.ReferenceEquals(this.HomePhoneField, value) != true)) {
                    this.HomePhoneField = value;
                    this.RaisePropertyChanged("HomePhone");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime InjuryDate {
            get {
                return this.InjuryDateField;
            }
            set {
                if ((this.InjuryDateField.Equals(value) != true)) {
                    this.InjuryDateField = value;
                    this.RaisePropertyChanged("InjuryDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string InjuryType {
            get {
                return this.InjuryTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.InjuryTypeField, value) != true)) {
                    this.InjuryTypeField = value;
                    this.RaisePropertyChanged("InjuryType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsTriage {
            get {
                return this.IsTriageField;
            }
            set {
                if ((this.IsTriageField.Equals(value) != true)) {
                    this.IsTriageField = value;
                    this.RaisePropertyChanged("IsTriage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MobilePhone {
            get {
                return this.MobilePhoneField;
            }
            set {
                if ((object.ReferenceEquals(this.MobilePhoneField, value) != true)) {
                    this.MobilePhoneField = value;
                    this.RaisePropertyChanged("MobilePhone");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PatientContactNotes {
            get {
                return this.PatientContactNotesField;
            }
            set {
                if ((object.ReferenceEquals(this.PatientContactNotesField, value) != true)) {
                    this.PatientContactNotesField = value;
                    this.RaisePropertyChanged("PatientContactNotes");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool PatientContactedByInternalUser {
            get {
                return this.PatientContactedByInternalUserField;
            }
            set {
                if ((this.PatientContactedByInternalUserField.Equals(value) != true)) {
                    this.PatientContactedByInternalUserField = value;
                    this.RaisePropertyChanged("PatientContactedByInternalUser");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PatientID {
            get {
                return this.PatientIDField;
            }
            set {
                if ((this.PatientIDField.Equals(value) != true)) {
                    this.PatientIDField = value;
                    this.RaisePropertyChanged("PatientID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PostCode {
            get {
                return this.PostCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.PostCodeField, value) != true)) {
                    this.PostCodeField = value;
                    this.RaisePropertyChanged("PostCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Region {
            get {
                return this.RegionField;
            }
            set {
                if ((object.ReferenceEquals(this.RegionField, value) != true)) {
                    this.RegionField = value;
                    this.RaisePropertyChanged("Region");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> SupplierID {
            get {
                return this.SupplierIDField;
            }
            set {
                if ((this.SupplierIDField.Equals(value) != true)) {
                    this.SupplierIDField = value;
                    this.RaisePropertyChanged("SupplierID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TreatmentCategoryID {
            get {
                return this.TreatmentCategoryIDField;
            }
            set {
                if ((object.ReferenceEquals(this.TreatmentCategoryIDField, value) != true)) {
                    this.TreatmentCategoryIDField = value;
                    this.RaisePropertyChanged("TreatmentCategoryID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TreatmentCategoryName {
            get {
                return this.TreatmentCategoryNameField;
            }
            set {
                if ((object.ReferenceEquals(this.TreatmentCategoryNameField, value) != true)) {
                    this.TreatmentCategoryNameField = value;
                    this.RaisePropertyChanged("TreatmentCategoryName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TreatmentTypeName {
            get {
                return this.TreatmentTypeNameField;
            }
            set {
                if ((object.ReferenceEquals(this.TreatmentTypeNameField, value) != true)) {
                    this.TreatmentTypeNameField = value;
                    this.RaisePropertyChanged("TreatmentTypeName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WorkPhone {
            get {
                return this.WorkPhoneField;
            }
            set {
                if ((object.ReferenceEquals(this.WorkPhoneField, value) != true)) {
                    this.WorkPhoneField = value;
                    this.RaisePropertyChanged("WorkPhone");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ITSService.PatientService.IPatientService")]
    public interface IPatientService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPatientService/AddPatient", ReplyAction="http://tempuri.org/IPatientService/AddPatientResponse")]
        int AddPatient(ITSWebApp.ITSService.PatientService.Patient patient);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPatientService/UpdatePatientByPatientID", ReplyAction="http://tempuri.org/IPatientService/UpdatePatientByPatientIDResponse")]
        int UpdatePatientByPatientID(ITSWebApp.ITSService.PatientService.Patient patient);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPatientService/GetPatientByPatientID", ReplyAction="http://tempuri.org/IPatientService/GetPatientByPatientIDResponse")]
        ITSWebApp.ITSService.PatientService.Patient GetPatientByPatientID(int patientID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPatientService/GetAllPatient", ReplyAction="http://tempuri.org/IPatientService/GetAllPatientResponse")]
        ITSWebApp.ITSService.PatientService.Patient[] GetAllPatient();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPatientService/GetPatientAndCaseByCaseID", ReplyAction="http://tempuri.org/IPatientService/GetPatientAndCaseByCaseIDResponse")]
        ITSWebApp.ITSService.PatientService.CasePatientTreatment GetPatientAndCaseByCaseID(int caseID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPatientServiceChannel : ITSWebApp.ITSService.PatientService.IPatientService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PatientServiceClient : System.ServiceModel.ClientBase<ITSWebApp.ITSService.PatientService.IPatientService>, ITSWebApp.ITSService.PatientService.IPatientService {
        
        public PatientServiceClient() {
        }
        
        public PatientServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PatientServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PatientServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PatientServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int AddPatient(ITSWebApp.ITSService.PatientService.Patient patient) {
            return base.Channel.AddPatient(patient);
        }
        
        public int UpdatePatientByPatientID(ITSWebApp.ITSService.PatientService.Patient patient) {
            return base.Channel.UpdatePatientByPatientID(patient);
        }
        
        public ITSWebApp.ITSService.PatientService.Patient GetPatientByPatientID(int patientID) {
            return base.Channel.GetPatientByPatientID(patientID);
        }
        
        public ITSWebApp.ITSService.PatientService.Patient[] GetAllPatient() {
            return base.Channel.GetAllPatient();
        }
        
        public ITSWebApp.ITSService.PatientService.CasePatientTreatment GetPatientAndCaseByCaseID(int caseID) {
            return base.Channel.GetPatientAndCaseByCaseID(caseID);
        }
    }
}