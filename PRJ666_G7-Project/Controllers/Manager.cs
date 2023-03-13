using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using AutoMapper;
using PRJ666_G7_Project.Data;
using PRJ666_G7_Project.Models;

// ************************************************************************************
// WEB524 Project Template V3 == 2227-70e7829b-d30d-42f3-9587-e1b2964b69d6
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace PRJ666_G7_Project.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
            // Define the mappings below, for example...
            // cfg.CreateMap<SourceType, DestinationType>();
            // cfg.CreateMap<Product, ProductBaseViewModel>();

            // Object mapper definitions

            cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();

                cfg.CreateMap<Employee, EmployeeBaseViewModel>();
                cfg.CreateMap<Employee, EmployeeScheduleViewModel>();

                cfg.CreateMap<Shift, ShiftBaseViewModel>();
                cfg.CreateMap<Shift, ShiftWithDetailViewModel>();
                cfg.CreateMap<ShiftAddViewModel, Shift>();
                cfg.CreateMap<ShiftBaseViewModel, ShiftEditFormViewModel>();
                cfg.CreateMap<ShiftWithDetailViewModel, EmployeeScheduleEditFormViewModel>();

                cfg.CreateMap<Task, TaskBaseViewModel>();
                cfg.CreateMap<Task, TaskWithDetailViewModel>();
                cfg.CreateMap<TaskAddViewModel, Task>();

                cfg.CreateMap<Notification, NotificationBaseViewModel>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }


        // Add your methods below and call them from controllers. Ensure that your methods accept
        // and deliver ONLY view model objects and collections. When working with collections, the
        // return type is almost always IEnumerable<T>.
        //
        // Remember to use the suggested naming convention, for example:
        // ProductGetAll(), ProductGetById(), ProductAdd(), ProductEdit(), and ProductDelete().

        #region Use Case Methods

        // Check to see if this can be removed (it should be able to)
        public IEnumerable<ApplicationUser> EmployeesGetAll() 
        {
            return ds.Users.Where(emp => emp.Claims.Any(c => c.ClaimType == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" && c.ClaimValue == "Employee"))
                           .OrderBy(emp => emp.FullName).AsEnumerable();
        }

        public IEnumerable<EmployeeBaseViewModel> EmpGetAll()
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeBaseViewModel>>(ds.Employees);
        }

        public EmployeeBaseViewModel EmpGetByUserName(string userName)
        {
            var obj = ds.Employees.SingleOrDefault(e => e.UserName == userName);

            return mapper.Map<Employee, EmployeeBaseViewModel>(obj);
        }

        public EmployeeScheduleViewModel GetEmployeeScheduleByUserName(string userName)
        {
            var schedule = mapper.Map<Employee, EmployeeScheduleViewModel>(ds.Employees.Where(e => e.UserName == userName).SingleOrDefault());

            return schedule;
        }

        public EmployeeScheduleViewModel EmployeeScheduleEdit(EmployeeScheduleEditViewModel schedule, string userName)
        {
            var obj = ds.Shifts.Include("Employees").Include("Tasks").SingleOrDefault(a => a.Id == schedule.ShiftId);

            bool found;
            foreach (var task in obj.Tasks)
            {
                found = false;
                if (schedule.TaskIds != null)
                {
                    foreach (var taskId in schedule.TaskIds) if (task.Id == taskId)
                        {
                            task.Complete = true;
                            found = true;
                        }
                }
                if (!found) task.Complete = false;
            }

            obj.ClockInTime = schedule.ClockInTime;
            obj.ClockOutTime = schedule.ClockOutTime;

            ds.SaveChanges();

            return GetEmployeeScheduleByUserName(userName);

        }

        public void ShiftClockInOut(ShiftWithDetailViewModel shift, bool inOut)
        {
            var obj = ds.Shifts.Include("Employees").Include("Tasks").SingleOrDefault(a => a.Id == shift.Id);
            
            if (inOut)
            {
                obj.ClockInTime = DateTime.Now;
            } else
            {
                obj.ClockOutTime = DateTime.Now;
            }

            ds.SaveChanges();
        }

        public void EmployeeAdd(Employee newUser)
        {
            ds.Employees.Add(newUser);
            ds.SaveChanges();
        }

        public IEnumerable<ShiftBaseViewModel> ShiftGetAll()
        {
            return mapper.Map<IEnumerable<Shift>, IEnumerable<ShiftBaseViewModel>>(ds.Shifts.Include("Employees").OrderBy(a => a.ShiftStart));
        }

        public ShiftWithDetailViewModel ShiftGetByIdWithDetail(int id)
        {
            var obj = ds.Shifts.Include("Employees").Include("Tasks").SingleOrDefault(a => a.Id == id);

            return mapper.Map<Shift, ShiftWithDetailViewModel>(obj);
        }

        public IEnumerable<ShiftWithDetailViewModel> ShiftGetByEmployeeUserName(string userName)
        {
            var shifts = ds.Shifts.Include("Employees").Include("Tasks").ToHashSet();

            var emp = ds.Employees.SingleOrDefault(a => a.UserName == userName);

            if (emp == null) return null;

            var obj = new HashSet<Shift>();

            foreach (var shift in shifts)
            {
                foreach (var employee in shift.Employees)
                {
                    if (employee.UserName == userName) obj.Add(shift);
                }
            }
            obj = obj.Distinct().ToHashSet();

            return mapper.Map< IEnumerable<Shift>, IEnumerable<ShiftWithDetailViewModel>>(obj);
        }

        public ShiftWithDetailViewModel ShiftAdd(ShiftAddViewModel newShift)
        {

            ICollection<Task> tasks = new HashSet<Task>();
            ICollection<Employee> employees = new HashSet<Employee>();

            foreach (var taskId in newShift.TaskIds)
            {
                var task = ds.Tasks.Find(taskId);
                tasks.Add(task);
            }

            foreach (var userName in newShift.EmployeeUserNames)
            {
                var emp = ds.Employees.Where(a => a.UserName == userName).SingleOrDefault();
                employees.Add(emp);

                var notif = new Notification();
                notif.IssueDateTime = DateTime.Now;
                notif.Description = "You've been assigned to a new shift from " + newShift.ShiftStart + " to " + newShift.ShiftEnd + ".";
                notif.Employee = emp;
                emp.Notifications.Add(notif);
                ds.Notifications.Add(notif);
            }

            if (tasks.Count == 0)
            {
                return null;
            }
            else
            {
                var addedItem = ds.Shifts.Add(mapper.Map<ShiftAddViewModel, Shift>(newShift));

                addedItem.Tasks = tasks;

                addedItem.Employees = employees;

                addedItem.Manager = HttpContext.Current.User.Identity.Name;

                ds.SaveChanges();

                return (addedItem == null) ? null : mapper.Map<Shift, ShiftWithDetailViewModel>(addedItem);
            }
        }


        // Come back to this, can be optimized
        public ShiftWithDetailViewModel ShiftEdit(ShiftEditViewModel shift)
        {
            var isSuperAdmin = HttpContext.Current.User.IsInRole("Super Admin");
            var isAdmin = HttpContext.Current.User.IsInRole("Administrator");
            var isManager = HttpContext.Current.User.IsInRole("Manager");
            var isEmployee = HttpContext.Current.User.IsInRole("Employee");

            var obj = ds.Shifts.Include("Employees").Include("Tasks").SingleOrDefault(a => a.Id == shift.Id);

            if (obj == null)
            {
                return null;
            }
            else
            {
                if (isSuperAdmin || isAdmin || isManager)
                {

                    ICollection<Task> tasks = new HashSet<Task>();

                    foreach (var taskId in shift.TaskIds)
                    {
                        var task = ds.Tasks.Find(taskId);
                        tasks.Add(task);
                    }

                    obj.Tasks = tasks;

                    if (shift.ShiftStart != null) obj.ShiftStart = (DateTime)shift.ShiftStart;
                    if (shift.ShiftEnd != null) obj.ShiftEnd = (DateTime)shift.ShiftEnd;
                } 
                else if (isEmployee)
                {
                    bool found;
                    foreach(var task in obj.Tasks)
                    {
                        found = false;
                        foreach (var taskId in shift.TaskIds) if (task.Id == taskId)
                            {
                                task.Complete = true;
                                found = true;
                            }
                        if (!found) task.Complete = false; 
                    }

                    obj.ClockInTime = shift.ClockInTime;
                    obj.ClockOutTime = shift.ClockOutTime;

                    
                } else
                {
                    return null;
                }

                ds.SaveChanges();

                return mapper.Map<Shift, ShiftWithDetailViewModel>(obj);
            }
        }

        public bool ShiftDelete(int id)
        {
            var itemToDelete = ds.Shifts.Find(id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                ds.Shifts.Remove(itemToDelete);
                ds.SaveChanges();

                return true;
            }
        }

        public IEnumerable<TaskBaseViewModel> TaskGetAll()
        {
            return mapper.Map<IEnumerable<Task>, IEnumerable<TaskBaseViewModel>>(ds.Tasks.OrderBy(a => a.Id));
        }

        public IEnumerable<NotificationBaseViewModel> NotificationGetAllByEmp(string username)
        {
            return mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationBaseViewModel>>(ds.Notifications.Where(a => a.Employee.UserName == username).OrderBy(a => a.Id));
        }

        #endregion

        // *** Add your methods above this line **


        #region Role Claims

        public List<String> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        #endregion

        #region Load Data Methods

        // Add some programmatically-generated objects to the data store
        // You can write one method or many methods but remember to
        // check for existing data first.  You will call this/these method(s)
        // from a controller action.

        public bool LoadData()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // *** Role claims ***
            if (ds.RoleClaims.Count() == 0)
            {
                // Add role claims here
                ds.RoleClaims.Add(new RoleClaim { Name = "Super Admin" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Administrator" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Manager" });
                ds.RoleClaims.Add(new RoleClaim { Name = "Employee" });

                ds.SaveChanges();
                done = true;
            }

            // *** Shifts ***

            if (ds.Shifts.Count() == 0)
            {
                // Add shifts here


                ds.SaveChanges();
                done = true;
            }

            // *** Tasks ***

            if (ds.Tasks.Count() == 0)
            {
                // Add shifts here
                ds.Tasks.Add(new Task 
                { 
                    Name = "test1", 
                    Description = "test description 1",
                    Complete = false
                });
                ds.Tasks.Add(new Task
                {
                    Name = "test2",
                    Description = "test description 2",
                    Complete = false
                });

                ds.SaveChanges();
                done = true;
            }

            // *** Employees ***
            if (ds.Employees.Count() == 0)
            {
                // Add role claims here
                ds.Employees.Add(new Employee 
                { 
                    UserName = "jdoe@example.com",
                    FullName = "John Doe"
                });
                ds.Employees.Add(new Employee 
                {
                    UserName = "superadmin@example.com",
                    FullName = "Super Admin"
                });
                ds.Employees.Add(new Employee 
                {
                    UserName = "jdoer@example.com",
                    FullName = "John Doer"
                });
                ds.Employees.Add(new Employee 
                {
                    UserName = "employee1@example.com",
                    FullName = "Employee One"
                });

                ds.SaveChanges();
                done = true;
            }

            // *** Genres ***
            /*if (ds.Genres.Count() == 0)
            {
                // Add genres here
                ds.Genres.Add(new Genre { Name = "Alternative" });
                ds.Genres.Add(new Genre { Name = "Classical" });
                ds.Genres.Add(new Genre { Name = "Country" });
                ds.Genres.Add(new Genre { Name = "Easy Listening" });
                ds.Genres.Add(new Genre { Name = "Hip-Hop/Rap" });
                ds.Genres.Add(new Genre { Name = "Jazz" });
                ds.Genres.Add(new Genre { Name = "Pop" });
                ds.Genres.Add(new Genre { Name = "R&B" });
                ds.Genres.Add(new Genre { Name = "Rock" });
                ds.Genres.Add(new Genre { Name = "Soundtrack" });

                ds.SaveChanges();
                done = true;
            }*/

            // *** Artists ***
            /*if (ds.Artists.Count() == 0)
            {
                // Add artists here

                ds.Artists.Add(new Artist
                {
                    Name = "The Beatles",
                    BirthOrStartDate = new DateTime(1962, 8, 15),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/9/9f/Beatles_ad_1965_just_the_beatles_crop.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Adele",
                    BirthName = "Adele Adkins",
                    BirthOrStartDate = new DateTime(1988, 5, 5),
                    Executive = user,
                    Genre = "Pop",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/Adele_2016.jpg/800px-Adele_2016.jpg"
                });

                ds.Artists.Add(new Artist
                {
                    Name = "Bryan Adams",
                    BirthOrStartDate = new DateTime(1959, 11, 5),
                    Executive = user,
                    Genre = "Rock",
                    UrlArtist = "https://upload.wikimedia.org/wikipedia/commons/7/7e/Bryan_Adams_Hamburg_MG_0631_flickr.jpg"
                });

                ds.SaveChanges();
                done = true;
            }*/

            // *** Albums ***
            /*if (ds.Albums.Count() == 0)
            {
                // Add albums here

                // For "Bryan Adams"
                var bryan = ds.Artists.SingleOrDefault(a => a.Name == "Bryan Adams");

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "Reckless",
                    ReleaseDate = new DateTime(1984, 11, 5),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/en/5/56/Bryan_Adams_-_Reckless.jpg"
                });

                ds.Albums.Add(new Album
                {
                    Artists = new List<Artist> { bryan },
                    Name = "So Far So Good",
                    ReleaseDate = new DateTime(1993, 11, 2),
                    Coordinator = user,
                    Genre = "Rock",
                    UrlAlbum = "https://upload.wikimedia.org/wikipedia/pt/a/ab/So_Far_so_Good_capa.jpg"
                });

                ds.SaveChanges();
                done = true;
            }*/

            // *** Tracks ***
            /*if (ds.Tracks.Count() == 0)
            {
                // Add tracks

                // For "Reckless"
                var reck = ds.Albums.SingleOrDefault(a => a.Name == "Reckless");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Run To You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Heaven",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Somebody",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Summer of '69",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { reck },
                    Name = "Kids Wanna Rock",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                // For "So Far So Good"
                var so = ds.Albums.SingleOrDefault(a => a.Name == "So Far So Good");

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Straight from the Heart",
                    Composers = "Bryan Adams, Eric Kagna",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "It's Only Love",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "This Time",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "(Everything I Do) I Do It for You",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.Tracks.Add(new Track
                {
                    Albums = new List<Album> { so },
                    Name = "Heat of the Night",
                    Composers = "Bryan Adams, Jim Vallance",
                    Clerk = user,
                    Genre = "Rock"
                });

                ds.SaveChanges();
                done = true;
            }*/

            return done;
        }

        public bool RemoveData()
        {
            try
            {
                /*foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();*/

                foreach (var e in ds.Shifts)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();

                /*foreach (var e in ds.Tasks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();*/

                /*foreach (var e in ds.Employees)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();*/

                /*foreach (var e in ds.Tracks)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();*/

                /*foreach (var e in ds.Albums)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();*/

                /*foreach (var e in ds.Artists)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();*/

                /*foreach (var e in ds.Genres)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }
                ds.SaveChanges();*/

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

    }

    #region RequestUser Class

    // This "RequestUser" class includes many convenient members that make it
    // easier work with the authenticated user and render user account info.
    // Study the properties and methods, and think about how you could use this class.

    // How to use...
    // In the Manager class, declare a new property named User:
    //    public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value:
    //    User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;

                // You can change the string value in your app to match your app domain logic
                //IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }
        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }
        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }
        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

    #endregion

}