using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Common.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Qtc.Dashboard.BusinessLayer
{
    public class SqlEntities : DbContext
    {
        public SqlEntities()
        {

        }
        public SqlEntities(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Errors> Errors { get; set; }
        //public virtual DbSet<OrganizationDocument> OrganizationDocument { get; set; }
        //public virtual DbSet<Component> Component { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = @"Data Source=Devsql01\sqldev;initial catalog=Efmh;integrated security=True;MultipleActiveResultSets=True;TrustServerCertificate=False;";

                optionsBuilder.UseSqlServer(connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
