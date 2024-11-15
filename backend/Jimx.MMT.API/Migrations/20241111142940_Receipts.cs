using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Jimx.MMT.API.Migrations
{
	/// <inheritdoc />
	public partial class Receipts : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_SharedAccountToUser_SharedAccounts_SharedAccountId",
				table: "SharedAccountToUser");

			migrationBuilder.DropForeignKey(
				name: "FK_SharedAccountToUser_Users_UserId",
				table: "SharedAccountToUser");

			migrationBuilder.DropPrimaryKey(
				name: "PK_SharedAccountToUser",
				table: "SharedAccountToUser");

			migrationBuilder.RenameTable(
				name: "SharedAccountToUser",
				newName: "SharedAccountToUsers");

			migrationBuilder.RenameIndex(
				name: "IX_SharedAccountToUser_UserId",
				table: "SharedAccountToUsers",
				newName: "IX_SharedAccountToUsers_UserId");

			migrationBuilder.RenameIndex(
				name: "IX_SharedAccountToUser_SharedAccountId",
				table: "SharedAccountToUsers",
				newName: "IX_SharedAccountToUsers_SharedAccountId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_SharedAccountToUsers",
				table: "SharedAccountToUsers",
				column: "Id");

			migrationBuilder.CreateTable(
				name: "Currencies",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					Code = table.Column<string>(type: "text", nullable: false),
					Name = table.Column<string>(type: "text", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Currencies", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Locations",
				columns: table => new
				{
					Id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					CountryCode = table.Column<string>(type: "text", nullable: false),
					LocationCode = table.Column<string>(type: "text", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Locations", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Products",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					ParentId = table.Column<Guid>(type: "uuid", nullable: true),
					UserId = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<string>(type: "text", nullable: false),
					Description = table.Column<string>(type: "text", nullable: false),
					CategoryId = table.Column<int>(type: "integer", nullable: true),
					SectionId = table.Column<int>(type: "integer", nullable: true),
					IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
					CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					CreateUserId = table.Column<Guid>(type: "uuid", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Products", x => x.Id);
					table.ForeignKey(
						name: "FK_Products_Categories_CategoryId",
						column: x => x.CategoryId,
						principalTable: "Categories",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_Products_Products_ParentId",
						column: x => x.ParentId,
						principalTable: "Products",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_Products_Sections_SectionId",
						column: x => x.SectionId,
						principalTable: "Sections",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_Products_Users_CreateUserId",
						column: x => x.CreateUserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Products_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Receipts",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					UserId = table.Column<Guid>(type: "uuid", nullable: false),
					Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					LocationId = table.Column<int>(type: "integer", nullable: false),
					CurrencyId = table.Column<int>(type: "integer", nullable: false),
					Comment = table.Column<string>(type: "text", nullable: false),
					CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					CreateUserId = table.Column<Guid>(type: "uuid", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Receipts", x => x.Id);
					table.ForeignKey(
						name: "FK_Receipts_Currencies_CurrencyId",
						column: x => x.CurrencyId,
						principalTable: "Currencies",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Receipts_Locations_LocationId",
						column: x => x.LocationId,
						principalTable: "Locations",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Receipts_Users_CreateUserId",
						column: x => x.CreateUserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Receipts_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "ReceiptEntries",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uuid", nullable: false),
					ReceiptId = table.Column<Guid>(type: "uuid", nullable: false),
					ProductId = table.Column<Guid>(type: "uuid", nullable: false),
					Quantity = table.Column<decimal>(type: "numeric", nullable: false),
					Price = table.Column<decimal>(type: "numeric", nullable: false),
					CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
					CreateUserId = table.Column<Guid>(type: "uuid", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ReceiptEntries", x => x.Id);
					table.ForeignKey(
						name: "FK_ReceiptEntries_Products_ProductId",
						column: x => x.ProductId,
						principalTable: "Products",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_ReceiptEntries_Receipts_ReceiptId",
						column: x => x.ReceiptId,
						principalTable: "Receipts",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_ReceiptEntries_Users_CreateUserId",
						column: x => x.CreateUserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Products_CategoryId",
				table: "Products",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_Products_CreateUserId",
				table: "Products",
				column: "CreateUserId");

			migrationBuilder.CreateIndex(
				name: "IX_Products_ParentId",
				table: "Products",
				column: "ParentId");

			migrationBuilder.CreateIndex(
				name: "IX_Products_SectionId",
				table: "Products",
				column: "SectionId");

			migrationBuilder.CreateIndex(
				name: "IX_Products_UserId",
				table: "Products",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_ReceiptEntries_CreateUserId",
				table: "ReceiptEntries",
				column: "CreateUserId");

			migrationBuilder.CreateIndex(
				name: "IX_ReceiptEntries_ProductId",
				table: "ReceiptEntries",
				column: "ProductId");

			migrationBuilder.CreateIndex(
				name: "IX_ReceiptEntries_ReceiptId",
				table: "ReceiptEntries",
				column: "ReceiptId");

			migrationBuilder.CreateIndex(
				name: "IX_Receipts_CreateUserId",
				table: "Receipts",
				column: "CreateUserId");

			migrationBuilder.CreateIndex(
				name: "IX_Receipts_CurrencyId",
				table: "Receipts",
				column: "CurrencyId");

			migrationBuilder.CreateIndex(
				name: "IX_Receipts_LocationId",
				table: "Receipts",
				column: "LocationId");

			migrationBuilder.CreateIndex(
				name: "IX_Receipts_UserId",
				table: "Receipts",
				column: "UserId");

			migrationBuilder.AddForeignKey(
				name: "FK_SharedAccountToUsers_SharedAccounts_SharedAccountId",
				table: "SharedAccountToUsers",
				column: "SharedAccountId",
				principalTable: "SharedAccounts",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_SharedAccountToUsers_Users_UserId",
				table: "SharedAccountToUsers",
				column: "UserId",
				principalTable: "Users",
				principalColumn: "Id",
				onDelete: ReferentialAction.Cascade);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_SharedAccountToUsers_SharedAccounts_SharedAccountId",
				table: "SharedAccountToUsers");

			migrationBuilder.DropForeignKey(
				name: "FK_SharedAccountToUsers_Users_UserId",
				table: "SharedAccountToUsers");

			migrationBuilder.DropTable(
				name: "ReceiptEntries");

			migrationBuilder.DropTable(
				name: "Products");

			migrationBuilder.DropTable(
				name: "Receipts");

			migrationBuilder.DropTable(
				name: "Currencies");

			migrationBuilder.DropTable(
				name: "Locations");

			migrationBuilder.DropPrimaryKey(
				name: "PK_SharedAccountToUsers",
				table: "SharedAccountToUsers");

			migrationBuilder.RenameTable(
				name: "SharedAccountToUsers",
				newName: "SharedAccountToUser");

			migrationBuilder.RenameIndex(
				name: "IX_SharedAccountToUsers_UserId",
				table: "SharedAccountToUser",
				newName: "IX_SharedAccountToUser_UserId");

			migrationBuilder.RenameIndex(
				name: "IX_SharedAccountToUsers_SharedAccountId",
				table: "SharedAccountToUser",
				newName: "IX_SharedAccountToUser_SharedAccountId");

			migrationBuilder.AddPrimaryKey(
				name: "PK_SharedAccountToUser",
				table: "SharedAccountToUser",
				column: "Id");

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
		}
	}
}
