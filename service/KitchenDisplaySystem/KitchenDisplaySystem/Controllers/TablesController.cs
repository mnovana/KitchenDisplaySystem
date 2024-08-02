using AutoMapper;
using KitchenDisplaySystem.DTO;
using KitchenDisplaySystem.Repositories;
using KitchenDisplaySystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitchenDisplaySystem.Controllers
{
    [Authorize(Roles = "Admin")]
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetTables()
        {
            return Ok(await _tableRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetTableById(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            return table != null ? Ok(table) : NotFound();
        }
    }
}
