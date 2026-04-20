using Microsoft.EntityFrameworkCore;
using Jobinator.Data;
using Jobinator.Models;
using Jobinator.Helpers;
using System.Diagnostics;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Přidání služeb do DI kontejneru
        // Konfigurace DbContextu pro SQL Server s připojením definovaným v appsettings.json
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("JobinatorDatabase")));

        // Přidání podpory pro MVC (Controller s View)
        builder.Services.AddControllersWithViews();
        
        // Služba pro přístup k aktuálnímu HttpContextu (nutné pro IAuthHelper)
        builder.Services.AddHttpContextAccessor();
        
        // Přidání podpory pro Session (používá se pro přihlášení uživatele)
        builder.Services.AddSession();
        
        // Registrace naší vlastní služby pro autentizaci
        builder.Services.AddScoped<IAuthHelper, AuthHelper>();

        var app = builder.Build();

        // Konfigurace middleware pipeliny
        app.UseRouting();
        
        // Nastavení výchozí routy pro controllery
        app.MapDefaultControllerRoute();
        
        // Aktivace Session middleware
        app.UseSession();
        
        // Povolení statických souborů (CSS, JS, obrázky) ve wwwroot
        app.UseStaticFiles();

        // Automatické naplnění databáze testovacími daty při startu v režimu Development
        if (app.Environment.IsDevelopment())
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DataContext>();
                await DbSeeder.SeedAsync(context);
            }
        }
        
        app.Run();
    }

    // Simple function that tests the basics of our database, serves also as an example 
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
            var post = new Post {Type = Post.PostType.Offer, Category = Post.JobCategory.IT, Title = "My First Post", Content = "Hello World!", UserId = user.Id };
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
