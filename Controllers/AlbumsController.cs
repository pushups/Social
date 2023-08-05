using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Social.Models;

namespace Social.Controllers;

public class AlbumsController : Controller
{
    private readonly ILogger<AlbumsController> _logger;

    public AlbumsController(ILogger<AlbumsController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Details(int id)
    {
        var album = await Album.GetAlbumAsync(id);
        album!.Photos = await Photo.GetPhotosAsync(id);
        return View(album);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}