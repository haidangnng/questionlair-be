using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuestionLairBE.Migrations
{
    /// <inheritdoc />
    public partial class AddMaterial : Migration
    {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
        migrationBuilder.CreateTable(
          name: "Materials",
          columns: table => new
          {
            Id = table.Column<int>(type: "integer", nullable: false)
              .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Url = table.Column<string>(type: "text", nullable: false),
            CourseId = table.Column<int>(type: "integer", nullable: true),
            UploadedBy = table.Column<int>(type: "integer", nullable: true),
            CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Materials", x => x.Id);
            table.ForeignKey(
              name: "FK_Materials_Courses_CourseId",
              column: x => x.CourseId,
              principalTable: "Courses",
              principalColumn: "Id",
              onDelete: ReferentialAction.Cascade);
          });

        migrationBuilder.CreateIndex(
          name: "IX_Materials_CourseId",
          table: "Materials",
          column: "CourseId");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
        migrationBuilder.DropTable(
          name: "Materials");
      }
    }
}
