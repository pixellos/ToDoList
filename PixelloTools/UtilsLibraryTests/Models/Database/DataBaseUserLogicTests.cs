using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using DatabaseLib.Models.Database.Abstracts;
using TWIN_API.Models.Logging;
using static System.Data.Entity.Database;

namespace DatabaseLib.Models.Database.Tests
{
    [TestClass()]
    public class DataBaseUserLogicTests
    {
        const string databaseForTests = "TestString";
        private static Context.AuthenticationContext context = new Context.AuthenticationContext();
        private DataBaseUserLogic userLogic = new DataBaseUserLogic(context, ConsoleLogger.GetLoger());

        [TestMethod()]
        public void DataBaseUserLogicTest()
        {
            try
            {
                Delete(databaseForTests);
                Thread.Sleep(100);
                context = new Context.AuthenticationContext(databaseForTests);
                userLogic = new DataBaseUserLogic(context, ConsoleLogger.GetLoger());
            }
            catch (Exception exception)
            {
                ConsoleLogger.GetLoger().LogIt(exception);
                throw;
            }
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void AddTest()
        {
            userLogic.Add("ADM", "ADM", AccessLevel.Administrator);
            Assert.IsTrue(userLogic.Authorize("ADM", "ADM"));
        }

        public void GetUserIdTest()
        {
            var user = new User() { UserName = "UserIdTest", Password = "Password", UserAccessLevel = AccessLevel.Listener };
            var gettedUserId = userLogic.GetUserId(user.UserName);

            var userFromDataBase = context.UserDatabaseModels.Single(x => x.UserName == user.UserName && x.Password == user.Password);

            Assert.AreEqual(userFromDataBase.Id,gettedUserId);

        }

        [TestMethod()]
        public void AuthorizeTest()
        {
            context.UserDatabaseModels.Add(new UserDatabaseModel()
            {
                UserName = "Test",
                Password = "Test",
                UserAccessLevel = AccessLevel.Root
            });
            userLogic.Authorize("Test", "Test");
        }

        [TestMethod()]
        public void ModifyTest()
        {
            var user = new User() {UserName = "ModifyTest",Password = "ModifyPassword",UserAccessLevel = AccessLevel.Listener};
            var tochange = new User() {UserName = "Modified",Password = "ModifiedPassword",UserAccessLevel = AccessLevel.Administrator};
            userLogic.Add(user.UserName,user.Password,user.UserAccessLevel);

            Assert.IsTrue(
               userLogic.Authorize(user.UserName, user.Password));

            userLogic.Modify(model => model.UserName == user.UserName, tochange);
            Assert.IsTrue(
                userLogic.Authorize(tochange.UserName,tochange.Password));

            Assert.IsFalse(userLogic.Authorize(user.UserName,user.Password));
        }

        [TestMethod()]
        public void DeleteTest()
        {
            userLogic.Add("ADM", "ADM", AccessLevel.Administrator);
            userLogic.Delete(x=>x.UserName == "ADM");
            Assert.IsFalse(userLogic.Authorize("ADM", "ADM"));
        }
    }
}