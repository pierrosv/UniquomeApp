using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniquomeApp.EfCore.Migrations
{
    public partial class _001002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gene",
                table: "Protein",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "ProteinExistence",
                table: "Protein",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<int>(
                name: "ProteinStatus",
                table: "Protein",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SequenceLength",
                table: "Protein",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<short>(
                name: "SequenceVersion",
                table: "Protein",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gene",
                table: "Protein");

            migrationBuilder.DropColumn(
                name: "ProteinExistence",
                table: "Protein");

            migrationBuilder.DropColumn(
                name: "ProteinStatus",
                table: "Protein");

            migrationBuilder.DropColumn(
                name: "SequenceLength",
                table: "Protein");

            migrationBuilder.DropColumn(
                name: "SequenceVersion",
                table: "Protein");
        }
    }
}
