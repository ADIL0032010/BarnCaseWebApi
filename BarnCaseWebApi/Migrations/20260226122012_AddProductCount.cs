using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarnCaseWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddProductCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductCount",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCount",
                table: "Animals");
        }
    }
}
