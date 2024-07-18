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

        [Authorize(Roles = "Waiter,Kitchen")]
        [HttpGet("unserved")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUnservedOrders()
        {
            var orders = await _orderRepository.GetAllUnservedAsync();
            var ordersDTO = orders.AsQueryable().ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();

            return Ok(ordersDTO);
        }
    }
}
