﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiasGarden.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedCountToShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "ShoppingCarts");
        }
    }
}
