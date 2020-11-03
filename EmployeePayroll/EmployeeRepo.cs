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
                Employee dataSet = new Employee();
                using (this.sqlConnection)
                {
                    string query = @"UPDATE employee_payroll set basicPay=50000 
                                    where id in (select id from employee_payroll where name=@name)";
                    SqlParameter nameParam = new SqlParameter("@name", System.Data.SqlDbType.VarChar, 0);
                    nameParam.Value = "AR";

                    //define sql object
                    SqlCommand cmd = new SqlCommand(query, this.sqlConnection);
                    cmd.Parameters.Add(nameParam);

                    this.sqlConnection.Open();

                    //SqlDataReader dr = cmd.ExecuteReader();

                    int rows = cmd.ExecuteNonQuery();
                    this.sqlConnection.Close();
                    if (rows > 0)
                    {
                        Console.WriteLine(rows + " row(s) affected");
                    }
                    else
                    {
                        Console.WriteLine("Please check your query");
                    }
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
