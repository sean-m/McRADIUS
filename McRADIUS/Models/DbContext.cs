using System;
using System.Collections.Generic;
using System.Text;
using McRADIUS.Models;
using Microsoft.EntityFrameworkCore;

namespace McRADIUS.Models
{
    public class OTPDbContext : DbContext
    {
        public OTPDbContext (DbContextOptions<OTPDbContext> options) : base(options) { }

        public virtual DbSet<OTPRequest> OTPRequests { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {

        }
    }
}
