namespace KitchenDisplaySystem.DTO.StatsDTOs
{
    public class StatsDTO
    {
        public int AveragePrepareTimeMinutes { get; set; }
        public IEnumerable<OrdersByMonthDTO> MonthlyNumberOfOrders { get; set; }
        public IEnumerable<OrdersByWaiterDTO> NumberOfOrdersByWaiters { get; set; }
    }
}
