using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRJ666_G7_Project.Data
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(10000)]
        public string Description { get; set; }

        public bool Complete { get; set; }
    }
}