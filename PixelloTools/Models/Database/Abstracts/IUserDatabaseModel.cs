using System.ComponentModel.DataAnnotations;

namespace PixelloTools.Models.Database.Abstracts
{
    interface IUserDatabaseModel: IUser
    {
        [Key]
        int Id { get; set; }
    }
}