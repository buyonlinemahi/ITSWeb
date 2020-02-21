using ITS.Infrastructure.ApplicationServices.Contracts;
using System.IO;
using System.Web;
namespace ITS.Infrastructure.ApplicationServices
{
    public class ReferrerStorageService : IReferrerStorage
    {
        private static string[] CaseFolders = new string[] { "CaseUploads", "Initial Assessment", "Review Assessment", "Final Assessment", "Invoice", "Supplier Invoice" };
        private const string ReferrerRoot = "Referrers";
        private const string ReferrerUploads = "ReferrerUploads";
        private const string CaseUploads = "CaseUploads"; 
        public bool CreateReferrerFolder(int referrerID, string storagePath)
        {
            string path = Path.Combine(storagePath, ReferrerRoot, referrerID.ToString());
            return CreateDirectory(path);
        }

        public bool CreateReferrerProjectFolder(int referrerID, int referrerProjectTreatmentID, string storagePath)
        {
            string path = Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), referrerProjectTreatmentID.ToString());
            return CreateDirectory(path);
        }

        public bool CreateReferrerProjectCaseFolder(int referrerID, int projectID, int caseID, string storagePath)
        {
            string path = Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), projectID.ToString(), caseID.ToString());
            CreateDirectory(path);

            foreach (string folder in CaseFolders)
            {
                string caseFolderPath = Path.Combine(path, folder);
                CreateDirectory(caseFolderPath);
            }

            return true;
        }

        public bool CreateProjectTreatmentReferralUploadFolder(int referrerID, int referrerProjectTreatmentID , string storagePath)
        {
            string path = Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), referrerProjectTreatmentID.ToString(), ReferrerUploads);
            CreateDirectory(path); 
            return true;
        }

        public string GetProjectTreatmentReferralUploadFolderStoragePath(int referrerID, int referrerProjectTreatmentID, string storagePath, string fileName)
        {
            return Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), referrerProjectTreatmentID.ToString(), ReferrerUploads, fileName);
         
        }

        public string GetReferrerReferralsStoragePath(int referrerID, int referrerProjectTreatmentID, int caseID, string fileName,
                                                      string storagePath)
        {
            return System.Web.HttpContext.Current.Server.MapPath(Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), referrerProjectTreatmentID.ToString(), caseID.ToString(), CaseFolders[0], fileName ));

        }

        private static bool CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return true;
            }
            return false;
        }

        public bool ReferralFileExists(int referrerID, int referrerProjectTreatmentID, int caseID, string fileName, string storagePath)
        {
            return File.Exists(GetReferrerReferralsStoragePath(referrerID, referrerProjectTreatmentID, caseID, fileName, storagePath));

        }
        public string GetProjectTreatmentReferralUploadFolderStoragePathWithOutFileName(int referrerID, int referrerProjectTreatmentID, string storagePath)
        {
            return System.Web.HttpContext.Current.Server.MapPath(Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), referrerProjectTreatmentID.ToString(), ReferrerUploads));
        }


        public string GetProjectTreatmentReferralCaseUploadFolderStoragePath(int referrerID, int referrerProjectTreamentID, int caseID, string fileName,
                                                    string storagePath)
        {
            CreateDirectory(Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), referrerProjectTreamentID.ToString(), caseID.ToString(), CaseUploads));
            return Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), referrerProjectTreamentID.ToString(), caseID.ToString(), CaseUploads, fileName);

        }
    }
}
