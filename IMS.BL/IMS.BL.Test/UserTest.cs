using Microsoft.VisualStudio.TestTools.UnitTesting;

using IMS.BL;

namespace IMS.BL.Test
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void CreateValidUser()
        {
            //new user 
            var bob = User.GetUser("testuser1", "testpassword1");
            System.Console.WriteLine(bob.username);
            Assert.AreEqual("testuser1", bob.username);

        }
    }
}
