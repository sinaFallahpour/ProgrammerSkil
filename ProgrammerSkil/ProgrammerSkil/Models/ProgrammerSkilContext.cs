using ProgrammerSkil.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProgrammerSkil.Models
{
    public class ProgrammerSkilContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public ProgrammerSkilContext() : base("name=ProgrammerSkilContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //1 to 1  or 0  TBL_USer & TBL_UserProFile
            modelBuilder.Entity<TBL_UserProfile>()
                .HasKey(c => c.UserId)
                .HasRequired(c => c.User)
                .WithOptional(c => c.UserProfile)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<TBL_UserProfile>()
               .HasMany(c => c.SkillUserProfile)
               .WithOptional(c => c.UserProfile)
              .HasForeignKey(c => c.UserProfileId)
              .WillCascadeOnDelete(true);



            modelBuilder.Entity<TBL_User>()
               .HasMany(c => c.Tokens)
               .WithRequired(c => c.User)
              .HasForeignKey(c => c.UserId)
              .WillCascadeOnDelete(true);

            //1 to n    user samples
            //modelBuilder.Entity<TBL_Samples>()
            //   .HasOptional(c => c.User)
            //   .WithMany(c => c.Sampleses)
            //  .HasForeignKey(c => c.UserId);

            ////1 to n    user cpp[eration
            //modelBuilder.Entity<TBL_Cooperation>()
            //   .HasOptional(c => c.User)
            //   .WithMany(c => c.Cooperations)
            //  .HasForeignKey(c => c.UserId);

            ////1 to n user feedbacks
            //modelBuilder.Entity<TBL_FeedBacks>()
            // .HasOptional(c => c.User)
            // .WithMany(c => c.FeedBackses)
            //.HasForeignKey(c => c.UserId);



            //add default data for TBL_Setting
            //modelBuilder.Entity<TBL_Setting>()
            //    .(
            //        new Value { Id = 1, Name = "Value 101" },
            //        new Value { Id = 2, Name = "Value 102" },
            //        new Value { Id = 3, Name = "Value 103" }
            //    );
        }

        public DbSet<TBL_Samples> TBL_Samples { get; set; }
        public DbSet<TBL_User> TBL_User { get; set; }
        public DbSet<TBL_Token> TBL_Token { get; set; }
        public DbSet<TBL_UserProfile> TBL_UserProfile { get; set; }
        public DbSet<TBL_SkillUserProfile> TBL_SkillUserProfile { get; set; }
        public DbSet<TBL_Setting> TBL_Setting { get; set; }
        public DbSet<TBL_Images> TBL_Images { get; set; }
        public DbSet<TBL_FeedBacks> TBL_FeedBacks { get; set; }
        public DbSet<TBL_Cooperation> TBL_Cooperation { get; set; }
        public DbSet<TBL_Skill> TBL_Skill { get; set; }
        public DbSet<TBL_SocialMedia> TBL_SocialMedia { get; set; }
    }
}
