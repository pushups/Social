using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Social.Models;

namespace Social.Controllers;

public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Details(int id)
    {
        var user = await Social.Models.User.GetUserAsync(id);
        var albums = await Album.GetAlbumsAsync(id);
        if (user is null || albums is null) {
            return NotFound();
        }
        foreach (var album in albums) {
            album.Photos = await Photo.GetPhotosAsync(album.Id);
        }
        var userViewModel = new UserViewModel { User = user, Albums = albums };
        return View(userViewModel);
    }

    public async Task<IActionResult> Posts(int id)
    {
        var posts = await Post.GetPostsByUserAsync(id);
        var user = await Social.Models.User.GetUserAsync(id);
        if (posts is null || user is null) {
            return NotFound();
        }

        var postViewModels = posts!.Select(p => new PostViewModel { Post = p, User = user }).Reverse();
        return View(postViewModels);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}