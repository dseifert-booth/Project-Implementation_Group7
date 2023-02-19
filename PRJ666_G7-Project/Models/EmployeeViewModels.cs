using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRJ666_G7_Project.Models
{
    public class EmployeeBaseViewModel
    {

        [Key]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public ICollection<string> RoleClaims { get; set; }

    }
}