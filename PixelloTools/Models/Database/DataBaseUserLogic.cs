using System;
using System.Linq;
using System.Linq.Expressions;
using PixelloTools.Logging;
using PixelloTools.Models.Database.Abstracts;
using PixelloTools.Models.Database.Context;

namespace PixelloTools.Models.Database
{
    public class DataBaseUserLogic
    {
        private AuthenticationContext _authenticationContext;
        private Ilogger _ilogger;
        public DataBaseUserLogic(AuthenticationContext authenticationContext,Ilogger iIlogger)
        {
            _ilogger = iIlogger;
            _authenticationContext = authenticationContext;
        }

        private bool Validate(string UserName, string Password) => (UserName?.Length <= 40 && Password?.Length <= 40);

        private bool IsUserExist(string UserName) => _authenticationContext.UserDatabaseModels.Any(x => x.UserName == UserName);

        internal static string HashPassword(string Password) => Password.GetHashCode().ToString();

        public virtual bool Add(string UserName, string Password, AccessLevel UserAccessLevel)
        {
            try
            {
                if (Validate(UserName, Password) && !IsUserExist(UserName))
                { 
                    var userToAdd = new UserDatabaseModel()
                    {
                        UserName = UserName,
                        Password = HashPassword(Password),
                        UserAccessLevel = UserAccessLevel
                    };
                    _authenticationContext.UserDatabaseModels.Add(userToAdd);
                    _authenticationContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception exception)
            {
                _ilogger.LogIt(exception.Message);
                return false;
            }
            return false;
        }

        public virtual bool Authorize(string UserName, string AccessData)
        {
            if (IsUserExist(UserName))
            {
                var user = _authenticationContext.UserDatabaseModels.Single(x => x.UserName == UserName);
                return user.Password == HashPassword(AccessData);
            }
            return false;
        }

        internal int? GetUserId(string userName)
            => _authenticationContext.
            UserDatabaseModels.SingleOrDefault(x => x.UserName == userName)?.Id;

        public bool Modify(Expression<System.Func<UserDatabaseModel, bool>> userToModifyExpression, IUser AddUser)
        {
            if (Delete(userToModifyExpression))
            {
                var returnValue = Add(AddUser.UserName, AddUser.Password, AddUser.UserAccessLevel);
                _authenticationContext.SaveChanges();
                return returnValue;
            }
            return false;
        }

        internal bool Delete(Expression<System.Func<UserDatabaseModel,bool>> userDatabasaePredicate )
        {
            try
            {
                var user = _authenticationContext.UserDatabaseModels.First(userDatabasaePredicate);
                _authenticationContext.UserDatabaseModels.Remove(user);
                _authenticationContext.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                _ilogger.LogIt(exception.Message);
                return false;
            }
        }
    }
}
