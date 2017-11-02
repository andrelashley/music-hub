using System.Collections.Generic;
using System.Linq;
using MusicHub.Core.Models;
using MusicHub.Core.Repositories;

namespace MusicHub.Persistence.Repositories
{
    public class RelationshipRepository : IRelationshipRepository
    {
        private readonly ApplicationDbContext _context;

        public RelationshipRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Relationship GetRelationship(string userId, string artistId)
        {
            return _context.Relationships.SingleOrDefault(r => r.FollowerId == userId && r.FolloweeId == artistId);
        }

        public IEnumerable<ApplicationUser> GetArtistsFollowedByUser(string userId)
        {
            return _context.Relationships
                .Where(r => r.FollowerId == userId)
                .Select(a => a.Followee)
                .ToList();
        }

        public void Add(Relationship relationship)
        {
            _context.Relationships.Add(relationship);
        }

        public void Remove(Relationship relationship)
        {
            _context.Relationships.Remove(relationship);
        }
    }
}