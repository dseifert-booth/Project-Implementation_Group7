using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRJ666_G7_Project.Data;

namespace PRJ666_G7_Project.Models
{
    public class ShiftBaseViewModel
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Start time")]
        public DateTime ShiftStart { get; set; }

        [Display(Name = "End time")]
        public DateTime ShiftEnd { get; set; }

        [Display(Name = "Assigned employees")]
        public IEnumerable<ApplicationUser> Employees { get; set; }
    }

    public class ShiftWithDetailViewModel : ShiftBaseViewModel
    {
        [Display(Name = "Tasks to complete")]
        public IEnumerable<Task> Tasks { get; set; }

        [Display(Name = "Number of tasks")]
        public int NumTasks { get; set; }
    }

    public class ShiftAddFormViewModel
    {
        [Display(Name = "Assign Start time")]
        public DateTime ShiftStart { get; set; }

        [Display(Name = "Assign End time")]
        public DateTime ShiftEnd { get; set; }

        [Display(Name = "Select employees")]
        public MultiSelectList EmployeeList { get; set; }

        public IEnumerable<string> EmployeeUserNames { get; set; }

        [Display(Name = "Select tasks")]
        public MultiSelectList TaskList { get; set; }

        public IEnumerable<string> TaskIds { get; set; }
    }

    public class ShiftAddViewModel
    {
        public DateTime ShiftStart { get; set; }

        public DateTime ShiftEnd { get; set; }

        public MultiSelectList EmployeeList { get; set; }

        public IEnumerable<string> EmployeeUserNames { get; set; }

        public MultiSelectList TaskList { get; set; }

        public IEnumerable<int> TaskIds { get; set; }
    }
}