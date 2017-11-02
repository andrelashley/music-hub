using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicHub.Core;
using MusicHub.Core.Models;
using MusicHub.Core.Repositories;
using MusicHub.Persistence.Repositories;

namespace MusicHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public IRelationshipRepository Relationships { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public IUserNotificationsRepository UserNotifications { get; private set; }
        public IGenreRepository Genres { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(context);
            Attendances = new AttendanceRepository(context);
            Relationships = new RelationshipRepository(context);
            Notifications = new NotificationRepository(context);
            UserNotifications = new UserNotificationsRepository(context);
            Genres = new GenreRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}