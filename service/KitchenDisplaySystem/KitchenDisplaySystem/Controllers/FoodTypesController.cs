using KitchenDisplaySystem.DTO;
using KitchenDisplaySystem.Models;
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
        public async Task<IActionResult> GetFoodType(int id)
        {
            var foodType = await _foodTypeRepository.GetByIdAsync(id);
            return foodType != null ? Ok(foodType) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PostFoodType(FoodType foodType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _foodTypeRepository.AddAsync(foodType);

            return CreatedAtAction(nameof(GetFoodType), new { id = foodType.Id }, foodType);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutFoodType(int id, FoodType foodType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id != foodType.Id)
            {
                return BadRequest();
            }

            try
            {
                await _foodTypeRepository.UpdateAsync(foodType);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(foodType);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteFoodType(int id)
        {
            var foodType = await _foodTypeRepository.GetByIdAsync(id);

            if (foodType == null)
            {
                return BadRequest();
            }

            await _foodTypeRepository.DeleteAsync(foodType);

            return NoContent();
        }
    }
}
