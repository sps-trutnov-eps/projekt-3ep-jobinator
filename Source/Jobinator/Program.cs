using Microsoft.EntityFrameworkCore;
using Jobinator.Data;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Ensure you add DbContext and use the connection string from appsettings.json
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MyDatabase")));

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline, middleware, etc.
        app.UseRouting();
        app.MapDefaultControllerRoute();
        app.Run();
    }
}
