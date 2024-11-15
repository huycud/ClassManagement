using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassManagement.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddAColumnInIdentityUserTokensTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentClasses_AspNetUsers_UserId",
                table: "StudentClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentClasses_Classes_ClassId",
                table: "StudentClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentClasses",
                table: "StudentClasses");

            migrationBuilder.RenameTable(
                name: "StudentClasses",
                newName: "StudentsClasses");

            migrationBuilder.RenameIndex(
                name: "IX_StudentClasses_ClassId",
                table: "StudentsClasses",
                newName: "IX_StudentsClasses_ClassId");

            migrationBuilder.AddColumn<bool>(
                name: "IsRevoked",
                table: "AspNetUserTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentsClasses",
                table: "StudentsClasses",
                columns: new[] { "UserId", "ClassId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsClasses_AspNetUsers_UserId",
                table: "StudentsClasses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsClasses_Classes_ClassId",
                table: "StudentsClasses",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsClasses_AspNetUsers_UserId",
                table: "StudentsClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentsClasses_Classes_ClassId",
                table: "StudentsClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentsClasses",
                table: "StudentsClasses");

            migrationBuilder.DropColumn(
                name: "IsRevoked",
                table: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "StudentsClasses",
                newName: "StudentClasses");

            migrationBuilder.RenameIndex(
                name: "IX_StudentsClasses_ClassId",
                table: "StudentClasses",
                newName: "IX_StudentClasses_ClassId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentClasses",
                table: "StudentClasses",
                columns: new[] { "UserId", "ClassId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClasses_AspNetUsers_UserId",
                table: "StudentClasses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClasses_Classes_ClassId",
                table: "StudentClasses",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
