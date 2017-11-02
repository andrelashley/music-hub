using MusicHub.Core.Repositories;

namespace MusicHub.Core
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IAttendanceRepository Attendances { get; }
        IRelationshipRepository Relationships { get; }
        IUserNotificationsRepository UserNotifications { get; }
        INotificationRepository Notifications { get; }
        IGenreRepository Genres { get; }
        void Complete();
    }
}