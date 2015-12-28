using PixelloTools.Logging;
using PixelloTools.Models.Database;
using PixelloTools.Models.Database.Abstracts;
using PixelloTools.Models.Database.Context;

namespace PixelloTools.Models
{
    public class Authenticate : IAuthenthicate{
        private AuthenticationContext _authenticationContext ;
        internal DataBaseTokenLogic _dataBaseTokenLogic;
        internal DataBaseUserLogic _dataBaseUserLogic;

        internal IUser RootUser = new User()
        {
            Password = "RootPswrd",
            UserAccessLevel = AccessLevel.Root,
            UserName = "RootPWNED"
        };

        public Authenticate(AuthenticationContext authenticationContext)
        {
            _authenticationContext = authenticationContext;
            _dataBaseTokenLogic = new DataBaseTokenLogic(ConsoleLogger.GetLoger(), _authenticationContext);
            _dataBaseUserLogic = new DataBaseUserLogic(_authenticationContext,ConsoleLogger.GetLoger());
        }

        ~Authenticate()
        {
            _authenticationContext.Dispose();
        }

        /// <summary>
        /// After using this YOU MUST change password for Root/Password;
        /// </summary>
        internal void ResetDatabase()
        {
            _authenticationContext.Database.Delete();
            _dataBaseUserLogic.Add("Root", "Password", AccessLevel.Root);
            
        }

        public string GetToken(string UserName, string PasswordData)
        {
           return _dataBaseTokenLogic.AddNewToken(
                new User()
                {
                    UserName = UserName,
                    Password = PasswordData
                });
        }

        internal bool CheckToken(string token)
            => _dataBaseTokenLogic.AccessLevelOfTokenOwner(token) > AccessLevel.ERROR;
 
        
        public bool Add(string Token, IUser UserToAdd)
        {
            var accessLevel = _dataBaseTokenLogic.AccessLevelOfTokenOwner(Token);
            if ( accessLevel >= AccessLevel.Administrator && UserToAdd.UserAccessLevel <= accessLevel)
            {
                return _dataBaseUserLogic.Add(
                    UserToAdd.UserName,UserToAdd.Password,UserToAdd.UserAccessLevel
                    );
            }
            return false;
        }

        public bool Modyfy(string Token, string TargetUsername, IUser NewUserData)
        {
            if (_dataBaseTokenLogic.AccessLevelOfTokenOwner(Token) >= AccessLevel.Administrator)
            {
                return _dataBaseUserLogic.Modify(
                    model => model.UserName == TargetUsername ,NewUserData);
            }
            return false;
        }

        public bool Delete(string Token, string UsernameToDelete)
        {
            if (_dataBaseTokenLogic.IsTokenActive(Token))
            {
                return _dataBaseUserLogic.Delete(x => x.UserName == UsernameToDelete);
            }
            return false;
        }

        public AccessLevel GetAccessLevel(string Token)
        {
            return _dataBaseTokenLogic.AccessLevelOfTokenOwner(Token);
        }
    }
}