using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AMS.Migrations
{
    /// <inheritdoc />
    public partial class Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserMasters_CreatedByUserUserMasterId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CreatedByUserUserMasterId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedByUserUserMasterId",
                table: "Orders");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedById",
                table: "Orders",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserMasters_CreatedById",
                table: "Orders",
                column: "CreatedById",
                principalTable: "UserMasters",
                principalColumn: "UserMasterId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserMasters_CreatedById",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CreatedById",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserUserMasterId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedByUserUserMasterId",
                table: "Orders",
                column: "CreatedByUserUserMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserMasters_CreatedByUserUserMasterId",
                table: "Orders",
                column: "CreatedByUserUserMasterId",
                principalTable: "UserMasters",
                principalColumn: "UserMasterId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
