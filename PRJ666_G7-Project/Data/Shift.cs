using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRJ666_G7_Project.Data
{
    public class Shift
    {
        [Key]
        public int Id { get; set; }

        public DateTime ShiftStart { get; set; }

        public DateTime ShiftEnd { get; set; }

        public DateTime ClockInTime { get; set; }

        public DateTime ClockOutTime { get; set; }

        // Username who is assigned the shift
        [Required, StringLength(200)]
        public string Employee { get; set; }

        // Username who assigned the shift
        [Required, StringLength(200)]
        public string Manager { get; set; }

        public ICollection<Task> Tasks { get; set; }

    }
}