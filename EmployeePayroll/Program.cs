using Emp_wage_prob;
using System;
using System.Data;
using Newtonsoft.Json;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Linq;

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

            TSQL tsql = new TSQL();
            //repo.DelEmployee();
            //tsql.AddToEmpWithPayroll(5354675.00, "Abhimanyu", Convert.ToDateTime("2012-08-23"), "Marketing");

            writeContactInJson();
            //repo.UpdateEmployee();
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
        public static void writeContactInJson()
        {
            string impfp = @"C:\Users\priyadarshini roy\source\repos\EmployeePayroll\EmployeePayroll\EmployeeCSV.csv";
            string expfp = @"C:\Users\priyadarshini roy\source\repos\EmployeePayroll\EmployeePayroll\EmployeeJson.json";
            //reading csv
            using (var reader = new StreamReader(impfp))
                //Console.Write("\t" + "Employee");
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Employee>().ToList();
                Console.WriteLine("Read data successfully");
                foreach (Employee ad in records)
                {
                    var id = ad.empId;
                    Console.Write("\t" + id);
                    Console.Write("\t" + ad.empName);
                    Console.Write("\t" + ad.gender);
                    Console.Write("\t" + ad.phNo);
                    Console.Write("\t" + ad.addr);
                    //Console.Write("\t" + ad.getPhone());
                }

                //writing into json
                JsonSerializer ser = new JsonSerializer();
                using (StreamWriter sw = new StreamWriter(expfp))
                using (JsonWriter jw = new JsonTextWriter(sw))
                {
                    ser.Serialize(jw, records);
                }
                Console.WriteLine("\nWritten into json file");

            }
        }
    }
}
