using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TypeDetail_OrderIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderIndex",
                table: "TypeDetail",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderIndex",
                table: "TypeDetail");
        }
    }
}
