using System.ComponentModel.DataAnnotations;

namespace KitchenDisplaySystem.Models
{
    public class Table
    {
        public int Id { get; set; }
        [Required]
        public int Number { get; set; }
    }
}
