using System;
using System.Collections.Generic;
using System.Linq;
using MusicHub.Core.Models;
using MusicHub.Core.Repositories;

namespace MusicHub.Persistence.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IApplicationDbContext _context;

        public AttendanceRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Attendance GetAttendance(string userId, int gigId)
        {
            return _context.Attendances.SingleOrDefault(a => a.AttendeeId == userId && a.Gig.Id == gigId);
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }

        public void Add(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        public void Remove(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }

    }
}