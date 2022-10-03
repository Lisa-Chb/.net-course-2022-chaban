using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkWithEntity.Migrations
{
    public partial class fixModelsDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Clients_Clientid",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "clients");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "accounts");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "clients",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "SeriesOfPassport",
                table: "clients",
                newName: "series_of_passport");

            migrationBuilder.RenameColumn(
                name: "NumberOfPassport",
                table: "clients",
                newName: "number_of_passport");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "clients",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "clients",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "DateOfBirth",
                table: "clients",
                newName: "date_of_birth");

            migrationBuilder.RenameColumn(
                name: "BonusDiscount",
                table: "clients",
                newName: "bonus_discount");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "clients",
                newName: "client_Id");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "accounts",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "Clientid",
                table: "accounts",
                newName: "client_id");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "accounts",
                newName: "account_id");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_Clientid",
                table: "accounts",
                newName: "IX_accounts_client_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clients",
                table: "clients",
                column: "client_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_accounts",
                table: "accounts",
                column: "account_id");

            migrationBuilder.CreateTable(
                name: "currences",
                columns: table => new
                {
                    currency_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    account_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    employee_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    number_of_passport = table.Column<int>(type: "integer", nullable: true),
                    series_of_passport = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    bonus_discount = table.Column<int>(type: "integer", nullable: false),
                    contract = table.Column<string>(type: "text", nullable: false),
                    salary = table.Column<int>(type: "integer", nullable: false),
                    position = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.employee_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_currences_account_Id",
                table: "currences",
                column: "account_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_clients_client_id",
                table: "accounts",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "client_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_clients_client_id",
                table: "accounts");

            migrationBuilder.DropTable(
                name: "currences");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clients",
                table: "clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_accounts",
                table: "accounts");

            migrationBuilder.RenameTable(
                name: "clients",
                newName: "Clients");

            migrationBuilder.RenameTable(
                name: "accounts",
                newName: "Accounts");

            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Clients",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "series_of_passport",
                table: "Clients",
                newName: "SeriesOfPassport");

            migrationBuilder.RenameColumn(
                name: "number_of_passport",
                table: "Clients",
                newName: "NumberOfPassport");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Clients",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Clients",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "date_of_birth",
                table: "Clients",
                newName: "DateOfBirth");

            migrationBuilder.RenameColumn(
                name: "bonus_discount",
                table: "Clients",
                newName: "BonusDiscount");

            migrationBuilder.RenameColumn(
                name: "client_Id",
                table: "Clients",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "Accounts",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "client_id",
                table: "Accounts",
                newName: "Clientid");

            migrationBuilder.RenameColumn(
                name: "account_id",
                table: "Accounts",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_accounts_client_id",
                table: "Accounts",
                newName: "IX_Accounts_Clientid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "AccountId");

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    CurrencyId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.CurrencyId);
                    table.ForeignKey(
                        name: "FK_Currency_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    BonusDiscount = table.Column<int>(type: "integer", nullable: false),
                    Contract = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    NumberOfPassport = table.Column<int>(type: "integer", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: false),
                    Salary = table.Column<int>(type: "integer", nullable: false),
                    SeriesOfPassport = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Currency_AccountId",
                table: "Currency",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Clients_Clientid",
                table: "Accounts",
                column: "Clientid",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
