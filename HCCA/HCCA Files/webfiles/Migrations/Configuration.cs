namespace HCCADBWebAppPrototype1.Migrations
{
    using HCCADBWebAppPrototype1.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HCCADBWebAppPrototype1.DAL.HCCADatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HCCADBWebAppPrototype1.DAL.HCCADatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var AreasOfInterest = new List<ConsumerRepAreaOfInterestModel>
            {
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Safety & Quality" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "HIP" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "EHealth" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Cancer" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Mental Health" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Alcohol & Drugs" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Rehabilitation" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Aged Care" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Community Care" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Critical Care" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Medicine" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Womens Health" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Maternity" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Surgery" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Medical Imaging" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Nutrition" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Nursing & Midwifery" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Tertiary Education & Research" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Policy" },
                new ConsumerRepAreaOfInterestModel { AreaOfInterestName = "Other" },
            };

            AreasOfInterest.ForEach(s => context.ConsumerRepAreasOfInterest.AddOrUpdate(p => p.AreaOfInterestName, s));
            context.SaveChanges();

            var AreasOfHealth = new List<CommitteeAreaOfHealthModel>
            {
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Canberra Hospital & Health Services" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Cancer Ambulatory & Health Support" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Chief Medical Admin" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Clinical Support Services" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Critical Care" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Director General" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "EHealth & Clinical Records" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Medicine" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "MH JH Alcohol & Drug Service" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Nursing & Midwifery" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Policy & Government Relations" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Quality & Safety Branch" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Rehabilitation Aged & Community Care" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Service & Capital Planning" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Womens Youth & Children" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "ACT Bodies" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Calvary Hospital" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Department Of Health" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Medicare Local" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "National Bodies" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Unversity Of Canberra" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "University Of Sydney" },
                new CommitteeAreaOfHealthModel { AreaOfHealthName = "Australian National University" },
            };

            AreasOfHealth.ForEach(s => context.CommitteeAreaOfHealth.AddOrUpdate(p => p.AreaOfHealthName, s));
            context.SaveChanges();
        }
    }
}
