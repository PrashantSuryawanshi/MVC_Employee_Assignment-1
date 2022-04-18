using Microsoft.AspNetCore.Mvc;
using MVC_Demo2.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace MVC_Demo2.Controllers
{
    public class EmployeesController : Controller
    {
        EmployeesDAL employeesDAL = new EmployeesDAL();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            ViewBag.EmployeeList = employeesDAL.GetAllEmployees();
            return View();
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormCollection form)
        {
            Employee emp = new Employee();
            emp.Name = form["name"];
            emp.Salary = Convert.ToDecimal(form["salary"]);
            int res = employeesDAL.Save(emp);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employees = employeesDAL.GetEmployeeById(id);
            ViewBag.Name = employees.Name;
            ViewBag.Salary = employees.Salary;
            ViewBag.Id = employees.Id;
            return View();
        }

        [HttpPost]
        public IActionResult Edit(IFormCollection form)
        {
            Employee employees = new Employee();
            employees.Name = form["name"];
            employees.Salary = Convert.ToDecimal(form["salary"]);
            employees.Id = Convert.ToInt32(form["id"]);
            int res = employeesDAL.Update(employees);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Employee employees = employeesDAL.GetEmployeeById(id);
            ViewBag.Name = employees.Name;
            ViewBag.salary = employees.Salary;
            ViewBag.Id = employees.Id;
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            int res = employeesDAL.Delete(id);
            if (res == 1)
                return RedirectToAction("List");

            return View();
        }
    }
}