using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MusicHub.Core.Models;
using MusicHub.Core.Repositories;

namespace MusicHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Gig GetGigById(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetArtistGigsWithGenre(string userId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == userId &&
                    g.DateTime > DateTime.Now 
                    && !g.IsCanceled)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigForArtistWithAttendees(int gigId, string userId)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .Single(g => g.Id == gigId && g.ArtistId == userId);
        }

        public Gig GetGigWithArtist(int gigId)
        {
            return _context.Gigs
                .Where(g => g.Id == gigId)
                .Include(g => g.Artist)
                .SingleOrDefault();
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            return _context
                .Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Gig> GetAllUpcomingGigsWithArtistAndGenre()
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }
    }
}