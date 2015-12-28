using System.Data.Entity;
using System.Linq;
using PixelloTools.Models.Database.Abstracts;

namespace PixelloTools.Models.Database.Context
{
    public class AuthenticationContext : System.Data.Entity.DbContext
    {
        public AuthenticationContext(string ConnectionString) : base(ConnectionString){}

        public AuthenticationContext() : this(Constatnts.DefaultDBConnection){}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserToken>().HasRequired(x => x.UserDatabaseModel);
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<UserDatabaseModel> UserDatabaseModels { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }

        public UserDatabaseModel GetDatabaseModelByUser(IUser user) => UserDatabaseModels.First(x => x.UserName == user.UserName &&
                                                                        x.Password == user.Password);
    }
}
