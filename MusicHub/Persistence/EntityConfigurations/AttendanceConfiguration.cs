using System.Data.Entity.ModelConfiguration;
using MusicHub.Core.Models;

namespace MusicHub.Persistence.EntityConfigurations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            HasKey(a => new { a.GigId, a.AttendeeId });
        }
    }
}