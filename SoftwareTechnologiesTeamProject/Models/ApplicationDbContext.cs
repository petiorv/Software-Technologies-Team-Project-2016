﻿namespace SoftwareTechnologiesTeamProject.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<SoftwareTechnologiesTeamProject.Models.Post> Posts { get; set; }
    }
}