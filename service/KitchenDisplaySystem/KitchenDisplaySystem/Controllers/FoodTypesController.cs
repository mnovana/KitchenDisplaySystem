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
    public class FoodTypesController : ControllerBase
    {
        private readonly IFoodTypeRepository _foodTypeRepository;

        public FoodTypesController(IFoodTypeRepository foodTypeRepository)
        {
            _foodTypeRepository = foodTypeRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetFoodTypes()
        {
            return Ok(await _foodTypeRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetFoodTypeById(int id)
        {
            var foodType = await _foodTypeRepository.GetByIdAsync(id);
            return foodType != null ? Ok(foodType) : NotFound();
        }
    }
}
