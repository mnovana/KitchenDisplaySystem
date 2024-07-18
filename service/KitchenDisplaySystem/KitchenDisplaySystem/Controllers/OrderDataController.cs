using AutoMapper;
using AutoMapper.QueryableExtensions;
using KitchenDisplaySystem.DTO;
using KitchenDisplaySystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KitchenDisplaySystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderDataController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IFoodTypeRepository _foodTypeRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IWaiterRepository _waiterRepository;
        private readonly IMapper _mapper;

        public OrderDataController(IFoodRepository foodRepository, IFoodTypeRepository foodTypeRepository, ITableRepository tableRepository, IWaiterRepository waiterRepository, IMapper mapper)
        {
            _foodRepository = foodRepository;
            _foodTypeRepository = foodTypeRepository;
            _tableRepository = tableRepository;
            _waiterRepository = waiterRepository;
            _mapper = mapper;
        }

        // get all the data needed to create a new order
        [Authorize(Roles = "Waiter")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetOrderData()
        {
            var food = await _foodRepository.GetAllAsync();
            var foodTypes = await _foodTypeRepository.GetAllAsync();
            var tables = await _tableRepository.GetAllAsync();
            var waiters = await _waiterRepository.GetAllAsync();

            OrderDataDTO orderData = new OrderDataDTO()
            {
                Food = food.AsQueryable().ProjectTo<FoodDTO>(_mapper.ConfigurationProvider),
                FoodTypes = foodTypes,
                Tables = tables,
                Waiters = waiters.AsQueryable().ProjectTo<WaiterDTO>(_mapper.ConfigurationProvider)
            };

            return Ok(orderData);
        }
    }
}
