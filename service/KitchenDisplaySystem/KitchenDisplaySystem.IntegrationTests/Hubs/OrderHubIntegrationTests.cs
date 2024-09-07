using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenDisplaySystem.DTO;
using KitchenDisplaySystem.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace KitchenDisplaySystem.IntegrationTests.Hubs
{
    public class OrderHubIntegrationTests : IntegrationTest
    {
        public OrderHubIntegrationTests(KitchenDisplaySystemWebApplicationFactory appFactory) : base(appFactory)
        {
        }

        [Fact]
        public async Task OrderCreated_AsWaiter_ReturnsOrder()
        {
            // Arrange
            await AuthenticateAsync(Role.Waiter);

            Order orderFromClient = new Order()
            {
                Start = new DateTime(2024, 9, 6, 20, 15, 0),
                Note = "Salata bez ulja!",
                TableId = 1,
                WaiterId = 1,
                OrderItems = [new OrderItem() { FoodId = 1, Quantity = 1 }, new OrderItem() { FoodId = 3, Quantity = 2 }]
            };

            OrderDTO expectedOrder = new OrderDTO()
            {
                Id = 2,
                Start = new DateTime(2024, 9, 6, 20, 15, 0),
                Note = "Salata bez ulja!",
                TableNumber = 10,
                WaiterDisplayName = "Marko M.",
                OrderItems = [new OrderItemDTO() { FoodName = "Brusketi sa paradajzom i bosiljkom", Quantity = 1 }, new OrderItemDTO() { FoodName = "Tortilja sa kremastom dimljenom butkicom", Quantity = 2 }]
            };

            OrderDTO? orderFromServer = null;

            var hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7141/hubs/order", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(TestClient.DefaultRequestHeaders.Authorization.Parameter);
                    options.HttpMessageHandlerFactory = _ => appFactory.Server.CreateHandler();

                })
                .Build();

            string msg;
            hubConnection.On<OrderDTO>("AddOrder", order => orderFromServer = order);
            hubConnection.On<string>("ReceiveError", errorMsg => msg = errorMsg);

            await hubConnection.StartAsync();

            // Act
            await hubConnection.InvokeAsync("OrderCreated", orderFromClient);
            await Task.Delay(5000);

            // Assert
            Assert.Equal(expectedOrder.Id, orderFromServer.Id);
            Assert.Equal(expectedOrder.Start, orderFromServer.Start);
            Assert.Equal(expectedOrder.End, orderFromServer.End);
            Assert.Equal(expectedOrder.Served, orderFromServer.Served);
            Assert.Equal(expectedOrder.Note, orderFromServer.Note);
            Assert.Equal(expectedOrder.TableNumber, orderFromServer.TableNumber);
            Assert.Equal(expectedOrder.WaiterDisplayName, orderFromServer.WaiterDisplayName);
            Assert.Equal(expectedOrder.OrderItems.Count, orderFromServer.OrderItems.Count);

            await hubConnection.StopAsync();
        }
    }
}
