using Domain.Domain_Models.MusicStoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IArtistService
    {
        List<Artist> GetArtists();
        List<Album> GetAlbums();
    }
}
