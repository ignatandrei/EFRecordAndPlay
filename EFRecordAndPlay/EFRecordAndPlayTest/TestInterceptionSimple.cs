using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.IO;
using EFRecordAndPlay;
using EFRecordAndPlayTest.DatabaseRelated;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EFRecordAndPlayTest
{
    [TestClass]
    public class TestInterceptionSimple
    {
        

        [TestMethod]
        public void TestRecordAndPlayDefault()
        {
            //Database.SetInitializer<ContextForDatabase>(new CreateDatabaseIfNotExists<ContextForDatabase>());

            Database.SetInitializer<ContextForDatabase>(null);
            #region set record EF
            var record = new InterceptionRecordOrPlay(@"a.zip", ModeInterception.Record);

            DbInterception.Add(record);
            #endregion
            var employeeFromDatabase = EmployeeWithDepartment();

            DbInterception.Remove(record);

            File.Copy("a.zip","b.zip",true);
            #region set play what is recorded EF
            var play = new InterceptionRecordOrPlay(@"b.zip", ModeInterception.Play);            
            DbInterception.Add(play);
            #endregion
            var employeeFromPlay = EmployeeWithDepartment();
            // assert id's are equal 
            Assert.AreEqual(employeeFromDatabase.Id, employeeFromPlay.Id);
            Assert.AreEqual(employeeFromDatabase.Department.Id,employeeFromPlay.Department.Id);
            DbInterception.Remove(play);
            
            

        }

        [TestMethod]
        public void TestDifferentWhenCreating2Times()
        {
            Database.SetInitializer<ContextForDatabase>(null);

            var employeeFromDatabase = EmployeeWithDepartment();

            var employeeFromDatabaseAgain = EmployeeWithDepartment();
            // assert id's are equal 
            Assert.AreNotEqual(employeeFromDatabase.Id, employeeFromDatabaseAgain.Id);
            Assert.AreNotEqual(employeeFromDatabase.Department.Id, employeeFromDatabaseAgain.Department.Id);

        }


        Employee EmployeeWithDepartment()
        {


            
            var dep = new Department { NameDepartment = "andrei" };
            using (var m = new ContextForDatabase())
            {
                m.Department.Add(dep);
                m.SaveChanges();
            }
            

            Console.WriteLine(dep.Id);
            var emp = new Employee() { FirstNameEmployee = "Andrei", LastNameEmployee = "Ignat" };
            emp.Department = dep;
            using (var m = new ContextForDatabase())
            {
                m.Employee.Add(emp);
                try
                {
                    m.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return emp;

        }
        
    }
}
