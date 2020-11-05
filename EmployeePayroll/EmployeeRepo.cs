using EmployeePayroll;
using System;
using System.Collections.Generic;
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
                    nameParam.Value = "NR";

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
                            //emp.compId = dr.GetString(1);
                            emp.empName = dr.GetString(1);
                            if (dr["gender"] != DBNull.Value)
                                emp.gender = dr.GetString(2);
                            if (dr["empPhone"] != DBNull.Value)
                                emp.phNo = dr.GetString(3);
                            if (dr["addr"] != DBNull.Value)
                                emp.addr = dr.GetString(4);
                            dept.deptId = dr.GetString(5);
                            dept.deptName = dr.GetString(6);
                            payroll.salId = dr.GetInt32(7);
                            payroll.startDate = dr.GetDateTime(9);
                            payroll.basicPay = Convert.ToDouble(dr.GetDecimal(10));
                            payroll.ded = Convert.ToDouble(dr.GetDecimal(11));
                            payroll.tax = Convert.ToDouble(dr.GetDecimal(12));
                            payroll.incomeTax = Convert.ToDouble(dr.GetDecimal(13));
                            payroll.NetPay = Convert.ToDouble(dr.GetDecimal(14));

                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}",
                                emp.empId, emp.empName, emp.gender, emp.phNo, emp.addr,
                                dept.deptId, dept.deptName,
                                payroll.salId, payroll.startDate, payroll.basicPay, payroll.ded, payroll.tax, payroll.incomeTax, payroll.NetPay);
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

        public void PutEmployee()
        {
            try
            {
                Employee emp = new Employee();
                using (this.sqlConnection)
                {
                    string query = @" insert into employee(name)
                                      values(@name)";

                    //define sql object
                    SqlCommand cmd = new SqlCommand(query, this.sqlConnection);

                    this.sqlConnection.Open();

                    cmd.Parameters.AddWithValue("@name", "nallia");

                    cmd.ExecuteNonQuery();
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

        public Employee UpdateEmployee()
        {
            Employee emp = new Employee();
            try
            {
                using (this.sqlConnection)
                {
                    string query = @"UPDATE employee set gender ='M' 
                                     where name = 'Neha' or name ='Shreya'";

                    SqlCommand cmd = new SqlCommand(query, this.sqlConnection);
                    this.sqlConnection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            emp.empId = dr.GetInt32(0);
                            //emp.compId = dr.GetString(1);
                            emp.empName = dr.GetString(1);
                            if (dr["gender"] != DBNull.Value)
                                emp.gender = dr.GetString(2);
                            if (dr["empPhone"] != DBNull.Value)
                                emp.phNo = dr.GetString(3);
                            if (dr["addr"] != DBNull.Value)
                                emp.addr = dr.GetString(4);

                            Console.WriteLine("{0},{1},{2},{3},{4}",
                                emp.empId, emp.empName, emp.gender, emp.phNo, emp.addr);
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
            return emp;
        }

        public List<int> GetEmployeeWithinDates(DateTime date)
        {
            Employee emp = new Employee();
            List<int> lempId = new List<int>();
            try
            {
                using (this.sqlConnection)
                {
                    string query = @"SELECT e.empId FROM employee e, payroll p
                                    WHERE startDate BETWEEN CAST(@date AS DATE) AND getDATE()
                                    and e.empId=p.empId;";

                    this.sqlConnection.Open();

                    SqlCommand cmd = new SqlCommand(query, this.sqlConnection);
                    cmd.Parameters.AddWithValue("@date",date);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            emp.empId = dr.GetInt32(0);
                            Console.WriteLine("{0}", emp.empId);
                            Console.WriteLine("\n");
                            lempId.Add(emp.empId);
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
            return lempId;
        }

        public List<double> GetMinMaxAvg()
        {
            Employee emp = new Employee();
            List<double> lempId = new List<double>();
            try
            {
                using (this.sqlConnection)
                {
                    string query = @"SELECT SUM(basicPay) as SumF,Avg(basicPay) as AvgF,
                                    Max(basicPay) as MaxF, min(basicPay) as MinF
                                    FROM payroll";

                    this.sqlConnection.Open();

                    SqlCommand cmd = new SqlCommand(query, this.sqlConnection);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            double sum = Convert.ToDouble(dr.GetDecimal(0));
                            double avg = Convert.ToDouble(dr.GetDecimal(1));
                            double max = Convert.ToDouble(dr.GetDecimal(2));
                            double min = Convert.ToDouble(dr.GetDecimal(3));

                            Console.WriteLine("{0},{1},{2},{3}", sum,avg,max,min);
                            Console.WriteLine("\n");
                            lempId.Add(sum);
                            lempId.Add(avg);
                            lempId.Add(max);
                            lempId.Add(min);
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
            return lempId;
        }


        public Employee AddEmployeeWithPayroll()
        {
            Employee emp = new Employee();
            try
            {
                using (this.sqlConnection)
                {
                    string query = @"UPDATE employee set gender ='M' 
                                     where name = 'Neha' or name ='Shreya'";

                    SqlCommand cmd = new SqlCommand(query, this.sqlConnection);
                    this.sqlConnection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            emp.empId = dr.GetInt32(0);
                            //emp.compId = dr.GetString(1);
                            emp.empName = dr.GetString(1);
                            if (dr["gender"] != DBNull.Value)
                                emp.gender = dr.GetString(2);
                            if (dr["empPhone"] != DBNull.Value)
                                emp.phNo = dr.GetString(3);
                            if (dr["addr"] != DBNull.Value)
                                emp.addr = dr.GetString(4);

                            Console.WriteLine("{0},{1},{2},{3},{4}",
                                emp.empId, emp.empName, emp.gender, emp.phNo, emp.addr);
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
            return emp;
        }

    }
}