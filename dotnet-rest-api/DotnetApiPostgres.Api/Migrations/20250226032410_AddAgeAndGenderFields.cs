using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotnetApiPostgres.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAgeAndGenderFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Person",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Person",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Person");
        }
    }
}
