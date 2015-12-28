using Microsoft.VisualStudio.TestTools.UnitTesting;
using TWIN_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLib.Models.Database;
using DatabaseLib.Models.Database.Abstracts;
using DatabaseLib.Models.Database.Context;

namespace TWIN_API.Models.Tests
{
    [TestClass()]
    public class AutenthicateTests
    {
        private const string ConnectionString = "TestAuthenticate";
        private static Authenticate Authenticate = new Authenticate(new AuthenticationContext(ConnectionString));

        IUser user = new User() { UserName = "123123", Password = "dfsdssdf", UserAccessLevel = AccessLevel.Root };
        IUser root = new User() {Password = "Password", UserName = "Root", UserAccessLevel = AccessLevel.Root};
        IUser changedRoot = new User(){ UserName = "ChangedRoot", Password = "ChangedRootPswrd", UserAccessLevel = AccessLevel.Root};
        IUser justUser = new User() {UserName = "just",Password = "tetesed",UserAccessLevel = AccessLevel.User};

        [TestMethod()]
        public void AutenthicateTest()  {   }

        [TestMethod()]
        public void GetTokenTest()
        {
            Authenticate.ResetDatabase();
            var token =  Authenticate.GetToken(root.UserName, root.Password);
            Assert.IsTrue(
                Authenticate.CheckToken(token));
        }

        [TestMethod()]
        public void AddTest()
        {
            Assert.IsTrue(
               Authenticate.Add(
                   Authenticate.GetToken(root.UserName,root.Password),user));
        }

        [TestMethod()]
        public void ModyfyTest()
        {

            var firstToken = Authenticate.
                GetToken(root.UserName,root.Password);
            Authenticate.Add(firstToken, user);
            Authenticate.Modyfy(firstToken, user.UserName, justUser);

            Assert.IsNotNull(
                Authenticate.GetToken(justUser.UserName,justUser.Password));

            Assert.IsNull(Authenticate.GetToken(user.UserName,user.Password));
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var token = Authenticate.GetToken(root.UserName, root.Password);
            Authenticate.Add(token, user);
            var tokenToCheck = Authenticate.GetToken(user.UserName, user.Password);
            Assert.IsTrue(Authenticate.CheckToken(tokenToCheck));

            Authenticate.Delete(token, user.UserName);
            Assert.IsFalse(Authenticate.CheckToken(tokenToCheck));
        }

        [TestMethod()]
        public void GetAccessLevelTest()
        {
            Assert.AreEqual(
                Authenticate.GetAccessLevel(
                    Authenticate.GetToken(root.UserName, root.Password))
                    ,root.UserAccessLevel);
        }
    }
}