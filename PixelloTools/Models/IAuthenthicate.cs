using PixelloTools.Models.Database;
using PixelloTools.Models.Database.Abstracts;

namespace PixelloTools.Models
{
    public interface IAuthenthicate
    {
        string GetToken(string UserName, string PasswordData);
        /// <summary>
        /// Under this line implementation should make usability restriction Only for admin+ access level
        /// </summary>
        bool Add(string Token, IUser UserToAdd);
        bool Modyfy(string Token, string TargetUsername, IUser NewUserData);
        bool Delete(string Token, string UsernameToDelete);
        AccessLevel GetAccessLevel(string Token);
    }
}
