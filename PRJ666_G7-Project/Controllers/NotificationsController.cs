using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRJ666_G7_Project.Models;

namespace PRJ666_G7_Project.Controllers
{
    public class NotificationsController : Controller
    {
        Manager m = new Manager();

        // GET: Notifications
        public ActionResult Index(string username)
        {
            return View(m.NotificationGetAllByEmp(username));
        }

        [Authorize(Roles = "Manager,Administrator,Super Admin")]
        // GET: Notifications/Create
        public ActionResult Create()
        {
            var form = new NotificationAddFormViewModel();

            return View(form);
        }

        [Authorize(Roles = "Manager,Administrator,Super Admin")]
        [HttpPost]
        // POST: Notifications/Create
        public ActionResult Create(NotificationAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            m.NotificationAdd(newItem);

            return RedirectToAction("Index", "Notifications");
        }
    }
}