using KitchenDisplaySystem.Controllers;
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
    public class TablesControllerTests
    {
        [Fact]
        public async Task GetTables_ReturnsCollection()
        {
            //Arrange
            List<Table> tables = new List<Table>()
            {
                new Table { Id = 1, Number = 10 },
                new Table { Id = 2, Number = 11 },
                new Table { Id = 3, Number = 12 }
            };

            var mockRepository = new Mock<ITableRepository>();
            mockRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(tables);

            var tablesController = new TablesController(mockRepository.Object);

            //Act
            var result = await tablesController.GetTables() as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);

            List<Table> listResult = (List<Table>)result.Value;

            for (int i = 0; i < listResult.Count; i++)
            {
                Assert.Equal(tables[i].Id, listResult[i].Id);
                Assert.Equal(tables[i].Number, listResult[i].Number);
            }
        }
    }
}
