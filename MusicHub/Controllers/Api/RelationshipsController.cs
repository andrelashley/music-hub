using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using MusicHub.Core;
using MusicHub.Core.Dtos;
using MusicHub.Core.Models;
using MusicHub.Persistence;

namespace MusicHub.Controllers.Api
{
    [Authorize]
    public class RelationshipsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public RelationshipsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(RelationshipDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_unitOfWork.Relationships.GetRelationship(userId, dto.ArtistId) != null)
                return BadRequest();

            var relationship = new Relationship
            {
                FollowerId = userId,
                FolloweeId = dto.ArtistId
            };

            _unitOfWork.Relationships.Add(relationship);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Unfollow(RemoveRelationshipDto dto)
        {
            var userId = User.Identity.GetUserId();
            var relationship = _unitOfWork.Relationships.GetRelationship(userId, dto.ArtistId);

            if (relationship == null)
                return NotFound();

            _unitOfWork.Relationships.Remove(relationship);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}