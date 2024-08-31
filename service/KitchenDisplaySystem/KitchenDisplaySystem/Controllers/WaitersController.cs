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
        public async Task<IActionResult> GetWaiter(int id)
        {
            var waiter = await _waiterRepository.GetByIdAsync(id);
            return waiter != null ? Ok(_mapper.Map<WaiterDTO>(waiter)) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PostTable(Waiter waiter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _waiterRepository.AddAsync(waiter);

            return CreatedAtAction(nameof(GetWaiter), new { id = waiter.Id }, waiter);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutWaiter(int id, Waiter waiter)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id != waiter.Id)
            {
                return BadRequest();
            }

            try
            {
                await _waiterRepository.UpdateAsync(waiter);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(waiter);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteWaiter(int id)
        {
            if (!await _waiterRepository.DeleteAsync(id))
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
