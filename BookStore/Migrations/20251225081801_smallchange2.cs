using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class smallchange2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionId",
                table: "orderDetails",
                newName: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_orderDetails_ProductId",
                table: "orderDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_product_ProductId",
                table: "orderDetails",
                column: "ProductId",
                principalTable: "product",
                principalColumn: "productID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_product_ProductId",
                table: "orderDetails");

            migrationBuilder.DropIndex(
                name: "IX_orderDetails_ProductId",
                table: "orderDetails");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "orderDetails",
                newName: "ProductionId");
        }
    }
}
