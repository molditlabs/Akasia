using Akasia.Domain.Entity;
using Akasia.Infra.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akasia.Infra.Data.Contexts
{
    public class AkasiaDbContext : DbContext
    {
        public AkasiaDbContext(DbContextOptions<AkasiaDbContext> options)
         : base(options)
        {

        }
        public DbSet<BlogPost> Post { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new BlogPostModelConfiguration().Configure(modelBuilder.Entity<BlogPost>());
        }
    
    }
}
