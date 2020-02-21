
namespace ITS.Infrastructure.ApplicationServices.Contracts
{
    public interface IReferrerStorage
    {
        bool CreateReferrerFolder(int referrerID, string storagePath);
        bool CreateReferrerProjectFolder(int referrerID, int referrerProjectTreatmentID, string storagePath);
        bool CreateReferrerProjectCaseFolder(int referrerID, int projectID, int caseID, string storagePath);
        string GetReferrerReferralsStoragePath(int referrerID, int referrerProjectTreatmentID, int caseID, string filename, string appSetting);
        bool ReferralFileExists(int referrerID, int referrerProjectTreatmentID, int caseID, string fileName, string storagePath);
        bool CreateProjectTreatmentReferralUploadFolder(int referrerID, int referrerProjectTreatmentID, string storagePath);
        string GetProjectTreatmentReferralUploadFolderStoragePath(int referrerID, int referrerProjectTreatmentID, string storagePath, string fileName);
        string GetProjectTreatmentReferralUploadFolderStoragePathWithOutFileName(int referrerID, int referrerProjectTreatmentID, string storagePath);
        string GetProjectTreatmentReferralCaseUploadFolderStoragePath(int referrerID, int referrerProjectTreamentID, int caseID, string fileName,
                                                   string storagePath);

    }
}
