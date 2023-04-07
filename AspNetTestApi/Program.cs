using Microsoft.EntityFrameworkCore;
using DataAccess;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<DB>(opt =>
           opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")));
        builder.Services.AddTransient<AspNetTestApi.Data.DbInitializer>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseStaticFiles();

        app.UseRouting();

        using(var scope = app.Services.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<AspNetTestApi.Data.DbInitializer>().Initialize();
        }

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}