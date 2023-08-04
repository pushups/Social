using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Social.Models;

namespace Social.Controllers;

public class PostsController : Controller
{
    private readonly ILogger<PostsController> _logger;

    public PostsController(ILogger<PostsController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {   
        var posts = await Post.GetPostsAsync();
        var userIds = posts!.Select(p => p.UserId).Distinct();
        var users = await Social.Models.User.GetUsersAsync();
        var usersById = users.ToDictionary(u => u.Id);
        var postViewModels = posts.Select(p => new PostViewModel { Post = p, User = usersById[p.UserId] });
        return View(postViewModels);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
