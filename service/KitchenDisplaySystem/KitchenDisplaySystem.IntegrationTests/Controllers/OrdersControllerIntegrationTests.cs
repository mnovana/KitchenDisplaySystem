using KitchenDisplaySystem.DTO;
using KitchenDisplaySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace KitchenDisplaySystem.IntegrationTests.Controllers
{
    public class OrdersControllerIntegrationTests : IntegrationTest
    {
        public OrdersControllerIntegrationTests(KitchenDisplaySystemWebApplicationFactory appFactory) : base(appFactory)
        {
        }

        [Fact]
        public async Task GetUnservedOrders_AsWaiter_ReturnsCollection()
        {
            // arrange
            await AuthenticateAsync(Role.Waiter);

            List<OrderItemDTO> orderItems = new List<OrderItemDTO>()
            {
                new OrderItemDTO { FoodName = "Brusketi sa paradajzom i bosiljkom", Quantity = 1 },
                new OrderItemDTO { FoodName = "Pikantni uštipci", Quantity = 2 },
                new OrderItemDTO { FoodName = "Vitaminska salata", Quantity = 1 },
                new OrderItemDTO { FoodName = "Pita sa borovnicom", Quantity = 3 }
            };

            List<OrderDTO> expectedOrders = new List<OrderDTO>()
            {
                new OrderDTO { Id = 1, Start = new DateTime(2024,6,30, 15, 30, 0), End = new DateTime(2024, 6, 30, 15, 44, 0), Served = false, Note = "Salata bez ulja!", TableNumber = 10, WaiterDisplayName = "Marko M.", OrderItems = orderItems }
            };

            // act
            var response = await TestClient.GetAsync("https://localhost:7141/orders/unserved");

            // assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var resultOrders = await response.Content.ReadFromJsonAsync<List<OrderDTO>>();
            Assert.Equal(expectedOrders.Count, resultOrders.Count);

            for (int i = 0; i < expectedOrders.Count; i++)
            {
                Assert.Equal(expectedOrders[i].Id, resultOrders[i].Id);
                Assert.Equal(expectedOrders[i].Start, resultOrders[i].Start);
                Assert.Equal(expectedOrders[i].End, resultOrders[i].End);
                Assert.Equal(expectedOrders[i].Served, resultOrders[i].Served);
                Assert.Equal(expectedOrders[i].Note, resultOrders[i].Note);
                Assert.Equal(expectedOrders[i].TableNumber, resultOrders[i].TableNumber);
                Assert.Equal(expectedOrders[i].WaiterDisplayName, resultOrders[i].WaiterDisplayName);

                var expectedOrderItems = expectedOrders[i].OrderItems.ToList();
                var resultOrderItems = resultOrders[i].OrderItems.ToList();
                Assert.Equal(expectedOrderItems.Count, resultOrderItems.Count);

                for (int j = 0; j < expectedOrderItems.Count; j++)
                {
                    Assert.Equal(expectedOrderItems[j].FoodName, resultOrderItems[j].FoodName);
                    Assert.Equal(expectedOrderItems[j].Quantity, resultOrderItems[j].Quantity);
                }
            }
        }
    }
}
