using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using MusicHub.Core.Models;

namespace MusicHub.Persistence.EntityConfigurations
{
    public class RelationshipConfiguration : EntityTypeConfiguration<Relationship>
    {
        public RelationshipConfiguration()
        {
            HasKey(r => new { r.FollowerId, r.FolloweeId });
        }
    }
}