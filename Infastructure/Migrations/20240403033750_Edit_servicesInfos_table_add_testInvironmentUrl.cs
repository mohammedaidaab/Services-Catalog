using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Edit_servicesInfos_table_add_testInvironmentUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL",
                table: "servicesInfos");

            migrationBuilder.AddColumn<string>(
                name: "Production_URL",
                table: "servicesInfos",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Test_URL",
                table: "servicesInfos",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Production_URL",
                table: "servicesInfos");

            migrationBuilder.DropColumn(
                name: "Test_URL",
                table: "servicesInfos");

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "servicesInfos",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");
        }
    }
}
