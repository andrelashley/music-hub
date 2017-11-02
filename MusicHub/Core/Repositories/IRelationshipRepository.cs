using System.Collections.Generic;
using MusicHub.Core.Models;

namespace MusicHub.Core.Repositories
{
    public interface IRelationshipRepository
    {
        Relationship GetRelationship(string userId, string artistId);
        IEnumerable<ApplicationUser> GetArtistsFollowedByUser(string userId);
        void Add(Relationship relationship);
        void Remove(Relationship relationship);
    }
}