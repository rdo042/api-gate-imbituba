using GateAPI.Infra.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GateAPI.Tests.Infra
{
    public class AppDbContextInMemory : IDisposable
    {
        public AppDbContext Context { get; }
        public AppDbContextInMemory()
        {
            Context = CreateInMemoryContext();
        }

        private static AppDbContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var accessor = new HttpContextAccessor();

            return new AppDbContext(options, accessor);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
