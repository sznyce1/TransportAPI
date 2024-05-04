using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportAPI.Migrations
{
    /// <inheritdoc />
    public partial class NullForForeginKeysAndRunList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Runs_Cars_CarId",
                table: "Runs");

            migrationBuilder.DropForeignKey(
                name: "FK_Runs_Drivers_DriverId",
                table: "Runs");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Runs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Runs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Runs_Cars_CarId",
                table: "Runs",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Runs_Drivers_DriverId",
                table: "Runs",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Runs_Cars_CarId",
                table: "Runs");

            migrationBuilder.DropForeignKey(
                name: "FK_Runs_Drivers_DriverId",
                table: "Runs");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Runs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Runs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Runs_Cars_CarId",
                table: "Runs",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Runs_Drivers_DriverId",
                table: "Runs",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
