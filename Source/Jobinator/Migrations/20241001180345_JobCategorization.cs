using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jobinator.Migrations
{
    /// <inheritdoc />
    public partial class JobCategorization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Posts",
                type: "int",
                maxLength: 50,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Posts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
