using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddBookingGroupsSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingGroupId",
                table: "RoomBookings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookingGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingGroups_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomBookings_BookingGroupId",
                table: "RoomBookings",
                column: "BookingGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingGroups_CustomerId",
                table: "BookingGroups",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomBookings_BookingGroups_BookingGroupId",
                table: "RoomBookings",
                column: "BookingGroupId",
                principalTable: "BookingGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomBookings_BookingGroups_BookingGroupId",
                table: "RoomBookings");

            migrationBuilder.DropTable(
                name: "BookingGroups");

            migrationBuilder.DropIndex(
                name: "IX_RoomBookings_BookingGroupId",
                table: "RoomBookings");

            migrationBuilder.DropColumn(
                name: "BookingGroupId",
                table: "RoomBookings");
        }
    }
}
