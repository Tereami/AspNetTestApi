using Microsoft.EntityFrameworkCore;
using DataAccess;
using DomainModel;
using Microsoft.AspNetCore.Identity;
using DomainModel.Identity;

namespace AspNetTestApi.Data
{
    public class DbInitializer
    {
        private readonly ILogger<DbInitializer> logger;
        private readonly DB db;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public DbInitializer(ILogger<DbInitializer> logger, DB db, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.logger = logger;
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void Initialize()
        {
            logger.LogInformation("DB initialization...");

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            if (db.Database.GetAppliedMigrations().Any())
            {
                logger.LogInformation("Apply migrations...");
                db.Database.Migrate();
                logger.LogInformation("Migrations applied");
            }
            else
            {
                logger.LogInformation("Migrations not required");
            }

            if (db.TestObjects.Any())
            {
                logger.LogInformation("TestObjects already added");
            }
            else
            {
                logger.LogInformation("Initialize testObjects");
                List<TestObject> objects = new List<TestObject>
                {
                    new TestObject(0, "First объект", "First описание", "tag1,тэг2"),
                    new TestObject(0, "Second объект", "Второе description", "tag1,tag3"),
                };
                db.TestObjects.AddRange(objects);
                db.SaveChanges();
                logger.LogInformation("TestObjects initialized!");
            }

            InitializeIdentity().Wait();
        }

        private async Task InitializeIdentity()
        {
            await CheckRole(Role.Users);
            await CheckRole(Role.Administrators);

            if (await userManager.FindByNameAsync(User.Administrator) == null)
            {
                logger.LogInformation($"No user {User.Administrator}, creating...");
                User admin = new User { UserName = User.Administrator };
                IdentityResult result = await userManager.CreateAsync(admin, User.DefaultAdminPassword);
                if (result.Succeeded)
                {
                    logger.LogInformation($"User {User.Administrator} created!");
                    await userManager.AddToRoleAsync(admin, Role.Administrators);
                    logger.LogInformation($"Set user {User.Administrator} admin");
                }
                else
                {
                    string error = string.Join(',', result.Errors.Select(i => i.Description));
                    logger.LogError(error);
                    throw new Exception(error);
                }
            }
        }

        private async Task CheckRole(string RoleName)
        {
            if (await roleManager.RoleExistsAsync(RoleName))
            {
                logger.LogInformation($"Role exists: {RoleName}");
            }
            else
            {
                await roleManager.CreateAsync(new Role { Name = RoleName });
                logger.LogInformation($"Role created: {RoleName}");
            }
        }

    }
}
