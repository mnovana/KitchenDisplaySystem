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
    public class WaitersControllerTests
    {
        [Fact]
        public async Task GetWaiters_ReturnsCollection()
        {
            //Arrange
            List<Waiter> waiters = new List<Waiter>()
            {
                new Waiter { Id = 1, Name = "Marko", Surname = "Marković", DisplayName = "Marko M.", Phone = "0618521114" },
                new Waiter { Id = 2, Name = "Marko", Surname = "Jovanović", DisplayName = "Marko J.", Phone = "0612336852" },
                new Waiter { Id = 3, Name = "Jovana", Surname = "Jovanović", DisplayName = "Jovana", Phone = "0632448752" }
            };

            var mockRepository = new Mock<IWaiterRepository>();
            mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(waiters);

            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfiles()));
            var mapper = mapperConfiguration.CreateMapper();

            var waitersController = new WaitersController(mockRepository.Object, mapper);

            //Act
            var result = await waitersController.GetWaiters() as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);

            List<WaiterDTO> listResult = (List<WaiterDTO>)result.Value;

            for (int i = 0; i < listResult.Count; i++)
            {
                Assert.Equal(waiters[i].Id, listResult[i].Id);
                Assert.Equal(waiters[i].DisplayName, listResult[i].DisplayName);
            }
        }
    }
}
