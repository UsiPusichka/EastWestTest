using Microsoft.EntityFrameworkCore;

namespace EastWestTest.Repository.Context
{
    public class Initializer
    {
        public static void ApplyMigrations(DataContext dataContext)
        {
            dataContext.Database.Migrate();
        }
    }
}
