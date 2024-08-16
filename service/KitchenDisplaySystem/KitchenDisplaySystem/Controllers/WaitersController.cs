using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class WaitersController : ControllerBase
    {
        private readonly IWaiterRepository _waiterRepository;
        private readonly IMapper _mapper;

        public WaitersController(IWaiterRepository waiterRepository, IMapper mapper)
        {
            _waiterRepository = waiterRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetWaiters()
        {
            var waiters = await _waiterRepository.GetAllAsync();
            return Ok(waiters.AsQueryable().ProjectTo<WaiterDTO>(_mapper.ConfigurationProvider).ToList());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetWaiterById(int id)
        {
            var waiter = await _waiterRepository.GetByIdAsync(id);
            return waiter != null ? Ok(_mapper.Map<WaiterDTO>(waiter)) : NotFound();
        }
    }
}
