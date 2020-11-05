using Emp_wage_prob;
using System;
using System.Data;

namespace EmployeePayroll
{
    public class Employee
    {
        public int empId { get; set; }
        public string empName { get; set; }
        public string phNo { get; set; }
        public string addr { get; set; }
        //public string compId { get; set; }
        public string gender { get; set; }

    }
    
    public class Department
    {
        public string deptId { get; set; }
        public string deptName { get; set; }
    }
    public class EmpDept
    {
        public int empId { get; set; }
        public string deptId { get; set; }
    }
    public class Payroll
    {
        public int salId { get; set; }
        public int empId { get; set; }
        //public string deptName { get; set; }
        public DateTime startDate { get; set; }
        public double basicPay { get; set; }
        public double ded { get; set; }
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
            //repo.GetAllEmployee();
            //repo.PutEmployee();

            /*TSQL tSQL = new TSQL();
            tSQL.UpdatePayroll(36578.00,"NR");*/

            /*emp.empId = 1;
            emp.compId = "C1";
            emp.empName = "AR";
            emp.gender = "F";
            emp.phNo = "568798089";
            emp.addr = "Mumbai";
*/
            

        }
    }
}
