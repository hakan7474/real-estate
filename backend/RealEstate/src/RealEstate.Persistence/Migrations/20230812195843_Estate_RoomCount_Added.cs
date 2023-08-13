using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Estate_RoomCount_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoomCount",
                table: "Estate",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomCount",
                table: "Estate");
        }
    }
}
