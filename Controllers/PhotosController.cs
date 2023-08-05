using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Social.Models;

namespace Social.Controllers;

public class PhotosController : Controller
{
    private readonly ILogger<PhotosController> _logger;

    public PhotosController(ILogger<PhotosController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Details(int id)
    {
        var photo = await Photo.GetPhotoAsync(id);
        return View(photo);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}