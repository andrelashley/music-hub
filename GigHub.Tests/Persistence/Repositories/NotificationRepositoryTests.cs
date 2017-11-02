using System;
using System.Data.Entity;
using System.Linq;
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
    public class NotificationRepositoryTests
    {
        private NotificationRepository _repository;
        private Mock<DbSet<UserNotification>> _mockUserNotifications;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUserNotifications = new Mock<DbSet<UserNotification>>();

            var mockContext = new Mock<IApplicationDbContext>();

            mockContext.SetupGet(c => c.UserNotifications).Returns(_mockUserNotifications.Object);
            _repository = new NotificationRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUnreadNotificationsForUser_UserNotificationIsRead_ShouldNotBeReturned()
        {
            var gig = new Gig();
            var user = new ApplicationUser {Id = "1"};
            var userNotification = new UserNotification(user, Notification.GigCanceled(gig));
            userNotification.MarkAsRead();

            _mockUserNotifications.SetSource(new[] { userNotification });
            var notifications = _repository.GetUnreadNotificationsForUser(user.Id);

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUnreadNotificstionsForUser_UserNotificationIsForAnotherUser_ShouldNotBeReturned()
        {
            var gig = new Gig();
            var notification = Notification.GigCanceled(gig);
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockUserNotifications.SetSource(new[] { userNotification });
            var notifications = _repository.GetUnreadNotificationsForUser(user.Id + "-");

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUnreadNotificationsForUser_NewNotificiationForTheGivenUser_ShouldBeReturned()
        {
            var gig = new Gig();
            var notification = Notification.GigCanceled(gig);
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockUserNotifications.SetSource(new[] { userNotification });
            var notifications = _repository.GetUnreadNotificationsForUser(user.Id);

            notifications.Should().HaveCount(1);
            notifications.First().Should().Be(notification);
        }
    }
}

