
namespace ITSPublicApp.Infrastructure.ApplicationServices.Contracts
{
   public interface IEncryption
    {
        string HashPassword(string password);
        bool VerifyHashedPassword(string password, string hashedPassword);
    }
}
