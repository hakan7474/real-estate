using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateSequence(
                name: "Estate_SysCode_seq",
                minValue: 1L,
                maxValue: 9223372036854775807L);

            migrationBuilder.CreateSequence(
                name: "TypeDetail_SysCode_seq",
                minValue: 1L,
                maxValue: 9223372036854775807L);

            migrationBuilder.CreateSequence(
                name: "Types_SysCode_seq",
                minValue: 1L,
                maxValue: 9223372036854775807L);

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    SysCode = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'TYP-'::text || nextval('\"Types_SysCode_seq\"')"),
                    TypeCode = table.Column<string>(type: "text", nullable: true),
                    TypeName = table.Column<string>(type: "text", nullable: true),
                    TypeDescription = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    TypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemCode = table.Column<string>(type: "text", nullable: true),
                    ItemName = table.Column<string>(type: "text", nullable: true),
                    ItemDescription = table.Column<string>(type: "text", nullable: true),
                    SysCode = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'TYP-'::text || nextval('\"TypeDetail_SysCode_seq\"')"),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TypeDetail_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    SysCode = table.Column<string>(type: "text", nullable: true, defaultValueSql: "'EST-'::text || nextval('\"Estate_SysCode_seq\"')"),
                    EstateCode = table.Column<string>(type: "text", nullable: true),
                    EstateName = table.Column<string>(type: "text", nullable: true),
                    FloorNumber = table.Column<int>(type: "integer", nullable: false),
                    BuildingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: true),
                    RoomCount = table.Column<int>(type: "integer", nullable: false),
                    GrossArea = table.Column<decimal>(type: "numeric", nullable: true),
                    NetArea = table.Column<decimal>(type: "numeric", nullable: true),
                    EstateTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Contact_Country = table.Column<string>(type: "text", nullable: true),
                    Contact_City = table.Column<string>(type: "text", nullable: true),
                    Contact_State = table.Column<string>(type: "text", nullable: true),
                    Contact_District = table.Column<string>(type: "text", nullable: true),
                    Contact_ZipCode = table.Column<string>(type: "text", nullable: true),
                    Contact_Address = table.Column<string>(type: "text", nullable: true),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estate_TypeDetail_EstateTypeId",
                        column: x => x.EstateTypeId,
                        principalTable: "TypeDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estate_EstateTypeId",
                table: "Estate",
                column: "EstateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeDetail_TypeId",
                table: "TypeDetail",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estate");

            migrationBuilder.DropTable(
                name: "TypeDetail");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropSequence(
                name: "Estate_SysCode_seq");

            migrationBuilder.DropSequence(
                name: "TypeDetail_SysCode_seq");

            migrationBuilder.DropSequence(
                name: "Types_SysCode_seq");
        }
    }
}
