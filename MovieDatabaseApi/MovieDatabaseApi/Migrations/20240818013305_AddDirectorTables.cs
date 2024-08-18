using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDatabaseApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDirectorTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_directors",
                table: "directors");

            migrationBuilder.RenameTable(
                name: "directors",
                newName: "Directors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Directors",
                table: "Directors",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Directors",
                table: "Directors");

            migrationBuilder.RenameTable(
                name: "Directors",
                newName: "directors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_directors",
                table: "directors",
                column: "ID");
        }
    }
}
