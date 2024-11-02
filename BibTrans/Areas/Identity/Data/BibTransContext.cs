using BibTrans.Areas.Identity.Data;
using BibTrans.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibTrans.Areas.Identity.Data;

public class BibTransContext : IdentityDbContext<BibTransUser>
{
    public BibTransContext(DbContextOptions<BibTransContext> options)
        : base(options)
    {
     }
   
    public DbSet<Books> Books { get; set; }
    public DbSet<Borrowing> Borrowings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<BibTransUser>
{
    public void Configure(EntityTypeBuilder<BibTransUser> builder)
    {
        builder.Property(x=>x.First_name).HasMaxLength(255);
        builder.Property(x => x.Last_name).HasMaxLength(255);

    }
}