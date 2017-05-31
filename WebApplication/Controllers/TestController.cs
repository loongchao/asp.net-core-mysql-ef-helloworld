using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Entity;
using Repository;
using Services;

namespace WebApplication2.Controllers
{
    //public class Employee

    //{
    //    public string FirstName { get; set; }

    //    public string LastName { get; set; }

    //    public int Salary { get; set; }

    //}

    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public string GetString()
        //{
        //    return GetCustomer().ToString();
        //}

        //[NonAction]
        //public Customer GetCustomer()
        //{
        //    Customer c = new Customer();
        //    c.CustomerName = "Customer 1";
        //    c.Address = "Address1";
        //    return c;
        //}

        public ActionResult GetView()
        {
            // [loong] ÒÉÎÊµã
            //if (id == 1)
            //{
            //    return View("/Home/Index");
            //}

            //Employee emp = new Employee();
            //emp.FirstName = "Sukesh";
            //emp.LastName = "Marla";
            //emp.Salary = 20000;

            //ViewData["Employee"] = emp;

            //return View("MyView");

            Employee emp = new Employee();
            emp.FirstName = "Sukesh";
            emp.LastName = "Marla";
            emp.Salary = 20000;

            EmployeeViewModel vmEmp = new EmployeeViewModel();

            vmEmp.EmployeeName = emp.FirstName + " " + emp.LastName;
            vmEmp.Salary = emp.Salary.ToString("C");
            if (emp.Salary > 15000)
            {
                vmEmp.SalaryColor = "yellow";
            }
            else
            {
                vmEmp.SalaryColor = "green";
            }

            vmEmp.UserName = "Admin";

            return View("MyView", vmEmp);
        }

        public List<Employee> GetEmployees()
        {
            EmployeeService s = new EmployeeService();
            return s.Entities.ToList<Employee>();
        }
    }

    public class EmployeeViewModel
    {
        public string EmployeeName { get; set; }
        public string Salary { get; set; }
        public string SalaryColor { get; set; }
        public string UserName { get; set; }
    }
}