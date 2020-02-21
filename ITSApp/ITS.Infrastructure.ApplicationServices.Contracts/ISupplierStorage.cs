using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region comments

/*
 * Latest version : 1.0
 * 
 * Author   : Pardeep Kumar
 * Date     : 21-Dec-2012
 * Version  : 1.0
 * Purposes : Created Service Contracts to Add Supplier folders when a new Supplier is created
 * 
 */

#endregion

namespace ITS.Infrastructure.ApplicationServices.Contracts
{
    public interface ISupplierStorage
    {
        bool CreateSupplierFolder(int supplierID, string storagePath);
        string SupplierRootFolder { get; }
        string[] SupplierCaseFolders { get; }
        string SetSupplierStoragePath(string storagePath, int supplierID, string fileName, string documentType);
        string GetSupplierStoragePath(string storagePath, int supplierID, string fileName, string documentType);
        string GetProjectTreatmentSupplierUploadFolderStoragePath(int supplierID, int referrerProjectTreamentID, int caseID, string fileName, string storagePath);
        string GetProjectTreatmentSupplierUploadFolderStoragePathwithoutfile(int supplierID, int referrerProjectTreamentID, int caseID,string storagePath);
    }
}
