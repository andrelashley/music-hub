using System.Data.Entity.ModelConfiguration;
using MusicHub.Core.Models;

namespace MusicHub.Persistence.EntityConfigurations
{
    public class NotificationConfiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration()
        {
            HasRequired(n => n.Gig);
        }
    }
}