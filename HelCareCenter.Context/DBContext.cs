using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HelCareCenter.Models.Entity;
using Microsoft.Extensions.Configuration;

namespace HelCareCenter.Context
{
    public class DBContext : IdentityDbContext
    {
        public DBContext()
        {

        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {


        }
        

        #region Dbset
        public virtual DbSet<HelpCareCenter> HelpCareCenters { get; set; }
        public virtual DbSet<Clinic> Clinics { get; set; }

        public virtual DbSet<Doctor> Doctors { get; set; }


        public virtual DbSet<Appointment> Appointments { get; set; }





        #endregion



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // You don't actually ever need to call this
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // for chain with base for Idntity 
            base.OnModelCreating(modelBuilder);


          



          

        }
    }
}
