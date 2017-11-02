using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using FluentAssertions;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MusicHub.Core.Models;
using MusicHub.Persistence;
using MusicHub.Persistence.Repositories;

namespace GigHub.Tests.Persistence.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _gigsRepository;
        private AttendanceRepository _attendancesRepository;
        private Mock<DbSet<Gig>> _mockGigs;
        private Mock<DbSet<Attendance>> _mockAttendances;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            _mockAttendances = new Mock<DbSet<Attendance>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            mockContext.SetupGet(c => c.Attendances).Returns(_mockAttendances.Object);

            _gigsRepository = new GigRepository(mockContext.Object);
            _attendancesRepository = new AttendanceRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetArtistGigsWithGenre_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };
            _mockGigs.SetSource(new[] { gig });
            var gigs = _gigsRepository.GetArtistGigsWithGenre("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetArtistGigsWithGenre_GigIsCanceled_ShouldNotBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            gig.Cancel();

            _mockGigs.SetSource(new[] { gig });
            var gigs = _gigsRepository.GetArtistGigsWithGenre("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetArtistGigsWithGenre_GigIsForADifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });
            var gigs = _gigsRepository.GetArtistGigsWithGenre(gig.ArtistId + "-");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetArtistGigsWithGenre_GigIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSource(new[] { gig });
            var gigs = _gigsRepository.GetArtistGigsWithGenre(gig.ArtistId);

            gigs.Should().Contain(gig);
        }

        [TestMethod]
        public void GetGigsUserAttending_GigIsNotInThePast_ShouldBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), GenreId = 1, ArtistId = "2" };
            var attendance = new Attendance { AttendeeId = "1", Gig = gig };
            gig.Attendances.Add(attendance);

            _mockGigs.SetSource(new[] { gig });
            _mockAttendances.SetSource(new[] { attendance });
            var gigs = _gigsRepository.GetGigsUserAttending("1");

            gigs.Should().Contain(gig);
        }

        [TestMethod]
        public void GetGigsUserAttending_UserDoesNotHaveAnAttendance_ShouldNotBeReturned()
        {
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), GenreId = 1, ArtistId = "2" };
            var attendance = new Attendance { AttendeeId = "2", Gig = gig };
            gig.Attendances.Add(attendance);

            _mockGigs.SetSource(new[] { gig });
            _mockAttendances.SetSource(new[] { attendance });
            var gigs = _gigsRepository.GetGigsUserAttending("1");

            gigs.Should().BeEmpty();
        }

    }
}
