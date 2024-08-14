using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace KitchenDisplaySystem.Models
{
    public class Waiter
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Surname { get; set; }
        [Required]
        [StringLength(20)]
        public string DisplayName { get; set; }
        [Required]
        [StringLength(30)]
        public string Phone { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
