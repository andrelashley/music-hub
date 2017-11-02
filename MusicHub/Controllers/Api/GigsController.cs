using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MusicHub.Core;

namespace MusicHub.Controllers.Api
{
    [System.Web.Http.Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigForArtistWithAttendees(id, userId);

            if (gig == null || gig.IsCanceled)
                return NotFound();

            if (gig.ArtistId != userId)
                return Unauthorized();

            gig.Cancel();
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
