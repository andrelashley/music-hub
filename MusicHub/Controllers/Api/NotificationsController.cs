using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using MusicHub.Core;
using MusicHub.Core.Dtos;
using MusicHub.Core.Models;
using MusicHub.Persistence;

namespace MusicHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var notifications = _unitOfWork.Notifications.GetUnreadNotificationsForUser(User.Identity.GetUserId());
            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userNotifications = _unitOfWork.UserNotifications.GetUnreadUserNotificationsForUser(User.Identity.GetUserId());

            foreach (var notification in userNotifications)
            {
               notification.MarkAsRead();
            }
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
