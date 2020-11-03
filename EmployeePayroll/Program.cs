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
