using Emp_wage_prob;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayroll
{
    public class TSQL
    {
        public static SqlConnection ConnSetup()
        {
            return new SqlConnection(@"Data Source=(LocalDb)\RoyDB;Initial Catalog=tsetDB;Integrated Security=True");
        }

        public int UpdatePayroll(double changedPay, string name)
        {
            SqlConnection sqlConnection = ConnSetup();
            int salary = 0;
            try
            {
                Payroll payroll = new Payroll();
                Employee employee = new Employee();

                using (sqlConnection)
                {
                    sqlConnection.Open();

                    //define sql object
                    SqlCommand cmd = new SqlCommand("uPayroll", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@basicPay", changedPay);
                    cmd.Parameters.AddWithValue("@name", name);

                    //sqlConnection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    //sqlConnection.Close();
                    if (rows > 0)
                    {
                        Console.WriteLine(rows + " row(s) affected");
                    }
                    else
                    {
                        Console.WriteLine("Please check your query");
                    }

                    //sqlConnection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            payroll.salId = dr.GetInt32(0);
                            payroll.empId = dr.GetInt32(1);
                            payroll.startDate = dr.GetDateTime(2);
                            payroll.basicPay = Convert.ToDouble(dr.GetDecimal(3));
                            payroll.ded = Convert.ToDouble(dr.GetDecimal(4));
                            payroll.tax = Convert.ToDouble(dr.GetDecimal(5));
                            payroll.incomeTax = Convert.ToDouble(dr.GetDecimal(6));
                            payroll.NetPay = Convert.ToDouble(dr.GetDecimal(7));

                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7}",
                                payroll.salId, payroll.empId, payroll.startDate, payroll.basicPay, payroll.ded, payroll.tax, payroll.incomeTax, payroll.NetPay);
                            Console.WriteLine("\n");
                        }

                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }

                    dr.Close();
                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine("Null data found");
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return salary;
        }

        public int AddToEmpWithPayroll(double changedPay, string name, DateTime startDate, string deptName)
        {
            SqlConnection sqlConnection = ConnSetup();
            int salary = 0;
            try
            {
                Payroll payroll = new Payroll();
                Employee emp = new Employee();
                Department dept = new Department();
                EmpDept empDept = new EmpDept();

                using (sqlConnection)
                {
                    sqlConnection.Open();

                    //define sql object
                    SqlCommand cmd = new SqlCommand("spAddEmployee", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@basicPay", changedPay);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@start_date", startDate);
                    cmd.Parameters.AddWithValue("@deptName", deptName);


                    //sqlConnection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    /*int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        Console.WriteLine(rows + " row(s) affected");
                    }
                    else
                    {
                        Console.WriteLine("Please check your query");
                    }*/


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
                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine("Null data found");
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
            return salary;
        }

        public void AddToEmpWithPayrollWithThread(double changedPay, string name, DateTime startDate, string deptName)
        {
            Task thread = new Task(() =>
              {

                  SqlConnection sqlConnection = ConnSetup();
                  //int salary = 0;
                  try
                  {
                      Payroll payroll = new Payroll();
                      Employee emp = new Employee();
                      Department dept = new Department();
                      EmpDept empDept = new EmpDept();

                      using (sqlConnection)
                      {
                          sqlConnection.Open();

                          //define sql object
                          SqlCommand cmd = new SqlCommand("spAddEmployee", sqlConnection);

                          cmd.CommandType = CommandType.StoredProcedure;
                          cmd.Parameters.AddWithValue("@basicPay", changedPay);
                          cmd.Parameters.AddWithValue("@name", name);
                          cmd.Parameters.AddWithValue("@start_date", startDate);
                          cmd.Parameters.AddWithValue("@deptName", deptName);


                          /*sqlConnection.Open();
                          int rows = cmd.ExecuteNonQuery();
                          sqlConnection.Close();
                          if (rows > 0)
                          {
                              Console.WriteLine(rows + " row(s) affected");
                          }
                          else
                          {
                              Console.WriteLine("Please check your query");
                          }*/

                          sqlConnection.Open();
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
                          sqlConnection.Close();
                      }
                  }
                  catch (Exception e)
                  {
                      //Console.WriteLine("Null data found");
                      throw new Exception(e.Message);
                  }
                  finally
                  {
                      sqlConnection.Close();
                  }
                  //return salary;
              });
            thread.Start();
        }


    }
}
