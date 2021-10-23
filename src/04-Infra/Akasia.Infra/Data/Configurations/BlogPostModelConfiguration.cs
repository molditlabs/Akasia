using Akasia.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Akasia.Infra.Data.Configurations
{
    public class BlogPostModelConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.ToTable("BlogPost");

            builder.HasKey(o => o.Id);

            builder.Property(t => t.Title).HasMaxLength(1000);
            builder.Property(t => t.Content).HasMaxLength(1000000);
            builder.Property(t => t.PostDate).HasColumnType("datetime2");
            builder.Property(t => t.Status).HasColumnType("int");


            builder.Property(t => t.IsDeleted).HasDefaultValue(false);

            builder.Property(t => t.CreatedBy).HasMaxLength(450);
            builder.Property(t => t.CreatedDate).HasColumnType("datetime2");
            builder.Property(t => t.ModifiedBy).HasMaxLength(450);
            builder.Property(t => t.ModifiedDate).HasColumnType("datetime2");
        }
    }
}
