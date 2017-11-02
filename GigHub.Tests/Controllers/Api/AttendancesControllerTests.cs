using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MusicHub.Controllers.Api;
using MusicHub.Core;
using MusicHub.Core.Dtos;
using MusicHub.Core.Models;
using MusicHub.Core.Repositories;

namespace GigHub.Tests.Controllers.Api
{
    [TestClass]
    public class AttendancesControllerTests
    {
        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Attendances).Returns(_mockRepository.Object);

            _controller = new AttendancesController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Attend_AttendanceAlreadyExists_ShouldReturnBadRequest()
        {
            var gig = new Gig { Id = 1 };
            var existingAttendance = new Attendance { AttendeeId = _userId, GigId = gig.Id };

            _mockRepository.Setup(r => r.GetAttendance(_userId, gig.Id)).Returns(existingAttendance);
            var result = _controller.Attend(new AttendanceDto { GigId = gig.Id });
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidRequest_ShouldReturnOk()
        {
            var result = _controller.Attend(new AttendanceDto { GigId = 1 });
            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void DeleteAttendance_AttendanceDoesNotExist_ShouldReturnNotFound()
        {
            var result = _controller.DeleteAttendance(2);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void DeleteAttendance_ValidRequest_ShouldReturnOk()
        {
            var attendance = new Attendance { GigId = 1, AttendeeId = _userId};
            _mockRepository.Setup(r => r.GetAttendance(_userId, 1)).Returns(attendance);
            var result = _controller.DeleteAttendance(1);
            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }

        [TestMethod]
        public void DeleteAttendance_ValidRequest_ShouldReturnTheIdOfDeletedAttendance()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(r => r.GetAttendance(_userId, 1)).Returns(attendance);
            var result = (OkNegotiatedContentResult<int>) _controller.DeleteAttendance(1);
            result.Content.Should().Be(1);
        }
    }
}
