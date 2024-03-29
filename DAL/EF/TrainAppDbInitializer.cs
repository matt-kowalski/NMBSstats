using Microsoft.EntityFrameworkCore.Diagnostics;

namespace TrainApp.DAL.EF;

public static class TrainAppDbInitializer
{
    public static void Initialize(TrainAppDbContext dbContext, bool rebuild)
    {
        if (!dbContext.IsInitialized)
        {
            if (rebuild) dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            dbContext.IsInitialized = true;
        }
    }
}