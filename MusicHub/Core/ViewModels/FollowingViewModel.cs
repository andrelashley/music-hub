using System.Collections.Generic;
using MusicHub.Core.Models;

namespace MusicHub.Core.ViewModels
{
    public class FollowingViewModel
    {
        public IEnumerable<ApplicationUser> Following { get; set; }
        public string Heading { get; set; }
    }
}