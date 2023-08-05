using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Social.Models;
using Social.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient(
  "JsonPlaceholderClient",
  client => {
    client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
  }
);

builder.Services.AddDbContext<SocialContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("SocialContext")));

var app = builder.Build();

using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    Post.Initialize(services);
    User.Initialize(services);
    Comment.Initialize(services);
    Album.Initialize(services);
    Photo.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Posts}/{action=Index}/{id?}");

app.Run();
