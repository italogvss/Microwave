using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class MicrowaveConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isDefault",
                table: "ProgramConfigs",
                newName: "IsDefault");

            migrationBuilder.CreateTable(
                name: "MicrowaveConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProgramId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicrowaveConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MicrowaveConfigs_ProgramConfigs_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "ProgramConfigs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MicrowaveConfigs_ProgramId",
                table: "MicrowaveConfigs",
                column: "ProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MicrowaveConfigs");

            migrationBuilder.RenameColumn(
                name: "IsDefault",
                table: "ProgramConfigs",
                newName: "isDefault");
        }
    }
}
