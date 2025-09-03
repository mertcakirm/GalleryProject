using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalleryProject.Migrations
{
    public partial class FixFolderNameType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                    name: "Name",
                    table: "Folders",
                    type: "varchar(255)",
                    maxLength: 255,
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_UserId_Name",
                table: "Folders",
                columns: new[] { "UserId", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Folders_UserId_Name",
                table: "Folders");

            migrationBuilder.AlterColumn<string>(
                    name: "Name",
                    table: "Folders",
                    type: "longtext",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "varchar(255)",
                    oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}