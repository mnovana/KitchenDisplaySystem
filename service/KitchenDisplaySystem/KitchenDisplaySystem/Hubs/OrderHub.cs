using AutoMapper;
using KitchenDisplaySystem.CustomValidation;
using KitchenDisplaySystem.DTO;
using KitchenDisplaySystem.Models;
using KitchenDisplaySystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace KitchenDisplaySystem.Hubs
{
    [Authorize]
    public class OrderHub : Hub
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderHub(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "Waiter")]
        public async Task OrderCreated(Order order)
        {
            // validation
            if (!Validator.TryValidateObject(order, new ValidationContext(order), null, true))
            {
                await Clients.Caller.SendAsync("ReceiveError", "Bad request");
                return;
            }

            try
            {
                await _orderRepository.AddAsync(order);
            }
            catch
            {
                await Clients.Caller.SendAsync("ReceiveError", "Bad request");
            }

            // the "order" object doesn't inculde any referenced properties
            var newOrder = await _orderRepository.GetByIdAsync(order.Id);
            await Clients.All.SendAsync("AddOrder", _mapper.Map<OrderDTO>(newOrder));
        }

        [Authorize(Roles = "Kitchen")]
        public async Task OrderPrepared(int id, DateTime end)
        {
            if (end < new DateTime(2024, 1, 1) || end > DateTime.Now)
            {
                await Clients.Caller.SendAsync("ReceiveError", "Invalid end date");
                return;
            }

            var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
            {
                await Clients.Caller.SendAsync("ReceiveError", "Not found");
                return;
            }

            if (end < order.Start)
            {
                await Clients.Caller.SendAsync("ReceiveError", "Invalid dates");
                return;
            }

            bool updateSuccess = await _orderRepository.UpdateEndTimeAsync(id, end);
            if (!updateSuccess)
            {
                await Clients.Caller.SendAsync("ReceiveError", "Bad request");
                return;
            }

            await Clients.All.SendAsync("ReadyOrder", id, end);
        }

        [Authorize(Roles = "Waiter")]
        public async Task OrderServed(int id)
        {
            if (!await _orderRepository.UpdateServedAsync(id))
            {
                await Clients.Caller.SendAsync("ReceiveError", "Bad request");
                return;
            }

            await Clients.All.SendAsync("ServeOrder", id);
        }
    }
}
