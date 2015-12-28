using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseLib.Models.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DatabaseLib.Models.Database.Abstracts;
using DatabaseLib.Models.Database.Context;
using TWIN_API.Models.Logging;

namespace DatabaseLib.Models.Database.Tests
{
    [TestClass()]
    public class DatabaseTokenLogicHelperTests
    {
        private static Context.AuthenticationContext _context = new Context.AuthenticationContext();
        private DataBaseUserLogic _dataBaseUserLogic = new DataBaseUserLogic(_context, ConsoleLogger.GetLoger());
        private DataBaseTokenLogic _dataBaseTokenLogic = new DataBaseTokenLogic(ConsoleLogger.GetLoger(),_context);
        const string DatabaseName = "DatabaseForTests";
        public DatabaseTokenLogicHelperTests()
        {
            _context = new AuthenticationContext(DatabaseName);
            _context.Database.Delete();
            _dataBaseTokenLogic = new DataBaseTokenLogic(ConsoleLogger.GetLoger(), _context);
            _dataBaseUserLogic = new DataBaseUserLogic(_context,ConsoleLogger.GetLoger());
        }


        [TestMethod()]
        public void AddNewTokenTest()
        {
            IUser user = new User() {UserName = "Test",Password = "Test1",UserAccessLevel = AccessLevel.User};
            _dataBaseUserLogic.Add(user.UserName, user.Password, user.UserAccessLevel);
            _dataBaseTokenLogic.AddNewToken(user);

            var list = _context.UserTokens.ToList();
            var password = DataBaseUserLogic.HashPassword(user.Password);
            Assert.IsTrue(
                _context.UserTokens.Any(
                    x =>
                        x.UserDatabaseModel.UserName == user.UserName && x.UserDatabaseModel.Password == password
                      ));
        }

        [TestMethod()]
        public void IsTokenStillActiveTest()
        {
            IUser user = new User() { UserName = "Test", Password = "Test1", UserAccessLevel = AccessLevel.User };
            _dataBaseUserLogic.Add(user.UserName, user.Password, user.UserAccessLevel);
            var token = _dataBaseTokenLogic.AddNewToken(user);
            Assert.IsFalse(_dataBaseTokenLogic.IsTokenActive("testeetsetsets"));
            Assert.IsTrue(
                _dataBaseTokenLogic.IsTokenActive(token)
                );
        }

  

        [TestMethod()]
        public void AccessLevelOfTokenOwnerTest()
        {
            IUser user = new User()
            { UserName = "Test23", Password = "Test11", UserAccessLevel = AccessLevel.User };
            _dataBaseUserLogic.Add(user.UserName, user.Password, user.UserAccessLevel);
            var token = _dataBaseTokenLogic.AddNewToken(user);

            Assert.AreEqual(
                _dataBaseTokenLogic.AccessLevelOfTokenOwner(token)
                ,user.UserAccessLevel);
        }
    }
}