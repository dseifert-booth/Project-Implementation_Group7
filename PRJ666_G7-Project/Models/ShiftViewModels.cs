using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRJ666_G7_Project.Controllers;
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
        public IEnumerable<Employee> Employees { get; set; }
    }

    public class ShiftWithDetailViewModel : ShiftBaseViewModel
    {
        [Display(Name = "Employee clock-in time")]
        public DateTime? ClockInTime { get; set; }

        [Display(Name = "Employee clock-out time")]
        public DateTime? ClockOutTime { get; set; }

        [Display(Name = "Tasks to complete")]
        public IEnumerable<Task> Tasks { get; set; }
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

    public class ShiftEditFormViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Start time")]
        public DateTime? ShiftStart { get; set; }

        [Display(Name = "End time")]
        public DateTime? ShiftEnd { get; set; }

        [Display(Name = "Employee clock-in time")]
        public DateTime? ClockInTime { get; set; }

        [Display(Name = "Employee clock-out time")]
        public DateTime? ClockOutTime { get; set; }

        [Display(Name = "Tasks to complete")]
        public MultiSelectList TaskList { get; set; }

        public IEnumerable<string> TaskIds { get; set; }
    }

    public class ShiftEditViewModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime? ShiftStart { get; set; }

        public DateTime? ShiftEnd { get; set; }

        public DateTime? ClockInTime { get; set; }

        public DateTime? ClockOutTime { get; set; }

        public MultiSelectList TaskList { get; set; }

        public IEnumerable<int> TaskIds { get; set; }
    }
}