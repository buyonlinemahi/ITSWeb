
namespace ITSPublicApp.Infrastructure.ApplicationServices.Contracts
{
    public interface ISupplierStorage
    {
        bool CreateSupplierFolder(int supplierID, string storagePath);
        string SupplierRootFolder { get; }
        string[] SupplierCaseFolders { get; }
        string SetSupplierStoragePath(string storagePath, int supplierID, string fileName, string documentType);
        string GetSupplierStoragePath(string storagePath, int supplierID, string fileName, string documentType);

        string GetProjectTreatmentSupplierUploadFolderStoragePath(int supplierID, int ReferrerProjectTreatmentID, int CaseID, string UploaedDocmentName, string storagePath);
        string GetProjectTreatmentSupplierUploadFolderStoragePath2(int supplierID, int referrerProjectTreamentID, int caseID, string fileName,
                                                      string storagePath);
    }
}
