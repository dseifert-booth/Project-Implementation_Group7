using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PRJ666_G7_Project.Models;

namespace PRJ666_G7_Project.Data
{
    public class Shift
    {

        public Shift()
        {
            Employees = new HashSet<Employee>();
            Tasks = new HashSet<Task>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ShiftStart { get; set; }

        [Required]
        public DateTime ShiftEnd { get; set; }

        public DateTime? ClockInTime { get; set; }

        public DateTime? ClockOutTime { get; set; }

        // Manager who assigned the shift
        [Required, StringLength(200)]
        public string Manager { get; set; }

        // Employees who is assigned the shift
        public ICollection<Employee> Employees { get; set; }

        public ICollection<Task> Tasks { get; set; }

    }
}