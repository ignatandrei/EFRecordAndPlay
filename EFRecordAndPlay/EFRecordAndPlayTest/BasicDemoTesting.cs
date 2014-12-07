using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using EFRecordAndPlay;
using EFRecordAndPlayTest.DatabaseRelated;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFRecordAndPlayTest
{
    /// <summary>
    /// Summary description for BasicDemoTesting
    /// </summary>
    [TestClass]
    public class BasicDemoTesting
    {
        public BasicDemoTesting()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void VerifyIValidatableWorks()
        {
            Database.SetInitializer<ContextForDatabase>(null);
            #region set record EF
            var record = new InterceptionRecordOrPlay(@"VerifyIValidatableWorks.zip", ModeInterception.Play);

            DbInterception.Add(record);
            #endregion
            var e= new Employee();
            e.ValidateEmployee = true;
            e.IDDepartment = 60000;
            var err= e.Validate(null).ToArray();
            Assert.IsNotNull(err);
            Assert.AreEqual(1, err.Length);

            
        }
    }
}
