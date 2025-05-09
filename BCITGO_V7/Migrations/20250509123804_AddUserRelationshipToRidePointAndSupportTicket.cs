using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BCITGO_V6.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRelationshipToRidePointAndSupportTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvailableSeats",
                table: "RIDES",
                newName: "TotalSeats");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "USERS",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "PetsAllowed",
                table: "RIDES",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "LuggageAllowed",
                table: "RIDES",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DriverName",
                table: "RIDES",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_USERS_UserId",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SUPPORT_TICKETS_UserId",
                table: "SUPPORT_TICKETS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RIDES_UserId",
                table: "RIDES",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RIDEPOINTS_UserId",
                table: "RIDEPOINTS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEWS_RevieweeId",
                table: "REVIEWS",
                column: "RevieweeId");

            migrationBuilder.CreateIndex(
                name: "IX_REVIEWS_ReviewerId",
                table: "REVIEWS",
                column: "ReviewerId");

            migrationBuilder.CreateIndex(
                name: "IX_BOOKINGS_UserId",
                table: "BOOKINGS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BOOKINGS_USERS_UserId",
                table: "BOOKINGS",
                column: "UserId",
                principalTable: "USERS",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_REVIEWS_USERS_RevieweeId",
                table: "REVIEWS",
                column: "RevieweeId",
                principalTable: "USERS",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_REVIEWS_USERS_ReviewerId",
                table: "REVIEWS",
                column: "ReviewerId",
                principalTable: "USERS",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RIDEPOINTS_USERS_UserId",
                table: "RIDEPOINTS",
                column: "UserId",
                principalTable: "USERS",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RIDES_USERS_UserId",
                table: "RIDES",
                column: "UserId",
                principalTable: "USERS",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SUPPORT_TICKETS_USERS_UserId",
                table: "SUPPORT_TICKETS",
                column: "UserId",
                principalTable: "USERS",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BOOKINGS_USERS_UserId",
                table: "BOOKINGS");

            migrationBuilder.DropForeignKey(
                name: "FK_REVIEWS_USERS_RevieweeId",
                table: "REVIEWS");

            migrationBuilder.DropForeignKey(
                name: "FK_REVIEWS_USERS_ReviewerId",
                table: "REVIEWS");

            migrationBuilder.DropForeignKey(
                name: "FK_RIDEPOINTS_USERS_UserId",
                table: "RIDEPOINTS");

            migrationBuilder.DropForeignKey(
                name: "FK_RIDES_USERS_UserId",
                table: "RIDES");

            migrationBuilder.DropForeignKey(
                name: "FK_SUPPORT_TICKETS_USERS_UserId",
                table: "SUPPORT_TICKETS");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_SUPPORT_TICKETS_UserId",
                table: "SUPPORT_TICKETS");

            migrationBuilder.DropIndex(
                name: "IX_RIDES_UserId",
                table: "RIDES");

            migrationBuilder.DropIndex(
                name: "IX_RIDEPOINTS_UserId",
                table: "RIDEPOINTS");

            migrationBuilder.DropIndex(
                name: "IX_REVIEWS_RevieweeId",
                table: "REVIEWS");

            migrationBuilder.DropIndex(
                name: "IX_REVIEWS_ReviewerId",
                table: "REVIEWS");

            migrationBuilder.DropIndex(
                name: "IX_BOOKINGS_UserId",
                table: "BOOKINGS");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "USERS");

            migrationBuilder.DropColumn(
                name: "DriverName",
                table: "RIDES");

            migrationBuilder.RenameColumn(
                name: "TotalSeats",
                table: "RIDES",
                newName: "AvailableSeats");

            migrationBuilder.AlterColumn<bool>(
                name: "PetsAllowed",
                table: "RIDES",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<bool>(
                name: "LuggageAllowed",
                table: "RIDES",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");
        }
    }
}
