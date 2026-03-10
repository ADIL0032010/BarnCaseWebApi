using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarnCaseWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAnimalAndUserModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Barns_BarnId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "DeathDate",
                table: "Animals");

            migrationBuilder.RenameColumn(
                name: "BarnId",
                table: "Animals",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_BarnId",
                table: "Animals",
                newName: "IX_Animals_UserId");

            migrationBuilder.AddColumn<int>(
                name: "LifespanInSeconds",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Animals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "Animals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Users_UserId",
                table: "Animals",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Users_UserId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "LifespanInSeconds",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "Animals");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Animals",
                newName: "BarnId");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_UserId",
                table: "Animals",
                newName: "IX_Animals_BarnId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeathDate",
                table: "Animals",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Barns_BarnId",
                table: "Animals",
                column: "BarnId",
                principalTable: "Barns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
