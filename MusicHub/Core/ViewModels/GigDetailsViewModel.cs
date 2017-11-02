using MusicHub.Core.Models;

namespace MusicHub.Core.ViewModels
{
    public class GigDetailsViewModel
    {
        public string Venue { get; set; }
        public bool CurrentUserIsAttending { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public bool ShowActions { get; set; }
        public ApplicationUser Artist { get; set; }
        public bool CurrentUserIsFollowingArtist { get; set; }
    }
}