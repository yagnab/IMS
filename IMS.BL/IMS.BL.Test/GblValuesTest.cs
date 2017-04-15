using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using IMS.BL;

namespace IMS.BL.Test
{
    [TestClass]
    public class GblValuesTest
    {
        
        private TestContext testContextInstance { get; set; }

        [TestMethod]

        public void CheckConnectionString()
        {
            try
            {
                testContextInstance.WriteLine(GblValues.connStr);
            }
            catch (TypeInitializationException e)
            {
                testContextInstance.WriteLine( e.InnerException.ToString() );
            }
            
        }

        public void CreateValidUserList()
        {
            List<User> values = GblValues.GetUsers();

        }
    }
}
