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
    public class FoodTypesControllerTests
    {
        [Fact]
        public async Task GetFoodTypes_ReturnsCollection()
        {
            //Arrange
            List<FoodType> foodTypes = new List<FoodType>()
            {
                new FoodType { Id = 1, Name = "Predjelo" },
                new FoodType { Id = 2, Name = "Glavno jelo" },
                new FoodType { Id = 3, Name = "Salata" }
            };

            var mockRepository = new Mock<IFoodTypeRepository>();
            mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(foodTypes);

            var foodTypesController = new FoodTypesController(mockRepository.Object);

            //Act
            var result = await foodTypesController.GetFoodTypes() as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);

            List<FoodType> listResult = (List<FoodType>)result.Value;

            for (int i = 0; i < listResult.Count; i++)
            {
                Assert.Equal(foodTypes[i].Id, listResult[i].Id);
                Assert.Equal(foodTypes[i].Name, listResult[i].Name);
            }
        }
    }
}
