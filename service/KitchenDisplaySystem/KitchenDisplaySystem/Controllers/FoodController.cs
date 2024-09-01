using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IMapper _mapper;

        public FoodController(IFoodRepository foodRepository, IMapper mapper)
        {
            _foodRepository = foodRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetFood()
        {
            var foods = await _foodRepository.GetAllAsync();
            return Ok(foods.AsQueryable().ProjectTo<FoodDTO>(_mapper.ConfigurationProvider).ToList());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetFoodById(int id)
        {
            var food = await _foodRepository.GetByIdAsync(id);
            return food != null ? Ok(_mapper.Map<FoodDTO>(food)) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PostFood(Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _foodRepository.AddAsync(food);

            // the "food" object doesn't inculde any referenced properties
            var newFood = await _foodRepository.GetByIdAsync(food.Id);
            return CreatedAtAction(nameof(GetFoodById), new { id = food.Id }, _mapper.Map<FoodDTO>(newFood));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutFood(int id, Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id != food.Id)
            {
                return BadRequest();
            }

            try
            {
                await _foodRepository.UpdateAsync(food);
            }
            catch
            {
                return BadRequest();
            }
            // the "food" object doesn't inculde any referenced properties
            var updatedFood = await _foodRepository.GetByIdAsync(food.Id);
            return Ok(_mapper.Map<FoodDTO>(updatedFood));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteFood(int id)
        {
            if (!await _foodRepository.DeleteAsync(id))
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
