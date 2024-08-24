using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDatabaseApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUpdatedDirectorsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Directors",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Directors",
                newName: "ID");
        }
    }
}
