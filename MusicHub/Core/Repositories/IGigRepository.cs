using System.Collections.Generic;
using MusicHub.Core.Models;

namespace MusicHub.Core.Repositories
{
    public interface IGigRepository
    {
        Gig GetGigById(int gigId);
        IEnumerable<Gig> GetArtistGigsWithGenre(string userId);
        IEnumerable<Gig> GetAllUpcomingGigsWithArtistAndGenre();
        Gig GetGigForArtistWithAttendees(int gigId, string userId);
        Gig GetGigWithArtist(int gigId);
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId);
        void Add(Gig gig);
    }
}