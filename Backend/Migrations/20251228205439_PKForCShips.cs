using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormulaOne.Migrations
{
    /// <inheritdoc />
    public partial class PKForCShips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverChampionship",
                table: "DriverChampionship");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConstructorsChampionship",
                table: "ConstructorsChampionship");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "DriverChampionship",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ConstructorsChampionship",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverChampionship",
                table: "DriverChampionship",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConstructorsChampionship",
                table: "ConstructorsChampionship",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DriverChampionship_DriverId",
                table: "DriverChampionship",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_ConstructorsChampionship_TeamId",
                table: "ConstructorsChampionship",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverChampionship",
                table: "DriverChampionship");

            migrationBuilder.DropIndex(
                name: "IX_DriverChampionship_DriverId",
                table: "DriverChampionship");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConstructorsChampionship",
                table: "ConstructorsChampionship");

            migrationBuilder.DropIndex(
                name: "IX_ConstructorsChampionship_TeamId",
                table: "ConstructorsChampionship");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DriverChampionship");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ConstructorsChampionship");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverChampionship",
                table: "DriverChampionship",
                columns: new[] { "DriverId", "SeasonId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConstructorsChampionship",
                table: "ConstructorsChampionship",
                columns: new[] { "TeamId", "SeasonId" });
        }
    }
}
