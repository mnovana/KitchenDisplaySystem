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
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderRepository.GetAllAsync();
            var ordersDTO = orders.AsQueryable().ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();

            return Ok(ordersDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("statistics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetOrdersStatistics()
        {
            var stats = await _orderRepository.GetStatisticsAsync();

            return Ok(stats);
        }
    }
}
