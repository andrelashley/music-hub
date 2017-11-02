using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MusicHub.Core;
using MusicHub.Core.Models;
using MusicHub.Core.ViewModels;
using MusicHub.Persistence;

namespace MusicHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ViewResult Mine()
        {
            var gigs = _unitOfWork.Gigs.GetArtistGigsWithGenre(User.Identity.GetUserId());
            return View(gigs);
        }

        public ActionResult Details(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithArtist(id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != userId)
                return new HttpUnauthorizedResult();

            var viewModel = new GigDetailsViewModel
            {
                Artist = gig.Artist,
                CurrentUserIsAttending = _unitOfWork.Attendances.GetAttendance(userId, gig.Id) != null,
                Date = gig.DateTime.ToString("MMM dd"),
                Time = gig.DateTime.ToShortTimeString(),
                Venue = gig.Venue,
                ShowActions = User.Identity.IsAuthenticated,
                CurrentUserIsFollowingArtist = _unitOfWork.Relationships.GetRelationship(userId, gig.ArtistId) != null
            };

            return View(viewModel);
        }


        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel()
            {
                UpcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };

            return View("Gigs", viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel()
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading = "Add a Gig"
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var gig = _unitOfWork.Gigs.GetGigById(id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId == User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new GigFormViewModel()
            {
                Heading = "Edit a Gig",
                Id = gig.Id,
                Genres = _unitOfWork.Genres.GetGenres(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };

            return View("GigForm", viewModel);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { q = viewModel.SearchTerm });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _unitOfWork.Gigs.Add(gig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Update(viewModel);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }
    }
}