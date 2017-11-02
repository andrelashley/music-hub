using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MusicHub.Core.Models;
using MusicHub.Core.Repositories;

namespace MusicHub.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IApplicationDbContext _context;

        public NotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetUnreadNotificationsForUser(string userId)
        {
            var rvl = _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            return rvl;
        }
    }
}