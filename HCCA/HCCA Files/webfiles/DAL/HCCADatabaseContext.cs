using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using HCCADBWebAppPrototype1.Models;

namespace HCCADBWebAppPrototype1.DAL
{
    public class HCCADatabaseContext : DbContext
    {
        public HCCADatabaseContext() : base("HCCADatbaseContext") { }

        public DbSet<CommitteeModel> Committees { get; set; }
        
        public DbSet<CommitteeModel_CommitteeAreaOfHealthModel> CommitteeModel_CommitteeAreaOfHealth { get; set; }

        public DbSet<CommitteeAreaOfHealthModel> CommitteeAreaOfHealth { get; set; }

        public DbSet<ConsumerRepModel> ConsumerReps { get; set; }

        public DbSet<ConsumerRepModel_ConsumerRepAreaOfInterestModel> ConsumerRepModel_ConsumerRepAreasOfInterestModel { get; set; }

        public DbSet<ConsumerRepAreaOfInterestModel> ConsumerRepAreasOfInterest { get; set; }

        public DbSet<ConsumerRepCommitteeHistoryModel> ConsumerRepCommitteeHistory { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
       
    }
}