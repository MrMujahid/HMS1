using Microsoft.EntityFrameworkCore;
using HMS.Models.EntityModels;

namespace HMS.Data
{
    public class Db : DbContext
    {

        public Db(DbContextOptions<Db> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //}

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

    }

    }
