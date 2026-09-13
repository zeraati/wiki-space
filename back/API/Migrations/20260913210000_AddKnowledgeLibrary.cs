using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations;

public partial class AddKnowledgeLibrary : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Subject",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Subject", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Knowledge",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                ProblemTitle = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                SubjectId = table.Column<long>(type: "bigint", nullable: false),
                CreatedByUserId = table.Column<long>(type: "bigint", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                ValidityDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IsPermanently = table.Column<bool>(type: "bit", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Knowledge", x => x.Id);
                table.ForeignKey("FK_Knowledge_Subject_SubjectId", x => x.SubjectId, "Subject", "Id", onDelete: ReferentialAction.Restrict);
                table.ForeignKey("FK_Knowledge_User_CreatedByUserId", x => x.CreatedByUserId, "User", "Id", onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "KnowledgeTag",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                KnowledgeId = table.Column<long>(type: "bigint", nullable: false),
                Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_KnowledgeTag", x => x.Id);
                table.ForeignKey("FK_KnowledgeTag_Knowledge_KnowledgeId", x => x.KnowledgeId, "Knowledge", "Id", onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex("IX_Knowledge_CreatedByUserId", "Knowledge", "CreatedByUserId");
        migrationBuilder.CreateIndex("IX_Knowledge_SubjectId", "Knowledge", "SubjectId");
        migrationBuilder.CreateIndex("IX_KnowledgeTag_KnowledgeId_Name", "KnowledgeTag", new[] { "KnowledgeId", "Name" }, unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "KnowledgeTag");
        migrationBuilder.DropTable(name: "Knowledge");
        migrationBuilder.DropTable(name: "Subject");
    }
}
