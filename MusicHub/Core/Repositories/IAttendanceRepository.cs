using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using MusicHub.Core.Models;

namespace MusicHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        Attendance GetAttendance(string userId, int gigId);
        IEnumerable<Attendance> GetFutureAttendances(string userId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}