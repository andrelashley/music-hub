using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MusicHub.Core;
using MusicHub.Core.ViewModels;

namespace MusicHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string q = null)
        {
            var upcomingGigs = _unitOfWork.Gigs.GetAllUpcomingGigsWithArtistAndGenre();

            if (!string.IsNullOrWhiteSpace(q))
            {
                upcomingGigs =
                    upcomingGigs.Where(g =>
                    g.Artist.Name.Contains(q) ||
                    g.Genre.Name.Contains(q) ||
                    g.Venue.Contains(q));
            }

            string userId = User.Identity.GetUserId();
            var attendances = _unitOfWork.Attendances.GetFutureAttendances(userId)
                .ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = q,
                Attendances = attendances
            };
            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}