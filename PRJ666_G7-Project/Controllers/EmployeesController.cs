﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRJ666_G7_Project.Controllers
{
    public class EmployeesController : Controller
    {
        Manager m = new Manager();

        // GET: Employees
        public ActionResult Index()
        {
            return View(m.EmpGetAll());
        }
    }
}