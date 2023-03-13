using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PRJ666_G7_Project.Models
{
    public class NotificationBaseViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Issued: ")]
        public DateTime IssueDateTime { get; set; }

        public string Description { get; set; }
    }
}