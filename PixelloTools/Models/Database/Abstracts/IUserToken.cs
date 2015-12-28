using System;
using System.ComponentModel.DataAnnotations;

namespace PixelloTools.Models.Database.Abstracts
{
    interface IUserToken
    {
        [Key]
        int Id { get; set; }
        DateTime DateTime { get; set; }
        string Token{ get; set; }
        int UserDatabaseModelId { get; set; }
        UserDatabaseModel UserDatabaseModel { get; set; }
    }
}