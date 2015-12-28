namespace PixelloTools.Models.Database.Abstracts
{
    public interface IUser
    {
        AccessLevel UserAccessLevel { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }

    public class User : IUser
    {
        public AccessLevel UserAccessLevel { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}