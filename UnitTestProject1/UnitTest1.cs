using Emp_wage_prob;
using EmployeePayroll;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        /*/// <summary>
        /// Adds the employee.
        /// </summary>
        [TestMethod]
        public void AddEmployee()
        {
            //Arrange
            EmpRepo repo = new EmpRepo();
            Employee expected = new Employee();

            //Act
            Employee actual = repo.UpdateEmployee();

            //Arrange
            Assert.AreEqual(expected, actual);
        }*/

        /// <summary>
        /// Given the name and salary to change update payroll.Stored Procedure
        /// </summary>
        [TestMethod]
        public void SP_GivenSalaryDetails_UpdateUsingName()
        {
            //Arrange
            TSQL tSQL = new TSQL();
            Payroll payroll = new Payroll();
            
            //Act
            int sal = tSQL.UpdatePayroll(56000.00,"NR");

            //Arrange
            Assert.AreEqual(payroll.basicPay, sal);
        }
        /// <summary>
        /// Given the name and salary to change update payroll.Stored Procedure
        /// </summary>
        [TestMethod]
        public void GetEmpId_FromGivenDateToNow()
        {
            //Arrange
            EmpRepo repo = new EmpRepo();
            //Employee emp = new Employee();
            List<int> expected = new List<int>(){ 1, 2 };

            //Act
            DateTime d = Convert.ToDateTime("2010-09-22");
            List<int> l = repo.GetEmployeeWithinDates(d);

            //Arrange
            Assert.AreEqual(expected[0], l[0]);
        }
    }
}
