using System.Collections.Generic;
using MusicHub.Core.Models;

namespace MusicHub.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetUnreadNotificationsForUser(string userId);
    }
}