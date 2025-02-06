using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Web.Controllers
{
    [Route("MusicStore")]
    public class MusicStoreController : Controller
    {
        private readonly IArtistService _service;
        public MusicStoreController(IArtistService service)
        {
            _service = service;
        }
        public ActionResult HomePage()
        {
            return View();
        }

        [Route("artists")]
        public IActionResult Index()
        {
            var artists = _service.GetArtists();
            return View(artists);
        }

        [Route("albums")]
        public IActionResult Albums()
        {
            var albums = _service.GetAlbums();
            return View(albums);
        }


    }
}
