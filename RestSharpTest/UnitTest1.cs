using EmployeePayroll;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        /// <summary>
        /// Gets the employee list.
        /// </summary>
        /// <returns></returns>
        private IRestResponse getEmployeeList()
        {
            RestRequest request = new RestRequest("/Employee", Method.GET);
            IRestResponse response = client.Execute(request);
            return response;
        }

        /// <summary>
        /// Return employee list count.
        /// </summary>
        [TestMethod]
        public void OnCallingList_CheckCount()
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

        /// <summary>
        /// Adds Employee
        /// </summary>
        [TestMethod]
        public void GivenEmployee_OnPost_ReturnAddedEmployee()
        {
            RestRequest request = new RestRequest("/Employee", Method.POST);
            JObject jObject = new JObject();
            jObject.Add("empName", "Samay");
            jObject.Add("phNo", "68687980");
            jObject.Add("addr", "Hyderabad");
            jObject.Add("gender", "M");

            request.AddParameter("application/json", jObject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Samay", dataResponse.empName);
            Assert.AreEqual("Hyderabad", dataResponse.addr);
            Assert.AreEqual("68687980", dataResponse.phNo);
            Assert.AreEqual("M", dataResponse.gender);

            System.Console.WriteLine(response.Content);

        }

        /// <summary>
        /// Adds Multiple Employees
        /// </summary>
        [TestMethod]
        public void AddMultipleEmployee()
        {
            List<Employee> employeeList = new List<Employee>();
            employeeList.Add(new Employee { empName="A",phNo="459389", addr="Mumbai",gender="M" });
            employeeList.Add(new Employee { empName = "B", phNo = "989459389", addr = "Pune", gender = "F" });
            foreach (Employee e in employeeList)
            {
                RestRequest request = new RestRequest("/Employee", Method.POST);
                JObject jObject = new JObject();
                jObject.Add("empName", e.empName);
                jObject.Add("phNo", e.phNo);
                jObject.Add("addr", e.addr);
                jObject.Add("gender", e.gender);

                request.AddParameter("application/json", jObject, ParameterType.RequestBody);
                IRestResponse response1 = client.Execute(request);
                Assert.AreEqual(response1.StatusCode, HttpStatusCode.Created);
            }

            IRestResponse response = getEmployeeList();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(8, dataResponse.Count);

            foreach (Employee e in dataResponse)
            {
                System.Console.WriteLine("ID : " + e.empId + " Name : " + e.empName + " Address : " + e.addr);
            }
        }

        /// <summary>
        /// Updates Employee
        /// </summary>
        [TestMethod]
        public void UpdateEmployee()
        {
            RestRequest request = new RestRequest("/Employee/4", Method.PUT);
            JObject jObject = new JObject();
            jObject.Add("name", "Shreya");
            jObject.Add("addr", "Bhilai");

            request.AddParameter("application/json", jObject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Shreya", dataResponse.empName);
            Assert.AreEqual("Bhilai", dataResponse.addr);
            System.Console.WriteLine(response.Content);
        }

        /// <summary>
        /// Deletes Employee
        /// </summary>
        [TestMethod]
        public void DeleteEmployee()
        {
            RestRequest request = new RestRequest("/Employee/3010", Method.DELETE);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            System.Console.WriteLine(response.Content);
        }
    }
}