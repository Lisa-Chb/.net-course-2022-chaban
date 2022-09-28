
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WorkWithEntity.Migrations
{
    public partial class fixedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "currences");

            migrationBuilder.AddColumn<int>(
                name: "currency_code",
                table: "accounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "currencies",
                columns: table => new
                {
                    currency_code = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currencies", x => x.currency_code);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_currency_code",
                table: "accounts",
                column: "currency_code");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_currencies_currency_code",
                table: "accounts",
                column: "currency_code",
                principalTable: "currencies",
                principalColumn: "currency_code",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_currencies_currency_code",
                table: "accounts");

            migrationBuilder.DropTable(
                name: "currencies");

            migrationBuilder.DropIndex(
                name: "IX_accounts_currency_code",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "currency_code",
                table: "accounts");

            migrationBuilder.CreateTable(
                name: "currences",
                columns: table => new
                {
                    currency_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currences", x => x.currency_Id);
                    table.ForeignKey(
                        name: "FK_currences_accounts_account_Id",
                        column: x => x.account_Id,
                        principalTable: "accounts",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_currences_account_Id",
                table: "currences",
                column: "account_Id",
                unique: true);
        }
    }
}
