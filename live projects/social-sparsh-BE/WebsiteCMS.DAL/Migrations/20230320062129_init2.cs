using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebsiteCMS.DAL.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSocialPlateformWisePosts_tblFacebookPagesTokens_PageId",
                table: "tblSocialPlateformWisePosts");

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "tblSocialPlateformWisePosts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tblSocialPlateformWisePosts_tblFacebookPagesTokens_PageId",
                table: "tblSocialPlateformWisePosts",
                column: "PageId",
                principalTable: "tblFacebookPagesTokens",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblSocialPlateformWisePosts_tblFacebookPagesTokens_PageId",
                table: "tblSocialPlateformWisePosts");

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "tblSocialPlateformWisePosts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tblSocialPlateformWisePosts_tblFacebookPagesTokens_PageId",
                table: "tblSocialPlateformWisePosts",
                column: "PageId",
                principalTable: "tblFacebookPagesTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
