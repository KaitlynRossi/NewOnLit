using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPProject.Migrations
{
    /// <inheritdoc />
    public partial class hopefulFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bookID",
                table: "Transactions",
                newName: "transBookID");

            migrationBuilder.CreateTable(
                name: "Community",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PostTitle = table.Column<string>(type: "TEXT", nullable: false),
                    PostRating = table.Column<int>(type: "INTEGER", nullable: false),
                    PostContent = table.Column<string>(type: "TEXT", nullable: false),
                    userID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Community", x => x.PostID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    email = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    password = table.Column<string>(type: "TEXT", nullable: false),
                    memberRole = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_transBookID",
                table: "Transactions",
                column: "transBookID");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Books_transBookID",
                table: "Transactions",
                column: "transBookID",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Books_transBookID",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Community");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_transBookID",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "transBookID",
                table: "Transactions",
                newName: "bookID");
        }
    }
}
