﻿using KitchenDisplaySystem.Context;
using KitchenDisplaySystem.DTO.StatsDTOs;
using KitchenDisplaySystem.Models;
using KitchenDisplaySystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace KitchenDisplaySystem.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<Order?> AddAsync(Order order)
        {
            // EF will also insert every OrderItem
            await _context.Orders.AddAsync(order);

            // SaveChangesAsync() will assign an ID to the new order object
            await _context.SaveChangesAsync();

            if (order.Id == 0)
            {
                return null;
            }

            return await GetByIdAsync(order.Id);
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Food)
                .Include(o => o.Waiter)
                .Include(o => o.Table)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllUnservedAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Food)
                .Include(o => o.Waiter)
                .Include(o => o.Table)
                .Where(o => !o.Served).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync(DateTime date)
        {
            return await _context.Orders
                .Where(o => o.Start.Date == date.Date)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Food)
                .Include(o => o.Waiter)
                .Include(o => o.Table)
                .OrderByDescending(o => o.Start).ToListAsync();
        }

        public async Task UpdateEndTimeAsync(int id, DateTime end)
        {
            await _context.Orders
                .Where(o => o.Id == id)
                .ExecuteUpdateAsync(x => x.SetProperty(o => o.End, end));
        }

        public async Task<bool> UpdateServedAsync(int id)
        {
            // order can't be served it's not prepared yet
            int rowsAffected = await _context.Orders
                .Where(o => o.Id == id)
                .Where(o => o.End != null)
                .ExecuteUpdateAsync(x => x.SetProperty(o => o.Served, true));

            return rowsAffected > 0;
        }

        public StatsDTO GetStatistics()
        {
            var averagePrepareTimeMinutes = _context.Orders
                .Where(o => o.End != null)
                .AsEnumerable()
                .Select(o => (o.End.Value - o.Start).TotalMinutes)
                .Average();

            var ordersToday = _context.Orders
                .Where(o => o.Start.Date == DateTime.Today.Date)
                .Count();

            var ordersBreakfast = _context.Orders
                .Where(o => o.Start.Date == DateTime.Today.Date)
                .Where(o => o.Start.Hour <= 12)
                .Count();

            var ordersLunch = _context.Orders
                .Where(o => o.Start.Date == DateTime.Today.Date)
                .Where(o => o.Start.Hour > 12 && o.Start.Hour <= 17)
                .Count();

            var ordersDinner = _context.Orders
                .Where(o => o.Start.Date == DateTime.Today.Date)
                .Where(o => o.Start.Hour > 17)
                .Count();

            return new StatsDTO()
            {
                AveragePrepareTimeMinutes = (int)Math.Round(averagePrepareTimeMinutes),
                OrdersToday = ordersToday,
                OrdersBreakfast = ordersBreakfast,
                OrdersLunch = ordersLunch,
                OrdersDinner = ordersDinner
            };
        }

        public async Task<IEnumerable<OrdersByMonthDTO>> GetOrdersByMonthAsync(int year)
        {
            var monthlyNumberOfOrders = await _context.Orders
                .Where(o => o.Start.Year == year)
                .GroupBy(o => o.Start.Month)
                .Select(group => new OrdersByMonthDTO()
                {
                    Month = new DateTime(1, group.Key, 1).ToString("MMM", new CultureInfo("sr-Latn-RS")),  // get month string
                    NumberOfOrders = group.Count()
                })
                .ToListAsync();

            return monthlyNumberOfOrders;
        }

        public async Task<IEnumerable<OrdersByWaiterDTO>> GetOrdersByWaiterAsync()
        {
            var numberOfOrdersByWaiters = await _context.Orders
                .Include(o => o.Waiter)
                .Where(o => o.Waiter.Active)
                .GroupBy(o => o.Waiter)
                .Select(group => new OrdersByWaiterDTO()
                {
                    WaiterDisplayName = group.Key.DisplayName,
                    NumberOfOrders = group.Count()
                })
                .OrderByDescending(o => o.NumberOfOrders).ToListAsync();

            return numberOfOrdersByWaiters;
        }
    }
}
