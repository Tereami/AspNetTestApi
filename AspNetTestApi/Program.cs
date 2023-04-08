using Microsoft.EntityFrameworkCore;
using DataAccess;
using DomainModel.Identity;
using Microsoft.AspNetCore.Identity;

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


        #region Identity
        builder.Services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<DB>()
            .AddDefaultTokenProviders();
        builder.Services.Configure<IdentityOptions>(opt =>
        {
#if DEBUG
            opt.Password.RequireDigit = false;
            opt.Password.RequiredLength = 3;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredUniqueChars = 3;
#else
            opt.Password.RequiredLength = 8;
            opt.Password.RequireNonAlphanumeric = false;
#endif
            //opt.User.RequireUniqueEmail = true;
            opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 -._@+/ ";

            opt.Lockout.AllowedForNewUsers = false;
            opt.Lockout.MaxFailedAccessAttempts = 10;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(10);
        });

        builder.Services.ConfigureApplicationCookie(opt =>
        {
            opt.Cookie.Name = "AspNetTestApi";
            opt.Cookie.HttpOnly = true;
            opt.ExpireTimeSpan = TimeSpan.FromDays(10);
            opt.LoginPath = "/Account/Login";
            opt.LogoutPath = "/Account/Logout";
            opt.AccessDeniedPath = "/error/?code=403";
            opt.SlidingExpiration = true;
        });
        #endregion




        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

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