using Jobinator.Models;
using Microsoft.EntityFrameworkCore;

namespace Jobinator.Data
{
    // Třída pro automatické naplnění databáze počátečními daty
    public static class DbSeeder
    {
        public static async Task SeedAsync(DataContext context)
        {
            // Kontrola a naplnění uživatelů
            if (!await context.Users.AnyAsync())
            {
                var users = new List<User>
                {
                    new User { Username = "john_dev", Name = "John", Surname = "Smith", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123") },
                    new User { Username = "alice_it", Name = "Alice", Surname = "Johnson", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123") },
                    new User { Username = "bob_builder", Name = "Bob", Surname = "Miller", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123") },
                    new User { Username = "sarah_health", Name = "Sarah", Surname = "Wilson", PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123") }
                };

                context.Users.AddRange(users);
                await context.SaveChangesAsync();
            }

            // Kontrola a naplnění příspěvků
            if (!await context.Posts.AnyAsync())
            {
                var users = await context.Users.Take(4).ToListAsync();
                if (users.Any())
                {
                    var posts = new List<Post>
                    {
                        new Post { Title = "Full Stack Web Developer", Content = "I am looking for a talented developer to help build a custom e-commerce platform using ASP.NET Core and React.", Category = Post.JobCategory.IT, Type = Post.PostType.Demand, UserId = users[0].Id },
                        new Post { Title = "Professional Website Design", Content = "I offer high-quality UI/UX design services for your business. I have 5 years of experience with Figma and Adobe XD.", Category = Post.JobCategory.IT, Type = Post.PostType.Offer, UserId = users.Count > 1 ? users[1].Id : users[0].Id },
                        new Post { Title = "Furniture Assembly & Handyman", Content = "I can help you assemble furniture, fix minor leaks, or paint your rooms. Reliable and fast service.", Category = Post.JobCategory.Construction, Type = Post.PostType.Offer, UserId = users.Count > 2 ? users[2].Id : users[0].Id },
                        new Post { Title = "Home Renovation Help Needed", Content = "Searching for a helper to assist with a kitchen renovation project. No experience required, just a positive attitude!", Category = Post.JobCategory.Construction, Type = Post.PostType.Demand, UserId = users[0].Id },
                        new Post { Title = "Delivery Driver Services", Content = "Available for local deliveries and small moves. I have my own van and flexible hours.", Category = Post.JobCategory.Logistics, Type = Post.PostType.Offer, UserId = users.Count > 2 ? users[2].Id : users[0].Id },
                        new Post { Title = "Urgent: Courier for Weekend", Content = "In need of a reliable courier to deliver packages across the city this coming Saturday and Sunday.", Category = Post.JobCategory.Logistics, Type = Post.PostType.Demand, UserId = users.Count > 1 ? users[1].Id : users[0].Id },
                        new Post { Title = "Private Yoga Lessons", Content = "Certified instructor offering private yoga sessions at your home. Focus on flexibility and stress relief.", Category = Post.JobCategory.Healthcare, Type = Post.PostType.Offer, UserId = users.Count > 3 ? users[3].Id : users[0].Id },
                        new Post { Title = "Looking for a Tutor", Content = "Seeking a biology tutor for a high school student. Twice a week, preferably in the evenings.", Category = Post.JobCategory.Healthcare, Type = Post.PostType.Demand, UserId = users[0].Id },
                        new Post { Title = "Tax Preparation Services", Content = "I can help you prepare your annual tax returns and provide financial advice for small business owners.", Category = Post.JobCategory.Finance, Type = Post.PostType.Offer, UserId = users.Count > 1 ? users[1].Id : users[0].Id },
                        new Post { Title = "Bookkeeper Wanted", Content = "Our small construction firm is looking for a part-time bookkeeper to manage invoices and payroll.", Category = Post.JobCategory.Finance, Type = Post.PostType.Demand, UserId = users.Count > 2 ? users[2].Id : users[0].Id }
                    };

                    context.Posts.AddRange(posts);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
