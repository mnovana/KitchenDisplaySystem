using KitchenDisplaySystem.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenDisplaySystem.IntegrationTests
{
    public class KitchenDisplaySystemWebApplicationFactory : WebApplicationFactory<Program>
    {
        private string connectionString;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                // removing options for the real database so we can set it to use a new one for testing
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));

                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                connectionString = "Server=(localdb)\\mssqllocaldb;Database=KdsTestDb;Trusted_Connection=True;MultipleActiveResultSets=true";

                services.AddDbContext<AppDbContext>((options) =>
                {
                    options.UseSqlServer(connectionString);
                });

                // Create and migrate the database
                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    db.Database.EnsureCreated();
                }
            });
        }

        public override async ValueTask DisposeAsync()
        {
            // if the database was created delete it
            if (!string.IsNullOrEmpty(connectionString))
            {
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;

                using var context = new AppDbContext(options);
                await context.Database.EnsureDeletedAsync();
            }

            await base.DisposeAsync();
        }
    }
}
