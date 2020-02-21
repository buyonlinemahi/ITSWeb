using ITS.Infrastructure.ApplicationServices.Contracts;
using System.IO;
using ITS.Infrastructure.Global;
using System;


#region comments

/*
 * Latest version : 1.0
 * 
 * Author   : Pardeep Kumar
 * Date     : 21-Dec-2012
 * Version  : 1.0
 * Purposes : Created Service method to Add Supplier folders when a new Supplier is created
 * 
 */

#endregion

namespace ITS.Infrastructure.ApplicationServices
{
    public class SupplierStorageService : ISupplierStorage
    {

        public static string[] CaseFolders = new string[] { "SiteAudit", "Insurance", "Registration", "Clinical","Triage" };
        public const string CaseDocuments = "CaseDocuments";
        public const string SupplierRoot = "Suppliers";
        private const string SupplierUploads = "SupplierUploads";
        private const string SupplierUploads1 = "SupplierUploads/"; 
        public bool CreateSupplierFolder(int supplierID, string storagePath)
        {
            string path = Path.Combine(storagePath, SupplierRoot, supplierID.ToString());

            if (CreateDirectory(path))
            {
                foreach (string folder in CaseFolders)
                {
                    string caseFolderPath = Path.Combine(path, folder);
                    CreateDirectory(caseFolderPath);

                }
                return true;
            }
            else
            {
                return false;
            }
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

        public string SupplierRootFolder
        {
            get { return SupplierRoot; }
        }

        public string[] SupplierCaseFolders
        {
            get { return CaseFolders; }
        }



        // method to set the supplier storage path for new files
        public string SetSupplierStoragePath(string storagePath, int supplierID, string fileName, string documentType)
        {
            if (documentType == GlobalConst.SupplierDocumentType.Triage)
            {
                string path = Path.Combine(storagePath, CaseDocuments);
                CreateDirectory(path);
               // return Path.Combine(path, Guid.NewGuid().ToString() + Path.GetFileName(fileName));
                return Path.Combine(path, supplierID.ToString() + "_" + documentType +"_" + DateTime.Now.ToString("yyyy-MM-ddThh_mm") + Path.GetFileName(fileName));
            }

          /// return Path.Combine(storagePath, SupplierRoot, supplierID.ToString(), GetFolder(documentType), Guid.NewGuid().ToString() + Path.GetFileName(fileName));
            return Path.Combine(storagePath, SupplierRoot, supplierID.ToString(), GetFolder(documentType), supplierID.ToString() + "_" + documentType + "_" + DateTime.Now.ToString("yyyy-MM-ddThh_mm") + Path.GetFileName(fileName));
        }

        // method to get the supplier storage path for Old saved files
        public string GetSupplierStoragePath(string storagePath, int supplierID, string fileName, string documentType)
        {
            if (documentType == GlobalConst.SupplierDocumentType.Triage)
                return Path.Combine(storagePath, CaseDocuments, fileName);

            return Path.Combine(storagePath, SupplierRoot, supplierID.ToString(), GetFolder(documentType), fileName);
        }

        public string GetFolder(string documentType)
        {
            string folder = "";
            switch (documentType)
            {

                case GlobalConst.SupplierDocumentType.Registration:
                    folder = CaseFolders[2].ToString();
                    break;
                case GlobalConst.SupplierDocumentType.Insurance:
                    folder = CaseFolders[1].ToString();
                    break;
                case GlobalConst.SupplierDocumentType.SupplierAudit:

                    folder = CaseFolders[0].ToString();
                    break;
                case GlobalConst.SupplierDocumentType.SupplierClinicalAudit:
                    folder = CaseFolders[3].ToString();
                    break;
                case GlobalConst.SupplierDocumentType.Triage:
                    folder = CaseFolders[4].ToString();
                    break;

            }
            return folder;
        }

        public string GetProjectTreatmentSupplierUploadFolderStoragePath(int supplierID, int referrerProjectTreamentID, int caseID, string fileName, string storagePath)
        {
            CreateDirectory(Path.Combine(storagePath, SupplierRoot, supplierID.ToString(), referrerProjectTreamentID.ToString(), caseID.ToString(), SupplierUploads));
             return Path.Combine(storagePath, SupplierRoot, supplierID.ToString(), referrerProjectTreamentID.ToString(), caseID.ToString(), SupplierUploads, fileName);

        }

        public string GetProjectTreatmentSupplierUploadFolderStoragePathwithoutfile(int supplierID, int referrerProjectTreamentID, int caseID, string storagePath)
        {
            return Path.Combine(storagePath, SupplierRoot, supplierID.ToString(), referrerProjectTreamentID.ToString(), caseID.ToString(), SupplierUploads1);

        }

    }
}
