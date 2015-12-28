using System;
using PixelloTools.Models.Database.Abstracts;

namespace PixelloTools.Models.Database
{
    public class UserToken : IUserToken
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Token { get; set; }
        public int UserDatabaseModelId { get; set; }
        public UserDatabaseModel UserDatabaseModel { get; set; }
    }
}