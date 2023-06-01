using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteCMS.DAL.Migrations
{
    public partial class Updatedon28Feb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblBOTImageOrFile_tblBOTQuestion_BotQuestionId",
                table: "tblBOTImageOrFile");

            migrationBuilder.DropForeignKey(
                name: "FK_tblSocialPlateformWisePosts_tblSocialPlatforms_Plateformid",
                table: "tblSocialPlateformWisePosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblBOTImageOrFile",
                table: "tblBOTImageOrFile");

            migrationBuilder.DropIndex(
                name: "IX_tblBOTImageOrFile_BotQuestionId",
                table: "tblBOTImageOrFile");

            migrationBuilder.DropColumn(
                name: "BotQuestionId",
                table: "tblBOTImageOrFile");

            migrationBuilder.RenameTable(
                name: "tblBOTImageOrFile",
                newName: "tblBOTImagesOrFile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblBOTImagesOrFile",
                table: "tblBOTImagesOrFile",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "tblBOTQuestionLink",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBOTQuestionLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblBOTQuestionLink_tblBOTQuestion_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "tblBOTQuestion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBOTQuestionLink_QuestionId",
                table: "tblBOTQuestionLink",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSocialPlateformWisePosts_tblSocialPlatforms_Plateformid",
                table: "tblSocialPlateformWisePosts",
                column: "Plateformid",
                principalTable: "tblSocialPlatforms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSocialPlateformWisePosts_tblSocialPlatforms_Plateformid",
                table: "tblSocialPlateformWisePosts");

            migrationBuilder.DropTable(
                name: "tblBOTQuestionLink");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblBOTImagesOrFile",
                table: "tblBOTImagesOrFile");

            migrationBuilder.RenameTable(
                name: "tblBOTImagesOrFile",
                newName: "tblBOTImageOrFile");

            migrationBuilder.AddColumn<long>(
                name: "BotQuestionId",
                table: "tblBOTImageOrFile",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblBOTImageOrFile",
                table: "tblBOTImageOrFile",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_tblBOTImageOrFile_BotQuestionId",
                table: "tblBOTImageOrFile",
                column: "BotQuestionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tblBOTImageOrFile_tblBOTQuestion_BotQuestionId",
                table: "tblBOTImageOrFile",
                column: "BotQuestionId",
                principalTable: "tblBOTQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSocialPlateformWisePosts_tblSocialPlatforms_Plateformid",
                table: "tblSocialPlateformWisePosts",
                column: "Plateformid",
                principalTable: "tblSocialPlatforms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
