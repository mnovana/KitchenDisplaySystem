using KitchenDisplaySystem.Context;
using KitchenDisplaySystem.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestExecutor;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace KitchenDisplaySystem.IntegrationTests
{
    public enum Role { Kitchen, Waiter, Admin }

    public class IntegrationTest : IClassFixture<KitchenDisplaySystemWebApplicationFactory>
    {
        protected readonly HttpClient TestClient;
        protected readonly IServiceScopeFactory scopeFactory;

        protected IntegrationTest(KitchenDisplaySystemWebApplicationFactory appFactory)
        {
            TestClient = appFactory.CreateClient();
            scopeFactory = appFactory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        protected async Task AuthenticateAsync(Role role)
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(role));
        }

        private async Task<string> GetJwtAsync(Role role)
        {
            string username = Guid.NewGuid().ToString();    // new user created for each test method

            using (var scope = scopeFactory.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                // create and seed the database
                var db = scopedServices.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();

                
                // register new user
                var userManager = scopedServices.GetRequiredService<UserManager<AppUser>>();
                AppUser user = new AppUser { UserName = username };
                var createResult = await userManager.CreateAsync(user, "Password123!");
                Assert.True(createResult.Succeeded);

                // add role
                var addRoleResult = await userManager.AddToRoleAsync(user, role.ToString());
                Assert.True(addRoleResult.Succeeded);
            }
            
            // login
            var response = await TestClient.PostAsJsonAsync("https://localhost:7141/login", new LoginDTO
            {
                Username = username,
                Password = "Password123!"
            });

            // get token from login response
            var responseBody = await response.Content.ReadFromJsonAsync<TokenDTO>();
            
            return responseBody.Token;
        }
    }
}
