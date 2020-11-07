using EmployeePayroll;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace RestSharpTest
{
    /*public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public string salary { get; set; }
    }*/
    [TestClass]
    public class RestSharpTestCase
    {
        RestClient client;
        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient("http://localhost:3000");
        }
        private IRestResponse getEmployeeList()
        {
            RestRequest request = new RestRequest("/Employee", Method.GET);
            IRestResponse response = client.Execute(request);
            return response;
        }
        [TestMethod]
        public void OnCallingList_ReturnEmployeeList()
        {
            IRestResponse response = getEmployeeList();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(4, dataResponse.Count);

            foreach (Employee e in dataResponse)
            {
                Console.WriteLine("ID : " + e.empId + " Name : " + e.empName + " Address : " + e.addr);
            }
        }
    }
}