using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPProject.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    transID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userID = table.Column<int>(type: "INTEGER", nullable: false),
                    bookID = table.Column<int>(type: "INTEGER", nullable: false),
                    qty = table.Column<int>(type: "INTEGER", nullable: false),
                    saleAmount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.transID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
