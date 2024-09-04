using KitchenDisplaySystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KitchenDisplaySystem.IntegrationTests.Controllers
{
    public class OrderDataControllerIntegrationTests : IntegrationTest
    {
        public OrderDataControllerIntegrationTests(KitchenDisplaySystemWebApplicationFactory appFactory) : base(appFactory)
        {
        }

        [Fact]
        public async Task GetOrderData_AsKitchen_ReturnsForbidden()
        {
            // arrange
            await AuthenticateAsync(Role.Kitchen);

            // act
            var response = await TestClient.GetAsync("https://localhost:7141/orderdata");

            // assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task GetOrderData_NotAuthenticated_ReturnsUnauthorized()
        {
            // arrange
            
            // act
            var response = await TestClient.GetAsync("https://localhost:7141/orderdata");

            // assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetOrderData_AsWaiter_ReturnsOk()
        {
            // arrange
            await AuthenticateAsync(Role.Waiter);

            // act
            var response = await TestClient.GetAsync("https://localhost:7141/orderdata");

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
