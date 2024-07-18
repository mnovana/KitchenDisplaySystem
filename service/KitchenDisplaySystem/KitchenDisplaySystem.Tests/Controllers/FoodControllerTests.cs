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
    public class FoodControllerTests
    {
        [Fact]
        public async Task GetFood_ReturnsCollection()
        {
            //Arrange
            FoodType foodType = new FoodType { Id = 1, Name = "Predjelo" };
            List<Food> food = new List<Food>()
            {
                new Food { Id = 1, Name = "Brusketi sa paradajzom i bosiljkom", FoodTypeId = 1, FoodType = foodType },
                new Food { Id = 2, Name = "Brusketi sa dimljenim lososom i rukolom", FoodTypeId = 1, FoodType = foodType },
                new Food { Id = 3, Name = "Tortilja sa kremastom dimljenom butkicom", FoodTypeId = 1, FoodType = foodType }
            };

            var mockRepository = new Mock<IFoodRepository>();
            mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(food);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
            var mapper = mapperConfiguration.CreateMapper();

            var foodController = new FoodController(mockRepository.Object, mapper);

            //Act
            var result = await foodController.GetFood() as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);

            List<FoodDTO> listResult = (List<FoodDTO>)result.Value;

            for (int i = 0; i < listResult.Count; i++)
            {
                Assert.Equal(food[i].Id, listResult[i].Id);
                Assert.Equal(food[i].Name, listResult[i].Name);
                Assert.Equal(food[i].FoodTypeId, listResult[i].FoodTypeId);
                Assert.Equal(food[i].FoodType.Name, listResult[i].FoodTypeName);
            }
        }
    }
}
