using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace traker.Models
{
    public class job
    {
        public int Id { get; set; }
        public string employeeId { get; set; }
        public string employerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int TotalHoursWorked { get; set; }
        public int PricePerHour { get; set; }
        public List<screenShots> screenshots { get; set; }
    }

    public class screenShots
    {
        public int id { get; set; }
        public string image { get; set; }
    }


    public class jobDb : DbContext
    {
        public DbSet<job> job { get; set; }
        public DbSet<screenShots> screenshots { get; set; }
    }

}