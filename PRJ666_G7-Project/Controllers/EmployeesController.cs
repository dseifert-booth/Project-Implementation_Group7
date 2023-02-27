using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRJ666_G7_Project.Models;

namespace PRJ666_G7_Project.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        Manager m = new Manager();

        // GET: Employees
        public ActionResult Index()
        {
            ViewBag.UserAuthLevel = m.EmpGetByUserName(m.User.Name).AuthLevel;
            return View(m.EmpGetAll());
        }

        // GET: Employees/username/Schedule
        [Route("Employees/{username}/Schedule")]
        public ActionResult Schedule(string userName)
        {
            ViewBag.UserAuthLevel = m.EmpGetByUserName(m.User.Name).AuthLevel;

            var schedule = m.GetEmployeeScheduleByUserName(userName);
            schedule.ShiftList = new SelectList(m.ShiftGetByEmployeeUserName(userName), "Id", "ShiftStart");

            return View(schedule);
        }

        // GET: Employees/{username}/{shiftId}
        [Route("Employees/{username}/{shiftId}")]
        public ActionResult _ScheduleEdit(int? shiftId)
        {

            if (shiftId == null)
            {
                return HttpNotFound();
            }
            else
            {
                var shift = m.ShiftGetByIdWithDetail((int)shiftId);
                var formObj = m.mapper.Map<ShiftWithDetailViewModel, EmployeeScheduleEditFormViewModel>(shift);

                formObj.UserName = User.Identity.Name;
                formObj.ShiftStart = shift.ShiftStart;
                formObj.ClockInTime = shift.ClockInTime;
                formObj.ClockOutTime = shift.ClockOutTime;

                formObj.TaskList = new MultiSelectList
                (items: shift.Tasks,
                dataValueField: "Id",
                dataTextField: "Name",
                selectedValues: shift.Tasks.Where(t => t.Complete).Select(t => t.Id)
                );

                // SelectedValues do not recieve anything, page does not show correct tasks as "Completed"
                return PartialView(formObj);
            }
        }

        // POST: Employees/{username}/?shiftId={shiftId}
        [Route("Employees/{username}/{shiftId}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _ScheduleEdit(string username, EmployeeScheduleEditViewModel schedule)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Schedule", new { username = schedule.UserName });
            }

            if (username != schedule.UserName)
            {
                return RedirectToAction("Schedule", new { username = schedule.UserName });
            }

            var editedItem = m.EmployeeScheduleEdit(schedule, username);

            return RedirectToAction("Schedule", new { username = editedItem.UserName });
        }
    }
}