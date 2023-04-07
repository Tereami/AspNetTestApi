using Microsoft.EntityFrameworkCore;
using DataAccess;
using DomainModel;


namespace AspNetTestApi.Data
{
    public class DbInitializer
    {
        private readonly ILogger<DbInitializer> logger;
        private readonly DB db;

        public DbInitializer(ILogger<DbInitializer> logger, DB db)
        {
            this.logger = logger;
            this.db = db;
        }

        public void Initialize()
        {
            logger.LogInformation("DB initialization...");
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
        }
    }
}
