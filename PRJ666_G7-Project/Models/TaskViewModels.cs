using PRJ666_G7_Project.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRJ666_G7_Project.Models
{
    public class TaskBaseViewModel
    {

        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(10000)]
        public string Description { get; set; }

        public DateTime? Deadline { get; set; }

        public  Employee Employee { get; set; }

    }

    public class TaskWithDetailViewModel
    {

    }

    public class TaskIndexViewModel
    {
        public IEnumerable<Task>TaskList { get; set; }
        public IEnumerable<EmployeeBaseViewModel> EmployeeList { get; set; }
    }
    public class TaskAddFormViewModel
    {
        [Display(Name = "Task name")]
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Task Description")]
        [StringLength(10000)]
        public string Description { get; set; }

        public string EmployeeUserName { get; set; }



    }

    public class TaskAddViewModel
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(10000)]
        public string Description { get; set; }

        public string EmployeeUserName { get; set; }
        
        public DateTime Deadline { get; set; }
    }
}