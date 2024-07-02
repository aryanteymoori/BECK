﻿using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Mapping
{
    public class AccountMapping:IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.FullName).HasMaxLength(128).IsRequired();
            builder.Property(x=>x.UserName).HasMaxLength(255).IsRequired();
            builder.Property(x=>x.Password).HasMaxLength(1000).IsRequired();
            builder.Property(x=>x.Mobile).HasMaxLength(12).IsRequired();

            builder.HasOne(x => x.Role).WithMany(x => x.Accounts)
                .HasForeignKey(x => x.RoleId);
        }
    }
}
