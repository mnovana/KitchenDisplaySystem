using KitchenDisplaySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KitchenDisplaySystem.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key for a many-to-many table between order and food
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.FoodId });

            // Unique constraint for a displayed waiter name
            modelBuilder.Entity<Waiter>()
                .HasIndex(w => w.DisplayName)
                .IsUnique();

            base.OnModelCreating(modelBuilder);

            // Seed
            SeedData(modelBuilder);
            SeedUsers(modelBuilder);
            SeedRoles(modelBuilder);
            SeedUserRoles(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FoodType>().HasData(
                new FoodType { Id = 1, Name = "Predjelo" },
                new FoodType { Id = 2, Name = "Glavno jelo" },
                new FoodType { Id = 3, Name = "Salata" },
                new FoodType { Id = 4, Name = "Desert" }
            );

            modelBuilder.Entity<Food>().HasData(
                new Food { Id = 1, Name = "Brusketi sa paradajzom i bosiljkom", FoodTypeId = 1 },
                new Food { Id = 2, Name = "Brusketi sa dimljenim lososom i rukolom", FoodTypeId = 1 },
                new Food { Id = 3, Name = "Tortilja sa kremastom dimljenom butkicom", FoodTypeId = 1 },
                new Food { Id = 4, Name = "Focaccia sa grilovanim sirom, čeri paradajzom i pršutom", FoodTypeId = 1 },
                new Food { Id = 5, Name = "Ćuretina sa mlincima", FoodTypeId = 2 },
                new Food { Id = 6, Name = "Bečke kobasice sa sirom", FoodTypeId = 2 },
                new Food { Id = 7, Name = "Tradicionalni ćevapi na kajmaku (350g)", FoodTypeId = 2 },
                new Food { Id = 8, Name = "Pikantni uštipci", FoodTypeId = 2 },
                new Food { Id = 9, Name = "Piletina terijaki", FoodTypeId = 2 },
                new Food { Id = 10, Name = "Miks zelenih salata", FoodTypeId = 3 },
                new Food { Id = 11, Name = "Šopska salata", FoodTypeId = 3 },
                new Food { Id = 12, Name = "Srpska salata", FoodTypeId = 3 },
                new Food { Id = 13, Name = "Vitaminska salata", FoodTypeId = 3 },
                new Food { Id = 14, Name = "Palačinke sa medom i orasima", FoodTypeId = 4 },
                new Food { Id = 15, Name = "Pita sa borovnicom", FoodTypeId = 4 },
                new Food { Id = 16, Name = "Šnenokle", FoodTypeId = 4 }
            );

            modelBuilder.Entity<Table>().HasData(
                new Table { Id = 1, Number = 10 },
                new Table { Id = 2, Number = 11 },
                new Table { Id = 3, Number = 12 },
                new Table { Id = 4, Number = 13 },
                new Table { Id = 5, Number = 14 }
            );

            modelBuilder.Entity<Waiter>().HasData(
                new Waiter { Id = 1, Name = "Marko", Surname = "Marković", DisplayName = "Marko M.", Phone = "0618521114" },
                new Waiter { Id = 2, Name = "Marko", Surname = "Jovanović", DisplayName = "Marko J.", Phone = "0612336852" },
                new Waiter { Id = 3, Name = "Jovana", Surname = "Jovanović", DisplayName = "Jovana", Phone = "0632448752" },
                new Waiter { Id = 4, Name = "Petar", Surname = "Petrović", DisplayName = "Petar", Phone = "0603352291" }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, Start = new DateTime(2024,6,30, 15, 30, 0), End = new DateTime(2024, 6, 30, 15, 44, 0), Served = false, Note = "Salata bez ulja!", TableId = 1, WaiterId = 1 }
            );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { OrderId = 1, FoodId = 1, Quantity = 1 },
                new OrderItem { OrderId = 1, FoodId = 8, Quantity = 2 },
                new OrderItem { OrderId = 1, FoodId = 13, Quantity = 1 },
                new OrderItem { OrderId = 1, FoodId = 15, Quantity = 3 }
            );
        }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            AppUser kitchenUser = new AppUser()
            {
                Id = "10335569-4b56-45f7-b029-c705d304bf52",
                UserName = "kitchen",
                NormalizedUserName = "KITCHEN",
            };

            AppUser waiterUser = new AppUser()
            {
                Id = "7df3d20c-7e1b-4581-8546-f03510dda802",
                UserName = "waiter",
                NormalizedUserName = "WAITER",
            };

            AppUser adminUser = new AppUser()
            {
                Id = "5efd9e33-1d82-49ef-950d-6c34917f9a26",
                UserName = "admin",
                NormalizedUserName = "ADMIN"
            };

            PasswordHasher<AppUser> passwordHasher = new PasswordHasher<AppUser>();
            kitchenUser.PasswordHash = passwordHasher.HashPassword(kitchenUser, "Kuhinja-0");
            waiterUser.PasswordHash = passwordHasher.HashPassword(waiterUser, "Konobar-0");
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Administrator-0");

            modelBuilder.Entity<AppUser>().HasData(kitchenUser, waiterUser, adminUser);
        }

        private void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "a4cb9cf6-e1fe-4913-969d-f622cbd2bd84", Name = "Kitchen", NormalizedName = "KITCHEN", ConcurrencyStamp = "1" },
                new IdentityRole { Id = "061d252f-f801-443d-9506-900d13f090fd", Name = "Waiter", NormalizedName = "WAITER", ConcurrencyStamp = "2"},
                new IdentityRole { Id = "2b1ad17d-c6f5-4de9-b637-c91488504334", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "3" }
            );
        }

        private void SeedUserRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = "10335569-4b56-45f7-b029-c705d304bf52", RoleId = "a4cb9cf6-e1fe-4913-969d-f622cbd2bd84" },
                new IdentityUserRole<string> { UserId = "7df3d20c-7e1b-4581-8546-f03510dda802", RoleId = "061d252f-f801-443d-9506-900d13f090fd" },
                new IdentityUserRole<string> { UserId = "5efd9e33-1d82-49ef-950d-6c34917f9a26", RoleId = "2b1ad17d-c6f5-4de9-b637-c91488504334" }
            );
        }
    }
}
