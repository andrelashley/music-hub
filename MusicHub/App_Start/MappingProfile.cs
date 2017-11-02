using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MusicHub.Controllers.Api;
using MusicHub.Core.Dtos;
using MusicHub.Core.Models;

namespace MusicHub.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, UserDto>();
            Mapper.CreateMap<Gig, GigDto>();
            Mapper.CreateMap<Notification, NotificationDto>();
        }
    }
}