using System;
using Microsoft.EntityFrameworkCore;

namespace Stable.Repositories;

public class Storage : DbContext
{
    public Storage(DbContextOptions<Storage> options) : base(options){}

    public DbSet<Domain.Product> Products => Set<Domain.Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain.Product>(entity =>
        {
           entity.HasKey(property => property.Identity);
           entity.Property(property => property.Identity).UseIdentityByDefaultColumn();
        });
    }

}
