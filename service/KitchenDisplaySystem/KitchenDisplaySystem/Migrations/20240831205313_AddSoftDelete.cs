using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenDisplaySystem.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Waiters",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Tables",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "FoodTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Food",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10335569-4b56-45f7-b029-c705d304bf52",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1864652-aab6-4ff5-b80d-b3c874c47180", "AQAAAAIAAYagAAAAENHcxJcbIgmJznDXW/MWld7/1ZUeFR6f6sxX/GR1xC2Cioiqq9nAfiZ9iqPKo41R6g==", "141c09d7-4c54-438d-a403-62fa3672d086" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5efd9e33-1d82-49ef-950d-6c34917f9a26",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "799930f1-8c03-415a-8091-905165e56347", "AQAAAAIAAYagAAAAECNJIfxaCwdrEspW1NtiRtPDFSLsf1sfOZHXZltZonuRu82f4pNJM03hkIFsRy0gpA==", "efdfbc0a-931a-4192-8fdf-de5e10aa7119" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7df3d20c-7e1b-4581-8546-f03510dda802",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e3b8f24-a140-4a7e-b615-436c7bbeca4f", "AQAAAAIAAYagAAAAEF1RVOd/RDulEJOuKMjPVNNZRGyvhWjmYKT9Gpy4kM3tRYvtTHSQ8SYFrOgDxksgKw==", "e3e57946-3d8d-48b1-a1f0-36bfb43f839c" });

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 2,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 3,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 4,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 5,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 6,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 7,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 8,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 9,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 10,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 11,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 12,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 13,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 14,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 15,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Food",
                keyColumn: "Id",
                keyValue: 16,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "FoodTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "FoodTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "FoodTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "FoodTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 2,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 3,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 4,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 5,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Waiters",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Waiters",
                keyColumn: "Id",
                keyValue: 2,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Waiters",
                keyColumn: "Id",
                keyValue: 3,
                column: "Deleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Waiters",
                keyColumn: "Id",
                keyValue: 4,
                column: "Deleted",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Waiters");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "FoodTypes");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Food");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10335569-4b56-45f7-b029-c705d304bf52",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a2f485d-41be-4558-88fe-d4b7493b8fee", "AQAAAAIAAYagAAAAEHTpiGS34eoe0z7BlFNPoc36qwgyVfu7Len+4cGwMeyOeO20UlXAJwQf72QSoc8lQA==", "764f0190-87be-4731-b38c-92b53ee2272f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5efd9e33-1d82-49ef-950d-6c34917f9a26",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d6c2337-62f3-471f-b192-73f3e9620f50", "AQAAAAIAAYagAAAAEEIE9bbtR+RE7X6nvA0qCpxYaGjyfBdb/LOqahoq75VnZSbn4MxIUTa5FgY6IYZlHw==", "c69a62ea-469d-4547-80f7-4c2609decc4e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7df3d20c-7e1b-4581-8546-f03510dda802",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ce3e42d-98dd-415c-9a4d-9034d694f0d3", "AQAAAAIAAYagAAAAEDpaWpDeJTVe0TqpRgKdN+WzEuCoAcz0rq5wPeJy+6Jzu3zmKOXlsDMTxzwdZCa3Mw==", "32d08d0b-f8a4-45d1-9fd8-9c885e8b5a74" });
        }
    }
}
