using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trackio_API.Migrations
{
    /// <inheritdoc />
    public partial class AddJobsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jPostedDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JPostURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jSalary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
