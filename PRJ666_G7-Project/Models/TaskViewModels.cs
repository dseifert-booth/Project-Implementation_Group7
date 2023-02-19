using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

    }

    public class TaskWithDetailViewModel
    {

    }

    public class TaskAddFormViewModel
    {
        [Display(Name = "Task name")]
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Task Description")]
        [StringLength(10000)]
        public string Description { get; set; }

    }

    public class TaskAddViewModel
    {
        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(10000)]
        public string Description { get; set; }
    }
}