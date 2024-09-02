using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jimx.MMT.API.Migrations
{
    /// <inheritdoc />
    public partial class UserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_SharedAccount_SharedAccountId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedAccountToUser_SharedAccount_SharedAccountId",
                table: "SharedAccountToUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedAccount",
                table: "SharedAccount");

            migrationBuilder.RenameTable(
                name: "SharedAccount",
                newName: "SharedAccounts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedAccounts",
                table: "SharedAccounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedAccountToUser_UserId",
                table: "SharedAccountToUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_UserId",
                table: "Sections",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_SharedAccounts_SharedAccountId",
                table: "Sections",
                column: "SharedAccountId",
                principalTable: "SharedAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Users_UserId",
                table: "Sections",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedAccountToUser_SharedAccounts_SharedAccountId",
                table: "SharedAccountToUser",
                column: "SharedAccountId",
                principalTable: "SharedAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SharedAccountToUser_Users_UserId",
                table: "SharedAccountToUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Users_UserId",
                table: "Wallets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_SharedAccounts_SharedAccountId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Users_UserId",
                table: "Sections");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedAccountToUser_SharedAccounts_SharedAccountId",
                table: "SharedAccountToUser");

            migrationBuilder.DropForeignKey(
                name: "FK_SharedAccountToUser_Users_UserId",
                table: "SharedAccountToUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Users_UserId",
                table: "Wallets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_SharedAccountToUser_UserId",
                table: "SharedAccountToUser");

            migrationBuilder.DropIndex(
                name: "IX_Sections_UserId",
                table: "Sections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedAccounts",
                table: "SharedAccounts");

            migrationBuilder.RenameTable(
                name: "SharedAccounts",
                newName: "SharedAccount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedAccount",
                table: "SharedAccount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_SharedAccount_SharedAccountId",
                table: "Sections",
                column: "SharedAccountId",
                principalTable: "SharedAccount",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SharedAccountToUser_SharedAccount_SharedAccountId",
                table: "SharedAccountToUser",
                column: "SharedAccountId",
                principalTable: "SharedAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
