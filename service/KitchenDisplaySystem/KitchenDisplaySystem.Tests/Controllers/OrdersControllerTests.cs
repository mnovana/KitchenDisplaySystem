using AutoMapper;
using KitchenDisplaySystem.Controllers;
using KitchenDisplaySystem.DTO;
using KitchenDisplaySystem.DTO.Mapping;
using KitchenDisplaySystem.Models;
using KitchenDisplaySystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KitchenDisplaySystem.Tests.Controllers
{
    public class OrdersControllerTests
    {
        [Fact]
        public async Task GetUnservedOrders_ReturnsCollection()
        {
            //Arrange
            Order order1 = new Order
            {
                Id = 1,
                Start = DateTime.Now,
                End = null,
                Served = false,
                Note = null,
                TableId = 1,
                WaiterId = 1,
                Table = new Table { Id = 1, Number = 10 },
                Waiter = new Waiter { Id = 1, Name = "Marko", Surname = "Marković", DisplayName = "Marko M.", Phone = "0618521114" }
            };

            Order order2 = new Order
            {
                Id = 2,
                Start = DateTime.Now,
                End = null,
                Served = false,
                Note = null,
                TableId = 1,
                WaiterId = 1,
                Table = new Table { Id = 1, Number = 10 },
                Waiter = new Waiter { Id = 1, Name = "Marko", Surname = "Marković", DisplayName = "Marko M.", Phone = "0618521114" }
            };

            FoodType foodType = new FoodType { Id = 1, Name = "Predjelo" };

            Food food1 = new Food { Id = 1, Name = "Brusketi sa paradajzom i bosiljkom", FoodTypeId = 1, FoodType = foodType };
            Food food2 = new Food { Id = 8, Name = "Pikantni uštipci", FoodTypeId = 2, FoodType = foodType };

            order1.OrderItems = new List<OrderItem>() { new OrderItem { OrderId = 1, FoodId = 1, Quantity = 1, Food = food1 }, new OrderItem { OrderId = 1, FoodId = 8, Quantity = 2, Food = food2 } };

            order2.OrderItems = new List<OrderItem>() { new OrderItem { OrderId = 2, FoodId = 1, Quantity = 1, Food = food1 }, new OrderItem { OrderId = 2, FoodId = 8, Quantity = 2, Food = food2 } };

            List<Order> orders = new List<Order>() { order1, order2 };

            var mockRepository = new Mock<IOrderRepository>();
            mockRepository.Setup(x => x.GetAllUnservedAsync()).ReturnsAsync(orders);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
            var mapper = mapperConfiguration.CreateMapper();

            var ordersController = new OrdersController(mockRepository.Object, mapper);

            //Act
            var result = await ordersController.GetUnservedOrders() as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);

            List<OrderDTO> listResult = (List<OrderDTO>)result.Value;
            Assert.NotNull(listResult);
            Assert.Equal(orders.Count, listResult.Count);

            for (int i = 0; i < listResult.Count; i++)
            {
                Assert.Equal(orders[i].Id, listResult[i].Id);
                Assert.Equal(orders[i].Start, listResult[i].Start);
                Assert.Equal(orders[i].End, listResult[i].End);
                Assert.Equal(orders[i].Note, listResult[i].Note);
                Assert.Equal(orders[i].Served, listResult[i].Served);
                Assert.Equal(orders[i].Table.Number, listResult[i].TableNumber);
                Assert.Equal(orders[i].Waiter.DisplayName, listResult[i].WaiterDisplayName);

                var expectedItems = orders[i].OrderItems.ToList();
                var actualItems = listResult[i].OrderItems.ToList();

                for(int j = 0; j < expectedItems.Count; j++)
                {
                    Assert.Equal(expectedItems[j].Food.Name, actualItems[j].FoodName);
                    Assert.Equal(expectedItems[j].Quantity, actualItems[j].Quantity);
                }
            }
        }
    }
}
