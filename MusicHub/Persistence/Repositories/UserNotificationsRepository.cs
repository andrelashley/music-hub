using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MusicHub.Core.Models;
using MusicHub.Core.Repositories;

namespace MusicHub.Persistence.Repositories
{
    public class UserNotificationsRepository : IUserNotificationsRepository
    {
        private readonly IApplicationDbContext _context;

        public UserNotificationsRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserNotification> GetUnreadUserNotificationsForUser(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();
        }

    }
}