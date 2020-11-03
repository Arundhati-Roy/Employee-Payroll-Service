using Emp_wage_prob;
using System;

namespace EmployeePayroll
{
    class Employee
    {
        public int empId { get; set; }
        public string empName { get; set; }
        public string phNo { get; set; }
        public string addr { get; set; }
        public string compId { get; set; }
        public string gender { get; set; }

    }
    class Department
    {
        public string deptId { get; set; }
        public string deptName { get; set; }
    }
    class EmpDept
    {
        public int empId { get; set; }
        public string deptId { get; set; }

    }
    class Payroll
    {
        public int empId { get; set; }
        public string deptName { get; set; }
        public DateTime startDate { get; set; }
        public double basicPay { get; set; }
        public double deductions { get; set; }
        public double tax { get; set; }
        public double incomeTax { get; set; }
        public double NetPay { get; set; }

    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll");
            EmpRepo repo = new EmpRepo();
            Employee emp = new Employee();

            /*emp.empId = 1;
            emp.compId = "C1";
            emp.empName = "AR";
            emp.gender = "F";
            emp.phNo = "568798089";
            emp.addr = "Mumbai";
*/
            repo.GetAllEmployee();
        }
    }
}
