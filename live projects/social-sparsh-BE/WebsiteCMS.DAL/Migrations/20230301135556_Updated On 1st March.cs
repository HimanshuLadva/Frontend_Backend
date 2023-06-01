using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteCMS.DAL.Migrations
{
    public partial class UpdatedOn1stMarch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSociaMediaPost_tblFacebookPagesTokens_FacebookPagesTokensId",
                table: "tblSociaMediaPost");

            migrationBuilder.DropIndex(
                name: "IX_tblSociaMediaPost_FacebookPagesTokensId",
                table: "tblSociaMediaPost");

            migrationBuilder.DropColumn(
                name: "FacebookPagesTokensId",
                table: "tblSociaMediaPost");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateImageURL",
                table: "tblSCRMTemplate",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "tblBOTQuestionLink",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Target",
                table: "tblBOTQuestionLink",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "tblBOTQuestionLink");

            migrationBuilder.DropColumn(
                name: "Target",
                table: "tblBOTQuestionLink");

            migrationBuilder.AddColumn<int>(
                name: "FacebookPagesTokensId",
                table: "tblSociaMediaPost",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TemplateImageURL",
                table: "tblSCRMTemplate",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblSociaMediaPost_FacebookPagesTokensId",
                table: "tblSociaMediaPost",
                column: "FacebookPagesTokensId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSociaMediaPost_tblFacebookPagesTokens_FacebookPagesTokensId",
                table: "tblSociaMediaPost",
                column: "FacebookPagesTokensId",
                principalTable: "tblFacebookPagesTokens",
                principalColumn: "Id");
        }
    }
}
