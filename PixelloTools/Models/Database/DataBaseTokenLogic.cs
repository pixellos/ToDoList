using System;
using System.Linq;
using PixelloTools.Logging;
using PixelloTools.Models.Database.Abstracts;
using PixelloTools.Models.Database.Context;

namespace PixelloTools.Models.Database
{
    public static class DatabaseTokenLogicHelper
    {
        public static bool IsTokenStillActive(this DateTime time)
        {
            return (DateTime.Now - time) < Constatnts.TokenTime;
        }
    }

    class DataBaseTokenLogic
    {
        private readonly Ilogger _ilogger;
        private readonly AuthenticationContext _authenticationContext;

        public DataBaseTokenLogic(Ilogger ilogger, AuthenticationContext authenticationContext)
        {
            _ilogger = ilogger;
            _authenticationContext = authenticationContext;
        }

        protected virtual string GetUniqueToken() => Guid.NewGuid().ToString();

        /// <param name="Token"></param>
        /// <returns>Null if TokenDoesnt exist</returns>
        internal AccessLevel AccessLevelOfTokenOwner(string Token)
        {
            try
            {
                var user = _authenticationContext.
                    UserTokens.First(x => x.Token == Token);
                if (user.DateTime.IsTokenStillActive())
                {
                    return user.UserDatabaseModel.UserAccessLevel;
                }
               
            }
            catch (InvalidOperationException exception)
            {
                _ilogger.LogIt(exception.Message + exception.Source);
                return    AccessLevel.ERROR;
            }
            catch (Exception exception)
            {
                _ilogger.LogIt(exception.Message);
                throw;
            }
            return AccessLevel.ERROR;
        }

        internal bool IsTokenActive(string token)
        {
            try
            {
                var user = from thisToken in _authenticationContext.UserTokens
                    where thisToken.Token == token
                    select thisToken;
                return user.First().DateTime.IsTokenStillActive();
            }

            catch (InvalidOperationException ex)
            {
                _ilogger.LogIt(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                _ilogger.LogIt(ex);
                throw;
            }
        }

        internal string AddNewToken(IUser user)
        {
            try
            {
                var token = GetUniqueToken();
                _authenticationContext.UserTokens.Add(
                    new UserToken()
                    {
                        DateTime = DateTime.Now,
                        Token = token,
                        UserDatabaseModel = _authenticationContext.
                            UserDatabaseModels.Single(X => X.UserName == user.UserName),
                    });
                _authenticationContext.SaveChanges();
                return token;
            }
            catch (InvalidOperationException exception)
            {
                _ilogger.LogIt(exception);
                return null;
            }
            catch (Exception exception)
            {
                _ilogger.LogIt(exception);
                throw;
            }
        }
    }
}
