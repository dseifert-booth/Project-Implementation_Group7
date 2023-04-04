using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRJ666_G7_Project.Data;

namespace PRJ666_G7_Project.Models
{
    public class EmployeeBaseViewModel
    {

        [Key]
        public int Id { get; set; }

        [Display(Name = "Email")]
        public string UserName { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }

        public int AuthLevel { get; set; }

    }

    public class EmployeeScheduleViewModel : EmployeeBaseViewModel
    {
        [Display(Name = "Select a shift:")]
        public SelectList ShiftList { get; set; }

        public IEnumerable<Shift> Shifts { get; set; }

        //public List<List<Shift>> ShiftsWeekly { get; set; }
        public List<EmployeeShiftsWeekly> ShiftsWeekly { get; set; }

    }

    public class EmployeeShiftsWeekly
    {
        [Key]
        public int Id { get; set; }
        public DateTime ShiftsDate { get; set; }
        public List<Shift> ShiftsDaily { get; set; }
    }

    public class EmployeeScheduleEditFormViewModel
    {
        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public int ShiftId { get; set; }

        public DateTime ShiftStart { get; set; }

        [Display(Name = "Clock-in time")]
        public DateTime? ClockInTime { get; set; }

        [Display(Name = "Clock-out time")]
        public DateTime? ClockOutTime { get; set; }

        public MultiSelectList TaskList { get; set; }

        public IEnumerable<int> TaskIds { get; set; }
    }

    public class EmployeeScheduleEditViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Email")]
        public string UserName { get; set; }

        public int ShiftId { get; set; }

        [Display(Name = "Employee clock-in time")]
        public DateTime? ClockInTime { get; set; }

        [Display(Name = "Employee clock-out time")]
        public DateTime? ClockOutTime { get; set; }

        public MultiSelectList TaskList { get; set; }

        public IEnumerable<int> TaskIds { get; set; }
    }
}