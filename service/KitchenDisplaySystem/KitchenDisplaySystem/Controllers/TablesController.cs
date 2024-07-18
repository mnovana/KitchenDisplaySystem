using AutoMapper;
using KitchenDisplaySystem.DTO;
using KitchenDisplaySystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitchenDisplaySystem.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableRepository _tableRepository;

        public TablesController(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTables()
        {
            return Ok(await _tableRepository.GetAllAsync());
        }
    }
}
