using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRJ666_G7_Project.Data
{
    public class Employee
    {

        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public int AuthLevel { get; set; }

        public ICollection<Shift> Shifts { get; set; }

    }
}