using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}