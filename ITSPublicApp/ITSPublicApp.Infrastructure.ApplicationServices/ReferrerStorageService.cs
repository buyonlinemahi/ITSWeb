using ITSPublicApp.Infrastructure.ApplicationServices.Contracts;
using System.IO;

namespace ITSPublicApp.Infrastructure.ApplicationServices
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

        public bool CreateReferrerProjectFolder(int referrerID, int projectID, string storagePath)
        {
            string path = Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), projectID.ToString());
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

        public string SetTempReferralStorage(string storagePath, string tempCaseID)
        {
            string path = Path.Combine(storagePath, ReferrerRoot, tempCaseID, CaseFolders[0]) + "\\";
            if(!Directory.Exists(path))
                CreateDirectory(path);
            return path;
        }

        public string GetCompleteTempReferralStorage(string storagePath, string tempCaseID)
        {
            string path = Path.Combine(storagePath, ReferrerRoot, tempCaseID, CaseFolders[0]) + "\\";
            if (!Directory.Exists(path))
                path = null;
            return path;
        }

        public string GetTempReferralStorage(string storagePath, string tempCaseID)
        {
            string path = Path.Combine(storagePath, ReferrerRoot, tempCaseID) + "\\";
            if (!Directory.Exists(path))
                path = null;
            return path;
        }


        public string SetReferrerReferralsStoragePath(int referrerID, int referrerProjectTreamentID, int caseID, string fileName, string storagePath)
        {
            string path = Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), referrerProjectTreamentID.ToString(), caseID.ToString(), CaseFolders[0]);
            CreateDirectory(path);
            return Path.Combine(path, fileName);
        }

        public string GetReferrerReferralsStoragePath(int referrerID, int referrerProjectTreamentID, int caseID, string fileName,
                                                      string storagePath)
        {
            return Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), referrerProjectTreamentID.ToString(), caseID.ToString(), CaseFolders[0], fileName);
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
            return File.Exists(System.Web.HttpContext.Current.Server.MapPath(GetReferrerReferralsStoragePath(referrerID, referrerProjectTreatmentID, caseID, fileName, storagePath)));

        }

        public bool CreateProjectTreatmentReferralUploadFolder(int referrerID, int referrerProjectTreatmentID, string storagePath)
        {
            string path = Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), referrerProjectTreatmentID.ToString());
            CreateDirectory(path);
            return true;
        }

        public string GetProjectTreatmentReferralUploadFolderStoragePath(int referrerID, int referrerProjectTreamentID, int caseID, string fileName,
                                                      string storagePath)
        {
            return Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), referrerProjectTreamentID.ToString(), caseID.ToString(), ReferrerUploads, fileName);

        }

        public string GetReferralUploadFolderStoragePath(int referrerID, int referrerProjectTreamentID, string fileName,
                                                      string storagePath)
        {
            return Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), referrerProjectTreamentID.ToString(), ReferrerUploads, fileName);

        }

        public string GetProjectTreatmentReferralCaseUploadFolderStoragePath(int referrerID, int referrerProjectTreamentID, int caseID, string fileName,
                                                     string storagePath)
        {
            CreateDirectory(Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), referrerProjectTreamentID.ToString(), caseID.ToString(), CaseUploads));
            return Path.Combine(storagePath, ReferrerRoot, referrerID.ToString(), referrerProjectTreamentID.ToString(), caseID.ToString(), CaseUploads, fileName);

        }

    }
}
