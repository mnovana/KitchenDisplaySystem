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

        [Fact]
        public async Task GetFoodById_ValidId_ReturnsObject()
        {
            // Arrange
            Food food = new Food { 
                Id = 1, 
                Name = "Brusketi sa paradajzom i bosiljkom", 
                FoodTypeId = 1, 
                FoodType = new FoodType { Id = 1, Name = "Predjelo" } 
            };

            var mockRepository = new Mock<IFoodRepository>();
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(food);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
            var mapper = mapperConfiguration.CreateMapper();

            var foodController = new FoodController(mockRepository.Object, mapper);

            // Act
            var result = await foodController.GetFoodById(1) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);

            var objectResult = (FoodDTO)result.Value;

            Assert.Equal(food.Id, objectResult.Id);
            Assert.Equal(food.Name, objectResult.Name);
            Assert.Equal(food.FoodTypeId, objectResult.FoodTypeId);
            Assert.Equal(food.FoodType.Name, objectResult.FoodTypeName);
        }

        [Fact]
        public async Task GetFoodById_InvalidId_ReturnsNotFound()
        {
            // Assert
            var mockRepository = new Mock<IFoodRepository>();

            var mockMapper = new Mock<IMapper>();

            var foodController = new FoodController(mockRepository.Object, mockMapper.Object);

            // Act
            var result = await foodController.GetFoodById(1) as NotFoundResult;
            
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteFood_ValidId_ReturnsNoContent()
        {
            // Assert
            var mockRepository = new Mock<IFoodRepository>();
            mockRepository.Setup(x => x.DeleteAsync(1)).ReturnsAsync(true);

            var mockMapper = new Mock<IMapper>();

            var foodController = new FoodController(mockRepository.Object, mockMapper.Object);

            // Act
            var result = await foodController.DeleteFood(1) as NoContentResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteFood_InvalidId_ReturnsBadRequest()
        {
            // Assert
            var mockRepository = new Mock<IFoodRepository>();
            mockRepository.Setup(x => x.DeleteAsync(1)).ReturnsAsync(false);

            var mockMapper = new Mock<IMapper>();

            var foodController = new FoodController(mockRepository.Object, mockMapper.Object);

            // Act
            var result = await foodController.DeleteFood(1) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task PostFood_ValidModelState_ReturnsCreatedAtAction()
        {
            // Arrange
            Food userFood = new Food()
            {
                Name = "Brusketi sa paradajzom i bosiljkom",
                FoodTypeId = 1
            };

            Food dbFood = new Food()
            {
                Id = 1,
                Name = "Brusketi sa paradajzom i bosiljkom",
                Deleted = false,
                FoodTypeId = 1,
                FoodType = new FoodType { Id = 1, Name = "Predjelo" }
            };

            var mockRepository = new Mock<IFoodRepository>();
            mockRepository.Setup(x => x.AddAsync(userFood)).Callback(() => userFood.Id = 1);
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(dbFood);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
            var mapper = mapperConfiguration.CreateMapper();

            var foodController = new FoodController(mockRepository.Object, mapper);

            // Act
            var result = await foodController.PostFood(userFood) as CreatedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal("GetFoodById", result.ActionName);
            Assert.Equal(1, result.RouteValues["id"]);

            var objectResult = (FoodDTO)result.Value;

            Assert.Equal(dbFood.Name, objectResult.Name);
            Assert.Equal(dbFood.FoodTypeId, objectResult.FoodTypeId);
            Assert.Equal(dbFood.FoodType.Name, objectResult.FoodTypeName);
        }

        [Fact]
        public async Task PostFood_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var food = new Food();
            var mockRepository = new Mock<IFoodRepository>();
            var mockMapper = new Mock<IMapper>();

            var foodController = new FoodController(mockRepository.Object, mockMapper.Object);

            foodController.ModelState.AddModelError("test", "test");

            // Act
            var result = await foodController.PostFood(food) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task PostFood_AddAsyncError_ReturnsBadRequest()
        {
            // Arrange
            var food = new Food();

            var mockRepository = new Mock<IFoodRepository>();
            mockRepository.Setup(x => x.AddAsync(food)).ThrowsAsync(new Exception());

            var mockMapper = new Mock<IMapper>();

            var foodController = new FoodController(mockRepository.Object, mockMapper.Object);

            // Act
            var result = await foodController.PostFood(food) as BadRequestResult;
        }

        [Fact]
        public async Task PutFood_ValidRequest_ReturnsOkObject()
        {
            // Arrange
            Food userFood = new Food()
            {
                Id = 1,
                Name = "Brusketi sa paradajzom i bosiljkom",
                FoodTypeId = 1
            };

            Food dbFood = new Food()
            {
                Id = 1,
                Name = "Brusketi sa paradajzom i bosiljkom",
                Deleted = false,
                FoodTypeId = 1,
                FoodType = new FoodType { Id = 1, Name = "Predjelo" }
            };

            var mockRepository = new Mock<IFoodRepository>();
            mockRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(dbFood);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
            var mapper = mapperConfiguration.CreateMapper();

            var foodController = new FoodController(mockRepository.Object, mapper);

            // Act
            var result = await foodController.PutFood(1, userFood) as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);

            var objectResult = (FoodDTO)result.Value;

            Assert.Equal(dbFood.Id, objectResult.Id);
            Assert.Equal(dbFood.Name, objectResult.Name);
            Assert.Equal(dbFood.FoodTypeId, objectResult.FoodTypeId);
            Assert.Equal(dbFood.FoodType.Name, objectResult.FoodTypeName);
        }

        [Fact]
        public async Task PutFood_InvalidId_ReturnsBadRequest()
        {
            // Arrange
            Food food = new Food()
            {
                Id = 1,
                Name = "test",
                FoodTypeId = 1
            };

            var mockRepository = new Mock<IFoodRepository>();
            var mockMapper = new Mock<IMapper>();

            var foodController = new FoodController(mockRepository.Object, mockMapper.Object);

            // Act
            var result = await foodController.PutFood(2, food) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task PutFood_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var food = new Food();
            var mockRepository = new Mock<IFoodRepository>();
            var mockMapper = new Mock<IMapper>();

            var foodController = new FoodController(mockRepository.Object, mockMapper.Object);
            foodController.ModelState.AddModelError("test", "test");

            // Act
            var result = await foodController.PutFood(1, food) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task PutFood_UpdateAsyncError_ReturnsBadRequest()
        {
            // Arrange
            var food = new Food() { Id = 1 };

            var mockRepository = new Mock<IFoodRepository>();
            mockRepository.Setup(x => x.UpdateAsync(food)).ThrowsAsync(new Exception());

            var mockMapper = new Mock<IMapper>();

            var foodController = new FoodController(mockRepository.Object, mockMapper.Object);

            // Act
            var result = await foodController.PutFood(1, food) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}
