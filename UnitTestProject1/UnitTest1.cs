using Emp_wage_prob;
using EmployeePayroll;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;

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
            CollectionAssert.AreEqual((System.Collections.ICollection)expected, (System.Collections.ICollection)actual);
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
            int sal = tSQL.UpdatePayroll(56000.00, "NR");

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
/*
        /// <summary>
        /// Given the name and salary to change update payroll.Stored Procedure
        /// </summary>
        [TestMethod]
        public void GetMinMaxAvgSum()
        {
            //Arrange
            EmpRepo repo = new EmpRepo();
            //Employee emp = new Employee();
            Payroll payroll = new Payroll();
            List<double> expected = new List<double>() { 167769.00, 55923.000000, 75000.00, 36769.00 };

            //Act
            List<double> l = repo.GetMinMaxAvg();

            //Arrange
            Assert.AreEqual(expected[0], l[0]);
            Assert.AreEqual(expected[1], l[1]);
            Assert.AreEqual(expected[2], l[2]);
            Assert.AreEqual(expected[3], l[3]);
        }*/

        /// <summary>
        /// Given the name and salary to change update payroll.Stored Procedure
        /// </summary>

        [TestMethod]
        public void SP_AddEmployee_UpdateOtherTables()
        {
            //Arrange
            TSQL tSQL = new TSQL();
            Payroll payroll = new Payroll();

            //Act
            int sal = tSQL.AddToEmpWithPayroll(5354675.00, "Abhimanyu", Convert.ToDateTime("2012-08-23"), "Marketing");

            //Arrange
            Assert.AreEqual(payroll.basicPay, sal);
        }

        [TestMethod]
        public void CascadeDeleteEmployee()
        {
            //Arrange
            EmpRepo empRepo = new EmpRepo();
            Payroll payroll = new Payroll();

            //Act
            empRepo.DelEmployee();

            //Arrange
            //Assert.AreEqual(3, );
        }

        [TestMethod]
        public void AddMultEmployee_RecordTime()
        {
            //Arrange
            TSQL tSQL = new TSQL();
            //Payroll payroll = new Payroll();

            //Act
            //int sal = 
            DateTime sd = DateTime.Now;
            tSQL.AddToEmpWithPayroll(5354675.00, "Abhimanyu", Convert.ToDateTime("2012-08-23"), "Marketing");
            tSQL.AddToEmpWithPayroll(1587676.00, "Samay", Convert.ToDateTime("2014-04-24"), "Sales");
            tSQL.AddToEmpWithPayroll(24675.00, "Upmanyu", Convert.ToDateTime("2015-05-25"), "Finance");
            tSQL.AddToEmpWithPayroll(354675.00, "Biswa", Convert.ToDateTime("2016-06-26"), "Operating");
            tSQL.AddToEmpWithPayroll(4675.00, "Sagar", Convert.ToDateTime("2017-07-27"), "Marketing");
            DateTime ed = DateTime.Now;
            //Arrange
            //Assert.AreEqual(payroll.basicPay, sal);
            Console.WriteLine("Duration {0}" ,sd-ed);
        }
        [TestMethod]
        public void AddMultEmployee_RecordTimeWithThread()
        {
            //Arrange
            TSQL tSQL = new TSQL();
            EmpRepo empRepo = new EmpRepo();

            //Act
            empRepo.DelEmployee();
            DateTime sd = DateTime.Now;
            tSQL.AddToEmpWithPayrollWithThread(5354675.00, "Abhimanyu", Convert.ToDateTime("2012-08-23"), "Marketing");
            tSQL.AddToEmpWithPayrollWithThread(1587676.00, "Samay", Convert.ToDateTime("2014-04-24"), "Sales");
            tSQL.AddToEmpWithPayrollWithThread(24675.00, "Upmanyu", Convert.ToDateTime("2015-05-25"), "Finance");
            tSQL.AddToEmpWithPayrollWithThread(354675.00, "Biswa", Convert.ToDateTime("2016-06-26"), "Operating");
            tSQL.AddToEmpWithPayrollWithThread(4675.00, "Sagar", Convert.ToDateTime("2017-07-27"), "Marketing");
            DateTime ed = DateTime.Now;
            //Arrange
            //Assert.AreEqual(payroll.basicPay, sal);
            Console.WriteLine("Duration {0}", sd - ed);
        }
    }
}
