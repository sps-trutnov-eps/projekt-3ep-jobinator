using Microsoft.EntityFrameworkCore;
using Jobinator.Data;
using Jobinator.Models;
using System;
using Azure.Identity;
using System.Diagnostics;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Ensure you add DbContext and use the connection string from appsettings.json
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("JobinatorDatabase")));

        builder.Services.AddControllersWithViews();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSession();

        var app = builder.Build();

        // Code to test the database in early stages of development
        //TestDB(app);
        // Configure the HTTP request pipeline, middleware, etc.
        app.UseRouting();
        app.MapDefaultControllerRoute();
        app.UseSession();
        app.Run();
    }
    private static void TestDB(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            // Add a new user
            var user = new User {Username = "username2", Name = "John", Surname = "Doe", PasswordHash = "passwordHash" };
            context.Users.Add(user);
            context.SaveChanges();

            // Add a new post
            var post = new Post { Type = "Blog", Title = "My First Post", Content = "Hello World!", UserId = user.Id };
            context.Posts.Add(post);
            context.SaveChanges();

            // Retrieve and display the post
            var RetrievedPost = context.Posts.Include(p => p.User).FirstOrDefault(p => p.Id == post.Id);
            Debug.WriteLine($"Post Title: {RetrievedPost.Title}, Author: {RetrievedPost.User.Name}");
            var UserWithPosts = context.Users
                               .Include(u => u.Posts)
                               .FirstOrDefault(u => u.Id == RetrievedPost.User.Id);
            //Print out list of all posts
            foreach (var UserPost in UserWithPosts.Posts)
            {
                Debug.WriteLine($"Post: {post.Title}");
            }

        }
    }
}
