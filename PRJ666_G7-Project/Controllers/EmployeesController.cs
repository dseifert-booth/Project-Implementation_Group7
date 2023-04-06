using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRJ666_G7_Project.Data;
using PRJ666_G7_Project.Models;

namespace PRJ666_G7_Project.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
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

            var sunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Sunday);

            var schedule = m.GetEmployeeScheduleByUserNameWithShift(userName);
            schedule.ShiftList = new SelectList(m.ShiftGetByEmployeeUserName(userName), "Id", "ShiftStart");

            schedule.ShiftsWeekly = new List<EmployeeShiftsWeekly>();
            for (int i = 0; i < 7; i++)
            {
                EmployeeShiftsWeekly sd = new EmployeeShiftsWeekly();
                var day = sunday.AddDays(i);
                sd.ShiftsDate = day;
                List<Shift> shiftsDaily = new List<Shift>();
                foreach (var shift in schedule.Shifts)
                {
                    if (shift.ShiftStart.Date == day.Date)
                    {
                        shiftsDaily.Add(shift);
                    }
                }
                sd.ShiftsDaily = shiftsDaily;
                schedule.ShiftsWeekly.Add(sd);
            }

            return View(schedule);
        }

        [Route("Employees/{username}/tasklist")]
        public ActionResult _TaskListEdit(string userName)
        {
            TaskIndexEditFormViewModel viewModel = new TaskIndexEditFormViewModel();
            viewModel.TaskList = db.Tasks.Include("Employee").Where(x => x.Employee.UserName == userName).ToList().OrderBy(x => x.Deadline);

            return PartialView(viewModel);
        }

        // POST: Employees/{username}/tasklist
        [Route("Employees/{username}/tasklist")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _TaskListEdit(string username, TaskIndexEditViewModel tasks)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Schedule", new { username = username });
            }

            IEnumerable<Task> taskList = tasks.TaskList;


            foreach(var task in taskList)
            {
                foreach(var taskId in tasks.TaskIds)
                {
                    task.Complete = false;
                    if (task.Id == taskId)
                    {
                        task.Complete = true;
                    }
                }
            }

            var editedItem = m.EmployeeTasksEdit(taskList, username);

            return RedirectToAction("Schedule", new { username = editedItem.UserName });
        }
    }
}