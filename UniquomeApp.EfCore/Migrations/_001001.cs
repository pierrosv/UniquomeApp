using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UniquomeApp.EfCore.Migrations
{
    public partial class _001001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Institution = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Position = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Country = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsletterRegistration",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AcceptedDate = table.Column<Instant>(type: "timestamp with time zone", nullable: false),
                    RemovedDate = table.Column<Instant>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsletterRegistration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organism",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organism", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proteome",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Version = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proteome", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uniquome",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Version = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreationDate = table.Column<Instant>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uniquome", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Protein",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InProteomeId = table.Column<long>(type: "bigint", nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Sequence = table.Column<string>(type: "character varying(100000)", maxLength: 100000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protein", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Protein_Proteome_InProteomeId",
                        column: x => x.InProteomeId,
                        principalTable: "Proteome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UniquomeProtein",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ForUniquomeId = table.Column<long>(type: "bigint", nullable: false),
                    ForProteinId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniquomeProtein", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UniquomeProtein_Protein_ForProteinId",
                        column: x => x.ForProteinId,
                        principalTable: "Protein",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UniquomeProtein_Uniquome_ForUniquomeId",
                        column: x => x.ForUniquomeId,
                        principalTable: "Uniquome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Peptide",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InUniquomeProteinId = table.Column<long>(type: "bigint", nullable: false),
                    Sequence = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FirstLocation = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peptide", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Peptide_UniquomeProtein_InUniquomeProteinId",
                        column: x => x.InUniquomeProteinId,
                        principalTable: "UniquomeProtein",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Peptide_InUniquomeProteinId",
                table: "Peptide",
                column: "InUniquomeProteinId");

            migrationBuilder.CreateIndex(
                name: "IX_Protein_InProteomeId",
                table: "Protein",
                column: "InProteomeId");

            migrationBuilder.CreateIndex(
                name: "IX_UniquomeProtein_ForProteinId",
                table: "UniquomeProtein",
                column: "ForProteinId");

            migrationBuilder.CreateIndex(
                name: "IX_UniquomeProtein_ForUniquomeId",
                table: "UniquomeProtein",
                column: "ForUniquomeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "NewsletterRegistration");

            migrationBuilder.DropTable(
                name: "Organism");

            migrationBuilder.DropTable(
                name: "Peptide");

            migrationBuilder.DropTable(
                name: "UniquomeProtein");

            migrationBuilder.DropTable(
                name: "Protein");

            migrationBuilder.DropTable(
                name: "Uniquome");

            migrationBuilder.DropTable(
                name: "Proteome");
        }
    }
}
