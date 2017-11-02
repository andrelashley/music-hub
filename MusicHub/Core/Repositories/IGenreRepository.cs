using System.Collections.Generic;
using MusicHub.Core.Models;

namespace MusicHub.Core.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}