using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations;

public partial class AddKnowledgeReviewHistory : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "KnowledgeReviewHistory",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                KnowledgeId = table.Column<long>(type: "bigint", nullable: false),
                ReviewerUserId = table.Column<long>(type: "bigint", nullable: false),
                Action = table.Column<int>(type: "int", nullable: false),
                Reason = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_KnowledgeReviewHistory", x => x.Id);
                table.ForeignKey("FK_KnowledgeReviewHistory_Knowledge_KnowledgeId", x => x.KnowledgeId,
                    "Knowledge", "Id", onDelete: ReferentialAction.Cascade);
                table.ForeignKey("FK_KnowledgeReviewHistory_User_ReviewerUserId", x => x.ReviewerUserId,
                    "User", "Id", onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_KnowledgeReviewHistory_KnowledgeId_CreatedAt",
            table: "KnowledgeReviewHistory",
            columns: new[] { "KnowledgeId", "CreatedAt" });

        migrationBuilder.CreateIndex(
            name: "IX_KnowledgeReviewHistory_ReviewerUserId",
            table: "KnowledgeReviewHistory",
            column: "ReviewerUserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "KnowledgeReviewHistory");
    }
}
