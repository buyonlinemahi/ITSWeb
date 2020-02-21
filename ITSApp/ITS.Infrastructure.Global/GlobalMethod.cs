using System.IO;

/*
FileName:         GlobalMethod
Description:  Created a file for Global Methods
Auther:       Anuj Batra
 */
namespace ITS.Infrastructure.Global
{
   public class GlobalMethod
    {
       public static string GetMimeType(string fileName)
       {
           string mimeType = "application/unknown";
           string ext = Path.GetExtension(fileName).ToLower();
           Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
           if (regKey != null && regKey.GetValue("Content Type") != null)
           {
               mimeType = regKey.GetValue("Content Type").ToString();
           }
           else if (ext == ".png")
           {
               mimeType = "image/png";
           }
           else if (ext == ".flv")
           {
               mimeType = "video/x-flv";
           }
           return mimeType;
       }
       

    }


}
