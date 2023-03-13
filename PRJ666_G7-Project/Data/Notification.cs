using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRJ666_G7_Project.Data
{
    public class Notification
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime IssueDateTime { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Employee Employee { get; set; }
    }
}