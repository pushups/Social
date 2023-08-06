# Social

## Overview

This repo contains an example social media site written in C# using the ASP.NET Core MVC framework.
The data that are presented in this application come from the [JSON Placeholder](https://jsonplaceholder.typicode.com/) web service.
The starting page shows a single page of posts rendered to show the latest (highest post id) posts first.
From this page you can explore the data by clicking on the user name to see user details and photo albums, or you can see a post details view
by clicking on the post title text.

## Setup

1. Install [.NET 7](https://learn.microsoft.com/en-us/dotnet/core/install/)
1. Clone this repository by running the following command in your shell: ```$ git clone git@github.com:pushups/Social.git```.
1. In your shell change to the project directory: ```$ cd Social```
1. Build and run the project: ```$ dotnet run```
1. Your default web browser should open to a port on localhost, and the application should start.

## Potential Improvements

### Automated tests

As it stands, there are few enough pages that manually testing this application is not too time consuming.
However, if I were to continue working on it I would add both unit tests and integration tests.

### Refactor HttpClient code to reduce duplication

I put the methods to fetch the data as static methods on each class.
For example, the Post class has a static method *GetPostsAsync()*.
I chose to use static methods since it makes more sense to me that the Post model itself
would be able to query the posts, rather than an individual instance of a Post making that request.

In order to make the request we use an instance of *IHttpClientFactory* which is injected
during startup in *Program.cs*. Each of the models then repeats much of the same code to
make the request, deserialize the JSON response, and then return the correct type.

```c#
internal static async Task<IEnumerable<Post>?> GetPostsAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "posts");
        var client = ClientFactory.CreateClient("JsonPlaceholderClient");

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode) {
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var posts = await JsonSerializer.DeserializeAsync<IEnumerable<Post>>(responseStream);
            return posts;
        } else {
            return Array.Empty<Post>();
        }
    }
```

To remove this duplication I'd put this code into a single client implementation,
and use generics to correctly handle the types.

### Database caching layer

This application essentially proxies requests to the JSON Placeholder service.
In order to build some of the views we need to have data from multiple different API endpoints.
For example, in order to have the username attached to a post we need to query both the posts
and the users associated with the post. Once we make both requests we then essentially do an in-memory join to package up the data
before sending it to the views.

In a real-world scenario these multiple requests could cause challenges with scaling.
Since, the JSON Placeholder data are static we could add a caching layer like redis, memcached or even a SQL database.

Using a database would improve the performance of the posts index page since we could request only a single page of data
at a time, along with a small slice of user data by joining on the post's foreign key to the users table.
