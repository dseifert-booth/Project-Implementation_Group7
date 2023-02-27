using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PRJ666_G7_Project.Data;

namespace PRJ666_G7_Project.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        { }

        // Add DbSet<TEntity> properties here
        public DbSet<RoleClaim> RoleClaims { get; set; }

        public DbSet<Shift> Shifts { get; set; }

        public DbSet<Data.Task> Tasks { get; set; }

        public DbSet<Employee> Employees { get; set; }

        /*public DbSet<Genre> Genres { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Track> Tracks { get; set; }*/


        // Turn OFF cascade delete, which is (unfortunately) the default setting
        // for Code First generated databases
        // For most apps, we do NOT want automatic cascade delete behaviour
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // First, call the base OnModelCreating method,
            // which uses the existing class definitions and conventions

            base.OnModelCreating(modelBuilder);

            // Then, turn off "cascade delete" for 
            // all default convention-based associations

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<PRJ666_G7_Project.Models.ShiftBaseViewModel> ShiftBaseViewModels { get; set; }

        //public System.Data.Entity.DbSet<PRJ666_G7_Project.Models.ApplicationUser> ApplicationUsers { get; set; }

        public System.Data.Entity.DbSet<PRJ666_G7_Project.Models.EmployeeBaseViewModel> EmployeeBaseViewModels { get; set; }

        public System.Data.Entity.DbSet<PRJ666_G7_Project.Models.EmployeeScheduleViewModel> EmployeeScheduleViewModels { get; set; }
    }
}