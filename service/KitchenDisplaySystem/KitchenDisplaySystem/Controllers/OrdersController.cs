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
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<OrderDTO>(order));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrders(DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (date.Year < 2023 || date.Date > DateTime.Today.Date)
            {
                return BadRequest();
            }

            var orders = await _orderRepository.GetAllAsync(date);
            var ordersDTO = orders.AsQueryable().ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();

            return Ok(ordersDTO);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _orderRepository.AddAsync(order);
            }
            catch
            {
                return BadRequest();
            }

            // the "order" object doesn't inculde any referenced properties
            var newOrder = await _orderRepository.GetByIdAsync(order.Id);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, _mapper.Map<OrderDTO>(newOrder));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            try
            {
                await _orderRepository.UpdateAsync(order);
            }
            catch
            {
                return BadRequest();
            }

            // the "order" object doesn't inculde any referenced properties
            var updatedOrder = await _orderRepository.GetByIdAsync(order.Id);
            return Ok(_mapper.Map<OrderDTO>(updatedOrder));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                return BadRequest();
            }

            await _orderRepository.DeleteAsync(order);
            
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("today")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetNumberOfOrdersToday()
        {
            var stats = await _orderRepository.GetOrdersToday();

            return Ok(stats);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("time")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAveragePrepareTime()
        {
            var stats = await _orderRepository.GetAveragePrepareTime();

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
