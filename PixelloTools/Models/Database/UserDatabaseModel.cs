using PixelloTools.Models.Database.Abstracts;

namespace PixelloTools.Models.Database
{
    public class UserDatabaseModel :IUser
    {
        public int Id { get; set; }
        public AccessLevel UserAccessLevel { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}