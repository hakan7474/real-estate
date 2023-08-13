using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Estate_RoomCount_Delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomCount",
                table: "Estate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomCount",
                table: "Estate",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
