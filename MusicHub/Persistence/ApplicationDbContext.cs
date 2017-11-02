using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using MusicHub.Core.Models;
using MusicHub.Persistence.EntityConfigurations;

namespace MusicHub.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new AttendanceConfiguration());
            modelBuilder.Configurations.Add(new GenreConfiguration());
            modelBuilder.Configurations.Add(new GigConfiguration());
            modelBuilder.Configurations.Add(new NotificationConfiguration());
            modelBuilder.Configurations.Add(new RelationshipConfiguration());
            modelBuilder.Configurations.Add(new UserNotificationConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}