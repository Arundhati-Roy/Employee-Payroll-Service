using Emp_wage_prob;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayroll
{
    public class TSQL
    {
        public static SqlConnection ConnSetup()
        {
            return new SqlConnection(@"Data Source=(LocalDb)\RoyDB;Initial Catalog=tsetDB;Integrated Security=True");
        }

        public int UpdatePayroll(double changedPay,string name)
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
                    cmd.Parameters.AddWithValue("@basicPay",changedPay);
                    cmd.Parameters.AddWithValue("@name", name);

                    //sqlConnection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    sqlConnection.Close();
                    if (rows > 0)
                    {
                        Console.WriteLine(rows + " row(s) affected");
                    }
                    else
                    {
                        Console.WriteLine("Please check your query");
                    }

                    sqlConnection.Open();
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

    }
}
