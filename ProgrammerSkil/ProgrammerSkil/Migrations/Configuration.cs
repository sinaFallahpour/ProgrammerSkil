namespace ProgrammerSkil.Migrations
{
    using ProgrammerSkil.Models;
    using System;
    using System.Collections;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Linq;
    using ProgrammerSkil.Models.Utilities;
    using ProgrammerSkil.Models.Enums;
    

    internal sealed class Configuration : DbMigrationsConfiguration<ProgrammerSkilContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ProgrammerSkilContext context)
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


            //context.TBL_Setting.AddOrUpdate(
            //     new TBL_Setting
            //     {
            //         Id = Static.SettingId,
            //         Phone = "",
            //         AboutUs = "",
            //     }
            //    );

            context.TBL_Setting.AddOrUpdate(
                new TBL_Setting
                {
                    Id = Static.SettingId,
                    Phone = "",
                    AboutUs = string.Empty,
                }
               ) ;
            //context.SaveChanges();
        }
    }
}
