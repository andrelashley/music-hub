using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MusicHub.Core.ViewModels;
using MusicHub.Persistence;

namespace MusicHub.Controllers
{
    public class FollowingController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public FollowingController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Following()
        {
            var following = _unitOfWork.Relationships.GetArtistsFollowedByUser(User.Identity.GetUserId());

            var viewModel = new FollowingViewModel
            {
                Following = following,
                Heading = "Who I'm Following"
            };

            return View(viewModel);
        }
    }
}