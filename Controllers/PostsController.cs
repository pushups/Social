﻿using System.Diagnostics;
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

    public async Task<IActionResult> Index(int page)
    {   
        int pageSize = 5;
        var posts = await Post.GetPostsAsync();

        if (posts is null) {
            return NotFound();
        }

        var userIds = posts.Select(p => p.UserId).Distinct();
        var users = await Social.Models.User.GetUsersAsync(userIds);

        if (users is null) {
            return NotFound();
        }

        var usersById = users.ToDictionary(u => u.Id);
        var postViewModels = posts.Select(p => new PostViewModel { Post = p, User = usersById[p.UserId] }).Reverse();

        var postCount = postViewModels.Count();
        postViewModels = postViewModels.Skip(page * pageSize).Take(pageSize);
        @ViewBag.Page = page;
        @ViewBag.PostCount = postCount;
        @ViewBag.HasNextPage = (page + 1) * pageSize < postCount;
        @ViewBag.HasPreviousPage = page > 0;
        @ViewBag.PageSize = pageSize;
        return View(postViewModels);
    }

    public async Task<IActionResult> Details(int id)
    {
        var post = await Post.GetPostAsync(id);
        if (post is null) {
            return NotFound();
        }

        var user = await Social.Models.User.GetUserAsync(post.UserId);

        if (user is null) {
            return NotFound();
        }

        var comments = await Comment.GetCommentsAsync(id);

        if (comments is null) {
            return NotFound();
        }
        comments = comments.Reverse();
        var postViewModel = new PostViewModel { Post = post, User = user, Comments = comments };
        return View(postViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
