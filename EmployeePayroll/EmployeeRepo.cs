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
                    string query = @"SELECT * from employee";

                    //define sql object
                    SqlCommand cmd = new SqlCommand(query, this.sqlConnection);

                    this.sqlConnection.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            dataSet.empId = dr.GetInt32(0);
                            dataSet.compId = dr.GetString(1);
                            dataSet.empName = dr.GetString(2);
                            try
                            {
                                dataSet.gender = dr.GetString(3);
                            }
                            catch
                            {
                                dataSet.gender = "";
                            }
                            try
                            {
                                dataSet.phNo = dr.GetString(4);
                            }
                            catch
                            {
                                dataSet.phNo = "";
                            }
                            try
                            {
                                dataSet.addr = dr.GetString(5);
                            }
                            catch
                            {
                                dataSet.addr = "";
                            }
                            /*if (dataReader["phoneNo"] != DBNull.Value)
                                employee.PhoneNumber = dataReader.GetDecimal(4).ToString();*/
                            //display retrieved record
                            Console.WriteLine("{0},{1},{2},{3},{4},{5}", dataSet.empId, dataSet.empName, dataSet.compId, dataSet.gender, dataSet.phNo, dataSet.addr);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    dr.Close();
                    this.sqlConnection.Close();
                    //return dataSet;
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
