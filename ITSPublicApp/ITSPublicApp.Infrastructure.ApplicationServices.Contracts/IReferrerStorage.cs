namespace ITSPublicApp.Infrastructure.ApplicationServices.Contracts
{
    public interface IReferrerStorage
    {
        bool CreateReferrerFolder(int referrerID, string storagePath);
        bool CreateReferrerProjectFolder(int referrerID, int projectID, string storagePath);
        bool CreateReferrerProjectCaseFolder(int referrerID, int projectID, int caseID, string storagePath);
        string SetReferrerReferralsStoragePath(int referrerID, int referrerProjectTreamentID, int caseID, string fileName,
                                               string storagePath);

        string GetReferrerReferralsStoragePath(int referrerID, int referrerProjectTreamentID, int caseID, string fileName,
                                               string storagePath);
        bool ReferralFileExists(int referrerID, int referrerProjectTreatmentID, int caseID, string fileName, string storagePath);
        string GetProjectTreatmentReferralUploadFolderStoragePath(int referrerID, int referrerProjectTreamentID, int caseID, string fileName,
                                                      string storagePath);
        string GetReferralUploadFolderStoragePath(int referrerID, int referrerProjectTreamentID, string fileName,
                                                     string storagePath);
        string SetTempReferralStorage(string storagePath, string tempCaseID);
        string GetTempReferralStorage(string storagePath, string tempCaseID);
        string GetCompleteTempReferralStorage(string storagePath, string tempCaseID);

        string GetProjectTreatmentReferralCaseUploadFolderStoragePath(int referrerID, int referrerProjectTreamentID, int caseID, string fileName,
                                                     string storagePath);
        
    }
}
