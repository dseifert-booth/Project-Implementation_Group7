using System.ComponentModel.DataAnnotations;

namespace PRJ666_G7_Project.Data
{
    public class RoleClaim
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}