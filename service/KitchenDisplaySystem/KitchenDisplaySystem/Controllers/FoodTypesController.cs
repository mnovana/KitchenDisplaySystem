using KitchenDisplaySystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitchenDisplaySystem.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> GetFoodTypes()
        {
            return Ok(await _foodTypeRepository.GetAllAsync());
        }
    }
}
