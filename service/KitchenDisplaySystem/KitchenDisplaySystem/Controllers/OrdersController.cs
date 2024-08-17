using AutoMapper;
using AutoMapper.QueryableExtensions;
using KitchenDisplaySystem.DTO;
using KitchenDisplaySystem.Models;
using KitchenDisplaySystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KitchenDisplaySystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "Waiter,Kitchen,Admin")]
        [HttpGet("unserved")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUnservedOrders()
        {
            var orders = await _orderRepository.GetAllUnservedAsync();
            var ordersDTO = orders.AsQueryable().ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();

            return Ok(ordersDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrders(DateTime date)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            if(date.Year < 2023 || date.Date > DateTime.Today.Date)
            {
                return BadRequest();
            }

            var orders = await _orderRepository.GetAllAsync(date);
            var ordersDTO = orders.AsQueryable().ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();

            return Ok(ordersDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("statistics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult GetOrdersStatistics()
        {
            var stats = _orderRepository.GetStatistics();

            return Ok(stats);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("monthly")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrdersByMonth(int year)
        {
            if(year < 2023 || year > DateTime.Today.Year)
            {
                return BadRequest();
            }
            
            var stats = await _orderRepository.GetOrdersByMonthAsync(year);

            return Ok(stats);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("waiters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetOrdersByWaiter()
        {
            var stats = await _orderRepository.GetOrdersByWaiterAsync();

            return Ok(stats);
        }

    }
}
