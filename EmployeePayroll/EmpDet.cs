using System;

namespace Emp_wage_prob
{
    public class EmpDet
    {
        public int empId { get; set; }
        public string empName { get; set; }
        public string phNo { get; set; }
        public string addr { get; set; }
        //public string compId { get; set; }
        public string gender { get; set; }
        public string deptId { get; set; }
        public string deptName { get; set; }
        public int salId { get; set; }
        //public string deptName { get; set; }
        public DateTime startDate { get; set; }
        public double basicPay { get; set; }
        public double ded { get; set; }
        public double tax { get; set; }
        public double incomeTax { get; set; }
        public double NetPay { get; set; }
    }
}