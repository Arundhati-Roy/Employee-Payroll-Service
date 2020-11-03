using EmployeePayroll;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Emp_wage_prob
{
    public class EmpRepo
    {
        public static string connectString = @"Data Source=(LocalDb)\RoyDB;Initial Catalog=tsetDB;Integrated Security=True";
        SqlConnection sqlConnection = new SqlConnection(connectString);
        public void GetAllEmployee()
        {
            try
            {
                Employee emp = new Employee();
                Department dept = new Department();
                EmpDept empDept = new EmpDept();
                Payroll payroll = new Payroll();
                using (this.sqlConnection)
                {
                    string query = @"select * from employee e,department d,payroll p,empDept ed
                                     where ed.deptId=d.deptId and e.empId=p.empId and ed.empId=e.empId
                                     and e.name=@name";
                    SqlParameter nameParam = new SqlParameter("@name", System.Data.SqlDbType.VarChar, 0);
                    nameParam.Value = "PR";

                    //define sql object
                    SqlCommand cmd = new SqlCommand(query, this.sqlConnection);
                    cmd.Parameters.Add(nameParam);

                    this.sqlConnection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            emp.empId = dr.GetInt32(0);
                            emp.compId = dr.GetString(1);
                            emp.empName = dr.GetString(2);
                            if (dr["gender"] != DBNull.Value)
                                emp.gender = dr.GetString(3);
                            if (dr["empPhone"] != DBNull.Value)
                                emp.phNo = dr.GetString(4);
                            if (dr["addr"] != DBNull.Value)
                                emp.addr = dr.GetString(5);
                            dept.deptId = dr.GetString(6);
                            dept.deptName = dr.GetString(7);
                            payroll.startDate = dr.GetDateTime(10);
                            payroll.basicPay = Convert.ToDouble(dr.GetDecimal(11));
                            payroll.ded = Convert.ToDouble(dr.GetDecimal(12));
                            payroll.tax = Convert.ToDouble(dr.GetDecimal(13));
                            payroll.incomeTax = Convert.ToDouble(dr.GetDecimal(14));
                            payroll.NetPay = Convert.ToDouble(dr.GetDecimal(15));

                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}",
                                emp.empId, emp.empName, emp.compId, emp.gender, emp.phNo, emp.addr,
                                dept.deptId, dept.deptName,
                                payroll.startDate, payroll.basicPay, payroll.ded, payroll.tax, payroll.incomeTax, payroll.NetPay);
                            Console.WriteLine("\n");

                        }

                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    dr.Close();
                    this.sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine("Null data found");
                throw new Exception(e.Message);
            }
            finally
            {
                this.sqlConnection.Close();
            }
        }
    }
}