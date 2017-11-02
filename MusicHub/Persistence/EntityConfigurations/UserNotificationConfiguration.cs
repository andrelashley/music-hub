using System.Data.Entity.ModelConfiguration;
using MusicHub.Core.Models;

namespace MusicHub.Persistence.EntityConfigurations
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {
            HasKey(un => new {un.UserId, un.NotificationId});

            HasRequired(n => n.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);
        }
    }
}