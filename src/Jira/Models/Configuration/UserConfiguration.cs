using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jira.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jira.Models.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .HasMaxLength(50)
                .IsRequired(true);

            builder.Property(x => x.FullName)
                .HasMaxLength(100)
                .IsRequired(true);

            builder.Property(x => x.Password)
                .HasMaxLength(50);

            builder.Property(x => x.Salt)
                .HasMaxLength(50);

            builder.Property(x => x.OTP)
                .HasMaxLength(10);

            // builder.HasOne(x=>x.Project)
            //     .WithMany(x => x.User)
            //     .HasForeignKey(x => x.ProjectId)
            //     .IsRequired(false);

        }
    }
}