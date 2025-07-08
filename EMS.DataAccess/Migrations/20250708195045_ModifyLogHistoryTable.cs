using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ModifyLogHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyValues",
                table: "LogHistories");

            migrationBuilder.DropColumn(
                name: "NewValues",
                table: "LogHistories");

            migrationBuilder.DropColumn(
                name: "OldValues",
                table: "LogHistories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "KeyValues",
                table: "LogHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NewValues",
                table: "LogHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldValues",
                table: "LogHistories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
