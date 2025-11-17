using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AirBB.Migrations
{
    /// <inheritdoc />
    public partial class CompleteAdminCRUDWithValidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "LocationId", "Name" },
                values: new object[,]
                {
                    { 6, "Seattle" },
                    { 7, "Austin" }
                });

            migrationBuilder.InsertData(
                table: "Residences",
                columns: new[] { "ResidenceId", "BathroomNumber", "BedroomNumber", "BuiltYear", "GuestNumber", "LocationId", "Name", "OwnerId", "PricePerNight", "ResidencePicture" },
                values: new object[] { 5, 1.5m, 1, 2020, 3, 5, "LA Modern Apartment", 2, 180.00m, "la-apartment.jpg" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "john.owner@example.com", "John PropertyOwner" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "Email", "Name" },
                values: new object[] { "sarah.owner@example.com", "Sarah EstateManager" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "Email",
                value: "admin@airbb.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "Name",
                value: "Regular Client");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "DOB", "Email", "Name", "PhoneNumber", "SSN", "UserType" },
                values: new object[] { 5, new DateTime(1988, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "mike.owner@example.com", "Mike HouseOwner", null, "567-89-0123", 1 });

            migrationBuilder.InsertData(
                table: "Residences",
                columns: new[] { "ResidenceId", "BathroomNumber", "BedroomNumber", "BuiltYear", "GuestNumber", "LocationId", "Name", "OwnerId", "PricePerNight", "ResidencePicture" },
                values: new object[] { 4, 3m, 4, 1995, 8, 4, "Miami Beach House", 5, 300.00m, "miami-beach.jpg" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Locations",
                keyColumn: "LocationId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Residences",
                keyColumn: "ResidenceId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Residences",
                keyColumn: "ResidenceId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "john@example.com", "John Doe" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "Email", "Name" },
                values: new object[] { "jane@example.com", "Jane Smith" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "Email",
                value: "admin@example.com");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "Name",
                value: "Client User");
        }
    }
}
