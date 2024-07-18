using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        public async Task<IActionResult> GetWaiters()
        {
            var waiters = await _waiterRepository.GetAllAsync();
            return Ok(waiters.AsQueryable().ProjectTo<WaiterDTO>(_mapper.ConfigurationProvider).ToList());
        }
    }
}
