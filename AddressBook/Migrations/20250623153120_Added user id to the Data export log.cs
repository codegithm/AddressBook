using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AddressBook.Migrations
{
    /// <inheritdoc />
    public partial class AddeduseridtotheDataexportlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DataExportLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DataExportLogs");
        }
    }
}
