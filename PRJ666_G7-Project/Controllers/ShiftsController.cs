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
    public class ShiftsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Manager m = new Manager();

        // GET: Shifts
        public ActionResult Index()
        {
            return View(m.ShiftGetAll());
        }

        // GET: Shifts/Details/5
        public ActionResult Details(int? id)
        {
            var shift = m.ShiftGetByIdWithDetail(id.GetValueOrDefault());

            if (id == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(shift);
            }
        }

        [Authorize(Roles = "Manager,Administrator,Super Admin")]
        // GET: Shifts/Create
        public ActionResult Create()
        {
            var form = new ShiftAddFormViewModel();

            form.EmployeeList = new MultiSelectList
                (items: m.EmployeesGetAll(),
                dataValueField: "UserName",
                dataTextField: "FullName"
                );

            form.TaskList = new MultiSelectList
                (items: m.TaskGetAll(),
                dataValueField: "Id",
                dataTextField: "Name"
                );

            return View(form);
        }

        [Authorize(Roles = "Manager,Administrator,Super Admin")]
        // POST: Shifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ShiftAddViewModel newItem)
        {
            // Validate the input
            if (!ModelState.IsValid)
            {
                return View(newItem);
            }

            // Process the input
            var addedItem = m.ShiftAdd(newItem);

            if (addedItem == null)
            {
                return View(newItem);
            }
            else
            {
                return RedirectToAction("Details", "Shifts", new { id = addedItem.Id });
            }
        }

        [Authorize(Roles = "Employee,Manager,Administrator,Super Admin")]
        // GET: Shifts/Edit/5
        public ActionResult Edit(int? id)
        {
            var obj = m.ShiftGetByIdWithDetail(id.GetValueOrDefault());

            if (obj == null)
            {
                return HttpNotFound();
            }
            else
            {
                var formObj = m.mapper.Map<ShiftBaseViewModel, ShiftEditFormViewModel>(obj);

                formObj.UserAuthLevel = m.EmpGetByUserName(m.User.Name).AuthLevel;

                if (formObj.UserAuthLevel < 2)
                {
                    formObj.TaskList = new MultiSelectList
                    (items: obj.Tasks,
                    dataValueField: "Id",
                    dataTextField: "Name",
                    selectedValues: obj.Tasks.Where(t => t.Complete).Select(t => t.Id)
                    );

                    // SelectedValues do not recieve anything, page does not show correct tasks as "Completed"
                }
                else
                {
                    formObj.TaskList = new MultiSelectList
                    (items: m.TaskGetAll(),
                    dataValueField: "Id",
                    dataTextField: "Name",
                    selectedValues: obj.Tasks.Select(t => t.Id)
                    );
                }

                return View(formObj);
            }
        }

        [Authorize(Roles = "Employee,Manager,Administrator,Super Admin")]
        // POST: Shifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, ShiftEditViewModel shift)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id = shift.Id });
            }

            if (id.GetValueOrDefault() != shift.Id)
            {
                return RedirectToAction("Index");
            }

            var editedItem = m.ShiftEdit(shift);

            if (editedItem == null)
            {
                return RedirectToAction("Edit", new { id = shift.Id });
            }
            else
            {
                return RedirectToAction("Details", "Shifts", new { id = shift.Id });
            }
        }

        [Authorize(Roles = "Manager,Administrator,Super Admin")]
        // GET: Shifts/Delete/5
        public ActionResult Delete(int? id)
        {
            var itemToDelete = m.ShiftGetByIdWithDetail(id.GetValueOrDefault());

            if (itemToDelete == null)
            {
                return RedirectToAction("Index");
            }
            else return View(itemToDelete);
        }

        [Authorize(Roles = "Manager,Administrator,Super Admin")]
        // POST: Shifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            var result = m.ShiftDelete(id.GetValueOrDefault());

            return RedirectToAction("Index");
        }
/*
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }*/
    }
}
