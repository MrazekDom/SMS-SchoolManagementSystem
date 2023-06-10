using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class ConnectUsersToStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserStudent",
                columns: table => new
                {
                    AssignedStudentsId = table.Column<int>(type: "int", nullable: false),
                    AssignedUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserStudent", x => new { x.AssignedStudentsId, x.AssignedUsersId });
                    table.ForeignKey(
                        name: "FK_AppUserStudent_AspNetUsers_AssignedUsersId",
                        column: x => x.AssignedUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserStudent_Students_AssignedStudentsId",
                        column: x => x.AssignedStudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserStudent_AssignedUsersId",
                table: "AppUserStudent",
                column: "AssignedUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserStudent");
        }
    }
}
