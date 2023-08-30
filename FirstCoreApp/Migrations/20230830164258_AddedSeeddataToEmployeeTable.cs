using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstCoreApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeeddataToEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Age", "Email", "Mobile", "Name" },
                values: new object[] { 1, 25, "Tom@gmail.com", null, "Tom" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Employees");
        }
    }
}
