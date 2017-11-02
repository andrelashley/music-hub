using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.Core.Models;

namespace MusicHub.Core.Repositories
{
    public interface IUserNotificationsRepository
    {
        IEnumerable<UserNotification> GetUnreadUserNotificationsForUser(string userId);
    }
}