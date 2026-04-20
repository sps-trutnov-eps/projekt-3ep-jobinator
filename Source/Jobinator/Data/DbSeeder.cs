using Bogus;
using Jobinator.Models;
using Microsoft.EntityFrameworkCore;

namespace Jobinator.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(DataContext context)
        {
            // Seed Users if empty
            if (!await context.Users.AnyAsync())
            {
                var userFaker = new Faker<User>()
                    .RuleFor(u => u.Username, f => f.Internet.UserName())
                    .RuleFor(u => u.Name, f => f.Name.FirstName())
                    .RuleFor(u => u.Surname, f => f.Name.LastName())
                    .RuleFor(u => u.PasswordHash, f => BCrypt.Net.BCrypt.HashPassword("Password123")); // Known password for dev ease

                var users = userFaker.Generate(10);
                context.Users.AddRange(users);
                await context.SaveChangesAsync();
            }

            // Seed Posts if empty
            if (!await context.Posts.AnyAsync())
            {
                var userIds = await context.Users.Select(u => u.Id).ToListAsync();
                if (userIds.Any())
                {
                    var postFaker = new Faker<Post>()
                        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
                        .RuleFor(p => p.Content, f => f.Lorem.Paragraphs(1))
                        .RuleFor(p => p.Category, f => f.PickRandom<Post.JobCategory>())
                        .RuleFor(p => p.Type, f => f.PickRandom<Post.PostType>())
                        .RuleFor(p => p.UserId, f => f.PickRandom(userIds));

                    var posts = postFaker.Generate(30);
                    context.Posts.AddRange(posts);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
