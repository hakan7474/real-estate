using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Estate_Contact_Location_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contact_ZipCode",
                table: "Estate",
                newName: "Contact_Location");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contact_Location",
                table: "Estate",
                newName: "Contact_ZipCode");
        }
    }
}
