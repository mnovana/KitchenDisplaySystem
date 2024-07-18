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
    public class OrderDataControllerTests
    {
        [Fact]
        public async Task GetOrderData_ReturnsObject()
        {
            //Arrange
            List<Food> food = new List<Food>()
            {
                new Food { Id = 1, Name = "Brusketi sa paradajzom i bosiljkom", FoodTypeId = 1, FoodType = new FoodType { Id = 1, Name = "Predjelo" } },
                new Food { Id = 2, Name = "Brusketi sa dimljenim lososom i rukolom", FoodTypeId = 1, FoodType = new FoodType { Id = 1, Name = "Predjelo" } },
                new Food { Id = 3, Name = "Tortilja sa kremastom dimljenom butkicom", FoodTypeId = 1, FoodType = new FoodType { Id = 1, Name = "Predjelo" } }
            };

            List<FoodType> foodTypes = new List<FoodType>()
            {
                new FoodType { Id = 1, Name = "Predjelo" },
                new FoodType { Id = 2, Name = "Glavno jelo" }
            };

            List<Table> tables = new List<Table>()
            {
                new Table { Id = 1, Number = 10 },
                new Table { Id = 2, Number = 11 }
            };

            List<Waiter> waiters = new List<Waiter>()
            {
                new Waiter { Id = 1, Name = "Marko", Surname = "Marković", DisplayName = "Marko M.", Phone = "0618521114" },
                new Waiter { Id = 2, Name = "Marko", Surname = "Jovanović", DisplayName = "Marko J.", Phone = "0612336852" }
            };

            var mockFoodRepository = new Mock<IFoodRepository>();
            var mockFoodTypeRepository = new Mock<IFoodTypeRepository>();
            var mockTableRepository = new Mock<ITableRepository>();
            var mockWaiterRepository = new Mock<IWaiterRepository>();
            mockFoodRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(food);
            mockFoodTypeRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(foodTypes);
            mockTableRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(tables);
            mockWaiterRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(waiters);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
            var mapper = mapperConfiguration.CreateMapper();

            var orderDataController = new OrderDataController(mockFoodRepository.Object, mockFoodTypeRepository.Object, mockTableRepository.Object, mockWaiterRepository.Object, mapper);

            //Act
            var result = await orderDataController.GetOrderData() as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);

            OrderDataDTO objectResult = (OrderDataDTO)result.Value;
            List<FoodDTO> foodList = objectResult.Food.ToList();
            List<FoodType> foodTypeList = objectResult.FoodTypes.ToList();
            List<Table> tableList = objectResult.Tables.ToList();
            List<WaiterDTO> waiterList = objectResult.Waiters.ToList();

            for (int i = 0; i < foodList.Count; i++)
            {
                Assert.Equal(food[i].Id, foodList[i].Id);
                Assert.Equal(food[i].Name, foodList[i].Name);
                Assert.Equal(food[i].FoodTypeId, foodList[i].FoodTypeId);
                Assert.Equal(food[i].FoodType.Name, foodList[i].FoodTypeName);
            }
            for (int i = 0; i < foodTypeList.Count; i++)
            {
                Assert.Equal(foodTypes[i].Id, foodTypeList[i].Id);
                Assert.Equal(foodTypes[i].Name, foodTypeList[i].Name);
            }
            for (int i = 0; i < tableList.Count; i++)
            {
                Assert.Equal(tables[i].Id, tableList[i].Id);
                Assert.Equal(tables[i].Number, tableList[i].Number);
            }
            for (int i = 0; i < waiterList.Count; i++)
            {
                Assert.Equal(waiters[i].Id, waiterList[i].Id);
                Assert.Equal(waiters[i].DisplayName, waiterList[i].DisplayName);
            }

        }
    }
}
