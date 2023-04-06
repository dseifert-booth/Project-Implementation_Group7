using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PRJ666_G7_Project.Data;
using PRJ666_G7_Project.Models;

namespace PRJ666_G7_Project.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Manager m = new Manager();
        // GET: Tasks
        [Authorize(Roles = "Manager,Administrator,Super Admin")]
        public ActionResult Index()
        {
            TaskIndexViewModel viewModel = new TaskIndexViewModel();
            viewModel.TaskList = db.Tasks.Include("Employee").ToList().OrderBy(x=> x.Deadline);

            List<EmployeeBaseViewModel> emplList = new List<EmployeeBaseViewModel>();
            emplList.Add(new EmployeeBaseViewModel() { FullName = "Not Selected", UserName="" });
            emplList.AddRange(m.EmpGetAll());
            viewModel.EmployeeList = emplList;

            return View(viewModel);
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( TaskAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.TaskAdd(newItem);

            if (addedItem == null)
            {
                return View(addedItem);
            }
            else
            {
                return RedirectToAction("Index");
            }

            //if (ModelState.IsValid)
            //{
             //db.Tasks.Add(task);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskEditViewModel updatedItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(updatedItem);
            }

            // Process the input
            if (updatedItem.CompleteChbx == "on")
            {
                updatedItem.Complete = true;
            }
            else
            {
                updatedItem.Complete = false;
            }

            var addedItem = m.TaskEdit(updatedItem);

            if (addedItem == null)
            {
                return View(updatedItem);
            }
            else
            {
                return RedirectToAction("Index");
            }
            //if (ModelState.IsValid)
            //{
            //    db.Entry(task).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
