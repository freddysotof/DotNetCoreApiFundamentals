using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecurityFundamentals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityFundamentals.Context
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var roleAdmin = new IdentityRole
            {
                Id = "b8ed172e-f5c8-489d-adc2-8986e6a04921",
                Name = "admin",
                NormalizedName = "admin"
            };

            builder.Entity<IdentityRole>().HasData(roleAdmin);


            base.OnModelCreating(builder);
        }
    }
}
