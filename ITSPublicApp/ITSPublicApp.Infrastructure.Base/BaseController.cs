using ITSPublicApp.Domain.Models;
using ITSPublicApp.Infrastructure.Global;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace ITSPublicApp.Infrastructure.Base
{
    public abstract class BaseController : Controller
    {
        public ITSUser ITSUser
        {
            get { return (ITSUser)Session[GlobalConst.SessionKeys.USER]; }
            set { Session[GlobalConst.SessionKeys.USER] = value; }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this.ITSUser == null)
            {
                RedirectToAction("Index", "Home");
            }
            
            base.OnActionExecuting(filterContext);
        }
        string _key = "qwertyuiopasdfghjklzxcvbnm1234567890";
        public string EncryptString(string encryptString)
        {
            string _sessionID = "";
            if (ITSUser != null) 
            _sessionID = ITSUser.UserID.ToString() + "," + "qwerty12345asdfgh67890" ;
            string concatenateEncryptString = encryptString + "," + _sessionID;
            TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
            byte[] byteHash, byteBuff;
            string strTempKey = _key;
            byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            objHashMD5 = null;
            objDESCrypto.Key = byteHash;
            objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = ASCIIEncoding.ASCII.GetBytes(concatenateEncryptString);
            return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
            TransformFinalBlock(byteBuff, 0, byteBuff.Length)).Replace("/", "!").Replace("+", "]");
        }

        public string DecryptString(string cipherText)
        {
            cipherText = cipherText.Replace("!", "/").Replace("]", "+");
            TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
            byte[] byteHash, byteBuff;
            string strTempKey = _key;
            byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            objHashMD5 = null;
            objDESCrypto.Key = byteHash;
            objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = Convert.FromBase64String(cipherText);
            string strDecrypted = ASCIIEncoding.ASCII.GetString
            (objDESCrypto.CreateDecryptor().TransformFinalBlock
            (byteBuff, 0, byteBuff.Length));
            objDESCrypto = null;
            string _sessionID = "";
            if (ITSUser != null) 
            _sessionID = ITSUser.UserID.ToString();
            var _onlyDecryptedId = strDecrypted.Split(',');

            if (_onlyDecryptedId[1].ToString() == _sessionID)
            {
                return _onlyDecryptedId[0];
            }
            else
            {
                return "0";
            }
        }



        public string EncryptString2(string encryptString)
        {
            string concatenateEncryptString = encryptString;
            TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
            byte[] byteHash, byteBuff;
            string strTempKey = _key;
            byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            objHashMD5 = null;
            objDESCrypto.Key = byteHash;
            objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = ASCIIEncoding.ASCII.GetBytes(concatenateEncryptString);
            return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
            TransformFinalBlock(byteBuff, 0, byteBuff.Length)).Replace("/", "!").Replace("+", "]");
        }

        public string DecryptString2(string cipherText)
        {
            cipherText = cipherText.Replace("!", "/").Replace("]", "+");
            TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
            byte[] byteHash, byteBuff;
            string strTempKey = _key;
            byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            objHashMD5 = null;
            objDESCrypto.Key = byteHash;
            objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = Convert.FromBase64String(cipherText);
            string strDecrypted = ASCIIEncoding.ASCII.GetString
            (objDESCrypto.CreateDecryptor().TransformFinalBlock
            (byteBuff, 0, byteBuff.Length));
            objDESCrypto = null;
            return strDecrypted;
           
        }  


        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            ViewBag.LayoutPageDisplayUser = ITSUser != null ? ITSUser.FullName : string.Empty;
            ViewBag.LogOutURL = Url.Action(ITSPublicApp.Infrastructure.Global.GlobalConst.Actions.HomeController.LogOut, ITSPublicApp.Infrastructure.Global.GlobalConst.Controllers.Home, new { area = "" });
            if (ITSUser != null && ITSUser.UserTypeID == (int)GlobalConst.UserType.Referrer)
                ViewBag.HomeURL = Url.Action(ITSPublicApp.Infrastructure.Global.GlobalConst.Actions.Area.Referrer.HomeController.Index, ITSPublicApp.Infrastructure.Global.GlobalConst.Controllers.Area.Referrer.Home, new { area = GlobalConst.Areas.Referrer });
            else
                ViewBag.HomeURL = Url.Action(ITSPublicApp.Infrastructure.Global.GlobalConst.Actions.Area.Supplier.HomeController.Index, ITSPublicApp.Infrastructure.Global.GlobalConst.Controllers.Area.Supplier.Home, new { area = GlobalConst.Areas.Supplier });
        }
    }
}
