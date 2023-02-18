using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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

        [Display(Name = "Assigned employee")]
        [StringLength(200)]
        public string Employee { get; set; }

    }
}