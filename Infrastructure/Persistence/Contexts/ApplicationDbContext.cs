using Application.Common.Interfaces;
using Application.Interfaces.Services.CurrentUserService;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;


namespace Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        public ApplicationDbContext(
            DbContextOptions options,
           // IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService) : base(options)
        {
            this._currentUserService = currentUserService;
        }

        public DbSet<UploadFile> UploadFile { get; set; }
        public DbSet<UploadFileMorph> UploadFileMorph { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<CustomerMessage> CustomerMessages { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<ExtracurricularActivities> ExtracurricularActivities { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Academics> Academics { get; set; }





        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.Now;
                        entry.Entity.UpdatedBy = _currentUserService.UserId;
                        break;
                        //case EntityState.Deleted:
                        //    entry.Reload();
                        //    break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedDate = DateTime.Now;
                        entry.Entity.UpdatedBy = _currentUserService.UserId;
                        break;
                }
            }
            return base.SaveChanges();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<Student>()
              .HasOne(s => s.Academics)
              .WithOne(a => a.Student)
              .HasForeignKey<Academics>(a => a.StudentId);

            // Optional: Configuring cascade delete
            builder.Entity<Academics>()
                .HasOne(a => a.Student)
                .WithOne(s => s.Academics)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(builder);

            builder.Entity<Student>()
        .HasMany(s => s.Certifications)
        .WithOne(c => c.Student)
        .HasForeignKey(c => c.StudentId) // This defines the foreign key
        .OnDelete(DeleteBehavior.Cascade); // Optional cascade delete



        }

    }

}
